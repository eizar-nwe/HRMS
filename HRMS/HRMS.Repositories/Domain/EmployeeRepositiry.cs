using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Repositories.Common;

namespace HRMS.Repositories.Domain
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
