using SNB.Entities;
using SNB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Services
{
    public class SeatingTypeService
    {
        private SNBDbContext _context;
        private SeatingTypeUnitOfwork _SeatingTypeUnitOfwork;

        public SeatingTypeService()
        {
            _context = new SNBDbContext();
            _SeatingTypeUnitOfwork = new SeatingTypeUnitOfwork(_context);
        }

        public IEnumerable<SeatingType> GetAllSeatingType()
        {
            return _SeatingTypeUnitOfwork.SeatingTypeRepository.GetAll();
        }

        public bool IsTypeNameExist(string TypeName, string InitialTypeName)
        {
            return _SeatingTypeUnitOfwork.SeatingTypeRepository.IsTypeNameExist(TypeName, InitialTypeName);
        }

        public void AddSeatingType(SeatingType seatingType)
        {
            SeatingType newSeatingType = new SeatingType
            {
                TypeName = seatingType.TypeName,
                             
            };
            _SeatingTypeUnitOfwork.SeatingTypeRepository.Add(newSeatingType);
            _SeatingTypeUnitOfwork.Save();
        }

        public void EditSeatingType(SeatingType updateSeatingType)
        {
            SeatingType SeatingTypeEntry = _SeatingTypeUnitOfwork.SeatingTypeRepository.GetById(updateSeatingType.Id);
            if (SeatingTypeEntry != null)
            {
                SeatingTypeEntry.TypeName = updateSeatingType.TypeName;
                _SeatingTypeUnitOfwork.SeatingTypeRepository.Update(SeatingTypeEntry);
                _SeatingTypeUnitOfwork.Save();
            }
        }

        public SeatingType GetSeatingTypeById(int id)
        {
            return _SeatingTypeUnitOfwork.SeatingTypeRepository.GetById(id);
        }

        public void DeleteSeatingType(int id, int authorizeId)
        {
            _SeatingTypeUnitOfwork.SeatingTypeRepository.Disable(id);
            _SeatingTypeUnitOfwork.Save(authorizeId.ToString());
        }

        public void ActiveSeatingType(int id, int authorizeId)
        {
            _SeatingTypeUnitOfwork.SeatingTypeRepository.Enable(id);
            _SeatingTypeUnitOfwork.Save(authorizeId.ToString());
        }

        public void Dispose()
        {
            _SeatingTypeUnitOfwork.Dispose();
        }

    }
}
