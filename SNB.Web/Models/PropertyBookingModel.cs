﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SNB.Common;
using SNB.Entities;
using SNB.Services;

namespace SNB.Web.Models
{
    public class PropertyBookingModel : PropertyBooking
    {
        private PropertyBookingService _propertyBookingService;
        private int loggedInUserId;
        private SeatingAllocationService _seatingAllocationService;

        public PropertyBookingModel()
        {
            _propertyBookingService = new PropertyBookingService();
            _seatingAllocationService = new SeatingAllocationService();
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
        }

        public PropertyBookingModel(int id) : this()
        {
            var data = _propertyBookingService.GetById(id);
            if (data != null)
            {
                this.Id = data.Id;
                this.UserId = data.UserId;
                this.User = data.User;
                this.SeatingAllocationId = data.SeatingAllocationId;
                this.SeatingAllocation = data.SeatingAllocation;
                this.ConfirmDate = data.ConfirmDate;
                this.FromDate = data.FromDate;
                this.ToDate = data.ToDate;

                this.Status = data.Status;
                this.CreatedBy = data.CreatedBy;
                this.CreatedAt = data.CreatedAt;
                this.UpdatedBy = data.UpdatedBy;
                this.UpdatedAt = data.UpdatedAt;
            }
        }
        
        public IEnumerable<PropertyBooking> GetAll()
        {
            return _propertyBookingService.GetAll().ToList();
        }

        public IEnumerable<PropertyBooking> GetByUserId(int? id)
        {
            return _propertyBookingService.GetByUserId(loggedInUserId).ToList();
        }

        public PropertyBooking GetById(int id)
        {
            return _propertyBookingService.GetById(id);
        }

        public int Add()
        {
            this.UserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            return _propertyBookingService.Add(this, loggedInUserId);
        }

        public int Update()
        {
            return _propertyBookingService.Update(this, loggedInUserId);
        }

        public void LoadSeatingAllocationBySAId(int seatingAllocationId)
        {
            this.SeatingAllocationId = seatingAllocationId;
            this.SeatingAllocation = _seatingAllocationService.GetById(seatingAllocationId);
        }

        public void Disable(int id)
        {
            _propertyBookingService.Disable(id, loggedInUserId);
        }

        public void Enable(int id)
        {
            _propertyBookingService.Disable(id, loggedInUserId);
        }

        public void Dispose()
        {
            _propertyBookingService.Dispose();
        }
    }
}