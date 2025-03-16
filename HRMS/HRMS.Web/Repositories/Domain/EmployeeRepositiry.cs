using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;

namespace HRMS.Web.Repositories.Domain
{
    public class EmployeeRepositiry : BaseRepository<EmployeeEntity>, IEmployeeRepository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public EmployeeRepositiry(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}
