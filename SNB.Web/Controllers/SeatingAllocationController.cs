using SNB.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNB.Web.Controllers
{
    [Authorize]
    public class SeatingAllocationController : Controller
    {
        private SeatingAllocationModel _seatingAllocationModel;

        public SeatingAllocationController()
        {
            _seatingAllocationModel = new SeatingAllocationModel();
        }

        // GET: SeatingAllocation
        public ActionResult Index()
        {
            return View(_seatingAllocationModel.GetByUserId(null));
        }

        [HttpGet]
        public ActionResult NewSeatingAllocation(int? propertyId)
        {
            _seatingAllocationModel.LoadAllListData();
            if (propertyId.HasValue)
            {
                _seatingAllocationModel.PropertyId = propertyId.Value;
            }
            return View(_seatingAllocationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewSeatingAllocation(SeatingAllocationModel model)
        {
            if (ModelState.IsValid)
            {
                var modelId = model.Add();
                return RedirectToAction("Details", "SeatingAllocation", new { id = modelId });
            }

           
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var model = new SeatingAllocationModel(id);
            model.LoadAllListData();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SeatingAllocationModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            new SeatingAllocationModel().Disable(id);
            //return Json(new { meg = "success" });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(new SeatingAllocationModel(id));
        }
    }
}