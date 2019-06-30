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
    public class SeatingTypeModel : SeatingType
    {
        [Required]
        [Remote("IsTypeNameExist", "SeatingType", AdditionalFields = "InitialTypeName",
            ErrorMessage = "Type Name Already Exist")]
        [Display(Name = "Type Name")]
        public new string TypeName
        {
            get { return base.TypeName; }
            set { base.TypeName = value; }

        }
        private SeatingTypeService _SeatingTypeService;
       
        
        public SeatingTypeModel()
        {
            _SeatingTypeService = new SeatingTypeService();
            
        }

        public SeatingTypeModel(int id) : this()
        {
            var SeatingTypeEntry = _SeatingTypeService.GetSeatingTypeById(id);
            if (SeatingTypeEntry != null)
            {
                base.Id = SeatingTypeEntry.Id;
                base.TypeName = SeatingTypeEntry.TypeName;           
            }
        }

        public void Add()
        {
            _SeatingTypeService.AddSeatingType(this);
        }

        public void Edit()
        {
            _SeatingTypeService.EditSeatingType(this);
        }

        public void Delete(int id)
        {
            _SeatingTypeService.DeleteSeatingType(id, AuthenticatedUser.GetUserFromIdentity().UserId);
        }

        public IEnumerable<SeatingType> GetAllSeatingTypes()
        {
            return _SeatingTypeService.GetAllSeatingType();
        }

        public bool IsTypeNameExist(string TypeName, string InitialTypeName)
        {
            return _SeatingTypeService.IsTypeNameExist(TypeName, InitialTypeName);
        }
    }
}