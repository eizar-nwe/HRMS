using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;

namespace HRMS.Web.Repositories.Domain
{
    public class PayrollRepository : BaseRepository<PayrollEntity>, IPayrollRepository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public PayrollRepository(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}
