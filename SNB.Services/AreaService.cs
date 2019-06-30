using SNB.Entities;
using SNB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Services
{
    public class AreaService
    {
        private SNBDbContext _context;
        private AreatUnitOfwork _AreatUnitOfwork;

        public AreaService()
        {
            _context = new SNBDbContext();
            _AreatUnitOfwork = new AreatUnitOfwork(_context);
        }

        public IEnumerable<Area> GetAllAreas()
        {
            return _AreatUnitOfwork.AreaRepository.GetAll();
        }

        public bool IsAreaNameExist(string AreaName, string InitialAreaName)
        {
            return _AreatUnitOfwork.AreaRepository.IsAreaNameExist(AreaName, InitialAreaName);
        }

        public void AddArea(Area Area)
        {
            Area newArea = new Area
            {
                AreaName = Area.AreaName,
                DistrictId = Area.DistrictId,

                //CreatedBy = City.CreatedBy,
                //CreatedAt = City.CreatedAt,
            };
            _AreatUnitOfwork.AreaRepository.Add(newArea);
            _AreatUnitOfwork.Save();
        }

        public void EditArea(Area updateArea)
        {
            Area AreaEntry = _AreatUnitOfwork.AreaRepository.GetById(updateArea.Id);
            if (AreaEntry != null)
            {
                AreaEntry.AreaName = updateArea.AreaName;
                AreaEntry.DistrictId = updateArea.DistrictId;

                //AreaEntry.UpdatedAt = updateArea.DateTime().now;
                //CityEntry.UpdatedBy = updateCity.UpdatedBy;

                _AreatUnitOfwork.AreaRepository.Update(AreaEntry);
                _AreatUnitOfwork.Save();
            }
        }

        public Area GetAreaById(int id)
        {
            return _AreatUnitOfwork.AreaRepository.GetById(id);
        }

        public void DeleteArea(int id, int authorizeId)
        {
            _AreatUnitOfwork.AreaRepository.Disable(id);
            _AreatUnitOfwork.Save(authorizeId.ToString());
        }

        public void ActiveArea(int id, int authorizeId)
        {
            _AreatUnitOfwork.AreaRepository.Enable(id);
            _AreatUnitOfwork.Save(authorizeId.ToString());
        }

        public void Dispose()
        {
            _AreatUnitOfwork.Dispose();
        }

        //public IEnumerable<Area> GetAllCityByCountryId(int id)
        //{
        //    return _cityUnitOfWork.CityRepository.GetAllCityByCountryId(id);
        //}
    }
}
