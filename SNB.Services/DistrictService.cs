using SNB.Entities;
using SNB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Services
{
    public class DistrictService
    {
        private SNBDbContext _context;
        private DistrictUnitOfwork _districtUnitOfwork;

        public DistrictService()
        {
            _context = new SNBDbContext();
            _districtUnitOfwork = new DistrictUnitOfwork(_context);
        }

        public IEnumerable<District> GetAllDistricts()
        {
            return _districtUnitOfwork.DistrictRepository.GetAll();
        }

        public bool IsDistrictNameExist(string DistrictName, string InitialDistrictName)
        {
            return _districtUnitOfwork.DistrictRepository.IsDistrictNameExist(DistrictName, InitialDistrictName);
        }

        public void AddDistrict(District district)
        {
            District newDistrict = new District
            {
                DistrictName = district.DistrictName,
                             
            };
            _districtUnitOfwork.DistrictRepository.Add(newDistrict);
            _districtUnitOfwork.Save();
        }

        public void EditDistrict(District updateDistrict)
        {
            District DistrictEntry = _districtUnitOfwork.DistrictRepository.GetById(updateDistrict.Id);
            if (DistrictEntry != null)
            {
                DistrictEntry.DistrictName = updateDistrict.DistrictName;

                _districtUnitOfwork.DistrictRepository.Update(DistrictEntry);
                _districtUnitOfwork.Save();
            }
        }

        public District GetDistrictById(int id)
        {
            return _districtUnitOfwork.DistrictRepository.GetById(id);
        }

        public void DeleteDistrict(int id, int authorizeId)
        {
            _districtUnitOfwork.DistrictRepository.Disable(id);
            _districtUnitOfwork.Save(authorizeId.ToString());
        }

        public void ActiveDistrict(int id, int authorizeId)
        {
            _districtUnitOfwork.DistrictRepository.Enable(id);
            _districtUnitOfwork.Save(authorizeId.ToString());
        }

        public void Dispose()
        {
            _districtUnitOfwork.Dispose();
        }

    }
}
