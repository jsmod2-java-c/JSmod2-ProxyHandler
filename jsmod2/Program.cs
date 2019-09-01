using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;

namespace jsmod2
{
    
    /**
     * ProxyHandler主端，用于交互Jsmod2协议
     * JSON交互采用Socket
     */
    //最新的设计 2019 7 24
    //在触发事件时，将不发出event的序列化对象，而是只发出apiId，之后的数据获取通过发包(GetPacket)获取
    //其他响应也是如此，不发对象，而是只发apiId（Vector则需要发送序列化对象，其他序列化对象设置为字符串""，apiId在apiMapper添加）

    public class RegisterEvents
    {
        private static HashMap<int,Type> events;
        
        private static HashMap<Type,int> events_Id;

        static RegisterEvents()
        {
            events = new HashMap<int, Type>();
            events_Id = new HashMap<Type, int>();
        }
        public static void registerEvents()
        {
            events.put(0x01,typeof( AdminQueryEvent));//packet 1
            events.put(0x03,typeof( AuthCheckEvent));//packet 1
            events.put(0x04,typeof( BanEvent));//packet 1
            events.put(0x05,typeof( SetConfigEvent));//packet 1
            events.put(0x06,typeof( GeneratorFinishEvent));//p  \1
            events.put(0x07,typeof( LCZDecontaminateEvent));//p
        events.put(0x08,typeof( SCP914ActivateEvent));//p
        events.put(0x09,typeof( ScpDeathAnnouncementEvent));//p
        events.put(0x0a,typeof( SummonVehicleEvent));//p
        events.put(0x0b,typeof(WarheadChangeLeverEvent));//p 1
        events.put(0x0c,typeof(WarheadDetonateEvent));//p
        events.put(0x0d,typeof(WarheadKeycardAccessEvent));//p
        events.put(0x0e,typeof(WarheadStartEvent));//p
        events.put(0x0f,typeof( Player079AddExpEvent));//p
        events.put(0x10,typeof( Player079CameraTeleportEvent));//p
        events.put(0x11,typeof( Player079DoorEvent));
        events.put(0x12,typeof( Player079ElevatorTeleportEvent));
        events.put(0x13,typeof( Player079LevelUpEvent));
        events.put(0x14,typeof(Player079LockdownEvent));
        events.put(0x15,typeof(Player079LockEvent));
        events.put(0x16,typeof(Player079StartSpeakerEvent));
        events.put(0x17,typeof(Player079StopSpeakerEvent));
        events.put(0x18,typeof(Player079TeslaGateEvent));
        events.put(0x19,typeof(Player079UnlockDoorsEvent));
        events.put(0x1a,typeof(Player106CreatePortalEvent));
        events.put(0x1b,typeof(Player106TeleportEvent));
        events.put(0x1c,typeof(PlayerCallCommandEvent));
        events.put(0x1d,typeof(PlayerCheckEscapeEvent));
        events.put(0x1e,typeof(PlayerContain106Event));
        events.put(0x1f,typeof(PlayerDeathEvent));
        events.put(0x20,typeof(PlayerDropItemEvent));
        events.put(0x21,typeof(PlayerElevatorUseEvent));
        events.put(0x22,typeof(PlayerGeneratorAccessEvent));
        events.put(0x23,typeof(PlayerGeneratorEjectTabletEvent));
        events.put(0x24,typeof(PlayerGeneratorInsertTabletEvent));
        events.put(0x25,typeof(PlayerGeneratorUnlockEvent));
        events.put(0x26,typeof(PlayerGrenadeExplosion));
        events.put(0x27,typeof(PlayerGrenadeHitPlayer));
        events.put(0x28,typeof(PlayerHandcuffedEvent));
        events.put(0x29,typeof(PlayerHurtEvent));
        events.put(0x2a,typeof(PlayerInitialAssignTeamEvent));
        events.put(0x2b,typeof(PlayerIntercomCooldownCheckEvent));
        events.put(0x2c,typeof(PlayerIntercomEvent));
        events.put(0x2d,typeof(PlayerJoinEvent));// 1
        events.put(0x2e,typeof(PlayerLureEvent));
        events.put(0x2f,typeof(PlayerMakeNoiseEvent));
        events.put(0x30,typeof(PlayerMedkitUseEvent));
        events.put(0x31,typeof(PlayerPickupItemEvent));// 1
        events.put(0x32,typeof(PlayerPickupItemLateEvent));// 1
        events.put(0x33,typeof(PlayerPocketDimensionEnterEvent));
        events.put(0x34,typeof(PlayerPocketDimensionExitEvent));
        events.put(0x35,typeof(PlayerRadioSwitchEvent));
        events.put(0x36,typeof(PlayerRecallZombieEvent));
        events.put(0x37,typeof(PlayerReloadEvent));
        events.put(0x38,typeof(PlayerSCP914ChangeKnobEvent));
        events.put(0x39,typeof(PlayerSetRoleEvent));
        events.put(0x3a,typeof(PlayerShootEvent));
        events.put(0x3b,typeof(PlayerSpawnEvent));
        events.put(0x3c,typeof(PlayerSpawnRagdollEvent));
        events.put(0x3d,typeof(PlayerThrowGrenadeEvent));//
        events.put(0x3e,typeof(PlayerTriggerTeslaEvent));
        events.put(0x3f,typeof(Scp096CooldownEndEvent));
        events.put(0x40,typeof(Scp096CooldownStartEvent));
        events.put(0x41,typeof(Scp096EnrageEvent));
        events.put(0x42,typeof(Scp096PanicEvent));
        events.put(0x43,typeof( ConnectEvent));
        events.put(0x44,typeof( DisconnectEvent));
        events.put(0x45,typeof( FixedUpdateEvent));
        events.put(0x46,typeof(LateDisconnectEvent));
        events.put(0x47,typeof( LateUpdateEvent));//
        events.put(0x48,typeof(RoundEndEvent));
        events.put(0x49,typeof(RoundRestartEvent));//
        events.put(0x4a,typeof(RoundStartEvent));
        events.put(0x4b,typeof(SceneChangedEvent));
        events.put(0x4c,typeof(SetServerNameEvent));
        events.put(0x4d,typeof(UpdateEvent));
        events.put(0x4e,typeof(WaitingForPlayersEvent));
        events.put(0x4f,typeof( DecideRespawnQueueEvent));
        events.put(0x50,typeof( SetNTFUnitNameEvent));
        events.put(0x51,typeof( SetSCPConfigEvent));
        events.put(0x52,typeof( TeamRespawnEvent));
        events.put(301,typeof(CheckRoundEndEvent));
        events.put(302,typeof(PlayerInfectedEvent));
        events.put(303,typeof(PlayerDoorAccessEvent));
        events.put(304,typeof(PlayerNicknameSetEvent));
        events.put(305,typeof(WarheadStopEvent));
        events_Id = events.keyToValue();
        }

