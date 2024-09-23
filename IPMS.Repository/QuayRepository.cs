using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public class QuayRepository : IQuayRepository
    {
        private IUnitOfWork _unitOfWork;

        public QuayRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// To Get Quay Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuayVO GetQuayId(string id)
        {
            var quayid = (from a in _unitOfWork.Repository<Quay>().Query().Select()
                          where a.QuayCode == id
                          select a).FirstOrDefault<Quay>();
            QuayVO quay = QuayMapExtension.MapToDto(quayid);

            string portname = (from p in _unitOfWork.Repository<Port>().Query().Select()
                               where p.PortCode == quayid.PortCode
                               select new { p.PortName }).SingleOrDefault().ToString();
            quay.PortName = portname;
            return quay;
        }

        /// <summary>
        /// To Delete Quay by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuayVO DeleteQuay(long id)
        {
            var quayObj = _unitOfWork.Repository<Quay>().Find(id);
            quayObj.RecordStatus = "I";
            quayObj.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<Quay>().Update(quayObj);
            _unitOfWork.SaveChanges();

            QuayVO quay = QuayMapExtension.MapToDto(quayObj);
            string portname = (from p in _unitOfWork.Repository<Port>().Query().Select()
                               where p.PortCode == quayObj.PortCode
                               select new { p.PortName }).SingleOrDefault().ToString();
            quay.PortName = portname;
            return quay;
        }

        /// <summary>
        /// To Get Quay Details
        /// </summary>
        /// <returns></returns>
        public List<QuayVO> QuayDetails(string portCode)
        {
            var QuayDetails = from q in _unitOfWork.Repository<Quay>().Queryable().AsEnumerable<Quay>()
                              join p in _unitOfWork.Repository<Port>().Queryable().AsEnumerable<Port>()
                               on q.PortCode equals p.PortCode
                              where q.PortCode == portCode
                              orderby q.CreatedDate descending
                              select new QuayVO
                              {
                                  QuayCode = q.QuayCode,
                                  QuayName = q.QuayName,
                                  ShortName = q.ShortName,
                                  PortName = p.PortName,
                                  PortCode = q.PortCode,
                                  Description = q.Description,
                                  QuayLength = q.QuayLength,
                                  RecordStatus = q.RecordStatus,
                                  CreatedBy = q.CreatedBy,
                                  CreatedDate = q.CreatedDate

                              };

            return QuayDetails.ToList();
        }

        /// <summary>
        /// To Get Berths based on Quay
        /// </summary>
        /// <returns></returns>
        public List<QuayVO> GetQuaysWithBerths(string portCode)
        {
            var Quays = (from b in _unitOfWork.Repository<Quay>().Query().Include(b => b.Berths).Select()
                         where b.PortCode == portCode
                         select b).ToList();

            return Quays.MapToListDto();
        }

        /// <summary>
        /// To Get Quays with Berths
        /// </summary>
        /// <returns></returns>
        public List<QuayVO> GetQuaysWithBerthsMobile(string portCode)
        {
            var Quays = (from b in _unitOfWork.Repository<Quay>().Query().Include(b => b.Berths).Include(b => b.Berths.Select(d => d.Bollards)).Include(b => b.Berths.Select(bc => bc.BerthCargoes)).Include(b => b.Berths.Select(bc => bc.BerthCargoes.Select(sc => sc.SubCategory))).Select()
                         where b.PortCode == portCode
                         select b).ToList();

            return Quays.MapToListDtoMobile();          
        }

        /// <summary>
        /// To Get Quay Details based on PortCode
        /// </summary>
        /// <returns></returns>
        public List<QuayVO> QuayPortDetails(string portCode)
        {
            var QuayDetails = from q in _unitOfWork.Repository<Quay>().Query().Select().AsEnumerable<Quay>()
                              join p in _unitOfWork.Repository<Port>().Query().Select().AsEnumerable<Port>()
                               on q.PortCode equals p.PortCode
                              where q.PortCode == portCode
                              orderby q.CreatedDate descending
                              select new QuayVO
                              {
                                  QuayCode = q.QuayCode,
                                  QuayName = q.QuayName,
                                  ShortName = q.ShortName,
                                  PortName = p.PortName,
                                  PortCode = q.PortCode,
                                  Description = q.Description,
                                  QuayLength = q.QuayLength,
                                  RecordStatus = q.RecordStatus,
                                  CreatedBy = q.CreatedBy,
                                  CreatedDate = q.CreatedDate

                              };

            return QuayDetails.ToList();
        }



    }
}
