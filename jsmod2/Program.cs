using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;

namespace jsmod2
{
    [PluginDetails(
        author = "Magiclu550",
        name = "ProxyHandler",
        description = "用于响应JSMOD2端",
        id = "cn.jsmod2.ProxyHandler",
        configPrefix = "ep",
        langFile = "ProxyHandler",
        version = "1.0",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 0
    )]
    /**
     * ProxyHandler主端，用于交互Jsmod2协议
     * JSON交互采用Socket
     */
    internal class ProxyHandler : Plugin
    {

        public Dictionary<String, object> apiMapping = new Dictionary<string, object>();
        
        public PropertiesReader reader = new PropertiesReader();
        
        UTF8Encoding utf8WithoutBom = new UTF8Encoding(false);

        public static ProxyHandler handler { get; set; }


        public const int MAX_LENGTH = 8 * 30;
        public override void Register()
        {
            handler = this;
            AddEventHandlers(new HandleAdmin());
            reader.append("this.ip","127.0.0.1")
                .append("this.port","19938")
                .append("jsmod2.ip","127.0.0.1")
                .append("jsmod2.port","19935")
                .create(Server.GetAppFolder()+"/jsmod2.conf");
        }

        public override void OnEnable()
        {
            Info("The ProxyHandler is Start!Please start the jsmod2 server");
           Thread thread = new Thread(listenerThread);
           thread.Start();
           //Console.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes("你好")));
        }

        public override void OnDisable()
        {
            Info("The ProxyHandler have stopped,please close the jsmod2");
        }

        public void listenerThread()
        {
            int port;
            int.TryParse(reader.get("this.port", false), out port);
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse(reader.get("this.ip",false)),port));
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                WorkThread thread = new WorkThread(client,this);
                ThreadPool.QueueUserWorkItem(new WaitCallback(thread.socketThread));
            }
           
        }
        
        
        public void sendEventObject(Event e,int id,IdMapping mapping)
        {
            sendObject(JsonConvert.SerializeObject(e),id,mapping);
        }

        public void sendObject(string json1, int id,IdMapping mapping)
        {
            sendObject(new TcpClient(),json1,id,mapping);
        }
        
        public void sendObject(TcpClient tcp,string json1, int id,IdMapping mapping)
        {
            //如何定位物品，并设置，通过itemMapping找到id归属对象(player这个字段就是id)
            //然后通过id定位到物品，并设置
            send(getProtocol(json1,id,mapping),tcp);
        }
        
        //发送协议集合，用于传输物品数组，有多个不同的id
        public void sendObjects(JsonSetting[] settings)
        {
            string all = getProtocol(JsonConvert.SerializeObject(settings[0].Object), settings[0].id,
                settings[0].idMapping);
            for (int i = 1; i < settings.Length; i++)
            {
                JsonSetting setting = settings[i];
                all = all + "@!" + getProtocol(JsonConvert.SerializeObject(settings[i].Object), settings[i].id,
                          settings[i].idMapping);
            }
            send(all,new TcpClient());
        }

        private void send(string jsons,TcpClient tcp)
        {
            int port;
            int.TryParse(reader.get("jsmod2.port", false), out port);
            byte[] bytes = utf8WithoutBom.GetBytes(Convert.ToBase64String(utf8WithoutBom.GetBytes(jsons)));
            tcp.Connect(new IPEndPoint(IPAddress.Parse(reader.get("jsmod2.ip",false)),port));
            tcp.GetStream().Write(bytes,0,bytes.Length);
        }

        public string getProtocol(string json1, int id,IdMapping mapping)
        {
            string json;
            if (mapping == null)
            {
                json = id + "-" + json1;
            }
            else
            {
                json = id + "-" + json1 + mapping.get();
            }

            return json;
        }

        
    }

    class WorkThread
    {
        private TcpClient client;

        private ProxyHandler handler;
        public WorkThread(TcpClient client,ProxyHandler handler)
        {
            this.client = client;
            this.handler = handler;
        }
        public void socketThread(object state)
        {
            byte[] bytes = new byte[ProxyHandler.MAX_LENGTH];
            client.GetStream().Read(bytes,0,getLen(bytes));
            var utf8WithoutBom = new UTF8Encoding(false);
            string base64String = getFullBytes(client,utf8WithoutBom.GetString(bytes));
            string[] base64s = base64String.Split(';');

            foreach (var base64 in base64s)
            {
                if (!base64.Equals(""))
                {
                   
                    string jsmod2Request = utf8WithoutBom.GetString(Convert.FromBase64String(utf8WithoutBom.GetString(toCommon(utf8WithoutBom.GetBytes(base64)))));
                    string head = getHead(jsmod2Request);
                    int id;
                    int.TryParse(head,out id);
                    string end = getEnd(jsmod2Request);
                    string json = getJson(jsmod2Request);
                    NetworkHandler.handleJsmod2(id,json,end,client);
                }
            }
            client.Close();
        }

        public string getEnd(string request)
        {
            return request.Substring(request.IndexOf('~')+1);
        }

        public string getHead(string request)
        {
            return request.Substring(0,request.IndexOf('-'));
        }

        public byte[] toCommon(byte[] bytes)
        {
            List<byte> list = new List<byte>();
            foreach (var thing in bytes)
            {
                if (thing != 0)
                {
                    list.Add(thing);
                }
            }

            return list.ToArray();
        }

        public string getJson(string request)
        {
            return request.Substring(request.IndexOf('-')+1, request.LastIndexOf('~')-request.IndexOf('-')-1);
        }

        public string getFullBytes(TcpClient client,string get)
        {
            StringBuilder builder = new StringBuilder(get);
            if (!get.EndsWith(";"))
            {
                int b = 0;
                while (b != ';')
                {
                    b = client.GetStream().ReadByte();
                    if(b == -1)
                        break;
                    builder.Append((char) b);
                    
                }
            }

            return builder.ToString();
        }

        public int getLen(byte[] bytes)
        {
            int len = 0;
            foreach (var b in bytes)
            {
                if (b != 0)
                {
                    len++;
                }
            }

            return len;
        }
    }
    
}