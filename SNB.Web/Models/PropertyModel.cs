using SNB.Common;
using SNB.Entities;
using SNB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNB.Web.Models
{
    public class PropertyModel : Property
    {
        private PropertyService _propertyService;
        private int loggedInUserId;

        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<District> Districts { get; set; }
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        public List<HttpPostedFileBase> ImageFileBases { get; set; }

        public PropertyModel()
        {
            _propertyService = new PropertyService();
            loggedInUserId = AuthenticatedUser.GetUserFromIdentity().UserId; 
        }

        public PropertyModel(int id) : this()
        {
            var data = _propertyService.GetById(id);
            if (data != null)
            {
                this.Id = data.Id;
                this.PropertyName = data.PropertyName;
                this.PropertyTitle = data.PropertyTitle;
                this.Address = data.Address;
                this.ContactNo = data.ContactNo;
                this.PropertyTypeId = data.PropertyTypeId;
                this.PropertyType = data.PropertyType;
                this.UserId = data.UserId;
                this.User = data.User;
                this.AreaId = data.AreaId;
                this.Area = data.Area;
                this.DistrictId = data.DistrictId;
                this.District = data.District;
                this.ImageCollection = data.ImageCollection;

                this.CreatedBy = data.CreatedBy;
                this.CreatedAt = data.CreatedAt;
                this.UpdatedBy = data.UpdatedBy;
                this.UpdatedAt = data.UpdatedAt;
            }
        }

        public void LoadAllListData()
        {
            this.PropertyTypes = new List<PropertyType>() {
                new PropertyType{ Id=1, TypeName = "Apartment" },
                new PropertyType{ Id=2, TypeName = "Hostel" },
                new PropertyType{ Id=3, TypeName = "Mess" }
            };
            this.Districts = new List<District>()
            {
                new District{ Id=1, DistrictName = "Dhaka" },
                new District{ Id=2, DistrictName = "Rajshahi" },
                new District{ Id=3, DistrictName = "Rangpur" }
            };
            this.Areas = new List<Area>()
            {
                new Area{ Id=1,AreaName = "Dhanmondi", DistrictId = this.Districts.FirstOrDefault(x => x.DistrictName == "Dhaka").Id },
                new Area{ Id=2, AreaName = "Mohakhali", DistrictId = this.Districts.FirstOrDefault(x => x.DistrictName == "Dhaka").Id },
                new Area{ Id=3, AreaName = "Mirpur", DistrictId = this.Districts.FirstOrDefault(x => x.DistrictName == "Dhaka").Id },
                new Area{ Id=4, AreaName = "Rangpur City", DistrictId = this.Districts.FirstOrDefault(x => x.DistrictName == "Rangpur").Id },
                new Area{ Id=5, AreaName = "Rajshahi City", DistrictId = this.Districts.FirstOrDefault(x => x.DistrictName == "Rajshahi").Id },
            };
        }

        public IEnumerable<Property> GetAll()
        {
            return _propertyService.GetAll().ToList();
        }

        public IEnumerable<Property> GetByUserId(int? id)
        {
            return _propertyService.GetByUserId(loggedInUserId).ToList();
        }

        public Property GetById(int id)
        {
            return _propertyService.GetById(id);
        }

        public int Add()
        {
            if (this.ImageFileBases.Any())
            {
                var attachmentFiles = CustomFile.SaveImageFile(this.ImageFileBases, this.PropertyTitle, "Property");
                if (attachmentFiles.Any())
                {
                    this.ImageCollection = attachmentFiles.Select(x => new PropertyImage() { AttachementFile = x }).ToList();
                }
            }
            this.UserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            return _propertyService.Add(this, loggedInUserId);
        }

        public int Update()
        {
            return _propertyService.Update(this, loggedInUserId);
        }

        public void Disable(int id)
        {
            _propertyService.Disable(id, loggedInUserId);
        }
        public void DeleteAttachmentFileImage(int id)
        {
            _propertyService.DeleteAttachmentFileImage(id, loggedInUserId);
        }
        public void Enable(int id)
        {
            _propertyService.Disable(id, loggedInUserId);
        }

        public void Dispose()
        {
            _propertyService.Dispose();
        }
    }
}