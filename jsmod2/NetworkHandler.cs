using System;
using System.Collections.Generic;
using System.Net.Sockets;
using jsmod2;
using jsmod2.command;
using Newtonsoft.Json;
using Smod2.API;
using Smod2.Events;

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
            handlers.Add(0x5c,new HandleItemSetComponent());
            handlers.Add(0x5d,new HandleItemGetKinematic());
        }
        public static void handleJsmod2(int id, String json,Dictionary<string,string> mapper, String end,TcpClient client) 
        {
            
            //指令注册
            if (id == 0x53)
            {
                //处理指令注册
                NativeCommand command = JsonConvert.DeserializeObject(json, typeof(NativeCommand)) as NativeCommand;
                ProxyHandler.handler.AddCommand(command.commandName,new CommandHandler(command));
            }
            else
            {
                string apiId = mapper["player"];//获取api对象id
                object o = ProxyHandler.handler.apiMapping[apiId];
                JsonSetting[] response = handlers[id].handle(o,mapper);
                if (response != null)
                {
                    //将response对象发出去
                    ProxyHandler.handler.sendObjects(client,response);
                }
            }
            
        }
    }
}

public interface Handler
{
    JsonSetting[] handle(object api,Dictionary<string,string> mapper);
}

//设置信息的监听器
//关于AdminQuery设置Admin
public class HandleAdminQuerySetAdmin : Handler
{
    public JsonSetting[] handle(object api, Dictionary<string, string> mapper)
    {
        AdminQueryEvent o = api as AdminQueryEvent;
        //根据id找到api对象
        Player admin = (Player)Lib.getObject(mapper,typeof(Player),"admin");//从json中获取设置的值，反序列化
        o.Admin = admin;//设置
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

public class HandleItemSetComponent : Handler
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