using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.Repositories.Domain;
using HRMS.Web.Services;
using HRMS.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class DailyAttendanceController : Controller
    {
        //private readonly HRMSWebDbContext _db;        
        private readonly IDailyAttendanceService _dailyAttendanceService;        
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public DailyAttendanceController(IDailyAttendanceService dailyAttendanceService,IDepartmentService departmentService,IEmployeeService employeeService)
        {                        
            this._dailyAttendanceService = dailyAttendanceService;            
            this._departmentService = departmentService;
            this._employeeService = employeeService;
        }
        public IActionResult List() => View(_dailyAttendanceService.GetAll().ToList());
        //{
        //    IList<DailyAttendanceViewModel> DailyAttVM = (from a in _db.DailyAttendance
        //                                    join e in _db.Employees
        //                                    on a.EmployeeId equals e.Id
        //                                    join d in _db.Departments
        //                                    on a.DepartmentId equals d.Id
        //                                    where a.IsActive && e.IsActive && d.IsActive

        //                                    select new DailyAttendanceViewModel
        //                                    {
        //                                        Id = a.Id,
        //                                        AttendanceDate = a.AttendanceDate,
        //                                        InTime = a.InTime,
        //                                        OutTime = a.OutTime,
        //                                        EmployeeInfo=e.Code + "/" + e.Name,
        //                                        DepartmentInfo=d.Code + "/" + d.Description
        //                                    }).ToList();                                            
        //    return View(DailyAttVM);
        //}

        [HttpGet]
        public IActionResult Entry()
        {
            DailyAttendanceViewModel DailyAttVM = new DailyAttendanceViewModel()
            {
                DepartmentViewModels = _departmentService.GetAll().ToList(),
                EmployeeViewModels = _employeeService.GetAll().ToList()
            };

            return View(DailyAttVM);
        }
        [HttpPost]
        public async Task<IActionResult> Entry(DailyAttendanceViewModel DailyAttendVM)
        {
            try
            {
                //DailyAttendanceEntity DailyAttEntity = new DailyAttendanceEntity()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    AttendanceDate = DailyAttendVM.AttendanceDate,
                //    InTime = DailyAttendVM.InTime,
                //    OutTime = DailyAttendVM.OutTime,
                //    DepartmentId = DailyAttendVM.DepartmentId,
                //    EmployeeId = DailyAttendVM.EmployeeId,
                //    CreatedAt = DateTime.Now,
                //    CreatedBy = "system",
                //    IsActive = true,
                //    Ip = await NetworkHelper.GetIpAddressAsnyc()
                //};
                //_db.DailyAttendance.Add(DailyAttEntity);
                //_db.SaveChanges();

                _dailyAttendanceService.Create(DailyAttendVM);

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
                //DailyAttendanceEntity DailyAttEntity = _db.DailyAttendance.Where(w => w.IsActive && w.Id == id).FirstOrDefault();
                //if (DailyAttEntity is not null)
                //{
                //    DailyAttEntity.IsActive = false;
                //    _db.Update(DailyAttEntity);
                //    _db.SaveChanges();
                _dailyAttendanceService.Delete(id);

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
            //DailyAttendanceViewModel DailyAttVM = _db.DailyAttendance.Where(w => w.IsActive && w.Id == id).Select(s => new DailyAttendanceViewModel
            //{
            //    Id = s.Id,
            //    AttendanceDate = s.AttendanceDate,
            //    InTime = s.InTime,
            //    OutTime = s.OutTime,
            //    EmployeeId = s.EmployeeId,
            //    DepartmentId = s.DepartmentId                
            //}).FirstOrDefault();

            DailyAttendanceViewModel DailyAttVM = _dailyAttendanceService.GetById(id);

            DailyAttVM.DepartmentViewModels = _departmentService.GetAll().ToList();
            DailyAttVM.EmployeeViewModels = _employeeService.GetAll().ToList();

            return View(DailyAttVM);
        }
        public IActionResult update(DailyAttendanceViewModel DailyAttVM)
        {
            try
            {
                //DailyAttendanceEntity DailyAttEntity = _db.DailyAttendance.Where(w => w.IsActive && w.Id == DailyAttVM.Id).FirstOrDefault();
                //if (DailyAttEntity is not null)
                //{
                //    DailyAttEntity.AttendanceDate = DailyAttVM.AttendanceDate;
                //    DailyAttEntity.InTime = DailyAttVM.InTime;
                //    DailyAttEntity.OutTime = DailyAttVM.OutTime;
                //    DailyAttEntity.EmployeeId = DailyAttVM.EmployeeId;
                //    DailyAttEntity.DepartmentId = DailyAttVM.DepartmentId;
                //    DailyAttEntity.UpdatedAt = DateTime.Now;
                //    DailyAttEntity.UpdatedBy = "system";
                //    DailyAttEntity.Ip = await NetworkHelper.GetIpAddressAsnyc();

                //    _db.DailyAttendance.Update(DailyAttEntity);
                //    _db.SaveChanges();

                _dailyAttendanceService.Update(DailyAttVM);
                TempData["Msg"] = "Data has been updated successfully";
                TempData["IsErrorOccur"] = false;
                //}
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh,Sorry error was occured when recrod is updated time.";
                TempData["IsErrorOccur"] = true;
            }

            return RedirectToAction("List");
        }
            
    }
}
