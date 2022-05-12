using Master.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Warehouse.Common;

namespace Master.WebApi.SignalRHubs
{
  //  [Authorize]
    public class ConnectRealTimeHub : Hub
    {
        private ICreatedByService _userService;

        public ConnectRealTimeHub()
        {

        }
        private void GetUserByService()
        {
            if (_userService == null)
                _userService = Context.GetHttpContext().RequestServices.GetService<ICreatedByService>();
        }

        public async Task WareHouseBookTrachking(string id)
        {
         //   GetUserByService();
            var res = new ResultMessageResponse()
            {
                data = id,
                message = "test"
            };
            await Clients.Others.SendAsync("WareHouseBookTrachkingToCLient", res, Guid.NewGuid().ToString());
        } 
    }
}
