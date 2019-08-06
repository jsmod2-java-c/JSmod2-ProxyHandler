using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    //最新的设计 2019 7 24
    //在触发事件时，将不发出event的序列化对象，而是只发出apiId，之后的数据获取通过发包(GetPacket)获取
    //其他响应也是如此，不发对象，而是只发apiId（Vector则需要发送序列化对象，其他序列化对象设置为字符串""，apiId在apiMapper添加）
    class ProxyHandler : Plugin
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
            AddEventHandlers(new HandleAuthCheck());
            AddEventHandlers(new HandleBan());
            AddEventHandlers(new HandlePickupItemLate());
            AddEventHandlers(new HandlePlayer());
            AddEventHandlers(new HandlePickupItem());
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
        }

        public override void OnDisable()
        {
            Info("The ProxyHandler have stopped,please close the jsmod2");
        }

        public void listenerThread()
        {
            int port;
            int.TryParse(reader.get("this.port"), out port);
            Info("启动了JSMOD2代理监听器，端口为"+port);
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse(reader.get("this.ip")),port));
            listener.Start();
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Info("监听到了一个来自jsmod2的数据包");
                    WorkThread thread = new WorkThread(client);
                    Thread t = new Thread(thread.socketThread);
                    t.Start(); 
                }
                catch (Exception e)
                {
                    //输出错误日志
                    Error(e.Message);
                }
                
            }
        }
        
        //触发事件只发id
        public void sendEventObject(Event e,int id,IdMapping mapping)
        {
            sendObject("{}",id,mapping);
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
        public void  sendObjects(TcpClient client,JsonSetting[] settings)
        {
            string all = getProtocol(JsonConvert.SerializeObject(settings[0].responseValue), settings[0].id,
                settings[0].idMapping);
            for (int i = 1; i < settings.Length; i++)
            {
                JsonSetting setting = settings[i];
                all = all + "@!" + getProtocol(JsonConvert.SerializeObject(settings[i].responseValue), settings[i].id,
                          settings[i].idMapping);
            }
            send(all,client);
        }


        public void sendObjects(JsonSetting[] settings)
        {
            sendObjects(new TcpClient(),settings);
        }
        private void send(string jsons,TcpClient tcp)
        {
            try
            {
                int port;
                int.TryParse(reader.get("jsmod2.port"), out port);
                byte[] bytes = utf8WithoutBom.GetBytes(Convert.ToBase64String(utf8WithoutBom.GetBytes(jsons)));
                if (!tcp.Connected)
                {
                    tcp.Connect(new IPEndPoint(IPAddress.Parse(reader.get("jsmod2.ip")),port));
                }
            
                tcp.GetStream().Write(bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
            

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
        
        public WorkThread(TcpClient client)
        {
            this.client = client;
        }
        public void socketThread()
        {
            byte[] bytes = new byte[ProxyHandler.MAX_LENGTH];
            client.GetStream().Read(bytes,0,getLen(bytes));
            var utf8WithoutBom = new UTF8Encoding(false);
            string base64String = getFullBytes(client,utf8WithoutBom.GetString(toCommon(bytes))).Trim();
            ProxyHandler.handler.Info(base64String);
            string[] base64s = base64String.Split(';');

            foreach (var base64 in base64s)
            {
                if (!base64.Equals(""))
                {
                   
                    string jsmod2Request = utf8WithoutBom.GetString(Convert.FromBase64String(utf8WithoutBom.GetString(toCommon(utf8WithoutBom.GetBytes(base64)))));
                    ProxyHandler.handler.Info("DECODE:"+jsmod2Request);
                    string json = getJson(jsmod2Request);
                    ProxyHandler.handler.Info("JSON:"+json);
                    Dictionary<string,string> mapper = (Dictionary<string,string>)JsonConvert.DeserializeObject(json,typeof(Dictionary<string,string>));
                    int id = 0x53;
                    
                    if (mapper.ContainsKey("id"))
                    {
                        String head = mapper["id"];
                        int.TryParse(head,out id);
                    }
                    
                    ProxyHandler.handler.Info("ID:"+id);
                    try
                    {
                        NetworkHandler.handleJsmod2(id,json,mapper,client);
                    }
                    catch (Exception e)
                    {
                        ProxyHandler.handler.Error(e.Message);
                    }
                   
                }
            }
            client.Close();
        }

        public string getEnd(string request)
        {
            if (request.IndexOf('~') == -1)
            {
                return "";
            }
            return request.Substring(request.IndexOf('~')+1);
        }

        public string getHead(string request)
        {
            if (request.IndexOf('-') == -1)
            {
                return "";
            }
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
            return request.Substring(request.IndexOf('-')+1);
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