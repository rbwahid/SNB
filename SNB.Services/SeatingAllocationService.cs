using SNB.Common;
using SNB.Entities;
using SNB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Services
{
    public class SeatingAllocationService
    {
        private SeatingAllocationUnitOfWork _seatingAllocationUnitOfWork;
        private SeatingAllocationImageUnitOfwork _seatingAllocationImageUnitOfwork; 

        public SeatingAllocationService()
        {
            _seatingAllocationUnitOfWork = new SeatingAllocationUnitOfWork(new SNBDbContext());
            _seatingAllocationImageUnitOfwork = new SeatingAllocationImageUnitOfwork(new SNBDbContext());
        }

        public IEnumerable<SeatingAllocation> GetAll()
        {
            return _seatingAllocationUnitOfWork.SeatingAllocationRepository.GetAll();
        }

        public IEnumerable<SeatingAllocation> GetAvailableSeatingAllocation()
        {
            return _seatingAllocationUnitOfWork.SeatingAllocationRepository.GetAvailableSeatingAllocation();
        }

        public SeatingAllocation GetById(int id)
        {
            return _seatingAllocationUnitOfWork.SeatingAllocationRepository.GetById(id);
        }

        public IEnumerable<SeatingAllocation> GetByUserId(int id)
        {
            return _seatingAllocationUnitOfWork.SeatingAllocationRepository.GetByUserId(id);
        }

        public IEnumerable<SeatingAllocation> GetByPropertyId(int? id)
        {
            return _seatingAllocationUnitOfWork.SeatingAllocationRepository.GetByPropertyId(id);
        }

        public int Add(SeatingAllocation entity, int loggedInUserId)
        {
            var newEntity = new SeatingAllocation()
            {
                SeatingAllocationTitle = entity.SeatingAllocationTitle,
                PropertyId = entity.PropertyId,
                SeatingTypeId = entity.SeatingTypeId,
                FeaturedImage = entity.FeaturedImage,
                Description = entity.Description,
                Rent = entity.Rent,
                AvailableDate = entity.AvailableDate,
                ImageCollection = entity.ImageCollection,

                Status = (int)EnumSeatingAllocationStatus.Available,
                CreatedBy = loggedInUserId,
            };
            _seatingAllocationUnitOfWork.SeatingAllocationRepository.Add(newEntity);
            _seatingAllocationUnitOfWork.Save(loggedInUserId.ToString());

            return newEntity.Id;
        }

        public int Update(SeatingAllocation entity, int loggedInUserId)
        {
            var existingEntity = _seatingAllocationUnitOfWork.SeatingAllocationRepository.GetById(entity.Id);

            if (existingEntity != null)
            {
                existingEntity.SeatingAllocationTitle = entity.SeatingAllocationTitle;
                existingEntity.PropertyId = entity.PropertyId;
                existingEntity.SeatingTypeId = entity.SeatingTypeId;
                existingEntity.FeaturedImageId = entity.FeaturedImageId;
                existingEntity.Description = entity.Description;
                existingEntity.Rent = entity.Rent;
                existingEntity.AvailableDate = entity.AvailableDate;

                existingEntity.UpdatedBy = loggedInUserId;
                existingEntity.UpdatedAt = DateTime.Now;

                _seatingAllocationUnitOfWork.SeatingAllocationRepository.Update(existingEntity);
                _seatingAllocationUnitOfWork.Save(loggedInUserId.ToString());

                if (entity.ImageCollection.Any())
                {
                    entity.ImageCollection.ToList().ForEach(x => x.SeatingAllocationId = existingEntity.Id);
                    _seatingAllocationImageUnitOfwork.SeatingAllocationImageRepository.AddRange(entity.ImageCollection);
                    _seatingAllocationImageUnitOfwork.Save(loggedInUserId.ToString());
                }
            }

            return entity.Id;
        }

        public void Disable(int id, int loggedInUserId)
        {
            _seatingAllocationUnitOfWork.SeatingAllocationRepository.Disable(id);
            _seatingAllocationUnitOfWork.Save(loggedInUserId.ToString());
        }
        public void DeleteSeatingAllocationImage(int id, int loggedInUserId)
        {
            _seatingAllocationImageUnitOfwork.SeatingAllocationImageRepository.DeleteFromDb(id);
            _seatingAllocationImageUnitOfwork.Save();
        }

        public void Enable(int id, int loggedInUserId)
        {
            _seatingAllocationUnitOfWork.SeatingAllocationRepository.Enable(id);
            _seatingAllocationUnitOfWork.Save(loggedInUserId.ToString());
        }

        public void Dispose()
        {
            _seatingAllocationUnitOfWork.Dispose();
        }
    }
}
