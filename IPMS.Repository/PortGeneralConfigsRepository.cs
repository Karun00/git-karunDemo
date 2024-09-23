using Core.Repository;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using IPMS.Domain.ValueObjects;
using System;
using IPMS.Domain;


namespace IPMS.Repository
{
    public class PortGeneralConfigsRepository : IPortGeneralConfigsRepository
    {
        private IUnitOfWork _unitOfWork;

        public PortGeneralConfigsRepository(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public List<PortGeneralConfig> GetAllPortGeneralConfigsDetails(string portCode)
        {

            var query = (from u in _unitOfWork.Repository<PortGeneralConfig>().Query().Select()
                         where u.PortCode == portCode
                         group u by new { u.GroupName} into gu
                      
                         select new PortGeneralConfig
                         {
                             GroupName = gu.Key.GroupName
                             //,
                             //RecordStatus = gu.Key.RecordStatus
                         }).ToList();
            return query;
          
        }

        public List<PortGeneralConfig> GetAllGroupNames(string GroupName, string portCode)
        {

            var GroupNames = (from q in _unitOfWork.Repository<PortGeneralConfig>().Query().Select()

                              where (q.GroupName == GroupName && q.PortCode == portCode)
                              //(q.RecordStatus == "A") &
                              select new PortGeneralConfig
                          {
                              PortGeneralConfigID = q.PortGeneralConfigID,
                              ConfigLabelName = q.ConfigLabelName,
                              ConfigValue = q.ConfigValue,
                              ConfigName = q.ConfigName,
                              GroupName =q.GroupName
                          }).ToList();
            return GroupNames;
        }

        public string GetWFApprovedCode(string portcode)
        {
            
            var approvalcode = (from pc in _unitOfWork.Repository<PortGeneralConfig>().Query().Select()
                                where pc.PortCode == portcode && pc.ConfigName == ConfigName.ApprovedCode
                                select pc).FirstOrDefault<PortGeneralConfig>();


            return approvalcode.ConfigValue;
        }
        public string GetPortConfiguration(string portcode,string configName)
        {
            portcode = (!String.IsNullOrEmpty(portcode) ? portcode : "DB");


            var portconfiguration = (from pc in _unitOfWork.Repository<PortGeneralConfig>().Queryable().Where(pc => pc.PortCode == portcode && pc.ConfigName == configName)
                                     //where pc.PortCode == portcode && pc.ConfigName == configName
                                     select pc).FirstOrDefault<PortGeneralConfig>();

            return portconfiguration.ConfigValue;
        }

        public string GetReportPeriod(string portcode)
        {
            var portconfiguration = (from pc in _unitOfWork.Repository<PortGeneralConfig>().Query().Select()
                                     where pc.PortCode == portcode && pc.ConfigName == DatesReportPeriod.ReportPeriod
                                     select pc).FirstOrDefault<PortGeneralConfig>();

            return portconfiguration.ConfigValue == null ? string.Empty : portconfiguration.ConfigValue;
        }
    }
}
