using Newtonsoft.Json;
using System.Text;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.WareHouse;

namespace Warehouse.WebApp.ApiClient.WareHouse
{
    public class WareHouseApiClient : IWareHouseApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WareHouseApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Method

        public async Task<string> Create(WareHouseModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.PostAsync("warehouse/create", httpContent);

            return await response.Content.ReadAsStringAsync();
        }
        #endregion

        #region List

        public async Task<ApiResult<Pagination<WareHouseModel>>> GetPagings(GetWareHousePagingRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");

            var response = await client.GetAsync($"/warehouse/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var warehouse = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<WareHouseModel>>>(body);
            return warehouse;
        }

        #endregion
    }
}
