using SNB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNB.Web.Controllers
{
    [Authorize]
    //[CustomExceptionFilter]
    public class DistrictController : Controller
    {
        
        

            [Roles("District_Configuration", "Global_SupAdmin")]
            public ActionResult Index()
            {
                return View(new DistrictModel().GetAllDistricts());
            }

            [Roles("City_Configuration", "Global_SupAdmin")]
            public ActionResult Add()
            {
                return View(new DistrictModel());
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Add(DistrictModel model)
            {
                if (ModelState.IsValid)
                {
                    model.Add();
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            [Roles("City_Configuration", "Global_SupAdmin")]
            public ActionResult Edit(int id)
            {
                return View(new DistrictModel(id));
            }

            public JsonResult LoadDistrict(int id)
            {
                var District = new DistrictModel(id);
                return Json(new { Id = District.Id, DistrictName = District.DistrictName});
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(DistrictModel model)
            {
                if (ModelState.IsValid)
                {
                    model.Edit();
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            [Roles("District_Configuration", "Global_SupAdmin")]
            public ActionResult Delete(int id)
            {
                new DistrictModel().Delete(id);
                return Json(new { meg = "success" });
            }
            public JsonResult IsDistrictNameExist(string DistrictName, string InitialDistrictName)
            {
                bool isNotExist = new DistrictModel().IsDistrictNameExist(DistrictName, InitialDistrictName);
                return Json(isNotExist, JsonRequestBehavior.AllowGet);
            }

        
    }
}