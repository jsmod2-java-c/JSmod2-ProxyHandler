using System;
using Smod2.EventHandlers;
using Smod2.Events;

namespace jsmod2
{
    public class HandlePickupItem : IEventHandlerPlayerPickupItem
    {
        void IEventHandlerPlayerPickupItem.OnPlayerPickupItem(PlayerPickupItemEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x31,new IdMapping()
                .appendId(Lib.ID,ev,Guid.NewGuid().ToString())
                .appendId(Lib.PLAYER_ID,ev.Player,Guid.NewGuid().ToString())
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,ev.Player.Scp079Data,Guid.NewGuid().ToString())
                .appendId(Lib.ITEM_EVENT_ID,ev.Item,Guid.NewGuid().ToString())
            );
        }
    }
}