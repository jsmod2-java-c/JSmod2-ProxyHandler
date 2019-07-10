using Smod2.EventHandlers;
using Smod2.Events;

namespace jsmod2
{
    public class HandleAdmin : IEventHandlerAdminQuery
    {
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x01,"","");
        }
    }
}