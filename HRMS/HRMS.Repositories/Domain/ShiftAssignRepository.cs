using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Repositories.Common;

namespace HRMS.Repositories.Domain
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
