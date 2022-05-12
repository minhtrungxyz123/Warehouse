using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Master.WebApp.SignalRHubs
{
    [Authorize]
    public class ConnectRealTimeHub : Hub
    {
    }
}
