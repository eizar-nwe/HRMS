using HRMS.Domain.Models.ViewModels;

namespace HRMS.Services
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
