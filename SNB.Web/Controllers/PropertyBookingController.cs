using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNB.Common;
using SNB.Web.Models;

namespace SNB.Web.Controllers
{
    [Authorize]
    public class PropertyBookingController : Controller
    {
        private PropertyBookingModel _propertyBookingModel;

        public PropertyBookingController()
        {
            _propertyBookingModel = new PropertyBookingModel();
        }

        // GET: PropertyBooking
        [Roles("Global_SupAdmin,Property_Booking_Report")]
        public ActionResult Index()
        {
            if (AuthenticatedUser.GetUserFromIdentity().UserType.ToLower() == DefaultValue.UserType.Landlord.ToLower())
            {
                return View(_propertyBookingModel.GetByLandlordId(null));
            }
            return View(_propertyBookingModel.GetByTenantId(null));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PropertyInfo(int propertyId)
        {
            var property = _propertyBookingModel.GetPropertyById(propertyId);
            return View(property);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PropertySeatingInfo(int seatingAllocationId)
        {
            _propertyBookingModel.LoadSeatingAllocationBySAId(seatingAllocationId);
            return View(_propertyBookingModel);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        [Roles("Global_SupAdmin,Property_Booking_Request")]
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

        public ActionResult PropertyBookingStatus(int id, int status)
        {
            _propertyBookingModel.ChangePropertyBookingStatus(id, status);
            return Json(new { msg = "Success" });
        }
    }
}