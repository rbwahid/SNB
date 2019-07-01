using SNB.Entities;
using SNB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNB.Web.Models
{
    public class PropertyTypeModel : PropertyType
    {
        [Required]
        [Remote("IsTypeNameExist", "PropertyType", AdditionalFields = "InitialTypeName",
            ErrorMessage = "Type Name Already Exist")]
        [Display(Name = "Type Name")]
        public new string TypeName
        {
            get { return base.TypeName; }
            set { base.TypeName = value; }

        }
        private PropertyTypeService _PropertyTypeService;
       
        
        public PropertyTypeModel()
        {
            _PropertyTypeService = new PropertyTypeService();
            
        }

        public PropertyTypeModel(int id) : this()
        {
            var PropertyTypeEntry = _PropertyTypeService.GetPropertyTypeById(id);
            if (PropertyTypeEntry != null)
            {
                base.Id = PropertyTypeEntry.Id;
                base.TypeName = PropertyTypeEntry.TypeName;           
            }
        }

        public void Add()
        {
            _PropertyTypeService.AddPropertyType(this);
        }

        public void Edit()
        {
            _PropertyTypeService.EditPropertyType(this);
        }

        public void Delete(int id)
        {
            _PropertyTypeService.DeletePropertyType(id, AuthenticatedUser.GetUserFromIdentity().UserId);
        }

        public IEnumerable<PropertyType> GetAllPropertyTypes()
        {
            return _PropertyTypeService.GetAllPropertyType();
        }

        public bool IsTypeNameExist(string TypeName, string InitialTypeName)
        {
            return _PropertyTypeService.IsTypeNameExist(TypeName, InitialTypeName);
        }
    }
}