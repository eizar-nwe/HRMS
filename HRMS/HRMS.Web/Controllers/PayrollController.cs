using HRMS.Domain.Models.DataModels;
using HRMS.Domain.Models.ViewModels;
using HRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPayrollservice _payrollservice;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public PayrollController(IPayrollservice payrollservice,IEmployeeService employeeService,IDepartmentService departmentService)
        {
            this._payrollservice = payrollservice;
            this._employeeService = employeeService;
            this._departmentService = departmentService;
        }
        public IActionResult List() => View(_payrollservice.GetAll().ToList());
        public IActionResult PayrollProcess()
        {
            PayrollViewModel payrollVM = new PayrollViewModel
            {
                EmployeeViewModels = _employeeService.GetAll().ToList(),
                DepartmentViewModels = _departmentService.GetAll().ToList()
            };

            return View(payrollVM);
        }
        [HttpPost]
        public IActionResult PayrollProcess(PayrollViewModel payroll)
        {
            try
            {
                _payrollservice.Delete(payroll.FromDate, payroll.ToDate, payroll.DepartmentId, payroll.EmployeeId);
                _payrollservice.PayrollProcess(payroll);
                TempData["Info"] = "successfully save a record to the system";
            }
            catch (Exception ex)
            {
                TempData["Info"] = "Error occur when  saving a record  to the system";
            }
            return RedirectToAction("List");
        }
    }
}
