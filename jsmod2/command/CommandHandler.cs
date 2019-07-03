using Smod2.Commands;

namespace jsmod2.command
{
    /**
     * 管理全部的jsmod2指令，中转指令
     */
    public class CommandHandler : ICommandHandler
    {
        public string[] OnCall(ICommandSender sender, string[] args)
        {
            throw new System.NotImplementedException();
        }

        public string GetUsage()
        {
            throw new System.NotImplementedException();
        }

        public string GetCommandDescription()
        {
            throw new System.NotImplementedException();
        }
    }
}