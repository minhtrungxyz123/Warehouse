using Newtonsoft.Json;
using System.Text;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.WareHouseItemCategory;

namespace Warehouse.WebApp.ApiClient
{
    public class WareHouseItemCategoryApiClient :IWareHouseItemCategoryApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WareHouseItemCategoryApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Fields

        #region Method

        public async Task<bool> Create(WareHouseItemCategoryModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.PostAsync("wareHouse-itemCategory/create", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string id, WareHouseItemCategoryModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.PutAsync($"wareHouse-itemCategory/update/" + id + "", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.DeleteAsync($"/wareHouse-itemCategory/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }

        #endregion Method

        #region List

        public async Task<ApiResult<Pagination<WareHouseItemCategoryModel>>> GetPagings(GetWareHouseItemCategoryPagingRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");

            var response = await client.GetAsync($"/wareHouse-itemCategory/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var vendor = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<WareHouseItemCategoryModel>>>(body);
            return vendor;
        }

        public async Task<ApiResult<WareHouseItemCategoryModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.GetAsync($"/wareHouse-itemCategory/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<WareHouseItemCategoryModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<WareHouseItemCategoryModel>>(body);
        }

        public async Task<IList<WareHouseItemCategoryModel>> GetAvailableList(bool showHidden = true)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.GetAsync($"/wareHouse-itemCategory/get-available?showHidden={showHidden}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IList<WareHouseItemCategoryModel>>(body);

            return JsonConvert.DeserializeObject<IList<WareHouseItemCategoryModel>>(body);
        }

        #endregion List
    }
}
