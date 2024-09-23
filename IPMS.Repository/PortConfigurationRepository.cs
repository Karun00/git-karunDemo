using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using Core.Repository;


namespace IPMS.Repository
{

    public class PortConfigurationRepository : IPortConfigurationRepository
    {
        private IUnitOfWork _unitOfWork;

        public PortConfigurationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetWFApprovedCode(string portcode)
        {
            var approvalcode = (from pc in _unitOfWork.Repository<PortConfiguration>().Query().Select()
                                where pc.PortCode == portcode
                                select pc).FirstOrDefault<PortConfiguration>();


            return approvalcode.ApproveCode;
        }
        public PortConfiguration GetPortConfiguration(string portcode)
        {
            var portconfiguration = (from pc in _unitOfWork.Repository<PortConfiguration>().Query().Select()
                                       where pc.PortCode == portcode
                                       select pc).FirstOrDefault<PortConfiguration>();

            return portconfiguration;
        }

    }
   
}
