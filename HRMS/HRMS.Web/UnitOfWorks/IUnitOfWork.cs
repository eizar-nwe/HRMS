using HRMS.Web.Repositories.Domain;

namespace HRMS.Web.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        IPositionRepository PositionRepository { get; }
        IDepartmentRepository DepartmentRespository { get; }
        IEmployeeRepository EmployeeRespository { get; }
        IDailyAttendanceRepository DailyAttendanceRepository { get; }
        IAttendancePolicyRepository AttendancePolicyRepository { get; }
        IAttendanceMasterRepository AttendanceMasterRepository { get; }        
        IShiftRepository ShiftRepository { get; }
        IPayrollRepository PayrollRepository { get; }
    }
}
