using HRMS.Web.Repositories.Domain;

namespace HRMS.Web.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        IPositionRepository PositionRepository { get; }
        IDepartmentRespository DepartmentRespository { get; }
        IEmployeeRepository EmployeeRespository { get; }
        IDailyAttendanceRepository DailyAttendanceRepository { get; }
    }
}
