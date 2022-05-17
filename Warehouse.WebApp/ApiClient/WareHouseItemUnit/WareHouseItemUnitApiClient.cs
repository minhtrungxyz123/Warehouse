using Newtonsoft.Json;
using Warehouse.Common;
using Warehouse.Model.WareHouseItemUnit;

namespace Warehouse.WebApp.ApiClient
{
    public class WareHouseItemUnitApiClient :IWareHouseItemUnitApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WareHouseItemUnitApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region Method
        public async Task<ApiResult<WareHouseItemUnitModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/wwarehouse-item-unit/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<WareHouseItemUnitModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<WareHouseItemUnitModel>>(body);
        }

        #endregion
    }
}
