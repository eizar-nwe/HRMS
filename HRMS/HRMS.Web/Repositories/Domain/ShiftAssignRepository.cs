using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;

namespace HRMS.Web.Repositories.Domain
{
    public class ShiftAssignRepository : BaseRepository<ShiftAssignEntity>, IShiftAssignRepository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public ShiftAssignRepository(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}
