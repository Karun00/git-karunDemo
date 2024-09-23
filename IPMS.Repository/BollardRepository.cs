using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public class BollardRepository : IPMS.Repository.IBollardRepository
    {
        private IUnitOfWork _unitOfWork;

        public BollardRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<QuayVO> GetPortQuays(string id)
        {
            var portquays = _unitOfWork.Repository<Quay>().Queryable().OrderBy(x => x.QuayName).Where(x => x.PortCode == id && x.RecordStatus == RecordStatus.Active).ToList();
            List<QuayVO> portquaysvos = new List<QuayVO>();
            foreach (var portquay in portquays)
            {
                QuayVO qvo = new QuayVO();
                qvo = QuayMapExtension.MapToDto(portquay);
                portquaysvos.Add(qvo);
            }
            return portquaysvos;
        }


        /// <summary>
        /// To Get Berths based on Port and Quay
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        public List<BerthVO> GetQuayBerths(string portCode, string quayCode)
        {
            var quayberths = _unitOfWork.Repository<Berth>().Queryable().OrderBy(x => x.BerthName).Where(x => x.QuayCode == quayCode && x.PortCode == portCode && x.RecordStatus == RecordStatus.Active && x.BerthType != DryDockStatus.DryDockType).ToList();
            List<BerthVO> BerthVOList = new List<BerthVO>();
            foreach (var berth in quayberths)
            {
                BerthVO objBerthVO = new BerthVO();
                objBerthVO = BerthMapExtension.MapToDto(berth);
                BerthVOList.Add(objBerthVO);
            }

            return BerthVOList;
        }

        /// <summary>
        /// To get Bollards based on port, quay and berth
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <param name="berthCode"></param>
        /// <returns></returns>
        public List<BollardVO> GetBollardsInBerths(string portCode, string quayCode, string berthCode)
        {
            var bollards = (from p in _unitOfWork.Repository<Port>().Queryable()
                            join qy in _unitOfWork.Repository<Quay>().Queryable()
                             on p.PortCode equals qy.PortCode
                            join be in _unitOfWork.Repository<Berth>().Queryable()
                            on new { pc = qy.PortCode, qc = qy.QuayCode } equals new { pc = be.PortCode, qc = be.QuayCode }
                            join b in _unitOfWork.Repository<Bollard>().Queryable()
                             on new { pc = be.PortCode, qc = be.QuayCode, bc = be.BerthCode } equals new { pc = b.PortCode, qc = b.QuayCode, bc = b.BerthCode }
                            where b.PortCode == portCode && b.QuayCode == quayCode && b.BerthCode == berthCode
                            select new BollardVO
                            {

                                BollardCode = b.BollardCode,
                                BollardName = b.BollardName,
                                ShortName = b.ShortName,
                                PortCode = b.PortCode,
                                PortName = p.PortName,
                                QuayCode = b.QuayCode,
                                QuayName = qy.QuayName,
                                QuayLength = qy.QuayLength,
                                BerthCode = b.BerthCode,
                                BerthName = be.BerthName,
                                BerthLength = be.Lengthm,
                                FromMeter = b.FromMeter,
                                ToMeter = b.ToMeter,
                                RecordStatus = b.RecordStatus,
                                Description = b.Description,
                                Continous = b.Continuous,
                                ContinousStatus = b.Continuous == "Y" ? true : false,
                                CreatedBy = b.CreatedBy,
                                CreatedDate = b.CreatedDate,
                                ModifiedBy = b.ModifiedBy,
                                ModifiedDate = b.ModifiedDate,
                                BolardKey = b.PortCode + "." + b.QuayCode + "." + b.BerthCode + "." + b.BollardCode
                            }).OrderByDescending(x => x.CreatedDate).ToList();
            return bollards;
        }

        /// <summary>
        /// To Get Bollard Details
        /// </summary>
        /// <returns></returns>
        public List<BollardVO> GetBollardDetails(string portCode)
        {
            var bollards = (from p in _unitOfWork.Repository<Port>().Queryable()
                            join qy in _unitOfWork.Repository<Quay>().Queryable()
                             on p.PortCode equals qy.PortCode
                            join be in _unitOfWork.Repository<Berth>().Queryable()
                            on new { pc = qy.PortCode, qc = qy.QuayCode } equals new { pc = be.PortCode, qc = be.QuayCode }
                            join b in _unitOfWork.Repository<Bollard>().Queryable()
                             on new { pc = be.PortCode, qc = be.QuayCode, bc = be.BerthCode } equals new { pc = b.PortCode, qc = b.QuayCode, bc = b.BerthCode }
                            where be.PortCode == portCode
                            select new BollardVO
                            {

                                BollardCode = b.BollardCode,
                                BollardName = b.BollardName,
                                ShortName = b.ShortName,
                                PortCode = b.PortCode,
                                PortName = p.PortName,
                                QuayCode = b.QuayCode,
                                QuayName = qy.QuayName,
                                QuayLength = qy.QuayLength,
                                BerthCode = b.BerthCode,
                                BerthName = be.BerthName,
                                BerthLength = be.Lengthm,
                                FromMeter = b.FromMeter,
                                ToMeter = b.ToMeter,
                                RecordStatus = b.RecordStatus,
                                Description = b.Description,
                                Continous = b.Continuous,
                                ContinousStatus = b.Continuous == "Y" ? true : false,
                                CreatedBy = b.CreatedBy,
                                CreatedDate = b.CreatedDate,
                                ModifiedBy = b.ModifiedBy,
                                ModifiedDate = b.ModifiedDate
                            }).OrderByDescending(x => x.CreatedDate).ToList();
            return bollards;
        }

        /// <summary>
        /// To Get Bollards By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BollardVO GetBollardById(string id)
        {
            var bollardObj = (from a in _unitOfWork.Repository<Bollard>().Query().Tracking(true).Select()
                              where a.PortCode == id
                              select a).FirstOrDefault<Bollard>();
            BollardVO bollardvo = new BollardVO();
            bollardvo = BollardMapExtension.MapToDTO(bollardObj);
            string portname = (from p in _unitOfWork.Repository<Port>().Query().Tracking(true).Select()
                               where p.PortCode == bollardObj.PortCode
                               select new { p.PortName }).SingleOrDefault().ToString();
            string quayname = (from p in _unitOfWork.Repository<Quay>().Query().Tracking(true).Select()
                               where p.QuayCode == bollardObj.QuayCode
                               select new { p.QuayName }).SingleOrDefault().ToString();
            string berthname = (from p in _unitOfWork.Repository<Berth>().Query().Tracking(true).Select()
                                where p.BerthCode == bollardObj.BerthCode
                                select new { p.BerthName }).SingleOrDefault().ToString();
            bollardvo.PortName = portname;
            bollardvo.QuayName = quayname;
            bollardvo.BerthName = berthname;
            return bollardvo;
        }


    }
}
