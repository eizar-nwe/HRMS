using HRMS.Domain.DAO;
using HRMS.Domain.Models.DataModels;
using HRMS.Domain.Models.ViewModels;
using HRMS.Services;
using HRMS.Domain.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class ShiftAssignController : Controller
    {
        private readonly IShiftAssignService _shiftAssignService;
        private readonly IDepartmentService _departmentService;
        private readonly IShiftService _shiftService;
        private readonly IEmployeeService _employeeService;

        //private readonly HRMSWebDbContext _db;

        public ShiftAssignController(IShiftAssignService shiftAssignService,IDepartmentService departmentService,IShiftService shiftService,IEmployeeService employeeService)
        {
            //this._db = db;
            this._shiftAssignService = shiftAssignService;
            this._departmentService = departmentService;
            this._shiftService = shiftService;
            this._employeeService = employeeService;
        }
        public IActionResult List() => View(_shiftAssignService.GetAll().ToList());
/*        {
            IList<ShiftAssignViewModel> ShiftAssignVM = (from a in _db.ShiftAssigns
                                    join e in _db.Employees
                                    on a.EmployeeId equals e.Id
                                    join d in _db.Departments
                                    on a.DepartmentId equals d.Id
                                    join s in _db.Shifts
                                    on a.ShiftId equals s.Id
                                    where a.IsActive && s.IsActive && e.IsActive && d.IsActive

                                    select new ShiftAssignViewModel
                                    {
                                        Id = a.Id,                                                                                
                                        FromDate = a.FromDate,
                                        ToDate = a.ToDate,
                                        EmployeeInfo=e.Code + "/" + e.Name,
                                        ShiftInfo= s.Name,
                                        DepartmentInfo=d.Code + "/" + d.Description,
                                    }).ToList();                                
            return View(ShiftAssignVM);
        }*/

        [HttpGet]
        public IActionResult Entry()
        {
            ShiftAssignViewModel ShiftAssVM = new ShiftAssignViewModel()
            {
                DepartmentViewModels = _departmentService.GetAll().ToList(),
                ShiftViewModels = _shiftService.GetAll().ToList(),
                EmployeeViewModels = _employeeService.GetAll().ToList()
            };

            return View(ShiftAssVM);
        }

        [HttpPost]
        public IActionResult Entry(ShiftAssignViewModel ShiftAssignVM)
        {
            try
            {
                //ShiftAssignEntity shiftAssignEntity = new ShiftAssignEntity()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    EmployeeId = ShiftAssignVM.EmployeeId,
                //    ShiftId = ShiftAssignVM.ShiftId,
                //    DepartmentId = ShiftAssignVM.DepartmentId,
                //    FromDate = ShiftAssignVM.FromDate,
                //    ToDate = ShiftAssignVM.ToDate,

                //    CreatedAt = DateTime.Now,
                //    CreatedBy = "system",
                //    IsActive = true,
                //    Ip = await NetworkHelper.GetIpAddressAsnyc()
                //};
                //_db.ShiftAssigns.Add(shiftAssignEntity);
                //_db.SaveChanges();

                _shiftAssignService.Create(ShiftAssignVM);

                TempData["Msg"] = "Data has been saved successfully";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh,Sorry error was occured when recrod is created time.";
                TempData["IsErrorOccur"] = true;
            }

            return RedirectToAction("List");
        }
        public IActionResult Delete(string id)
        {
            try
            {
                //ShiftAssignEntity ShiftAssignEntity = _db.ShiftAssigns.Where(w => w.IsActive && w.Id == id).FirstOrDefault();
                //if (ShiftAssignEntity is not null)
                //{
                //    ShiftAssignEntity.IsActive = false;
                //    _db.SaveChanges();

                //    TempData["Msg"] = "Data has been deleted successfully";
                //    TempData["IsErrorOccur"] = false;
                //}

                _shiftAssignService.Delete(id);
                TempData["Msg"] = "Data has been deleted successfully";
                TempData["IsErrorOccur"] = false;
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
            //ShiftAssignViewModel shiftAssignVM = _db.ShiftAssigns.Where(w => w.IsActive && w.Id == id).Select(s => new ShiftAssignViewModel
            //{
            //    Id = s.Id,
            //    EmployeeId = s.EmployeeId,
            //    ShiftId = s.ShiftId,
            //    DepartmentId = s.DepartmentId,
            //    FromDate = s.FromDate,
            //    ToDate = s.ToDate               
            //}).FirstOrDefault();

            ShiftAssignViewModel shiftAssignVM = _shiftAssignService.GetById(id);
            if (shiftAssignVM is not null)
            {
                shiftAssignVM.DepartmentViewModels = _departmentService.GetAll().ToList();
                shiftAssignVM.ShiftViewModels = _shiftService.GetAll().ToList();
                shiftAssignVM.EmployeeViewModels = _employeeService.GetAll().ToList();

                //shiftAssignVM.DepartmentViewModels = GetAllDepartment();
                //shiftAssignVM.ShiftViewModels = GetAllShift();
                //shiftAssignVM.EmployeeViewModels = GetAllEmployee();
            }
            
            return View(shiftAssignVM);
        }
        public IActionResult update(ShiftAssignViewModel shiftAssignVM)
        {
            try
            {
                //ShiftAssignEntity shiftAssignEntity = _db.ShiftAssigns.Where(w => w.IsActive && w.Id == shiftAssignVM.Id).FirstOrDefault();
                //if (shiftAssignEntity is not null)
                //{
                //    shiftAssignEntity.EmployeeId = shiftAssignVM.EmployeeId;
                //    shiftAssignEntity.ShiftId = shiftAssignVM.ShiftId;
                //    shiftAssignEntity.DepartmentId = shiftAssignVM.DepartmentId;
                //    shiftAssignEntity.UpdatedAt = DateTime.Now;
                //    shiftAssignEntity.UpdatedBy = "system";
                //    shiftAssignEntity.Ip = await NetworkHelper.GetIpAddressAsnyc();

                //    _db.ShiftAssigns.Update(shiftAssignEntity);
                //    _db.SaveChanges();

                //    TempData["Msg"] = "Data has been updated successfully";
                //    TempData["IsErrorOccur"] = false;
                //}
                _shiftAssignService.Update(shiftAssignVM);

                TempData["Msg"] = "Data has been updated successfully";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh,Sorry error was occured when recrod is updated time.";
                TempData["IsErrorOccur"] = true;
            }

            return RedirectToAction("List");
        }

        /*private IList<DepartmentViewModel> GetAllDepartment()
        {

            return _db.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description
            }).ToList();
        }

        private IList<ShiftViewModel> GetAllShift()
        {
            return _db.Shifts.Where(w => w.IsActive).Select(s => new ShiftViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
        private IList<EmployeeViewModel> GetAllEmployee()
        {
            return _db.Employees.Where(w => w.IsActive).Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name
            }).ToList();
        }*/
    }
}
