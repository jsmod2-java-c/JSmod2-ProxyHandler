using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using jsmod2;
using jsmod2.command;
using Newtonsoft.Json;
using Smod2.API;
using Smod2.Events;
using Smod2.EventSystem.Events;

namespace jsmod2
{
    /**
     * 根据事件发送序列化的Event对象
     */
    //handleJsmod2功能有
    //截取注册指令发包，分配一个CommandHandler [OK]
    //截取set和get包，调用相应方法，并将返回值返回
    //sendPacket方法
    //触发事件时，将事件对象序列化传递给jsmod2,如果其中含有Item，则生成一个id，把id和Item对象对应上
    //其他实体api也通过id定位
    //触发指令时，当指令来自于jsmod2注册，CommandHandler将指令信息封装(Command对象，Sender对象，参数)
    //，发到jsmod2 [OK]
    
    //TODO 截取set和get包，调用相应方法，并将返回值返回
    //TODO 触发事件时，将事件对象序列化传递给jsmod2
    //TODO 检测下Door的Name，和其他的GetComponent,GetGameObject方法是什么
    public class NetworkHandler
    {
        private static Dictionary<int,Handler> handlers = new Dictionary<int, Handler>();

        static NetworkHandler()
        {
            handlers.Add(0x66,new HandleAdminQuerySetAdmin());
            handlers.Add(0x57,new HandleItemDrop());
            handlers.Add(0x5c,new HandleItemGetComponent());
            handlers.Add(0x5d,new HandleItemGetKinematic());
            handlers.Add(0x5e,new HandleItemGetPosition());
            handlers.Add(0x58,new HandleItemRemove());
            handlers.Add(0x5a,new HandleItemSetKinematic());
            handlers.Add(0x5b,new HandleItemSetPosition());
            handlers.Add(107,new HandleCommand());
            handlers.Add(114,new HandleDoorGetBlockAfterWarheadDetonation());
            handlers.Add(110,new HandleDoorGetDestoryed());
            handlers.Add(112,new HandleDoorGetDontOpenOnWarhead());
            handlers.Add(116,new HandleDoorGetLocked());
            handlers.Add(108,new HandleGetDoorOpen());
            handlers.Add(119,new HandleGetDoorName());
            handlers.Add(120,new HandleGetDoorPermission());
            handlers.Add(118,new HandleDoorPosition());
            handlers.Add(115,new HandleDoorSetBlockAfterWarheadDetonation());
            handlers.Add(111,new HandleDoorSetDestory());
            handlers.Add(113,new HandleDoorSetDontOpenOnWarhead());
            handlers.Add(117,new HandleDoorSetLocked());
            handlers.Add(109,new HandleDoorSetOpen());
            handlers.Add(0x60,new HandleServerGetIpAddress());
            handlers.Add(0x64,new HandleServerGetMaxPlayers());
            handlers.Add(0x62,new HandleServerGetNumPlayers());
            handlers.Add(0x63,new HandleServerGetPlayers());
            handlers.Add(0x5f,new HandleServerGetPort());
            handlers.Add(0x65,new HandleServerSetMaxPlayersPacket());
            handlers.Add(123,new HandleGetElevatorLockable());
            handlers.Add(121,new HandleElevatorLocked());
            handlers.Add(125,new HandleGetElevatorMovingSpeed());
            handlers.Add(127,new HandleGetElevatorPositions());
            handlers.Add(129,new HandleGetElevatorStatus());
            handlers.Add(128,new HandleGetElevatorType());
            handlers.Add(124,new HandleSetElevatorLockable());
            handlers.Add(122,new HandleSetElevatorLocked());
            handlers.Add(126,new HandleSetElevatorMovingSpeed());
            handlers.Add(130,new HandleUseElevator());
            handlers.Add(134,new HandleGeneratorGetEngaged());
            handlers.Add(135,new HandleGeneratorGetHasTablet());
            handlers.Add(136,new HandleGeneratorGetLocked());
            handlers.Add(132,new HandleGetDoorOpen());
            handlers.Add(137,new HandleGeneratorGetPosition());
            handlers.Add(139,new HandleGeneratorGetStartTime());
            handlers.Add(140,new HandleGeneratorTimeLeft());
            handlers.Add(141,new HandleGeneratorSetHasTablet());
            handlers.Add(133,new HandleDoorSetOpen());
            handlers.Add(143,new HandleGeneratorSetTimeLeft());
            handlers.Add(131,new HandleGeneratorUnlock());
            handlers.Add(132,new HandleGeneratorGetOpen());
            handlers.Add(180,new SimpleHandler());
            handlers.Add(181,new SimpleHandler());
            handlers.Add(182,new HandlePlayerContain106GetScp106s());
            handlers.Add(183,new HandlePlayerSetRoleItems());
            handlers.Add(184,new HandleTeamRespawnEventGetPlayers());
            handlers.Add(185,new HandleTeamRespawnEventSetPlayers());
            handlers.Add(190,new HandleDo());
        }
        public static void handleJsmod2(int id, String json,Dictionary<string,string> mapper,TcpClient client) 
        {
            try
            {
                //指令注册
                if (id == 0x53)
                {
                    //处理指令注册
                    NativeCommand command = JsonConvert.DeserializeObject(json, typeof(NativeCommand)) as NativeCommand;
                    ProxyHandler.handler.Info("registered jsmod2 command");
                    ProxyHandler.handler.AddCommand(command.commandName,new CommandHandler(command));
                    client.Close();
                }
                else
                {
                    
                    object o = null;
                    if (mapper.ContainsKey("player"))
                    {
                        string apiId = mapper["player"];//获取api对象id
                        o = ProxyHandler.handler.apiMapping[apiId];
                    }
                
                    if (handlers.ContainsKey(id))
                    {
                        ProxyHandler.handler.Info("handling the "+id);
                        Handler handler = handlers[id];
                        JsonSetting[] response = handler.handle(o,mapper);
                        if (response != null)
                        {
                            //将response对象发出去
                            ProxyHandler.handler.sendObjects(client,response);
                            client.Close();
                        }
                        else
                        {
                            client.Close();
                        }
                    }
                    else
                    {
                        client.Close();
                    }
                
                
                }
            }
            catch (Exception e)
            {
                ProxyHandler.handler.Error(e.Message);
            }

            if (Lib.getBool(ProxyHandler.handler.reader.get("jsmod2.debug")))
            {
                ProxyHandler.handler.Info("packet: id: "+id+" json: "+json+" finish a packet about jsmod2");
            }
            
        }
    }
}

