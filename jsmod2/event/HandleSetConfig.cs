using System;
using Smod2.EventHandlers;
using Smod2.Events;

namespace jsmod2
{
    public class HandleSetConfig : IEventHandlerSetConfig
    {
        void IEventHandlerSetConfig.OnSetConfig(SetConfigEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x05,new IdMapping().appendId(Lib.ID,Guid.NewGuid().ToString(),ev));
        }
    }
}