using Smod2.EventHandlers;
using Smod2.Events;

namespace jsmod2
{
    public class HandlePlayer : IEventHandlerPlayerJoin
    {
        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x2d,
                new IdMapping()
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,System.Guid.NewGuid().ToString()));
        }
    }
}