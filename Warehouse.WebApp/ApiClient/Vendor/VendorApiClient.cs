using Newtonsoft.Json;
using System.Text;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.Vendor;

namespace Warehouse.WebApp.ApiClient
{
    public class VendorApiClient : IVendorApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VendorApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Create(VendorModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.PostAsync("vendor/create", httpContent);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<ApiResult<Pagination<VendorModel>>> GetPagings(GetVendorPagingRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");

            var response = await client.GetAsync($"/vendor/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var vendor = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<VendorModel>>>(body);
            return vendor;
        }
    }
}