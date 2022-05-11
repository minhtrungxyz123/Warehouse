using Newtonsoft.Json;
using System.Text;
using Warehouse.Common;
using Warehouse.Common.Common;
using Warehouse.Model.CreatedBy;

namespace Master.WebApp.ApiClient
{
    public class CreatedByApiClient : ICreatedByApiClient
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;

        public CreatedByApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #endregion Fields

        #region Method

        public async Task<bool> Create(CreatedByModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:1000");
            var response = await client.PostAsync("created-by/create", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Edit(string id, CreatedByModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:1000");
            var response = await client.PutAsync($"created-by/update/" + id + "", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:1000");
            var response = await client.DeleteAsync($"/created-by/delete?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(body);

            return JsonConvert.DeserializeObject<bool>(body);
        }
        #endregion Method

        #region List

        public async Task<ApiResult<Pagination<CreatedByModel>>> GetPagings(GetCreatedByPagingRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:1000");

            var response = await client.GetAsync($"/created-by/get?keyword={request.Keyword}&pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var createdBy = JsonConvert.DeserializeObject<ApiSuccessResult<Pagination<CreatedByModel>>>(body);
            return createdBy;
        }

        public async Task<ApiResult<CreatedByModel>> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:1000");
            var response = await client.GetAsync($"/created-by/get-by-id?id={id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<CreatedByModel>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<CreatedByModel>>(body);
        }

        #endregion List
    }
}
