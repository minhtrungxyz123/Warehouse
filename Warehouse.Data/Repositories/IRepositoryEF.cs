using System.Linq.Expressions;
using Warehouse.Data.Entities.Base;

namespace Warehouse.Data.Repositories
{
    public partial interface IRepositoryEF<T> where T : BaseEntity
    {
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
              string includeProperties = "");
    }
}