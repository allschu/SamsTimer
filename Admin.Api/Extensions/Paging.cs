using Admin.Shared.Contracts;
using Raven.Client.Documents;

namespace Admin.Api.Extensions
{
    public static class Paging
    {
        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken token)
        {
            var count = await source.CountAsync(token);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(token);

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
