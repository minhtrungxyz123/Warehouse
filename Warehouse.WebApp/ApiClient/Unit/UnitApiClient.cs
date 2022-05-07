using Newtonsoft.Json;
using System.Text;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Unit;

namespace Warehouse.WebApp.ApiClient
{
    public class UnitApiClient : IUnitApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Method

        public async Task<bool> Create(UnitModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.PostAsync("unit/create", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(UnitModel request, string id)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.PostAsync($"unit/update/"+id+"", httpContent);

            return response.IsSuccessStatusCode;
        }
        #endregion

        #region List

        public async Task<ApiResult<Pagination<UnitModel>>> GetPagings(GetUnitPagingRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");

            var response = await client.GetAsync($"/unit/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var unit = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<UnitModel>>>(body);
            return unit;
        }

        #endregion
    }
}