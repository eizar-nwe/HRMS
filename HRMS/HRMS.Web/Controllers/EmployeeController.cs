using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HRMSWebDbContext _db;

        public EmployeeController(HRMSWebDbContext db)
        {
            this._db = db;
        }
        public IActionResult List()
        {            
            return View();
        }
        [HttpGet]
        public IActionResult Entry()
        {
            EmployeeViewModel employeeVM = new EmployeeViewModel()
            {
                DepartmentViewModels = _db.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel{
                    Id=s.Id,
                    Code=s.Code,
                    Description=s.Description
                }).ToList(),
                PositionViewModels = _db.Positions.Where(w=>w.IsActive).Select(s=> new PositionViewModel
                {
                    Id = s.Id,
                    Code = s.Code,
                    Description = s.Description
                }).ToList()
            };
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Entry(EmployeeViewModel employeeVM)
        {
            try
            {
                //DTO process from view model to data model for save process
                EmployeeEntity employeeEntity = new EmployeeEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = employeeVM.Code,
                    Name = employeeVM.Name,
                    Email = employeeVM.Email,
                    Gender = employeeVM.Gender,
                    DOB = employeeVM.DOB,
                    DOE = employeeVM.DOE,
                    DOR = employeeVM.DOR,
                    Address = employeeVM.Address,
                    BasicSalary = employeeVM.BasicSalary,
                    Phone = employeeVM.Phone,
                    DepartmentId = employeeVM.DepartmentId,
                    PositionId = employeeVM.PositionId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = await NetworkHelper.GetIpAddressAsnyc()
                };

                _db.Employees.Add(employeeEntity);
                _db.SaveChanges();

                TempData["Msg"] = "Data has been saved successfully.";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh, sorry error was occured when record is created time.";
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }
    }
}
