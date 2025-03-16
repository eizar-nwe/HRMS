using System.Linq.Expressions;

namespace HRMS.Web.Repositories.Common
{
    public interface IBaseRepository<T> where T:class
    {
        //crud process in here
        //create
        void Create(T entity);
        //R
        IEnumerable<T> GetAll(Expression<Func<T,bool>> excreption);

        //U
        void Update(T entity);

        //D
        void Delete(T entity);
    }
}
