using HRMS.Domain.Models.ViewModels;

namespace HRMS.Services
{
    public interface IDailyAttendanceService
    {
        void Create(DailyAttendanceViewModel DailyAttendVM);
        IEnumerable<DailyAttendanceViewModel> GetAll();
        DailyAttendanceViewModel GetById(string id);
        void Update(DailyAttendanceViewModel DailyAttendVM);
        void Delete(string id);
    }
}
