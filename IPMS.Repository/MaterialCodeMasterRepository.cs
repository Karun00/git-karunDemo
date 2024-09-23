using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public class MaterialCodeMasterRepository : IMaterialCodeMasterRepository
    {
        private IUnitOfWork _unitOfWork;

        public MaterialCodeMasterRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  To get Material Code Details
        /// </summary>
        /// <returns></returns>
        public List<MaterialCodeMasterVO> GetMaterialCodeDetails(string portCode)
        {     

            var agentlist = (from m in _unitOfWork.Repository<MaterialCodeMaster>().Query().Select()
                             join mp in _unitOfWork.Repository<MaterialCodePort>().Query().Select()
                             on m.MaterialCodeMasterid equals mp.MaterialCodeMasterid
                             where mp.PortCode == portCode && m.RecordStatus == RecordStatus.Active
                             orderby m.MaterialCode ascending
                             select new MaterialCodeMasterVO
                             {
                                 GroupCode = m.GroupCode,
                                 MaterialCode = m.MaterialCode,
                                 Remarks = m.Remarks                                 
                             }).GroupBy(i => i.MaterialCode).Select(l => l.Last()).ToList();

            return agentlist;


        }
    }
}
