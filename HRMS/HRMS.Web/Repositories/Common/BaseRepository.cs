using HRMS.Web.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HRMS.Web.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly HRMSWebDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(HRMSWebDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
            _dbSet = _dbcontext.Set<T>();
        }
        public void Create(T entity)
        {
            _dbcontext.Add<T>(entity);
        }

        public void Delete(T entity)
        {
            _dbcontext.Update<T>(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().Where(expression).AsEnumerable();
        }

        public void Update(T entity)
        {
            _dbcontext.Update<T>(entity);
        }
    }
}
