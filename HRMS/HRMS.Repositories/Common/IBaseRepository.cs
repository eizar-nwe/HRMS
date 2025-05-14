using System.Linq.Expressions;

namespace HRMS.Repositories.Common
{
    public interface IBaseRepository<T> where T:class
    {
        //crud process in here
        //create
        void Create(T entity);
        void Create(IList<T> entities);
        //R
        IEnumerable<T> GetAll(Expression<Func<T,bool>> excreption);
        IEnumerable<T> GetBy(Expression<Func<T, bool>> expression);
        //U
        void Update(T entity);

        //D
        void Delete(T entity);
        void Delete(T entity,bool isHardDeleted);
    }
}