public class Utils
{
    public static JsonSetting[] getOne(string id,object val,IdMapping mapping)
    {
        return new[] {new JsonSetting(Lib.getInt(id), val, mapping)};
    }

    public static object getTypeValue(string val)
    {
        int i1;
        bool b = int.TryParse(val,out i1);
        if (b)
        {
            return i1;
        }

        bool b1;
        b = bool.TryParse(val, out b1);
        if (b)
        {
            return b1;
        }

        float f1;
        b = float.TryParse(val, out f1);
        if (b)
        {
            return f1;
        }

        Vector vector = Lib.getVector(val);

        if (vector != null)
        {
            return vector;
        }

        char c1;

        b = char.TryParse(val, out c1);
        if (b)
        {
            return c1;
        }

        return val;

    }

    public static bool isCommon(Type returnType)
    {
        return returnType == typeof(string) || returnType == typeof(bool) ||
            returnType == typeof(float)
            || returnType == typeof(double) || returnType == typeof(String) ||
            returnType == typeof(Vector)
            || returnType == typeof(int) || returnType == typeof(long) || returnType == typeof(char)
            || returnType == typeof(byte) || returnType == typeof(short);
    }
}

public class HandleDo : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Type type = api.GetType();
        MethodInfo info = type.GetMethod(mapper["do"]);
        if (mapper.ContainsKey("args"))
        {
            string[] args = Lib.getArray(mapper["args"]);
            object[] dArgs = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                dArgs[i] = Utils.getTypeValue(args[i]);
            }
            info.Invoke(api,dArgs);
        }
        info.Invoke(api,null);
        return null;
    }
}