        public static int getId(Type t)
        {
            return events_Id.get(t);
        }
        
    }
    
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
    class ProxyHandler : Plugin
    {

        private bool started;

        public Dictionary<String, object> apiMapping = new Dictionary<string, object>();
        
        public PropertiesReader reader = new PropertiesReader();
        
        UTF8Encoding utf8WithoutBom = new UTF8Encoding(false);
        
        public static ProxyHandler handler { get; set; }


        public const int MAX_LENGTH = 0x10 * 0x1E;
        public override void Register()
        {
            handler = this;
            started = true;
            Info("The ProxyHandler is Start!Please start the jsmod2 server");
            reader.append("this.ip","127.0.0.1")
                .append("this.port","19938")
                .append("jsmod2.ip","127.0.0.1")
                .append("jsmod2.port","19935")
                .append("jsmod2.debug","false")
                .create(Server.GetAppFolder()+"/jsmod2.conf");
            Thread thread = new Thread(listenerThread);
            thread.Start();
            Info("ProxyHandler config:"+Server.GetAppFolder()+"/jsmod2.conf");
            TcpClient client = new TcpClient();
            int port = Lib.getInt(reader.get("jsmod2.port"));
            Info("Connecting the Jsmod2....");
            while (!client.Connected)
            {
                try
                {
                    client.Connect(new IPEndPoint(IPAddress.Parse(reader.get("jsmod2.ip")), 20003));
                }
                catch (Exception e)
                {
                    
                }

            }
            client.Close();
            Info("Connect Successfully");
            RegisterEvents.registerEvents();
            Info("registered events");
            Type type = typeof(NewEventHandlers);
            NewEventHandlers handlers = new NewEventHandlers();
            Type[] types = type.GetInterfaces();
            foreach (var t in types)
            {
                if (t != typeof(IEventHandlerSetConfig))
                {
                    AddEventHandler(t,handlers);
                }
                
            }
            AddEventHandler(typeof(IEventHandlerSetConfig),handlers);
            Info("registered event handlers");
        }

        public override void OnEnable()
        { 
            
        }

        public override void OnDisable()
        {
            Info("The ProxyHandler have stopped,please close the jsmod2");
            started = false;
        }

