using Admin.Shared;

namespace Admin.Infrastructure.Interfaces
{
    public interface IGenericRepository <T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}
