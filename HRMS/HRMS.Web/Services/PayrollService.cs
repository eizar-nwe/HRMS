using HRMS.Web.Models.ViewModels;
using HRMS.Web.UnitOfWorks;

namespace HRMS.Web.Services
{
    public class PayrollService : IPayrollservice
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayrollService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<PayrollViewModel> GetAll()
        {
            return (from a in _unitOfWork.PayrollRepository.GetAll(w => w.IsActive)
                    join e in _unitOfWork.EmployeeRespository.GetAll(w => w.IsActive)
                    on a.EmployeeId equals e.Id
                    join d in _unitOfWork.DepartmentRespository.GetAll(w => w.IsActive)
                    on a.DepartmentId equals d.Id                    

                    select new PayrollViewModel
                    {
                        Id = a.Id,
                        FromDate = a.FromDate,
                        ToDate=a.ToDate,
                        EmployeeId=a.EmployeeId,
                        DepartmentId=a.DepartmentId,
                        IncomeTax=a.IncomeTax,
                        GrossPay=a.GrossPay,
                        NetPay=a.NetPay,
                        Allowance=a.Allowance,
                        Deduction=a.Deduction,
                        AttendanceDays=a.AttendanceDays,
                        AttendanceDeduction=a.AttendanceDeduction,
                        PayPerDay=a.PayPerDay,

                        EmployeeInfo=e.Code +"/"+ e.Name,
                        DepartmentInfo=d.Code +"/"+ d.Description

                    }).ToList();
        }

        public void PayrollProcess(PayrollViewModel payrollVM)
        {
            throw new NotImplementedException();
        }
    }
}
