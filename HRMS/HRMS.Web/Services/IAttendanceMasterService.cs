using HRMS.Web.Models.ViewModels;

namespace HRMS.Web.Services
{
    public interface IAttendanceMasterService
    {
        void DayEndProcess(AttendanceMasterViewModel attMstrVM);
        IEnumerable<AttendanceMasterViewModel> GetAll();
        void Delete(DateTime frDate, DateTime toDate, string departmentId, string employeeId);
    }
}
