using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.Services;
using HRMS.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HRMS.Web.Controllers
{
    public class AttendancePolicyController : Controller
    {
        private readonly IAttendancePolicyService _attendancePolicyService;

        //private readonly HRMSWebDbContext _db;

        public AttendancePolicyController(IAttendancePolicyService attendancePolicyService)
        {            
            this._attendancePolicyService = attendancePolicyService;
        }
        [HttpGet]
        public IActionResult List() => View(_attendancePolicyService.GetAll().ToList());
        //{
        //    IList<AttendancePolicyViewModel> attPolicyVM = _db.AttendancePolicy.Where(w => w.IsActive).Select(s => new AttendancePolicyViewModel
        //    {
        //        Id=s.Id,
        //        Name=s.Name,
        //        NumberOfLateTime=s.NumberOfLateTime,
        //        NumberOfEarlyOutTime=s.NumberOfEarlyOutTime,
        //        DeductionInAmount=s.DeductionInAmount,
        //        DeductionInDay=s.DeductionInDay
        //    }).ToList();

        //    return View(attPolicyVM);
        //}
        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(AttendancePolicyViewModel attPolicyVM)
        {
            try
            {
                //AttendancePolicyEntity attPolicyEntity = new AttendancePolicyEntity()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    Name = attPolicyVM.Name,
                //    NumberOfLateTime = attPolicyVM.NumberOfLateTime,
                //    NumberOfEarlyOutTime = attPolicyVM.NumberOfEarlyOutTime,
                //    DeductionInAmount = attPolicyVM.DeductionInAmount,
                //    DeductionInDay = attPolicyVM.DeductionInDay,
                //    CreatedAt = DateTime.Now,
                //    CreatedBy = "system",
                //    Ip = await NetworkHelper.GetIpAddressAsnyc(),
                //    IsActive = true
                //};
                //_db.AttendancePolicy.Add(attPolicyEntity);
                //_db.SaveChanges();

                _attendancePolicyService.Create(attPolicyVM);

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

        public IActionResult Edit(string id) => View(_attendancePolicyService.GetById(id));
        //{
        //    AttendancePolicyViewModel attPolicyVM = _db.AttendancePolicy.Where(w => w.IsActive && w.Id == id).Select(s => new AttendancePolicyViewModel
        //    {
        //        Id=s.Id,
        //        Name=s.Name,
        //        NumberOfLateTime=s.NumberOfLateTime,
        //        NumberOfEarlyOutTime=s.NumberOfEarlyOutTime,
        //        DeductionInAmount=s.DeductionInAmount,
        //        DeductionInDay=s.DeductionInDay
        //    }).FirstOrDefault();

        //    return View(attPolicyVM);
        //}

        public IActionResult Update(AttendancePolicyViewModel attPolicyVM)
        {
            try
            {
                //AttendancePolicyEntity attPolicyEntity = _db.AttendancePolicy.Where(w => w.IsActive && w.Id == attPolicyVM.Id).FirstOrDefault();
                //if (attPolicyEntity is not null)
                //{
                //    attPolicyEntity.Name = attPolicyVM.Name;
                //    attPolicyEntity.NumberOfLateTime = attPolicyVM.NumberOfLateTime;
                //    attPolicyEntity.NumberOfEarlyOutTime = attPolicyVM.NumberOfEarlyOutTime;
                //    attPolicyEntity.DeductionInAmount = attPolicyVM.DeductionInAmount;
                //    attPolicyEntity.DeductionInDay = attPolicyVM.DeductionInDay;
                //    attPolicyEntity.UpdatedBy = "system";
                //    attPolicyEntity.UpdatedAt = DateTime.Now;
                //    attPolicyEntity.Ip = await NetworkHelper.GetIpAddressAsnyc();

                //    _db.AttendancePolicy.Update(attPolicyEntity);
                //    _db.SaveChanges();

                _attendancePolicyService.Update(attPolicyVM);

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

        public IActionResult Delete(string id)
        {
            try
            {
                //AttendancePolicyEntity attPolicyEntity = _db.AttendancePolicy.Where(w => w.IsActive && w.Id == id).FirstOrDefault();
                //if (attPolicyEntity is not null)
                //{
                //    attPolicyEntity.IsActive = false;
                //    _db.Update(attPolicyEntity);
                //    _db.SaveChanges();
                
                _attendancePolicyService.Delete(id);

                TempData["Msg"] = "Data has been deleted successfully.";
                TempData["IsErrorOccur"] = false;
                //}

            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh, sorry error was occured when record is deleted time.";
                TempData["IsErrorOccur"] = true;
            }

            return RedirectToAction("List");
        }
    }
}
