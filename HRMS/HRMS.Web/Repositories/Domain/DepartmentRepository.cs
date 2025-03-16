using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;

namespace HRMS.Web.Repositories.Domain
{
    public class DepartmentRepository : BaseRepository<DepartmentEntity>, IDepartmentRespository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public DepartmentRepository(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}
