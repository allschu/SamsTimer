using Admin.Shared;
using Admin.Shared.Contracts;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http.Json;

namespace WebUI.Client.BackendServices
{
    public class UserService(HttpClient httpClient) : IUserService
    {
        public async Task<PagedList<User>> SearchUsersAsync(int page, int pageSize, CancellationToken token = default)
        {
            var queryParams = new Dictionary<string, string> { { "page", $"{page}" }, { "pageSize", $"{pageSize}" } };
            
            var result = await httpClient.GetFromJsonAsync<ApiResult<PagedList<User>>>(QueryHelpers.AddQueryString("api/users", queryParams), token);
            
            if (result.IsSuccess)
            {
                return result.Value;
            }
            else
            {
                throw new Exception("Failed to retrieve users");
            }
        }
    }
}
