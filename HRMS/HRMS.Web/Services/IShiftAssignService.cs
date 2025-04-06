using HRMS.Web.Models.ViewModels;

namespace HRMS.Web.Services
{
    public interface IShiftAssignService
    {
        void Create(ShiftAssignViewModel shiftAssignVM);
        IEnumerable<ShiftAssignViewModel> GetAll();
        ShiftAssignViewModel GetById(string id);
        void Update(ShiftAssignViewModel shiftAssignVM);
        void Delete(string id);
    }
}
