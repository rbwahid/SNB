using SNB.Common;
using SNB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNB.Web.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        private PropertyModel _propertyModel;

        public PropertyController()
        {
            _propertyModel = new PropertyModel();
        }

        [Roles("Global_SupAdmin,Property_Report")]
        // GET: Property
        public ActionResult Index()
        {
            return View(_propertyModel.GetByUserId(null));
        }

        [HttpGet]
        [Roles("Global_SupAdmin,Property_Create")]
        public ActionResult NewProperty()
        {
            _propertyModel.LoadAllListData();
            return View(_propertyModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewProperty(PropertyModel model)
        {
            if (ModelState.IsValid)
            {
                var modelId = model.Add();
                return RedirectToAction("Details", "Property", new { id = modelId });
            }

            model.LoadAllListData();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(new PropertyModel(id));
        }
    }
}