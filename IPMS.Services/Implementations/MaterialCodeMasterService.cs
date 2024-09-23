using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
      ConcurrencyMode = ConcurrencyMode.Multiple)]   
    public class MaterialCodeMasterService : ServiceBase, IMaterialCodeMasterService
    {
        private IMaterialCodeMasterRepository _materialcodeRepository;

          public MaterialCodeMasterService(IUnitOfWork unitOfWork)
          {
               _unitOfWork = unitOfWork;
               _UserId = GetUserIdByLoginname(_LoginName);
               _materialcodeRepository = new MaterialCodeMasterRepository(_unitOfWork);
          }
          public MaterialCodeMasterService()
          {
              _unitOfWork = new UnitOfWork(new TnpaContext());
              _UserId = GetUserIdByLoginname(_LoginName);
              _materialcodeRepository = new MaterialCodeMasterRepository(_unitOfWork);
          }

          /// <summary>
          ///  To get Material Code Details
          /// </summary>
          /// <returns></returns>
          public List<MaterialCodeMasterVO> GetMaterialCodeDetails()
          {
              return ExecuteFaultHandledOperation(() =>
              {
                  return _materialcodeRepository.GetMaterialCodeDetails(_PortCode);
              });
          }



        
    }
}
