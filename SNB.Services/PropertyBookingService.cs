using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNB.Common;
using SNB.Entities;
using SNB.Repository;

namespace SNB.Services
{
    public class PropertyBookingService
    {
        private PropertyBookingUnitOfWork _propertyBookingUnitOfWork;

        public PropertyBookingService()
        {
            _propertyBookingUnitOfWork = new PropertyBookingUnitOfWork(new SNBDbContext());
        }

        public IEnumerable<PropertyBooking> GetAll()
        {
            return _propertyBookingUnitOfWork.PropertyBookingRepository.GetAll();
        }

        public PropertyBooking GetById(int id)
        {
            return _propertyBookingUnitOfWork.PropertyBookingRepository.GetById(id);
        }

        public IEnumerable<PropertyBooking> GetByUserId(int id)
        {
            return _propertyBookingUnitOfWork.PropertyBookingRepository.GetByUserId(id);
        }

        public int Add(PropertyBooking entity, int loggedInUserId)
        {
            var newEntity = new PropertyBooking()
            {
                UserId = entity.UserId,
                SeatingAllocationId = entity.SeatingAllocationId,
                ConfirmDate = entity.ConfirmDate,
                FromDate = entity.FromDate,
                ToDate = entity.ToDate,

                Status = (int)EnumPropertyBookingStatus.Pending,
                CreatedBy = loggedInUserId,
            };
            _propertyBookingUnitOfWork.PropertyBookingRepository.Add(newEntity);
            _propertyBookingUnitOfWork.Save(loggedInUserId.ToString());

            return newEntity.Id;
        }

        public int Update(PropertyBooking entity, int loggedInUserId)
        {
            var existingEntity = _propertyBookingUnitOfWork.PropertyBookingRepository.GetById(entity.Id);

            if (existingEntity != null)
            {
                existingEntity.SeatingAllocationId = entity.SeatingAllocationId;
                existingEntity.ConfirmDate = entity.ConfirmDate;
                existingEntity.FromDate = entity.FromDate;
                existingEntity.ToDate = entity.ToDate;

                existingEntity.UpdatedBy = loggedInUserId;
                existingEntity.UpdatedAt = DateTime.Now;

                _propertyBookingUnitOfWork.PropertyBookingRepository.Update(existingEntity);
                _propertyBookingUnitOfWork.Save(loggedInUserId.ToString());
            }

            return entity.Id;
        }

        public void Disable(int id, int loggedInUserId)
        {
            _propertyBookingUnitOfWork.PropertyBookingRepository.Disable(id);
            _propertyBookingUnitOfWork.Save(loggedInUserId.ToString());
        }

        public void Enable(int id, int loggedInUserId)
        {
            _propertyBookingUnitOfWork.PropertyBookingRepository.Enable(id);
            _propertyBookingUnitOfWork.Save(loggedInUserId.ToString());
        }

        public void Dispose()
        {
            _propertyBookingUnitOfWork.Dispose();
        }
    }
}
