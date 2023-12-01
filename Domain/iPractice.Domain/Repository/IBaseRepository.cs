using iPractice.Domain.Entities;

namespace iPractice.Domain.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAsync(List<T> entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
