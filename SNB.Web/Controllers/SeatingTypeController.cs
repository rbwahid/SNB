using SNB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNB.Web.Controllers
{
    [Authorize]
    public class SeatingTypeController : Controller
    {
        [Roles("District_Configuration", "Global_SupAdmin")]
        public ActionResult Index()
        {
            return View(new SeatingTypeModel().GetAllSeatingTypes());
        }

        [Roles("City_Configuration", "Global_SupAdmin")]
        public ActionResult Add()
        {
            return View(new SeatingTypeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SeatingTypeModel model)
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
            return View(new SeatingTypeModel(id));
        }

        public JsonResult LoadSeatingType(int id)
        {
            var Property = new SeatingTypeModel(id);
            return Json(new { Id = Property.Id, TypeName = Property.TypeName });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SeatingTypeModel model)
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
            new SeatingTypeModel().Delete(id);
            return Json(new { meg = "success" });
        }
        public JsonResult IsTypeNameExist(string TypeName, string InitialTypeName)
        {
            bool isNotExist = new SeatingTypeModel().IsTypeNameExist(TypeName, InitialTypeName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
    }
}