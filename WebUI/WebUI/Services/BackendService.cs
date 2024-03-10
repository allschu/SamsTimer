using Admin.Shared;
using Admin.Shared.Contracts;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace WebUI.Services
{
    public class BackendService : IBackendService
    {
        private readonly HttpClient _backendClient;
        private readonly ILogger<BackendService> _logger;

        public BackendService(HttpClient httpClient, ILogger<BackendService> logger)
        {
            _logger = logger;
            _backendClient = httpClient;
        }
        public async Task<ApiResult<PagedList<User>>> SearchUsersAsync(int page, int pageSize, CancellationToken token = default)
        {
            return await SearchModelAsync<User>("/api/users", page, pageSize, token);
        }

        public async Task<ApiResult<PagedList<TModel>>> SearchModelAsync<TModel>(string uri, int page, int pageSize, CancellationToken token = default)
        {
            var queryParams = new Dictionary<string, string> { { "Page", $"{page}" }, { "PageSize", $"{pageSize}" } };
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    queryParams.Add("search", search);
            //}
            //if (!string.IsNullOrWhiteSpace(orderProperty))
            //{
            //    queryParams.Add("orderProperty", orderProperty);
            //    queryParams.Add("orderAscending", ascending ? "1" : "0");
            //}
            var url = QueryHelpers.AddQueryString(uri, queryParams);

            _logger.LogInformation($"Loading paged data from backend: {_backendClient.BaseAddress + url}");

            var responseTask = _backendClient.GetAsync(url, token);
            return await HandleJsonResponse(responseTask, PagedList<TModel>.Empty, token);
        }

        private async Task<ApiResult<TModel>> HandleJsonResponse<TModel>(Task<HttpResponseMessage> taskResponse, TModel? defaultValue = default, CancellationToken token = default)
        {
            try
            {
                using HttpResponseMessage response = await taskResponse.ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Failed to load JSON response from backend. Status code: {response.StatusCode}");
                }

                bool hasContent = response.StatusCode != HttpStatusCode.NoContent;

                if (hasContent && response.Content.Headers.ContentType?.MediaType != "application/json")
                {
                    return new ApiResult<TModel>()
                    {
                        Value = defaultValue,
                        IsSuccess = false,
                        ProblemDetails = await TryGetProblemDetails(response, token)
                    };
                }

                var data = hasContent ? await response.Content.ReadFromJsonAsync<TModel>(cancellationToken: token).ConfigureAwait(false) : defaultValue;

                return new ApiResult<TModel>()
                {
                    Value = data,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load JSON response from backend.", ex);

                return new ApiResult<TModel>()
                {
                    Value = defaultValue,
                    IsSuccess = false
                };
            }
        }

        private async Task<ProblemDetails?> TryGetProblemDetails(HttpResponseMessage response, CancellationToken token)
        {
            if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: token);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to parse problem details from response.", ex);
                }
            }
            return null;
        }
    }

}
