using Admin.Shared.Contracts;
using Admin.Shared;

namespace WebUI.Services
{
    public interface IBackendService
    {
        Task<ApiResult<PagedList<User>>> SearchUsersAsync(int page, int pageSize, CancellationToken token = default);
    }
}
