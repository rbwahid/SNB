using SNB.Entities;
using SNB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Services
{
    public class PropertyTypeService
    {
        private SNBDbContext _context;
        private PropertyTypeUnitOfwork _PropertyTypeUnitOfwork;

        public PropertyTypeService()
        {
            _context = new SNBDbContext();
            _PropertyTypeUnitOfwork = new PropertyTypeUnitOfwork(_context);
        }

        public IEnumerable<PropertyType> GetAllPropertyType()
        {
            return _PropertyTypeUnitOfwork.PropertyTypeRepository.GetAll();
        }

        public bool IsTypeNameExist(string TypeName, string InitialTypeName)
        {
            return _PropertyTypeUnitOfwork.PropertyTypeRepository.IsTypeNameExist(TypeName, InitialTypeName);
        }

        public void AddPropertyType(PropertyType propertyType )
        {
            PropertyType newPropertyType = new PropertyType
            {
                TypeName = propertyType.TypeName,
                             
            };
            _PropertyTypeUnitOfwork.PropertyTypeRepository.Add(newPropertyType);
            _PropertyTypeUnitOfwork.Save();
        }

        public void EditPropertyType(PropertyType updatePropertyType)
        {
            PropertyType PropertyTypeEntry = _PropertyTypeUnitOfwork.PropertyTypeRepository.GetById(updatePropertyType.Id);
            if (PropertyTypeEntry != null)
            {
                PropertyTypeEntry.TypeName = updatePropertyType.TypeName;

                _PropertyTypeUnitOfwork.PropertyTypeRepository.Update(PropertyTypeEntry);
                _PropertyTypeUnitOfwork.Save();
            }
        }

        public PropertyType GetPropertyTypeById(int id)
        {
            return _PropertyTypeUnitOfwork.PropertyTypeRepository.GetById(id);
        }

        public void DeletePropertyType(int id, int authorizeId)
        {
            _PropertyTypeUnitOfwork.PropertyTypeRepository.Disable(id);
            _PropertyTypeUnitOfwork.Save(authorizeId.ToString());
        }

        public void ActivePropertyType(int id, int authorizeId)
        {
            _PropertyTypeUnitOfwork.PropertyTypeRepository.Enable(id);
            _PropertyTypeUnitOfwork.Save(authorizeId.ToString());
        }

        public void Dispose()
        {
            _PropertyTypeUnitOfwork.Dispose();
        }

    }
}
