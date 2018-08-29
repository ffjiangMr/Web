using Microsoft.AspNet.SignalR;
using System.Linq;

namespace Source.Admin.Web.SignalR
{
    public class SysMessageHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}