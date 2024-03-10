using Admin.Shared;
using Admin.Shared.Contracts;
using WebUI.Client.BackendServices;
using WebUI.Services;

namespace WebUI.Frontend
{
    public class UserServerService : IUserService
    {
        private readonly IBackendService _backendService;
        public UserServerService(IBackendService backendService)
        {
            _backendService = backendService;
        }

        public async Task<PagedList<User>> SearchUsersAsync(int page, int pageSize, CancellationToken token = default)
        {
            var result = await _backendService.SearchUsersAsync(page, pageSize, token);

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
