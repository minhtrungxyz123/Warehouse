using Master.Service;
using Microsoft.AspNetCore.SignalR;
using Warehouse.Common;

namespace Master.WebApi.SignalRHubs
{
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
            //GetUserByService();
            var res = new ResultMessageResponse()
            {
                data = id,
                //success = _userService != null,
                //message = _userService != null ? "Dữ liệu cập nhật, sau khi " + _userService.User.AccountName + " chỉnh sửa !" : "Không tìm thấy dữ liệu"
                message = "test"
            };
            await Clients.Others.SendAsync("WareHouseBookTrachkingToCLient", res/*, _userService.User.Id*/);
        }

        public async Task UnitEdit(string id)
        {
            var res = new ResultMessageResponse()
            {
                data = id,
                message = "test"
            };
            await Clients.Others.SendAsync("UnitEditToCLient", res, id);
        }

        public async Task UnitDelete(string id)
        {
            var res = new ResultMessageResponse()
            {
                data = id,
                message = "test"
            };
            await Clients.Others.SendAsync("UnitDeleteToCLient", res, id);
        }
    }
}