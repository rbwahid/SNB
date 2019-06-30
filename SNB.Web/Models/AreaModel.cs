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
    public class AreaModel : Area 
    {
        [Required]
        [Remote("IsAreaNameExist", "Area", AdditionalFields = "InitialAreaName",
           ErrorMessage = "Area Name Already Exist")]
        [Display(Name = "Area Name")]
        public new string AreaName
        {
            get { return base.AreaName; }
            set { base.AreaName = value; }

        }
        private AreaService _areaService;
        private DistrictService _districtService;
        public List<District> DistrictList { get; set; }
        public AreaModel()
        {
            _areaService = new AreaService();
            _districtService = new DistrictService();
            DistrictList = _districtService.GetAllDistricts().ToList();
        }

        public AreaModel(int id) : this()
        {
            var AreaEntry = _areaService.GetAreaById(id);
            if (AreaEntry != null)
            {
                base.Id = AreaEntry.Id;
                base.AreaName = AreaEntry.AreaName;
                base.District = AreaEntry.District;
                base.DistrictId = AreaEntry.DistrictId;

                //base.CreatedBy = AreaEntry.CreatedBy;
                //base.CreatedAt = AreaEntry.CreatedAt;
                //base.CreatedByUser = AreaEntry.CreatedByUser;

                //base.UpdatedBy = AreaEntry.UpdatedBy;
                //base.UpdatedAt = AreaEntry.UpdatedAt;
                //base.UpdatedByUser = AreaEntry.UpdatedByUser;
            }
        }

        public void AddArea()
        {
            //base.CreatedAt = DateTime.Now;
            //base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _areaService.AddArea(this);
        }

        public void Edit()
        {
            //base.UpdatedAt = DateTime.Now;
            //base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            _areaService.EditArea(this);
        }

        public void DeleteCity(int id)
        {
            _areaService.DeleteArea(id, AuthenticatedUser.GetUserFromIdentity().UserId);
        }

        public IEnumerable<Area> GetAllAreas()
        {
            return _areaService.GetAllAreas();
        }
        public bool IsAreaNameExist(string AreaName, string InitialAreaName)
        {
            return _areaService.IsAreaNameExist(AreaName, InitialAreaName);
        }
    }
}