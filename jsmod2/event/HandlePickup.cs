using System;
using Smod2.EventHandlers;
using Smod2.Events;

namespace jsmod2
{
    //关于物品定位问题，通过id定位
    public class HandlePickup : IEventHandlerPlayerPickupItemLate
    {
        public void OnPlayerPickupItemLate(PlayerPickupItemLateEvent ev)
        {
            String id = System.Guid.NewGuid().ToString();
            ProxyHandler.handler.itemMapping.Add(id,ev.Item);
            ProxyHandler.handler.sendEventObject(ev,0x31,"item-playerName",id);
        }
    }
}