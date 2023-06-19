using Core.Entities;
using Optional;

namespace Core.Boundaries.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task Create(T entity);

        Task Update(T entity);

        Task Delete(Guid id);

        Task<IEnumerable<T>> GetAll(int currentPage, params string[] includes);

        Task<Option<T>> GetById(Guid id, params string[] includes);
    }
}
