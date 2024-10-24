using FormMaster.DAL.DataContext;
using FormMaster.DAL.Repositories.Contracts;
using System.Linq.Expressions;

namespace FormMaster.DAL.Repositories.Implementations;

public class GenericRepository<T>(FormMasterDbContext context) : IGenericRepository<T> where T : class
{
    public async Task Add(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task AddRange(ICollection<T> entities)
    {
        await context.Set<T>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public IQueryable<T> GetAll()
    {
        return context.Set<T>();
    }

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().Where(expression);
    }

    public T? GetById(int id)
    {
        return context.Set<T>().Find(id);
    }

    public async Task Update(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }
}
