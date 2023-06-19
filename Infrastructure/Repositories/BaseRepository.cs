using Core.Boundaries.Infrastructure.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Optional;
using Optional.Unsafe;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly SqlContext _context;
        protected readonly int MaxElementsPerPage = 10;

        public BaseRepository(SqlContext context)
        {
            _context = context;  
        }

        public virtual async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(Guid id)
        {
            Option<T> entityOption = (await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id)).SomeNotNull()!;

            if (entityOption.HasValue)
            {
                T entity = entityOption.ValueOrFailure();
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll(int currentPage, params string[] propertiesPath)
        {
            IQueryable<T> entities =  _context.Set<T>();

            int elementsToSkip = (currentPage - 1) * MaxElementsPerPage;

            foreach (string property in propertiesPath)
            {
                entities = _context.Set<T>().Include(property);
            }

            IEnumerable<T> result = await entities
                                            .Skip(elementsToSkip)
                                            .Take(MaxElementsPerPage)
                                            .ToListAsync();

            return result;
        }

        public virtual async Task<Option<T>> GetById(Guid id, params string[] includes)
        {
            IQueryable<T> entities = _context.Set<T>();

            foreach (string property in includes)
            {
                entities = _context.Set<T>().Include(property);
            }

            Option<T> entityOption = (await entities.FirstOrDefaultAsync(x => x.Id == id)).SomeNotNull()!;

            return entityOption;
        }

        public virtual async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
