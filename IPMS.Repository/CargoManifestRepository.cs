using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;

namespace IPMS.Repository
{
    public class CargoManifestRepository : ICargoManifestRepository
    {
        private IUnitOfWork _unitOfWork;

        public CargoManifestRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region AddCargoManifest
        /// <summary>
        /// To Save or Insert Cargo Manifest Data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public CargoManifestVO AddCargoManifest(CargoManifestVO entity, int UserId)
        {
            if (entity != null)
            {
                entity.CreatedBy = UserId;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = UserId;
                entity.ModifiedDate = DateTime.Now;

                CargoManifest CargoManifests = new CargoManifest();
                CargoManifests = CargoManifestMapExtension.MapToEntity(entity);

                List<CargoManifestDtlVO> _CargoManifDet = new List<CargoManifestDtlVO>();
                _CargoManifDet = entity.CargoManifests;

                CargoManifests.CargoManifestDtls = null;

                List<CargoManifestDtl> _cargmandet11 = new List<CargoManifestDtl>();
                foreach (var data11 in _CargoManifDet)
                {
                    CargoManifests.UOMCode = data11.UOMCode;
                }

                CargoManifests.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<CargoManifest>().Insert(CargoManifests);
                _unitOfWork.SaveChanges();

                List<CargoManifestDtl> _cargmandet = new List<CargoManifestDtl>();
                foreach (var data in _CargoManifDet)
                {
                    CargoManifestDtl _cargomanifestdtl = new CargoManifestDtl();
                    _cargomanifestdtl.CargoManifestDtlID = data.CargoManifestDtlID;
                    _cargomanifestdtl.CargoManifestID = CargoManifests.CargoManifestID;
                    _cargomanifestdtl.CargoTypeCode = data.CargoTypeCode;
                    _cargomanifestdtl.OutTurn = data.OutTurn.GetValueOrDefault();
                    _cargomanifestdtl.Quantity = data.Quantity.GetValueOrDefault();
                    _cargomanifestdtl.UOMCode = data.UOMCode;
                    _cargomanifestdtl.RecordStatus = "A";
                    _cargomanifestdtl.CreatedBy = entity.CreatedBy;
                    _cargomanifestdtl.CreatedDate = entity.CreatedDate;
                    _cargomanifestdtl.ModifiedBy = entity.ModifiedBy;
                    _cargomanifestdtl.ModifiedDate = entity.ModifiedDate;
                    _cargmandet.Add(_cargomanifestdtl);
                }
                _unitOfWork.Repository<CargoManifestDtl>().InsertRange(_cargmandet);
                _unitOfWork.SaveChanges();
            }
            return entity;
        }
        #endregion

