using Smod2.EventHandlers;
using Smod2.Events;

namespace jsmod2
{
    public class HandleAdmin : IEventHandlerAdminQuery
    {
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x01,
                new IdMapping()
                    .appendId(Lib.ID,System.Guid.NewGuid().ToString(),ev)
                    .appendId("admin",System.Guid.NewGuid().ToString(),ev.Admin)
                    .appendId(Lib.ADMIN_EVENT_SCPDATA_ID,System.Guid.NewGuid().ToString(),ev.Admin.Scp079Data)
                    
                );
        }
    }
}