using Newtonsoft.Json;
using System.Text;
using Warehouse.Common;
using Warehouse.Model.Unit;
using Warehouse.Model.Vendor;
using Warehouse.Model.WareHouseItem;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.WebApp.ApiClient
{
    public class WareHouseItemApiClient : IWareHouseItemApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WareHouseItemApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region Method

        public async Task<bool> Create(WareHouseItemModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("wareHouse-item/create", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string id, WareHouseItemModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PutAsync($"wareHouse-item/update/" + id + "", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.DeleteAsync($"/wareHouse-item/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }
        #endregion Method

        #region List

        public async Task<ApiResult<Pagination<WareHouseItemModel>>> GetPagings(GetWareHouseItemPagingRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.GetAsync($"/wareHouse-item/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var unit = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<WareHouseItemModel>>>(body);
            return unit;
        }

        public async Task<ApiResult<WareHouseItemModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/wareHouse-item/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<WareHouseItemModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<WareHouseItemModel>>(body);
        }

        public async Task<IList<UnitModel>> GetAvailableList(bool showHidden = true)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/unit/get-available?showHidden={showHidden}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IList<UnitModel>>(body);

            return JsonConvert.DeserializeObject<IList<UnitModel>>(body);
        }

        public async Task<IList<VendorModel>> GetVendor(bool showHidden = true)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/vendor/get-available?showHidden={showHidden}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IList<VendorModel>>(body);

            return JsonConvert.DeserializeObject<IList<VendorModel>>(body);
        }

        public async Task<IList<WareHouseItemCategoryModel>> GetCategory(bool showHidden = true)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/wareHouse-itemCategory/get-available?showHidden={showHidden}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IList<WareHouseItemCategoryModel>>(body);

            return JsonConvert.DeserializeObject<IList<WareHouseItemCategoryModel>>(body);
        }

        public async Task<WareHouseItemModel> GetByIdAync(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/wareHouse-item/edit?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<WareHouseItemModel>(body);

            return JsonConvert.DeserializeObject<WareHouseItemModel>(body);
        }

        public async Task<IList<WareHouseItemModel>> GetAvailableItem(bool showHidden = true)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"/wareHouse-item/get-available?showHidden={showHidden}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IList<WareHouseItemModel>>(body);

            return JsonConvert.DeserializeObject<IList<WareHouseItemModel>>(body);
        }

        #endregion List
    }
}
