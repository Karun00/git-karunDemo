using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CargoManifestService : ServiceBase, ICargoManifestService
    {
        private ICargoManifestRepository _cargomanifestRepository;

        public CargoManifestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cargomanifestRepository = new CargoManifestRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public CargoManifestService()
        {
            // TODO: Complete member initialization
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _cargomanifestRepository = new CargoManifestRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        /// <summary>
        /// To Add Cargo Manifest Data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CargoManifestVO AddCargoManifest(CargoManifestVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _cargomanifestRepository.AddCargoManifest(data, _UserId);
            });
        }

        /// <summary>
        /// To Modify Cargo Manifest Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CargoManifestVO ModifyCargoManifest(CargoManifestVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _cargomanifestRepository.ModifyCargoManifest(data, _UserId);
            });
        }

        /// <summary>
        ///  To Get Cargo Manifest Details
        /// </summary>
        /// <returns></returns>
        public List<VCNData> CargoManifestDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _cargomanifestRepository.CargoManifestDetails(_PortCode);
            });
        }

        /// <summary>
        ///  To Get Arrival Commodity Details
        /// </summary>
        /// <returns></returns>
        public List<ArrivalCommoditiesList> ArrivalCommodityDetails(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _cargomanifestRepository.ArrivalCommodityDetails(VCN);
            });
        }
    }
}
