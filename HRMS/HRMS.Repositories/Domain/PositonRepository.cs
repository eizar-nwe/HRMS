using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Repositories.Common;

namespace HRMS.Repositories.Domain
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
