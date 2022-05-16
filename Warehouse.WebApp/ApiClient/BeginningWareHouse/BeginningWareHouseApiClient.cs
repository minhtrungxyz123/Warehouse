using Newtonsoft.Json;
using System.Text;
using Warehouse.Common;
using Warehouse.Model.BeginningWareHouse;
using Warehouse.Model.Unit;
using Warehouse.Model.WareHouseItem;
using Warehouse.Model.WareHouseItemUnit;

namespace Warehouse.WebApp.ApiClient
{
    public class BeginningWareHouseApiClient : IBeginningWareHouseApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BeginningWareHouseApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region Method

        public async Task<bool> Create(BeginningWareHouseModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("beginning-wareHouse/create", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string id, BeginningWareHouseModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PutAsync($"beginning-wareHouse/update/" + id + "", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.DeleteAsync($"/beginning-wareHouse/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        #endregion Method

        #region List

        public async Task<ApiResult<Pagination<BeginningWareHouseModel>>> GetPagings(GetBeginningWareHousePagingRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/beginning-wareHouse/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var vendor = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<BeginningWareHouseModel>>>(body);
            return vendor;
        }

        public async Task<ApiResult<BeginningWareHouseModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/beginning-wareHouse/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<BeginningWareHouseModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<BeginningWareHouseModel>>(body);
        }

        public async Task<List<WareHouseItemUnitModel>> GetByWareHouseItemUnitId(string getUnitItem)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/warehouse-item-unit/get?ItemId={getUnitItem}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<WareHouseItemUnitModel>>(body);

            return JsonConvert.DeserializeObject<List<WareHouseItemUnitModel>>(body);
        }

        public async Task<WareHouseItemModel> GetByWareHouseItemId(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/wareHouse-item/get-by-id-unit?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<WareHouseItemModel>(body);

            return JsonConvert.DeserializeObject<WareHouseItemModel>(body);
        }

        #endregion List
    }
}