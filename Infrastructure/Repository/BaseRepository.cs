using Application.Repository;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(T entity)
        {
            _context.Add(entity);
            return true;
        }

        public bool Update(T entity)
        {
            _context.Update(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            _context.Remove(entity);
            return true;
        }

        public async Task<T> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<T> result, int allCount, int pagesAmount)> GetByPagination(int pageSize = 10, int pageNumber = 0, CancellationToken cancellationToken = default)
        {
            var set = _context.Set<T>().AsQueryable();

            int allCount = set.Count();

            int pagesAmount = Convert.ToInt32(Math.Ceiling((decimal)allCount / pageSize));

            int skip = pageSize * (pageNumber - 1);

            if(pageNumber > 1 && pagesAmount > 1)
            {
                set = set.Skip(pageNumber - 1);
            }

            if (pageSize != 0)
            {
                set = set.Take(pageSize);
            }

            var result = set.ToList();

            return (result, allCount, pagesAmount);
        }
    }
}
