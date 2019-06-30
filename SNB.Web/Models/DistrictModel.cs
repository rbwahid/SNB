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
    public class DistrictModel : District 
    {
        [Required]
        [Remote("IsDistrictNameExist", "District", AdditionalFields = "InitialDistrictName",
            ErrorMessage = "District Name Already Exist")]
        [Display(Name = "District Name")]
        public new string DistrictName
        {
            get { return base.DistrictName; }
            set { base.DistrictName = value; }

        }
        private DistrictService _districtService;
       
        
        public DistrictModel()
        {
            _districtService = new DistrictService();
            
        }

        public DistrictModel(int id) : this()
        {
            var DistrictEntry = _districtService.GetDistrictById(id);
            if (DistrictEntry != null)
            {
                base.Id = DistrictEntry.Id;
                base.DistrictName = DistrictEntry.DistrictName;           
            }
        }

        public void Add()
        {
            _districtService.AddDistrict(this);
        }

        public void Edit()
        {
            _districtService.EditDistrict(this);
        }

        public void Delete(int id)
        {
            _districtService.DeleteDistrict(id, AuthenticatedUser.GetUserFromIdentity().UserId);
        }

        public IEnumerable<District> GetAllDistricts()
        {
            return _districtService.GetAllDistricts();
        }

        public bool IsDistrictNameExist(string DistrictName, string InitialDistrictName)
        {
            return _districtService.IsDistrictNameExist(DistrictName, InitialDistrictName);
        }
    }
}