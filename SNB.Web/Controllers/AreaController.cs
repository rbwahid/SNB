using SNB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNB.Web.Controllers
{
    [Authorize]
    public class AreaController : Controller
    {
       // [Roles("City_Configuration", "Global_SupAdmin")]
        public ActionResult Index()
        {
            return View(new AreaModel().GetAllAreas());
        }

        //[Roles("City_Configuration", "Global_SupAdmin")]
        public ActionResult Add()
        {
            return View(new AreaModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AreaModel model)
        {
            if (ModelState.IsValid)
            {
                model.AddArea();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //[Roles("City_Configuration", "Global_SupAdmin")]
        public ActionResult Edit(int id)
        {
            return View(new AreaModel(id));
        }

        public JsonResult LoadArea(int id)
        {
            var City = new AreaModel(id);
            return Json(new { Id = City.Id, AreaName = City.AreaName, DistrictId = City.DistrictId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AreaModel model)
        {
            if (ModelState.IsValid)
            {
                model.Edit();
                return RedirectToAction("Index");
            }
            return View(model);
        }

       // [Roles("City_Configuration", "Global_SupAdmin")]
        public ActionResult Delete(int id)
        {
            new AreaModel().DeleteCity(id);
            return Json(new { meg = "success" });
        }
        public JsonResult IsAreaNameExist(string AreaName, string InitialAreaName)
        {
            bool isNotExist = new AreaModel().IsAreaNameExist(AreaName, InitialAreaName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
    }
}