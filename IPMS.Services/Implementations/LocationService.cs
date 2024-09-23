using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                   ConcurrencyMode = ConcurrencyMode.Multiple)]
   public class LocationService: ServiceBase, ILocationService
    {
          public LocationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
        }

          public LocationService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
        }

          /// <summary>
          /// To Get Location Details
          /// </summary>
          /// <returns></returns>
          public List<LocationVO> LocationDetails()
          {
              return ExecuteFaultHandledOperation(() =>
              {
                  var LocationDetails = from q in _unitOfWork.Repository<Location>().Queryable().AsEnumerable<Location>()
                                        join p in _unitOfWork.Repository<Port>().Queryable().AsEnumerable<Port>()
                                       on q.PortCode equals p.PortCode
                                        where q.PortCode == _PortCode 
                                             select new LocationVO
                                             {
                                                 LocationID = q.LocationID,
                                                 LocationName = q.LocationName,
                                                 PortCode=q.PortCode,
                                                 RecordStatus = q.RecordStatus,
                                                 CreatedBy = q.CreatedBy,
                                                 CreatedDate = q.CreatedDate,
                                                 LocationPortCode = _PortCode,
                                                 PortName=p.PortName

                                             };

                  return LocationDetails.OrderByDescending(x=>x.LocationID).ToList();

              });
          }

          /// <summary>
          /// To Add Location Data
          /// </summary>
          /// <param name="locationData"></param>
          /// <returns></returns>
          public LocationVO AddLocation(LocationVO locationData)
          {
              return EncloseTransactionAndHandleException(() =>
              {
                  locationData.CreatedBy = _UserId;
                  locationData.CreatedDate = DateTime.Now;
                  locationData.ModifiedBy = _UserId;
                  locationData.LocationPortCode = _PortCode;
                  locationData.ModifiedDate = DateTime.Now;
                  Location location = new Location();
                  location = LocationMapExtension.MapToEntity(locationData);
                  location.ObjectState = ObjectState.Added;
                  _unitOfWork.Repository<Location>().Insert(location);
                  _unitOfWork.SaveChanges();
                  return locationData;
              });
          }

          /// <summary>
          /// To Modify Location Data
          /// </summary>
          /// <param name="locationData"></param>
          /// <returns></returns>
          public LocationVO ModifyLocation(LocationVO locationData)
          {
              return EncloseTransactionAndHandleException(() =>
              {
                  locationData.ModifiedBy = _UserId;
                  locationData.ModifiedDate = DateTime.Now;
                  locationData.LocationPortCode = _PortCode;
                  Location location = new Location();
                  location = LocationMapExtension.MapToEntity(locationData);
                  location.ObjectState = ObjectState.Modified;
                  _unitOfWork.Repository<Location>().Update(location);
                  _unitOfWork.SaveChanges();
                  return locationData;
              });
          }
    }
}
