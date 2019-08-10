using System;
using Smod2.EventHandlers;
using Smod2.Events;

namespace jsmod2
{
    //关于物品定位问题，通过id定位
    public class HandlePickupItemLate : IEventHandlerPlayerPickupItemLate
    {
        public void OnPlayerPickupItemLate(PlayerPickupItemLateEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x31,
                new IdMapping()
                    .appendId(Lib.ID,Guid.NewGuid().ToString(),ev)
                    .appendId(Lib.ITEM_EVENT_ID,Guid.NewGuid().ToString(),ev.Item)
                    .appendId(Lib.PLAYER_ID, Guid.NewGuid().ToString(), ev.Player)
                    .appendId(Lib.PLAYER_EVENT_SCPDATA_ID, Guid.NewGuid().ToString(), ev.Player.Scp079Data)
                    .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,Guid.NewGuid().ToString(),ev.Player.TeamRole)
                
                );
        }
    }
}