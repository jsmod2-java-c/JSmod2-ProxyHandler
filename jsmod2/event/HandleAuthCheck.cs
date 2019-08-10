using System;
using Smod2.EventHandlers;
using Smod2.Events;

namespace jsmod2
{
    public class HandleAuthCheck : IEventHandlerAuthCheck
    {
        void IEventHandlerAuthCheck.OnAuthCheck(AuthCheckEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev, 0x03,
                new IdMapping()
                    .appendId(Lib.ID, System.Guid.NewGuid().ToString(), ev)
                    .appendId("requester-" + Lib.ID, Guid.NewGuid().ToString(), ev.Requester)
                    .appendId(Lib.AUTH_CHECK_EVENT_REQUESTER_SCPDATA_ID, Guid.NewGuid().ToString(),
                        ev.Requester.Scp079Data).appendId("requester-"+Lib.PLAYER_TEAM_ROLE_ID,Guid.NewGuid().ToString(),ev.Requester.TeamRole));

        }
    }
}