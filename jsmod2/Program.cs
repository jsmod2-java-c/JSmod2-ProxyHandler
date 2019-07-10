using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using jsmod2.command;
using Newtonsoft.Json;
using Smod2;
using Smod2.Attributes;
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

        public static ProxyHandler handler { get; set; }


        public const int MAX_LENGTH = 8 * 30;
        public override void Register()
        {
            handler = this;
            AddEventHandlers(new HandleAdmin());
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
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"),19938));
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                WorkThread thread = new WorkThread(client,this);
                ThreadPool.QueueUserWorkItem(new WaitCallback(thread.socketThread));
            }
           
        }

        public void sendEventObject(Event e,int id,string field,object o)
        {
            sendObject(JsonConvert.SerializeObject(e),id,field,o);
        }

        public void sendObject(string json1, int id, string field, object o)
        {
            sendObject(new TcpClient(),json1,id,field,o);
        }
        
        public void sendObject(TcpClient tcp,string json1, int id, string field, object o)
        {
            var utf8WithoutBom = new UTF8Encoding(false);
            if (field.Equals(""))
            {
                string json = id + "-" + json1;
            }
            else
            {
                string json = id+"-"+json1+"|"+field+":"+JsonConvert.SerializeObject(o);   
            }
            //TODO 配置文件设置端口 ip
            tcp.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"),19935));
            byte[] bytes = utf8WithoutBom.GetBytes(Convert.ToBase64String(utf8WithoutBom.GetBytes(json1)));
            tcp.GetStream().Write(bytes,0,bytes.Length);
            
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
            //1-sssss
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