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

            model.LoadAllListData();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(new SeatingAllocationModel(id));
        }
    }
}