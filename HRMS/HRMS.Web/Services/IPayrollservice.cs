using HRMS.Web.Models.ViewModels;

namespace HRMS.Web.Services
{
    public interface IPayrollservice
    {
        void PayrollProcess(PayrollViewModel payrollVM);

        IEnumerable<PayrollViewModel> GetAll();
        void Delete(DateTime fromDate, DateTime toDate, string departmentId, string employeeId);
    }
}
