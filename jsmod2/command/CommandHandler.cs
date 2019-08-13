using System;
using Newtonsoft.Json;
using Smod2.API;
using Smod2.Commands;

//指令的触发器
namespace jsmod2.command
{
    /**
     * 管理全部的jsmod2指令，中转指令
     */
    public class CommandHandler : ICommandHandler
    {
        private string name;

        private string description;

        public CommandHandler(NativeCommand cmd)
        {
            this.name = cmd.commandName;
            this.description = cmd.description;
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (sender is Server)
            {
                ServerCommandVO vo = new ServerCommandVO();
                vo.args = args;
                vo.commandName = name;
                ProxyHandler.handler.sendObject(JsonConvert.SerializeObject(vo),0x55,null,false);
            }
            else if (sender is Player){
                PlayerCommandVO vo = new PlayerCommandVO();
                vo.args = args;
                vo.commandName = name;
                ProxyHandler.handler.sendObject(JsonConvert.SerializeObject(vo),0x56,new IdMapping().appendId("player-playerName",Guid.NewGuid().ToString(),sender),false);
            }
            return new []{"success"};
        }

        public string GetUsage()
        {
            return name;
        }

        public string GetCommandDescription()
        {
            return description;
        }
    }
    //服务器的指令
    public class PlayerCommandVO : CommandVO
    {
    }
    //玩家的指令
    public class ServerCommandVO : CommandVO
    {
    }

    public class CommandVO
    {
        public String commandName { get; set; }
        public String[] args { get; set; }
        
    }

    public class NativeCommand
    {
        public String commandName { get; set; }
        
        public String power { get; set; }
        
        public String description { get; set; }
    }
    
    
}