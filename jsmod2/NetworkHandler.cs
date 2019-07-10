using System;
using System.Net.Sockets;
using jsmod2.command;
using Newtonsoft.Json;

namespace jsmod2
{
    /**
     * 根据事件发送序列化的Event对象
     */
    //handleJsmod2功能有
    //截取注册指令发包，分配一个CommandHandler [OK]
    //截取set和get包，调用相应方法，并将返回值返回
    //sendPacket方法
    //触发事件时，将事件对象序列化传递给jsmod2
    //触发指令时，当指令来自于jsmod2注册，CommandHandler将指令信息封装(Command对象，Sender对象，参数)
    //，发到jsmod2 [OK]
    
    //TODO 截取set和get包，调用相应方法，并将返回值返回
    //TODO 触发事件时，将事件对象序列化传递给jsmod2
    public class NetworkHandler
    {
        public static void handleJsmod2(int id, String json, String end,TcpClient stream) 
        {
            //指令注册
            if (id == 0x53)
            {
                //处理指令注册
                NativeCommand command = JsonConvert.DeserializeObject(json, typeof(NativeCommand)) as NativeCommand;
                ProxyHandler.handler.AddCommand(command.commandName,new CommandHandler(command));
            }
        }
    }
}