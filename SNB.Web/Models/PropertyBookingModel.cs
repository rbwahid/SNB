using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNB.Common;
using SNB.Entities;
using SNB.Services;

namespace SNB.Web.Models
{
    public class PropertyBookingModel : PropertyBooking
    {
        private PropertyBookingService _propertyBookingService;
        //private SeatingAllocationService _seatingAllocationService;
        private PropertyService _propertyService;
        private int loggedInUserId;

        public PropertyBookingModel()
        {
            _propertyBookingService = new PropertyBookingService();
            //_seatingAllocationService = new SeatingAllocationService();
            _propertyService = new PropertyService();
        }

        public PropertyBookingModel(int id) : this()
        {
            var data = _propertyBookingService.GetById(id);
            if (data != null)
            {
                this.Id = data.Id;
                this.UserId = data.UserId;
                this.User = data.User;
                this.PropertyId = data.PropertyId;
                this.Property = data.Property;
                this.TotalSeat = data.TotalSeat;
                this.ConfirmDate = data.ConfirmDate;
                this.FromDate = data.FromDate;
                this.ToDate = data.ToDate;
                this.Remarks = data.Remarks;

                this.Status = data.Status;
                this.CreatedBy = data.CreatedBy;
                this.CreatedAt = data.CreatedAt;
                this.UpdatedBy = data.UpdatedBy;
                this.UpdatedAt = data.UpdatedAt;
            }
        }

        public Property GetPropertyById(int id)
        {
            return _propertyService.GetById(id);
        }

        public IEnumerable<PropertyBooking> GetAll()
        {
            return _propertyBookingService.GetAll().ToList();
        }

        public IEnumerable<PropertyBooking> GetByUserId(int? id)
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            return _propertyBookingService.GetByUserId(loggedInUserId).ToList();
        }

        public IEnumerable<PropertyBooking> GetByTenantId(int? id)
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            return _propertyBookingService.GetByTenantId(loggedInUserId).ToList();
        }

        public IEnumerable<PropertyBooking> GetByLandlordId(int? id)
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            return _propertyBookingService.GetByLandlordId(loggedInUserId).ToList();
        }

        public PropertyBooking GetById(int id)
        {
            return _propertyBookingService.GetById(id);
        }

        public int Add()
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            this.UserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            this.TotalSeat = 1;
            return _propertyBookingService.Add(this, loggedInUserId);
        }

        public int Update()
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            this.TotalSeat = 1;
            return _propertyBookingService.Update(this, loggedInUserId);
        }

        public void LoadPropertyById(int propertyId)
        {
            this.Property = this.GetPropertyById(propertyId);
            this.PropertyId = this.Property.Id;
        }

        //public void LoadSeatingAllocationBySAId(int seatingAllocationId)
        //{
        //    this.SeatingAllocationId = seatingAllocationId;
        //    this.SeatingAllocation = _seatingAllocationService.GetById(seatingAllocationId);
        //}

        public void Disable(int id)
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            _propertyBookingService.Disable(id, loggedInUserId);
        }

        public void Enable(int id)
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            _propertyBookingService.Disable(id, loggedInUserId);
        }

        public void ChangePropertyBookingStatus(int id, int status)
        {
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            _propertyBookingService.ChangePropertyBookingStatus(id, status, loggedInUserId);
        }

        public void Dispose()
        {
            _propertyBookingService.Dispose();
        }
    }
}