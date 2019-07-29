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
            return _propertyBookingUnitOfWork.PropertyBookingRepository.GetAll().OrderByDescending(x => x.CreatedAt);
        }

        public PropertyBooking GetById(int id)
        {
            return _propertyBookingUnitOfWork.PropertyBookingRepository.GetById(id);
        }

        public IEnumerable<PropertyBooking> GetByUserId(int id)
        {
            return _propertyBookingUnitOfWork.PropertyBookingRepository.GetByUserId(id).OrderByDescending(x => x.CreatedAt);
        }

        public IEnumerable<PropertyBooking> GetByLandlordId(int id)
        {
            return _propertyBookingUnitOfWork.PropertyBookingRepository.GetByLandlordId(id).OrderByDescending(x => x.CreatedAt);
        }

        public IEnumerable<PropertyBooking> GetByTenantId(int id)
        {
            return _propertyBookingUnitOfWork.PropertyBookingRepository.GetByTenantId(id).OrderByDescending(x => x.CreatedAt);
        }

        public int Add(PropertyBooking entity, int loggedInUserId)
        {
            var newEntity = new PropertyBooking()
            {
                UserId = entity.UserId,
                PropertyId = entity.PropertyId,
                TotalSeat = entity.TotalSeat,
                ConfirmDate = entity.ConfirmDate,
                FromDate = entity.FromDate,
                ToDate = entity.ToDate,
                Remarks = entity.Remarks,

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
                existingEntity.PropertyId = entity.PropertyId;
                existingEntity.TotalSeat = entity.TotalSeat;
                existingEntity.ConfirmDate = entity.ConfirmDate;
                existingEntity.FromDate = entity.FromDate;
                existingEntity.ToDate = entity.ToDate;
                existingEntity.Remarks = entity.Remarks;

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

        public void ChangePropertyBookingStatus(int id, int status, int loggedInUserId)
        {
            var entity = _propertyBookingUnitOfWork.PropertyBookingRepository.GetById(id);
            if (entity != null)
            {
                if (status == (int)EnumPropertyBookingStatus.Accepted)
                {
                    entity.Status = (int)EnumPropertyBookingStatus.Accepted;
                    entity.UpdatedAt = DateTime.Now;
                    entity.UpdatedBy = loggedInUserId;

                    // Property updated...
                    entity.Property.AvailableSeat -= 1;
                    if(entity.Property.AvailableSeat==0)
                        entity.Property.Status = (int)EnumPropertyStatus.Not_Available;
                    entity.Property.UpdatedAt = DateTime.Now;
                    entity.Property.UpdatedBy = loggedInUserId;
                }
                else if (status == (int)EnumPropertyBookingStatus.Rejected)
                {
                    entity.Status = (int)EnumPropertyBookingStatus.Rejected;
                    entity.UpdatedAt = DateTime.Now;
                    entity.UpdatedBy = loggedInUserId;
                }

                _propertyBookingUnitOfWork.PropertyBookingRepository.Update(entity);
                _propertyBookingUnitOfWork.Save(loggedInUserId.ToString());
            }
        }

        public void Dispose()
        {
            _propertyBookingUnitOfWork.Dispose();
        }
    }
}
