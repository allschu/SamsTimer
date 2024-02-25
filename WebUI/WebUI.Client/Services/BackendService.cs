using Admin.Shared;
using Admin.Shared.Contracts;
using Microsoft.AspNetCore.WebUtilities;

namespace WebUI.Client.Services
{
    public class BackendService
    {
        private readonly HttpClient _backendClient;
        private readonly ILogger<BackendService> _logger;

        public BackendService(HttpClient backendClient, ILogger<BackendService> logger)
        {
            _backendClient = backendClient;
            _logger = logger;
        }
        public async Task<ApiResult<PagedList<User>>> SearchWasteFlowAsync(int page, int pageSize, string? search, string? orderProperty, bool ascending = false, bool isDangerous = false, CancellationToken token = default)
        {
            return await SearchModelAsync<User>("wasteflow", page, pageSize, search, orderProperty, ascending, isDangerous, token);
        }

        public async Task<ApiResult<PagedList<TModel>>> SearchModelAsync<TModel>(string uri, int page, int pageSize, string? search, string? orderProperty, bool ascending = false, bool isDangerous = false, CancellationToken token = default)
        {
            var queryParams = new Dictionary<string, string> { { "page", $"{page}" }, { "pageSize", $"{pageSize}" } };
            if (!string.IsNullOrWhiteSpace(search))
            {
                queryParams.Add("search", search);
            }
            if (!string.IsNullOrWhiteSpace(orderProperty))
            {
                queryParams.Add("orderProperty", orderProperty);
                queryParams.Add("orderAscending", ascending ? "1" : "0");
            }

            var responseTask = _backendClient.GetAsync(QueryHelpers.AddQueryString(uri, queryParams), token);
            return await HandleJsonResponse(responseTask, PagedList<TModel>.Empty, token);
        }
    }
}
