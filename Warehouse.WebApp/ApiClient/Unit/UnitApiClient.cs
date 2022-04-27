using Newtonsoft.Json;
using System.Text;
using Warehouse.Model.Unit;

namespace Warehouse.WebApp.ApiClient
{
    public class UnitApiClient : IUnitApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UnitApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Create(UnitModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.PostAsync("unit/create", httpContent);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAll()
        {
            var json = JsonConvert.SerializeObject(null);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:2000");
            var response = await client.GetAsync("api/unit/GetAll");

            return await response.Content.ReadAsStringAsync();
        }
    }
}