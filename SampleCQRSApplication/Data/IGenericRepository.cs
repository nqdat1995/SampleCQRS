using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SampleCQRSApplication.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetSingle(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> GetByID(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}