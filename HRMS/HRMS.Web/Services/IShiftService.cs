using HRMS.Web.Models.ViewModels;

namespace HRMS.Web.Services
{
    public interface IShiftService
    {
        void Create(ShiftViewModel ShiftVM);
        IEnumerable<ShiftViewModel> GetAll();
        ShiftViewModel GetById(string id);
        void Update(ShiftViewModel shiftVM);
        void Delete(string id);
    }
}
