using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.Services;
using HRMS.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace HRMS.Web.Controllers
{
    public class EmployeeController : Controller
    {        
        private readonly IEmployeeService _employeeService;        
        private readonly IPositionService _positionService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;

        public EmployeeController(IEmployeeService employeeService, IPositionService positionService, IDepartmentService departmentService, IUserService userService)
        {            
            this._employeeService = employeeService;            
            this._positionService = positionService;
            this._departmentService = departmentService;
            this._userService = userService;
        }
        public IActionResult List() => View(_employeeService.GetAll().ToList());
        //{
        //    IList<EmployeeViewModel> employees = (from e in _db.Employees
        //                        join p in _db.Positions
        //                        on e.PositionId equals p.Id
        //                        join d in _db.Departments
        //                        on e.DepartmentId equals d.Id
        //                        where e.IsActive && d.IsActive && p.IsActive

        //                        select new EmployeeViewModel
        //                        {
        //                            Id=e.Id,
        //                            Code=e.Code,
        //                            Name=e.Name,
        //                            DOB=e.DOB,
        //                            DOR=e.DOR,
        //                            DOE=e.DOE,
        //                            Address=e.Address,
        //                            Phone=e.Phone,
        //                            DepartmentInfo=d.Code + "/" + d.Description,
        //                            PositionInfo=p.Code + "/" + p.Description
        //                        }).ToList();

        //    return View(employees);
        //}

        [HttpGet]
        public IActionResult Entry()
        {
            EmployeeViewModel employeeVM = new EmployeeViewModel
            {
                //PositionViewModels = GetAllPositions(),
                //DepartmentViewModels = GetAllDepartments()

                PositionViewModels = _positionService.GetAll().ToList(),
                DepartmentViewModels = _departmentService.GetAll().ToList()
            };

            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Entry(EmployeeViewModel employeeVM)
        {          
            try
            {
                string userId = await _userService.CreateUserWithRole(employeeVM.Email, employeeVM.Email);

                if (userId.Equals("unknow"))
                {
                    TempData["Msg"] = "We face the error when";
                    TempData["IsErrorOccur"] = true;

                    return View();
                }
                else
                {
                    employeeVM.UserId = userId;
                }

                ////DTO process from view model to data model for save process
                //EmployeeEntity employeeEntity = new EmployeeEntity()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    Code = employeeVM.Code,
                //    Name = employeeVM.Name,
                //    Email = employeeVM.Email,
                //    Gender = employeeVM.Gender,
                //    DOB = employeeVM.DOB,
                //    DOE = employeeVM.DOE,
                //    DOR = employeeVM.DOR,
                //    Address = employeeVM.Address,
                //    BasicSalary = employeeVM.BasicSalary,
                //    Phone = employeeVM.Phone,
                //    DepartmentId = employeeVM.DepartmentId,
                //    PositionId = employeeVM.PositionId,
                //    CreatedAt = DateTime.Now,
                //    CreatedBy = "system",
                //    IsActive = true,
                //    Ip = await NetworkHelper.GetIpAddressAsnyc()
                //};

                //_db.Employees.Add(employeeEntity);
                //_db.SaveChanges();

                _employeeService.Create(employeeVM);

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

        public IActionResult Delete(string id)
        {
            try
            {
                //EmployeeEntity empeEntity = _db.Employees.Where(w => w.IsActive && w.Id == id).FirstOrDefault();

                //if (empeEntity is not null)
                //{
                //    empeEntity.IsActive = false;
                //    _db.Update(empeEntity);
                //    _db.SaveChanges();

                    _employeeService.Delete(id);
                    TempData["Msg"] = "Data has been deleted successfully";
                    TempData["IsErrorOccur"] = false;
                //}
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh,Sorry error was occured when recrod is deleted time.";
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string id)
        {
            //EmployeeViewModel employeeVM = _db.Employees.Where(w => w.IsActive && w.Id == id).Select(e => new EmployeeViewModel
            //{                
            //    Id = e.Id,
            //    Code = e.Code,
            //    Name = e.Name,
            //    DOB = e.DOB,
            //    DOR = e.DOR,
            //    DOE = e.DOE,
            //    Address = e.Address,
            //    Phone = e.Phone,
            //    PositionId=e.PositionId,
            //    DepartmentId=e.DepartmentId,
            //    Gender=e.Gender,
            //    Email=e.Email,
            //    BasicSalary=e.BasicSalary,
            //}).FirstOrDefault();
            //employeeVM.PositionViewModels = GetAllPositions();
            //employeeVM.DepartmentViewModels = GetAllDepartments();

            EmployeeViewModel employeeVM = _employeeService.GetById(id);

            employeeVM.PositionViewModels = _positionService.GetAll().ToList();
            employeeVM.DepartmentViewModels = _departmentService.GetAll().ToList();

            return View(employeeVM);
        }
        public IActionResult Update(EmployeeViewModel employeeVM)
        {
            try
            {
                ////DTO process from view model to data model for save process
                //EmployeeEntity employeeEntity = _db.Employees.Where(w => w.IsActive && w.Id == employeeVM.Id).FirstOrDefault();

                //if (employeeEntity is not null)
                //{
                //    employeeEntity.Code = employeeVM.Code;
                //    employeeEntity.Name = employeeVM.Name;
                //    employeeEntity.Email = employeeVM.Email;
                //    employeeEntity.Gender = employeeVM.Gender;
                //    employeeEntity.DOB = employeeVM.DOB;
                //    employeeEntity.DOE = employeeVM.DOE;
                //    employeeEntity.DOR = employeeVM.DOR;
                //    employeeEntity.Address = employeeVM.Address;
                //    employeeEntity.BasicSalary = employeeVM.BasicSalary;
                //    employeeEntity.Phone = employeeVM.Phone;
                //    employeeEntity.DepartmentId = employeeVM.DepartmentId;
                //    employeeEntity.PositionId = employeeVM.PositionId;
                //    employeeEntity.UpdatedAt = DateTime.Now;
                //    employeeEntity.UpdatedBy = "system";
                //    employeeEntity.Ip = await NetworkHelper.GetIpAddressAsnyc();

                //    _db.Employees.Update(employeeEntity);
                //    _db.SaveChanges();

                    _employeeService.Update(employeeVM);

                    TempData["Msg"] = "Data has been saved successfully.";
                    TempData["IsErrorOccur"] = false;
                //};
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh, sorry error was occured when record is created time.";
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }

        public IActionResult EmployeeDetailReport() => View();

        [HttpPost]
        public IActionResult EmployeeDetailReport(string fromCode,string toCode)
        {
            string fileName = $"EmployeeDetail-{DateTime.Now.ToString("yyy-MM-dd HH:mm:ss")}.xlsx";
            string fileContextType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            List<EmployeeDetailReportViewModel> reportData = _employeeService.GetByCode(fromCode, toCode).ToList();
            if(reportData.Count > 0)
            {
                var fileOutput = ReportHelper.ExportToExcel(reportData,fileName);
                ViewBag.Msg = "Employee detail report is successfully exported.";
                return File(fileOutput, fileContextType, fileName);
            }
            else
            {
                ViewBag.Msg = "There is no data to export employee detail report.";
            }
            return View();
        }

        //private IList<PositionViewModel> GetAllPositions()
        //{           
        //    return _db.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel
        //    {
        //        Id = s.Id,
        //        Code = s.Code,
        //        Description = s.Description
        //    }).ToList();
        //}

        //private IList<DepartmentViewModel> GetAllDepartments()
        //{
        //    return _db.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
        //    {
        //        Id = s.Id,
        //        Code = s.Code,
        //        Description = s.Description
        //    }).ToList();                
        //}
    }
}
