using System.Linq.Expressions;

namespace FormMaster.DAL.Repositories;

public interface IGenericRepository<T>
{
    Task Add(T entity);
    Task Delete(T entity);
    Task Update(T entity);
    IQueryable<T> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    T? GetById(int id);
}