        #region ModifyCargoManifest
        /// <summary>
        /// To Modify Cargo Manifest Data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public CargoManifestVO ModifyCargoManifest(CargoManifestVO entity, int UserId)
        {
            if (entity != null)
            {
                entity.ModifiedBy = UserId;
                entity.ModifiedDate = DateTime.Now;

                CargoManifest CargoManifests = new CargoManifest();
                CargoManifests = CargoManifestMapExtension.MapToEntity(entity);

                List<CargoManifestDtl> _CargoManifDet = new List<CargoManifestDtl>();
                _CargoManifDet = CargoManifestMapExtension.MapToEntity(entity.CargoManifests);

                var cargObj = _unitOfWork.Repository<CargoManifest>().Find(CargoManifests.CargoManifestID);

                if (cargObj != null)
                {
                    cargObj.FirstMoveDateTime = CargoManifests.FirstMoveDateTime;
                    cargObj.LastMoveDateTime = CargoManifests.LastMoveDateTime;
                    cargObj.ModifiedBy = CargoManifests.ModifiedBy;
                    cargObj.ModifiedDate = CargoManifests.ModifiedDate;

                    cargObj.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<CargoManifest>().Update(cargObj);
                    _unitOfWork.SaveChanges();
                }

                if (_CargoManifDet.Count > 0)
                {
                    foreach (var dc in _CargoManifDet)
                    {
                        dc.OutTurn = dc.OutTurn;
                        dc.ModifiedBy = UserId;
                        dc.ModifiedDate = DateTime.Now;
                        _unitOfWork.Repository<CargoManifestDtl>().Update(dc);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            return entity;
        }
        #endregion

        #region CargoManifestDetails
        /// <summary>
        /// To Get Cargo Manifest Details
        /// </summary>
        /// <param name="PortCode"></param>
        /// <returns></returns>
        public List<VCNData> CargoManifestDetails(string portCode)
        {
            List<VCNData> _VCNList = new List<VCNData>();
            var PortCode = new SqlParameter("@PortCode", portCode);
            var Request = _unitOfWork.SqlQuery<VCNData>("usp_GetCargoManifestDetails @PortCode", PortCode);
            _VCNList = Request.ToList();

            return _VCNList;
        }
        #endregion

        #region ArrivalCommodityDetails
        /// <summary>
        /// To Get Arrival Commodity Details
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        public List<ArrivalCommoditiesList> ArrivalCommodityDetails(string VCN)
        {
            var outturnCount = (from s in _unitOfWork.Repository<CargoManifest>().Query().Select().AsEnumerable<CargoManifest>()
                                where s.VCN == VCN
                                select s).Count();

            if (outturnCount == 0)
            {
                return GetArrivalCommodity(VCN);
            }
            else
            {
                return GetArrivalCommodityDetails(VCN);
            }
        }

        private List<ArrivalCommoditiesList> GetArrivalCommodityDetails(string VCN)
        {
            var ArrivalCommodityDetails = (from cmd in _unitOfWork.Repository<CargoManifestDtl>().Query().Select().AsEnumerable<CargoManifestDtl>()
                                           join sc1 in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>()
                                           on cmd.CargoTypeCode equals sc1.SubCatCode
                                           join sc2 in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>()
                                           on cmd.UOMCode equals sc2.SubCatCode
                                           join cm in _unitOfWork.Repository<CargoManifest>().Query().Select().AsEnumerable<CargoManifest>()
                                           on cmd.CargoManifestID equals cm.CargoManifestID
                                           join vc in _unitOfWork.Repository<VesselCall>().Query().Select().AsEnumerable<VesselCall>()
                                           on cm.VCN equals vc.VCN
                                           orderby cmd.CreatedDate descending
                                           where cmd.RecordStatus == "A" && cm.VCN == VCN
                                           select new ArrivalCommoditiesList
                                           {
                                               CargoManifestDtlID = cmd.CargoManifestDtlID,
                                               VCN = cm.VCN,
                                               CargoTypeCode = sc1.SubCatCode,
                                               CargoTypeName = sc1.SubCatName,
                                               UOM = sc2.SubCatName,
                                               UOMCode = sc2.SubCatCode,
                                               Quantity = cmd.Quantity,
                                               OutTurn = cmd.OutTurn,
                                               CargoManifestID = cmd.CargoManifestID,

                                               RecordStatus = cmd.RecordStatus,
                                               CreatedBy = cmd.CreatedBy,
                                               CreatedDate = cmd.CreatedDate,
                                               ModifiedBy = cmd.ModifiedBy,
                                               ModifiedDate = cmd.ModifiedDate
                                           });

            return ArrivalCommodityDetails.ToList();
        }

        private List<ArrivalCommoditiesList> GetArrivalCommodity(string vcn)
        {
            var ArrivalCommodityDetails = (from ac in _unitOfWork.Repository<ArrivalCommodity>().Query().Select().AsEnumerable<ArrivalCommodity>()
                                           join vc in _unitOfWork.Repository<VesselCall>().Query().Select().AsEnumerable<VesselCall>()
                                           on ac.VCN equals vc.VCN
                                           join sc1 in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>()
                                           on ac.CargoType equals sc1.SubCatCode
                                           join sc2 in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>()
                                           on ac.UOM equals sc2.SubCatCode
                                           orderby ac.CreatedDate descending
                                           where ac.RecordStatus == "A" && ac.VCN == vcn
                                           select new ArrivalCommoditiesList
                                           {
                                               VCN = ac.VCN,
                                               CargoTypeCode = sc1.SubCatCode,
                                               CargoTypeName = sc1.SubCatName,
                                               UOM = sc2.SubCatName,
                                               UOMCode = sc2.SubCatCode,
                                               Quantity = ac.Quantity
                                           });
            return ArrivalCommodityDetails.ToList();
        }
        #endregion
    }
}
