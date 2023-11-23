using MC_Projekt_wypozyczalni.Models;
using System.Linq.Expressions;

namespace MC_Projekt_wypozyczalni.Services
{
    public interface IRepositoryService<T> where T : IEntity
    {
        IQueryable<T> GetAllRecords();

        T GetSingle(int id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        ServiceResult Add(T entity);
        ServiceResult Delete(T entity);
        ServiceResult Edit(T entity);
        ServiceResult Save(T entity);
    }
}