public interface Handler
{
    JsonSetting[] handle(object api,Dictionary<string,string> mapper);
}

public class HandleTeamRespawnEventSetPlayers : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        TeamRespawnEvent e = api as TeamRespawnEvent;
        string[] ids = Lib.getArray(mapper["players"]);
        List<Player> players = new List<Player>();
        foreach (string id in ids)
        {
            players.Add(ProxyHandler.handler.apiMapping[id] as Player);
        }

        e.PlayerList = players;
        return null;
    }
}

public class HandleTeamRespawnEventGetPlayers : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        TeamRespawnEvent e = api as TeamRespawnEvent;
        List<Player> players = e.PlayerList;
        JsonSetting[] settings = new JsonSetting[players.Count];
        for (int i = 0; i < settings.Length; i++)
        {
            settings[i] = new JsonSetting(Lib.getInt(mapper["id"]),null,
                new IdMapping().appendId(Lib.ID,Guid.NewGuid().ToString(),players[i]).appendId(Lib.PLAYER_SCPDATA_ID,Guid.NewGuid().ToString(),players[i].Scp079Data).appendId(Lib.PLAYER_TEAM_ROLE_ID,Guid.NewGuid().ToString(),players[i].TeamRole)
            );
        }

        return settings;
    }
}

public class HandlePlayerContain106GetScp106s : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        PlayerContain106Event e = api as PlayerContain106Event;
        Player[] players = e.SCP106s;
        JsonSetting[] settings = new JsonSetting[players.Length];
        for (int i = 0; i < settings.Length; i++)
        {
            settings[i] = new JsonSetting(Lib.getInt(mapper["id"]),null,
            new IdMapping().appendId(Lib.ID,Guid.NewGuid().ToString(),players[i]).appendId(Lib.PLAYER_SCPDATA_ID,Guid.NewGuid().ToString(),players[i].Scp079Data).appendId(Lib.PLAYER_TEAM_ROLE_ID,Guid.NewGuid().ToString(),players[i].TeamRole)
            );
        }

        return settings;
    }
}

public class HandlePlayerSetRoleItems : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        string[] strs = Lib.getArray(mapper["items"]);
        List<ItemType> types = new List<ItemType>();
        for (int i = 0; i < strs.Length; i++)
        {
            types.Add((ItemType)JsonConvert.DeserializeObject(strs[i],typeof(ItemType)));
        }
        PlayerSetRoleEvent e = api as PlayerSetRoleEvent;
        e.Items = types;
        return null;
    }
}

public class SimpleHandler : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Type type = api.GetType();
        if (mapper["id"].Equals("180"))//180  Get
        {
            PropertyInfo info = type.GetProperty(mapper["field"]);
            object obj;
            if (info != null)
            {
                obj = info.GetValue(api);
            }
            else
            {
                return null;
            }
            
            Type returnType = obj.GetType();
            bool isCommonType = Utils.isCommon(returnType);
            if (isCommonType)
            {
                return Utils.getOne(mapper["id"], obj, null);
            }
            return null;
        }
        
        string fieldName = mapper["field"];
        string val = mapper[fieldName];
        //object result = Utils.getTypeValue(val);
        object result;
        PropertyInfo info2 = type.GetProperty(fieldName);
        Type returnType2 = info2.PropertyType;
        if (Utils.isCommon(returnType2))
        {
            result = Utils.getTypeValue(val);
        }
        else
        {
            result = JsonConvert.DeserializeObject(val,returnType2);
        }
        if (mapper.ContainsKey("apiId"))
        {
            result =  ProxyHandler.handler.apiMapping[val];
        }
        info2.SetValue(api,result);
        return null;
    }
}

