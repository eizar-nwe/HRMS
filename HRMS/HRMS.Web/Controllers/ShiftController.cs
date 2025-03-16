using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class ShiftController : Controller
    {
        private readonly HRMSWebDbContext _db;

        public ShiftController(HRMSWebDbContext db)
        {
            this._db = db;
        }
        public IActionResult List()
        {
            IList<ShiftViewModel> shiftVM = (from s in _db.Shifts
                                join a in _db.AttendancePolicy
                                on s.AttendancePolicyId equals a.Id
                                where s.IsActive && a.IsActive
                                select new ShiftViewModel
                                {
                                    Id = s.Id,
                                    Name = s.Name,
                                    InTime = s.InTime,
                                    OutTime = s.OutTime,
                                    LateAfter = s.LateAfter,
                                    EarlyOutBefore = s.EarlyOutBefore,
                                    AttendancePolicyId = a.Name
                                }).ToList();            
            return View(shiftVM);
        }
        public IActionResult Entry()
        {
            ShiftViewModel ShiftVM = new ShiftViewModel()
            {
                AttendancePolicyVM = GetAttendancePolicy()
            };
            return View(ShiftVM);
        }
        [HttpPost]
        public async Task<IActionResult> Entry(ShiftViewModel ShiftVM)
        {
            try
            {
                ShiftEntity shiftEntity = new ShiftEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = ShiftVM.Name,
                    InTime = ShiftVM.InTime,
                    OutTime = ShiftVM.OutTime,
                    LateAfter = ShiftVM.LateAfter,
                    EarlyOutBefore = ShiftVM.EarlyOutBefore,
                    AttendancePolicyId = ShiftVM.AttendancePolicyId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                    IsActive = true,
                    Ip = await NetworkHelper.GetIpAddressAsnyc()
                };

                _db.Shifts.Add(shiftEntity);
                _db.SaveChanges();

                TempData["Msg"] = "Data has been saved successfully.";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception)
            {
                TempData["Msg"] = "Oh, sorry error was occured when record is created time.";
                TempData["IsErrorOccur"] = true;
            }

            return RedirectToAction("List");
        }
        public IActionResult Edit(string id)
        {
            ShiftViewModel shiftVM = _db.Shifts.Where(w => w.IsActive && w.Id == id).Select(s => new ShiftViewModel
            {
                Id=s.Id,
                Name=s.Name,
                InTime=s.InTime,
                OutTime=s.OutTime,
                LateAfter=s.LateAfter,
                EarlyOutBefore=s.EarlyOutBefore,
                AttendancePolicyId=s.AttendancePolicyId
            }).FirstOrDefault();

            shiftVM.AttendancePolicyVM = GetAttendancePolicy();

            return View(shiftVM);
        }
        public async Task<IActionResult> Update(ShiftViewModel shiftVM)
        {
            try
            {
                ShiftEntity shiftEntity = _db.Shifts.Where(w => w.IsActive && w.Id == shiftVM.Id).FirstOrDefault();
                if (shiftEntity is not null)
                {
                    shiftEntity.Name = shiftVM.Name;
                    shiftEntity.InTime = shiftVM.InTime;
                    shiftEntity.OutTime = shiftVM.OutTime;
                    shiftEntity.LateAfter = shiftVM.LateAfter;
                    shiftEntity.EarlyOutBefore = shiftVM.EarlyOutBefore;
                    shiftEntity.AttendancePolicyId = shiftVM.AttendancePolicyId;

                    shiftEntity.UpdatedBy = "system";
                    shiftEntity.UpdatedAt = DateTime.Now;
                    shiftEntity.Ip = await NetworkHelper.GetIpAddressAsnyc();

                    _db.Shifts.Update(shiftEntity);
                    _db.SaveChanges();

                    TempData["Msg"] = "Data has been saved successfully.";
                    TempData["IsErrorOccur"] = false;
                }
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
                ShiftEntity shiftEntity = _db.Shifts.Where(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (shiftEntity is not null)
                {
                    shiftEntity.IsActive = false;
                    _db.Shifts.Update(shiftEntity);
                    _db.SaveChanges();

                    TempData["Msg"] = "Data has been deleted successfully";
                    TempData["IsErrorOccur"] = false;
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh,Sorry error was occured when recrod is deleted time.";
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }

        private IList<AttendancePolicyViewModel> GetAttendancePolicy()
        {
            return _db.AttendancePolicy.Where(w => w.IsActive).Select(s => new AttendancePolicyViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
    }
}
