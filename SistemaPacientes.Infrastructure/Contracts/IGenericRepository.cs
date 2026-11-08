using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Infrastructure.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T? GetById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
