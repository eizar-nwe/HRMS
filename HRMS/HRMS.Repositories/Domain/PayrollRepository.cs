using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Repositories.Common;

namespace HRMS.Repositories.Domain
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
