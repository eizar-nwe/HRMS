using HRMS.Web.Models.ViewModels;
using System.Collections.Generic;

namespace HRMS.Web.Services
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