public class HandleGeneratorGetEngaged : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]), generator.Engaged, null)};
    }
}

public class HandleGeneratorGetHasTablet : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),generator.HasTablet,null)};
    }
}

public class HandleGeneratorGetLocked : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        return Utils.getOne(mapper["id"], generator.Locked, null);
    }
}

public class HandleGeneratorGetOpen : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        return Utils.getOne(mapper["id"], generator.Open, null);
    }
}

public class HandleGeneratorGetPosition : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        return Utils.getOne(mapper["id"], generator.Position, null);
    }
}

public class HandleGeneratorGetStartTime : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        return Utils.getOne(mapper["id"], generator.StartTime, null);
    }
}

public class HandleGeneratorTimeLeft : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        return Utils.getOne(mapper["id"], generator.TimeLeft, null);
    }
}

public class HandleGeneratorSetHasTablet : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        generator.HasTablet = Lib.getBool(mapper["hasTablet"]);
        return null;
    }
}

public class HandleGeneratorSetOpen : Handler
{
    JsonSetting[] Handler.handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        generator.Open = Lib.getBool(mapper["open"]);
        return null;
    }
}

public class HandleGeneratorSetTimeLeft : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        generator.TimeLeft = Lib.getDouble(mapper["timeLeft"]);
        return null;
    }
}

public class HandleGeneratorUnlock : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Generator generator = api as Generator;
        generator.Unlock();
        return null;
    }
}


public class HandleGetElevatorLockable : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),elevator.Lockable,null)};
    }
}

public class HandleElevatorLocked : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),elevator.Locked,null)};
    }
}


public class HandleGetElevatorMovingSpeed : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),elevator.MovingSpeed,null)};
    }
}

public class HandleGetElevatorPositions : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),elevator.GetPositions(),null)};
    }
}

public class HandleGetElevatorStatus : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),elevator.ElevatorStatus,null)};
    }
}

public class HandleGetElevatorType : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]), elevator.ElevatorType, null)};
    }
}

public class HandleSetElevatorLockable : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        elevator.Lockable = Lib.getBool(mapper["lockable"]);
        return null;
    }
}

public class HandleSetElevatorLocked : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        elevator.Locked = Lib.getBool(mapper["locked"]);
        return null;
    }
}

public class HandleSetElevatorMovingSpeed : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        elevator.MovingSpeed = Lib.getDouble(mapper["movingSpeed"]);
        return null;
    }
}

public class HandleUseElevator : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Elevator elevator = api as Elevator;
        elevator.Use();
        return null;
    }
}


//设置信息的监听器
//关于AdminQuery设置Admin
public class HandleAdminQuerySetAdmin : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        AdminQueryEvent o = api as AdminQueryEvent;
        //根据id找到api对象
        Player admin = ProxyHandler.handler.apiMapping["admin"] as Player;//从json中获取设置的值，反序列化
        o.Admin = admin;//设置
        return null;
    }
}

public class HandleServerGetIpAddress : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]), ProxyHandler.handler.Server.IpAddress, null)};
    }
}

public class HandleServerGetMaxPlayers : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),ProxyHandler.handler.Server.MaxPlayers,null)};
    }
}

public class HandleServerGetNumPlayers : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),ProxyHandler.handler.Server.NumPlayers,null)};
    }
}

public class HandleServerGetPlayers : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        List<Player> players = ProxyHandler.handler.Server.GetPlayers();
        JsonSetting[] settings = new JsonSetting[players.ToArray().Length];
        for (int i = 0; i < settings.Length; i++)
        {
            settings[i] = new JsonSetting(Lib.getInt(mapper["id"]),null,new IdMapping().appendId(Lib.ID,Guid.NewGuid().ToString(),players[i]).appendId(Lib.PLAYER_SCPDATA_ID,Guid.NewGuid().ToString(),players[i].Scp079Data).appendId(Lib.PLAYER_TEAM_ROLE_ID,Guid.NewGuid().ToString(),players[i].TeamRole));
        }

        return settings;
    }
}

