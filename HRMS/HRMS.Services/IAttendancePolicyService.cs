using HRMS.Domain.Models.ViewModels;

namespace HRMS.Services
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
