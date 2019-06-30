using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNB.Web.Models;

namespace SNB.Web.Controllers
{
    public class PropertyBookingController : Controller
    {
        private PropertyBookingModel _propertyBookingModel;

        public PropertyBookingController()
        {
            _propertyBookingModel = new PropertyBookingModel();
        }

        // GET: PropertyBooking
        public ActionResult Index()
        {
            return View(_propertyBookingModel.GetByUserId(null));
        }

        [HttpGet]
        public ActionResult PropertyBookingInfo(int seatingAllocationId)
        {
            _propertyBookingModel.LoadSeatingAllocationBySAId(seatingAllocationId);
            return View(_propertyBookingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PropertyBookingRequest(PropertyBookingModel model)
        {
            if (ModelState.IsValid)
            {
                model.Add();
                return RedirectToAction("PropertyBookingSuccess", "PropertyBooking");
            }

            return RedirectToAction("PropertyBookingInfo", "PropertyBooking", new { seatingAllocationId = model.SeatingAllocationId});
        }

        [HttpGet]
        public ActionResult PropertyBookingSuccess()
        {
            return View();
        }

        
        public ActionResult Details(int id)
        {
            return PartialView("_Details",new PropertyBookingModel(id));
        }
    }
}