using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFactura.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // T es un tipo generico de clase (TEntityModel)
        Task<IQueryable<T>> GetAll();
        Task<T> GetById(object id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(object id);
    }
}
