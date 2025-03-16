using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Repositories.Common;

namespace HRMS.Web.Repositories.Domain
{
    public class PositonRepository : BaseRepository<PositionEntity>, IPositionRepository
    {
        private readonly HRMSWebDbContext _dbcontext;

        public PositonRepository(HRMSWebDbContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}
