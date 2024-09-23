using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PortGeneralConfigsService : ServiceBase, IPortGeneralConfigsService
    {
        private IPortGeneralConfigsRepository _portgeneralconfigRepository;

        public PortGeneralConfigsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _portgeneralconfigRepository = new PortGeneralConfigsRepository(_unitOfWork);
        }

        public PortGeneralConfigsService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _portgeneralconfigRepository = new PortGeneralConfigsRepository(_unitOfWork);
        }

        #region GetAllPortGeneralConfigsDetails
        public List<PortGeneralConfigsVO> GetAllPortGeneralConfigsDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _portgeneralconfigRepository.GetAllPortGeneralConfigsDetails(_PortCode).MapToDTO();
            });
        }
        #endregion

        #region GetAllGroupNames
        public List<PortGeneralConfigsVO> GetAllGroupNames(string GroupName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _portgeneralconfigRepository.GetAllGroupNames(GroupName, _PortCode).MapToDTO();
            });
        }
        #endregion

        #region UpdatePortGeneralConfigs
        /// <summary>
        ///  To Update new Port General Configs
        /// </summary>
        /// <param name="portgeneralconfigdata"></param>
        /// <returns></returns>
        public int UpdatePortGeneralConfigs(PortGeneralConfigsVO portgeneralconfigdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string name = _LoginName;
                int UserID = GetUserId(name);

                foreach (var item in portgeneralconfigdata.GroupNames)
                {
                    var portgeneralconfig = _unitOfWork.Repository<PortGeneralConfig>().Find(item.PortGeneralConfigID);
                    portgeneralconfig.ConfigLabelName = item.ConfigLabelName;
                    portgeneralconfig.ConfigValue = item.ConfigValue;
                    portgeneralconfig.ModifiedBy = UserID;
                    portgeneralconfig.PortCode = _PortCode;
                    portgeneralconfig.ModifiedDate = DateTime.Now;
                    portgeneralconfig.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<PortGeneralConfig>().Update(portgeneralconfig);
                }

                _unitOfWork.SaveChanges();

                return portgeneralconfigdata.PortGeneralConfigID;
            });
        }
        #endregion

        #region GetUserID
        /// <summary>
        /// To get User Id
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int GetUserId(string loginName)
        {

            var user = (from u in _unitOfWork.Repository<User>().Query().Select()
                        where u.UserName == loginName
                        select u).FirstOrDefault<User>();
            return user.UserID;

        }
        #endregion
    }
}
