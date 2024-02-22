using Admin.Infrastructure.Interfaces;
using Admin.Shared;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace Admin.Infrastructure.Repositories
{
    public class GenericRepository<T> where T : BaseEntity, IGenericRepository<T>
    {
        private readonly IAsyncDocumentSession _session;

        public GenericRepository()
        {
            _session = DocumentStoreWrapper.Store.OpenAsyncSession();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _session.LoadAsync<T>(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _session.Query<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _session.StoreAsync(entity);
            await _session.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _session.StoreAsync(entity);
            await _session.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            _session.Delete(entity);
            await _session.SaveChangesAsync();
        }
    }
}
