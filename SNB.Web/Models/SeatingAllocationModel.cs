using SNB.Common;
using SNB.Entities;
using SNB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNB.Web.Models
{
    public class SeatingAllocationModel : SeatingAllocation
    {
        [Required]
        [Display(Name = "Available Date")]
        public new DateTime AvailableDate { get { return base.AvailableDate??DateTime.Now; } set { base.AvailableDate = value; } }

        private SeatingAllocationService _seatingAllocationService;
        private PropertyService _propertyService;
        private SeatingTypeService _seatingTypeService;
        private int loggedInUserId;

        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<SeatingType> SeatingTypes { get; set; }
        public List<HttpPostedFileBase> ImageFileBases { get; set; }

        public SeatingAllocationModel()
        {
            _seatingAllocationService = new SeatingAllocationService();
            _propertyService = new PropertyService();
            _seatingTypeService = new SeatingTypeService();
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
        }
        public SeatingAllocationModel(int id) : this()
        {
            var data = _seatingAllocationService.GetById(id);
            if (data != null)
            {
                this.Id = data.Id;
                this.SeatingAllocationTitle = data.SeatingAllocationTitle;
                this.PropertyId = data.PropertyId;
                this.Property = data.Property;
                this.SeatingTypeId = data.SeatingTypeId;
                this.SeatingType = data.SeatingType;
                this.Description = data.Description;
                this.Rent = data.Rent;
                this.FeaturedImageId = data.FeaturedImageId;
                this.FeaturedImage = data.FeaturedImage;
                this.ImageCollection = data.ImageCollection;
                this.AvailableDate = data.AvailableDate??DateTime.Now;

                this.Status = data.Status;
                this.CreatedBy = data.CreatedBy;
                this.CreatedAt = data.CreatedAt;
                this.UpdatedBy = data.UpdatedBy;
                this.UpdatedAt = data.UpdatedAt;
            }
        }

        public void LoadAllListData()
        {

            this.SeatingTypes = _seatingTypeService.GetAllSeatingType().ToList();
            this.Properties = _propertyService.GetByUserId(loggedInUserId).ToList();
        }

        public IEnumerable<SeatingAllocation> GetAll()
        {
            return _seatingAllocationService.GetAll().ToList();
        }

        public IEnumerable<SeatingAllocation> GetByPropertyId(int? id)
        {
            return _seatingAllocationService.GetByPropertyId(loggedInUserId).ToList();
        }

        public IEnumerable<SeatingAllocation> GetByUserId(int? id)
        {
            return _seatingAllocationService.GetByUserId(loggedInUserId).ToList();
        }

        public SeatingAllocation GetById(int id)
        {
            return _seatingAllocationService.GetById(id);
        }

        public int Add()
        {
            if (this.ImageFileBases.Any())
            {
                var attachmentFiles = CustomFile.SaveImageFile(this.ImageFileBases, this.SeatingAllocationTitle, "SeatingAllocation");
                if (attachmentFiles.Any())
                {
                    this.ImageCollection = attachmentFiles.Select(x => new SeatingAllocationImage() { AttachementFile = x }).ToList();
                }
            }
            return _seatingAllocationService.Add(this, loggedInUserId);
        }

        public int Update()
        {
            return _seatingAllocationService.Update(this, loggedInUserId);
        }

        public void Disable(int id)
        {
            _seatingAllocationService.Disable(id, loggedInUserId);
        }
        public void DeleteSeatingAllocationImage(int id)
        {
            _seatingAllocationService.DeleteSeatingAllocationImage(id, loggedInUserId);
        }

        public void Enable(int id)
        {
            _seatingAllocationService.Disable(id, loggedInUserId);
        }

        public void Dispose()
        {
            _seatingAllocationService.Dispose();
        }
    }
}