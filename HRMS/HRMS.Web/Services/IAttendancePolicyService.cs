using HRMS.Web.Models.ViewModels;

namespace HRMS.Web.Services
{
    public interface IAttendancePolicyService
    {
        void Create(AttendancePolicyViewModel attPolicyVM);
        IEnumerable<AttendancePolicyViewModel> GetAll();
        AttendancePolicyViewModel GetById(string id);
        void Update(AttendancePolicyViewModel attPolicyVM);
        void Delete(string id);
    }
}