        public void listenerThread()
        {
            int port;
            int.TryParse(reader.get("this.port"), out port);
            Info("Jsmod2 ProxyHandler is Starting,Port: "+port);
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse(reader.get("this.ip")),port));
            listener.Start();
            while (true)
            {
                if (!started)
                {
                    ProxyHandler.handler.Info("Proxy Thread is exited");
                    break;
                }
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Info("listened a request from Jsmod2");
                    WorkThread thread = new WorkThread(client);
                    Thread t = new Thread(thread.socketThread);
                    t.Start(); 
                }
                catch (Exception e)
                {
                    //输出错误日志
                    Error(e.Message);
                    Error(e.GetType()+"");
                    Error(e.StackTrace);
                }
                
            }
        }
        
        //触发事件只发id
        //在jsmod2的监听器执行完前不能停止，get为true，在read处阻塞
        public void sendEventObject(Event e,int id,IdMapping mapping)
        {
            
            sendObject("{}",id,mapping,true);
        }

        public void sendEventObject(Event e, IdMapping mapping)
        {
            sendEventObject(e.GetType(),mapping);
        }

        public void sendEventObject(Type t,IdMapping mapping)
        {
            sendEventObject(null,RegisterEvents.getId(t),mapping);
        }

        public void sendObject(string json1, int id,IdMapping mapping,bool get)
        {
            sendObject(new TcpClient(),json1,id,mapping,get);
        }
        
        public void sendObject(TcpClient tcp,string json1, int id,IdMapping mapping,bool get)
        {
            //如何定位物品，并设置，通过itemMapping找到id归属对象(player这个字段就是id)
            //然后通过id定位到物品，并设置
            send0(getProtocol(json1,id,mapping),tcp,get);
        }
        
        //发送协议集合，用于传输物品数组，有多个不同的id
        public void  sendObjects(TcpClient client,JsonSetting[] settings)
        {
            string all = "";
            if (settings.Length != 0)
            {
                all = getProtocol(getJson(settings[0].responseValue), settings[0].id,
                    settings[0].idMapping);
                for (int i = 1; i < settings.Length; i++)
                {
                    all = all + "@!" + getProtocol(getJson(settings[i].responseValue), settings[i].id,
                              settings[i].idMapping);
                }
            }
            
            send0(all,client);
        }

        private string getJson(object o)
        {
            if (o is string)
            {
                if (((string) o).Equals("{}"))
                {
                    return "{}";
                }

                
            }

            if (o is Vector)
            {
                Vector v = o as Vector;
                o = new ProxyVector(v.x,v.y,v.z);
            }

            if (o is Dictionary<Vector, Vector>)
            {
                StringBuilder builder = new StringBuilder("{");
                foreach (var entry in (Dictionary<Vector,Vector>)o)
                {
                    Vector key = entry.Key;
                    Vector val = entry.Value;
                    string key1 = "\"(" + key.x + "-" + key.y + "-" + key.z + ")\"";
                    string val1 = "\"(" + val.x + "-" + val.y + "-" + val.z + ")\"";
                    builder.Append(key1);
                    builder.Append(":");
                    builder.Append(val1);
                }

                builder.Append("}");
                return builder.ToString();
            }
            return JsonConvert.SerializeObject(o);
        }


        public void sendObjects(JsonSetting[] settings)
        {
            sendObjects(new TcpClient(),settings);
        }

        private void send0(string jsons, TcpClient tcp, bool get)
        {
            try
            {
                int port;
                int.TryParse(reader.get("jsmod2.port"), out port);
                byte[] bytes = utf8WithoutBom.GetBytes(Convert.ToBase64String(utf8WithoutBom.GetBytes(jsons))+";");
                if (!tcp.Connected)
                {
                    tcp.Connect(new IPEndPoint(IPAddress.Parse(reader.get("jsmod2.ip")), port));
                }

                tcp.GetStream().Write(bytes, 0, bytes.Length);
                if (get)
                {
                    tcp.GetStream().ReadByte(); //停止
                }
                tcp.Close();
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }
        private void send0(string jsons,TcpClient tcp)
        {

            send0(jsons, tcp, false);

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
            string[] base64s = base64String.Split(';');

            foreach (var base64 in base64s)
            {
                if (!base64.Equals(""))
                {
                   
                    string jsmod2Request = utf8WithoutBom.GetString(Convert.FromBase64String(utf8WithoutBom.GetString(toCommon(utf8WithoutBom.GetBytes(base64)))));
                    string json = getJson(jsmod2Request);
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

    public class ProxyVector
    {
        public readonly float x;
        public readonly float y;
        public readonly float z; 
        public ProxyVector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    
}