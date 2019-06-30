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
        public IEnumerable<SeatingAllocation> SeatingAllocations { get; set; }

        private SeatingAllocationService _seatingAllocationService;

        public HomeModel()
        {
            _seatingAllocationService = new SeatingAllocationService();
            this.SeatingAllocations = _seatingAllocationService.GetAvailableSeatingAllocation();
        }

        public IEnumerable<SeatingAllocation> GetAvailableSeatingAllocation()
        {
            return _seatingAllocationService.GetAvailableSeatingAllocation();
        }

    }
}