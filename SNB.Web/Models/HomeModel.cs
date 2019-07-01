using SNB.Entities;
using SNB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNB.Web.Models
{
    public class HomeModel
    {
        //public IEnumerable<SeatingAllocation> SeatingAllocations { get; set; }
        public IEnumerable<Property> Properties { get; set; }

        //private SeatingAllocationService _seatingAllocationService;
        private PropertyService _propertyService;

        public HomeModel()
        {
            //_seatingAllocationService = new SeatingAllocationService();
            _propertyService = new PropertyService();

            //this.SeatingAllocations = _seatingAllocationService.GetAvailableSeatingAllocation();
            this.Properties = _propertyService.GetAvailableProperty();
        }

        //public IEnumerable<SeatingAllocation> GetAvailableSeatingAllocation()
        //{
        //    return _seatingAllocationService.GetAvailableSeatingAllocation();
        //}

        public IEnumerable<Property> GetAvailableProperty()
        {
            return _propertyService.GetAvailableProperty();
        }

    }
}