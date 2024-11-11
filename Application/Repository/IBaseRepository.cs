using Domain.Common;

namespace Application.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<(IEnumerable<T> result, int allCount, int pagesAmount)> GetByPagination(int pageSize = 10, int pageNumber = 0, CancellationToken cancellationToken = default);
    }
}