public class HandleServerGetPort : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),ProxyHandler.handler.Server.Port,null)};
    }
}

public class HandleServerSetMaxPlayersPacket : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        ProxyHandler.handler.Server.MaxPlayers = Lib.getInt(mapper["id"]);
        return null;
    }
}


public class HandleItemDrop : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Item item = api as Item;
        item.Drop();
        return null;
    }
}

public class HandleItemGetComponent : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Item item = api as Item;

        int id= Lib.getInt(mapper["id"]);
        object o = item.GetComponent();
        //是否赋予id 待定
        return new[] {new JsonSetting(id,o,null)};
    }
}

public class HandleItemGetKinematic : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Item item = api as Item;
        bool kinematic = item.GetKinematic();
        int id = Lib.getInt(mapper["id"]);
        return new[] {new JsonSetting(id,kinematic,null)};
    }
}

public class HandleItemGetPosition : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Item item = api as Item;
        Vector vector = item.GetPosition();
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),vector,null)};
    }
}

public class HandleItemRemove : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Item item = api as Item;
        item.Remove();
        return null;
    }
}

//这个设置不了 不能使用
[Obsolete("could not set")]
public class HandleItemSetInWorld : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Item item = api as Item;
        string inWorld = mapper["inWorld"];
        return null;
    }
}

public class HandleItemSetKinematic : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Item item = api as Item;
        item.SetKinematic(Lib.getBool(mapper["kinematic"]));
        return null;
    }
}

public class HandleItemSetPosition : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Item item = api as Item;

        item.SetPosition(Lib.getVector(mapper["position"]));

        return null;
    }
}

public class HandleDoorGetBlockAfterWarheadDetonation : Handler
{
    JsonSetting[] Handler.handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        bool b = door.BlockAfterWarheadDetonation;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),b,null)};
    }
}

public class HandleDoorGetDestoryed : Handler
{
    JsonSetting[] Handler.handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),door.Destroyed,null)};
    }
}

public class HandleDoorGetDontOpenOnWarhead : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),door.DontOpenOnWarhead,null)};
    }
}

public class HandleDoorGetLocked : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),door.Locked,null)};
    }
}

public class HandleGetDoorName : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),door.Name,null),};
    }
}

public class HandleGetDoorOpen : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),door.Open,null)};
    }
}

public class HandleGetDoorPermission : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),door.Permission,null)};
    }
}

public class HandleDoorPosition : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        return new[] {new JsonSetting(Lib.getInt(mapper["id"]),door.Position,null)};
    }
}

public class HandleDoorSetBlockAfterWarheadDetonation : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        bool baw = Lib.getBool(mapper["blockAfterWarheadDetonation"]);
        door.BlockAfterWarheadDetonation = baw;
        return null;
    }
}

public class HandleDoorSetDestory : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        bool destory = Lib.getBool(mapper["destory"]);
        door.Destroyed = destory;
        return null;
    }
}

public class HandleDoorSetDontOpenOnWarhead : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        door.DontOpenOnWarhead = Lib.getBool(mapper["dontOpenOnWarhead"]);
        return null;
    }
}

public class HandleDoorSetLocked : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        door.Locked = Lib.getBool(mapper["locked"]);
        return null;
    }
}

public class HandleDoorSetOpen : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        Door door = api as Door;
        door.Open = Lib.getBool(mapper["isOpen"]);
        return null;
    }
}

public class HandleCommand : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    { 
        string name = mapper["name"];
        string args = mapper["args"];
        string[] argsC;
        if (args.Equals(""))
        {
            argsC = new string[0];
        }
        else
        {
            argsC = Lib.getArray(args);
        }
        string[] res = ProxyHandler.handler.CommandManager.CallCommand(ProxyHandler.handler.Server, name, argsC);
        return new []{new JsonSetting(Lib.getInt(mapper["id"]),res,null)};
    }
}

