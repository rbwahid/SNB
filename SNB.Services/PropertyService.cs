using SNB.Entities;
using SNB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Services
{
    public class PropertyService
    {
        private PropertyUnitOfWork _propertyUnitOfWork;
        private PropertyImageUnitOfwork _propertyImageUnitOfwork ;

        public PropertyService()
        {
            _propertyUnitOfWork = new PropertyUnitOfWork(new SNBDbContext());
            _propertyImageUnitOfwork = new PropertyImageUnitOfwork(new SNBDbContext());
        }

        public IEnumerable<Property> GetAll()
        {
            return _propertyUnitOfWork.PropertyRepository.GetAll();
        }

        public Property GetById(int id)
        {
            return _propertyUnitOfWork.PropertyRepository.GetById(id);
        }

        public IEnumerable<Property> GetByUserId(int? id)
        {
            return _propertyUnitOfWork.PropertyRepository.GetByUserId(id);
        }

        public int Add(Property entity, int loggedInUserId)
        {
            var newEntity = new Property()
            {
                PropertyName = entity.PropertyName,
                PropertyTitle = entity.PropertyTitle,
                Address = entity.Address,
                ContactNo = entity.ContactNo,
                PropertyTypeId = entity.PropertyTypeId,
                UserId = entity.UserId,
                AreaId = entity.AreaId,
                DistrictId = entity.DistrictId,
                ImageCollection = entity.ImageCollection,

                CreatedBy = loggedInUserId,
            };
            _propertyUnitOfWork.PropertyRepository.Add(newEntity);
            _propertyUnitOfWork.Save(loggedInUserId.ToString());

            return newEntity.Id;
        }

        public int Update(Property entity, int loggedInUserId)
        {
            var existingEntity = _propertyUnitOfWork.PropertyRepository.GetById(entity.Id);

            if (existingEntity != null)
            {
                existingEntity.PropertyName = entity.PropertyName;
                existingEntity.PropertyTitle = entity.PropertyTitle;
                existingEntity.Address = entity.Address;
                existingEntity.ContactNo = entity.ContactNo;
                existingEntity.PropertyTypeId = entity.PropertyTypeId;
                existingEntity.UserId = entity.UserId;
                existingEntity.AreaId = entity.AreaId;
                existingEntity.DistrictId = entity.DistrictId;

                existingEntity.UpdatedBy = loggedInUserId;
                existingEntity.UpdatedAt = DateTime.Now;

                _propertyUnitOfWork.PropertyRepository.Update(existingEntity);
                _propertyUnitOfWork.Save(loggedInUserId.ToString());
            }

            return entity.Id;
        }

        public void DeleteAttachmentFileImage(int id, int loggedInUserId)
        {
            _propertyImageUnitOfwork.PropertyImageRepository.DeleteFromDb(id);
            _propertyImageUnitOfwork.Save(loggedInUserId.ToString());
        }

        public void Disable(int id, int loggedInUserId)
        {
            _propertyUnitOfWork.PropertyRepository.Disable(id);
            _propertyUnitOfWork.Save(loggedInUserId.ToString());
        }

        public void Enable(int id, int loggedInUserId)
        {
            _propertyUnitOfWork.PropertyRepository.Enable(id);
            _propertyUnitOfWork.Save(loggedInUserId.ToString());
        }

        public void Dispose()
        {
            _propertyUnitOfWork.Dispose();
        }
    }
}
