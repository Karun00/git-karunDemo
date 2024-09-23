using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Config;
using System.Data.Entity;
using System.Data.SqlClient;

namespace IPMS.Repository
{
    public class IncidentReportRepository : IIncidentReportRepository
    {
        private IUnitOfWork _unitOfWork;
        // private readonly ILog log;

        public IncidentReportRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            //  log = LogManager.GetLogger(typeof(IncidentReportRepository));
        }

        /// <summary>
        ///  To get incident report Details
        /// </summary>
        /// <returns></returns>
        public List<IncidentVO> GetIncidentReportList(string portCode)
        {
            var incidentReportList = new List<Incident>();

            incidentReportList = (from i in _unitOfWork.Repository<Incident>().Queryable()
                                    .Include(l => l.IncidentDocuments)
                                    .Include(l => l.IncidentNatures)
                                    .Include(l => l.IncidentNatures.Select(sc => sc.SubCategory))
                                  where i.PortCode == portCode
                                  select i).OrderByDescending(x => x.IncidentID).ToList<Incident>();
            return incidentReportList.MapToDto();
        }
    }
}
