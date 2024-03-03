using Admin.Shared.Contracts;
using Admin.Shared;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace BlazorUI.Services
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
        public async Task<ApiResult<PagedList<User>>> SearchUsersAsync(int page, int pageSize, CancellationToken token = default)
        {
            return await SearchModelAsync<User>("/api/users", page, pageSize, token);
        }

        public async Task<ApiResult<PagedList<TModel>>> SearchModelAsync<TModel>(string uri, int page, int pageSize, CancellationToken token = default)
        {
            var queryParams = new Dictionary<string, string> { { "page", $"{page}" }, { "pageSize", $"{pageSize}" } };
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    queryParams.Add("search", search);
            //}
            //if (!string.IsNullOrWhiteSpace(orderProperty))
            //{
            //    queryParams.Add("orderProperty", orderProperty);
            //    queryParams.Add("orderAscending", ascending ? "1" : "0");
            //}

            
            var responseTask = _backendClient.GetAsync(QueryHelpers.AddQueryString(uri, queryParams), token);
            return await HandleJsonResponse(responseTask, PagedList<TModel>.Empty, token);
        }


        private async Task<ApiResult<TModel>> HandleJsonResponse<TModel>(Task<HttpResponseMessage> taskResponse, TModel? defaultValue = default, CancellationToken token = default)
        {
            try
            {
                
                using HttpResponseMessage response = await taskResponse.ConfigureAwait(false);

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

    public class ApiResult<T>
    {
        public T? Value { get; set; }
        public bool IsSuccess { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }
    }
}
