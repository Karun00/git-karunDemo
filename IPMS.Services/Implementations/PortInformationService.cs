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
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PortInformationService : ServiceBase, IPortInformationService
    {
        private IPortInformationRepository _portInformationRepository;
        private IRolePrivilegeRepository _roleRepository = null;

        public PortInformationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _roleRepository = new RolePrivilegeRepository(_unitOfWork);
            _portInformationRepository = new PortInformationRepository(_unitOfWork);
        }

        public PortInformationService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _roleRepository = new RolePrivilegeRepository(_unitOfWork);
            _portInformationRepository = new PortInformationRepository(_unitOfWork);
        }

        #region GetPortInformationReferenceData
        /// <summary>
        /// Get Port Information Reference Data
        /// </summary>
        /// <returns></returns>
        public PortInformationReferenceVO GetPortInformationReferenceData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                PortInformationReferenceVO vo = new PortInformationReferenceVO();
                vo.GetRoles = _roleRepository.RolePrivilegeDetails().ToList();
                return vo;
            });
        }
        #endregion

        #region AddPortContent
        /// <summary>
        /// This method is used for insert the data
        /// </summary>
        /// <param name="portContentData"></param>
        /// <returns></returns>
        public PortContentVO AddPortContent(PortContentVO portContentData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _portInformationRepository.AddPortContent(portContentData, _UserId, _PortCode);
            });
        }
        #endregion

        #region ModifyPortContent
        /// <summary>
        /// Modify Port Content
        /// </summary>
        /// <param name="portContentData"></param>
        /// <returns></returns>
        public PortContentVO ModifyPortContent(PortContentVO portContentData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _portInformationRepository.ModifyPortContent(portContentData, _UserId, _PortCode);
            });
        }
        #endregion

        #region GetRolesforEmployee
        /// <summary>
        /// Get RolesforEmployee
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRolesForEmployee()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _portInformationRepository.GetRolesForEmployee();
            });
        }
        #endregion

        #region GetPortContentRoles
        /// <summary>
        /// Get PortContentRoles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PortContentRoleVO> GetPortContentRoles(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _portInformationRepository.GetPortContentRoles(id);
            });
        }
        #endregion

        #region GetDocumentDetails
        /// <summary>
        /// Get Document Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Document GetDocumentDetails(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _portInformationRepository.GetDocumentDetails(id);
            });
        }
        #endregion

        #region GetPortContenetforTreeview
        /// <summary>
        /// Get Port Contenet for Tree view
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PortContent> GetPortContentForTreeView()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var result = _portInformationRepository.GetPortContentForTreeView(_UserId, _LoginName);
                return result;
            });
        }
        #endregion
    }
}
