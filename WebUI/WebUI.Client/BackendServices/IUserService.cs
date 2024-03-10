using Admin.Shared;
using Admin.Shared.Contracts;

namespace WebUI.Client.BackendServices
{
    public interface IUserService
    {
        Task<PagedList<User>> SearchUsersAsync(int page, int pageSize, CancellationToken token = default);
    }
}
