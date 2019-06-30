using SNB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNB.Web.Controllers
{
    [Authorize]
    public class PropertyTypeController : Controller
    {


        [Roles("District_Configuration", "Global_SupAdmin")]
        public ActionResult Index()
        {
            return View(new PropertyTypeModel().GetAllPropertyTypes());
        }

        [Roles("City_Configuration", "Global_SupAdmin")]
        public ActionResult Add()
        {
            return View(new PropertyTypeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PropertyTypeModel model)
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
            return View(new PropertyTypeModel(id));
        }

        public JsonResult LoadPropertyType(int id)
        {
            var Property = new PropertyTypeModel(id);
            return Json(new { Id = Property.Id, TypeName = Property.TypeName });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyTypeModel model)
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
            new PropertyTypeModel().Delete(id);
            return Json(new { meg = "success" });
        }
        public JsonResult IsTypeNameExist(string TypeName, string InitialTypeName)
        {
            bool isNotExist = new PropertyTypeModel().IsTypeNameExist(TypeName, InitialTypeName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }

    }
}