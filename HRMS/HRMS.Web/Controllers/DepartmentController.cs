using HRMS.Web.DAO;
using HRMS.Web.Models.DataModels;
using HRMS.Web.Models.ViewModels;
using HRMS.Web.Services;
using HRMS.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers {
    public class DepartmentController : Controller {
        private readonly IDepartmentService _departmentService;

        //Dependedncy Injection dbContext;
        //private readonly HRMSWebDbContext _db;

        public DepartmentController(IDepartmentService departmentService) {            
            this._departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Entry() {
            return View();
        }

        [HttpPost]
        public IActionResult Entry(DepartmentViewModel departmentVM) {

            try {
                //DepartmentEntity deptEntity = new DepartmentEntity() {
                //    Id = Guid.NewGuid().ToString(),
                //    Code = departmentVM.Code,
                //    Description = departmentVM.Description,
                //    ExtensionPhone = departmentVM.ExtensionPhone,
                //    CreatedAt = DateTime.Now,
                //    CreatedBy = "system",
                //    IsActive = true,
                //    Ip = await NetworkHelper.GetIpAddressAsnyc()
                //};

                //_db.Departments.Add(deptEntity);
                //_db.SaveChanges();

                _departmentService.Create(departmentVM);

                TempData["Msg"] = "Data has been saved successfully.";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception) {
                TempData["Msg"] = "Oh, sorry error was occured when record is created time.";
                TempData["IsErrorOccur"] = true;
            }

            return RedirectToAction("List");
        }

        public IActionResult List() => View(_departmentService.GetAll().ToList());
        //{
        //    IList<DepartmentViewModel> deptVM = _db.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel {
        //        Id=s.Id,
        //        Code = s.Code,
        //        Description = s.Description,
        //        ExtensionPhone = s.ExtensionPhone
        //    }).ToList();


        //    return View(deptVM);
        //}
        public IActionResult Edit(string id) => View(_departmentService.GetById(id));
        //{
        //    DepartmentViewModel deptVM = _db.Departments.Where(w => w.IsActive && w.Id == id).Select(s => new DepartmentViewModel
        //    {
        //        Id=s.Id,
        //        Code=s.Code,
        //        Description=s.Description,
        //        ExtensionPhone=s.ExtensionPhone
        //    }).FirstOrDefault();

        //    return View(deptVM);
        //}
        public IActionResult Update(DepartmentViewModel deptVM) 
        {
            try
            {
                //DepartmentEntity deptEntity = _db.Departments.Where(w => w.IsActive && w.Id == deptVM.Id).FirstOrDefault();

                //if (deptEntity is not null)
                //{
                //    deptEntity.Code = deptVM.Code;
                //    deptEntity.Description = deptVM.Description;
                //    deptEntity.ExtensionPhone = deptVM.ExtensionPhone;
                //    deptEntity.UpdatedBy = "system";
                //    deptEntity.UpdatedAt = DateTime.Now;
                //    deptEntity.Ip = await NetworkHelper.GetIpAddressAsnyc();

                //    _db.Departments.Update(deptEntity);
                //    _db.SaveChanges();

                    _departmentService.Update(deptVM);
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
                //DepartmentEntity deptEntity = _db.Departments.Where(w => w.IsActive && w.Id == id).FirstOrDefault();

                //if (deptEntity is not null)
                //{
                //    deptEntity.IsActive = false;
                //    _db.Update(deptEntity);
                //    _db.SaveChanges();

                    _departmentService.Delete(id);
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
    }
}