using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PersonalPermitMapExtension
    {
        public static List<PersonalPermit> MapToEntity(this IEnumerable<PersonalPermitVO> vos)
        {
            List<PersonalPermit> entities = new List<PersonalPermit>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<PersonalPermitVO> MapToDTO(this IEnumerable<PersonalPermit> entities)
        {
            List<PersonalPermitVO> vos = new List<PersonalPermitVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static PersonalPermitVO MapToDTO(this  PersonalPermit data)
        {
            PersonalPermitVO Vo = new PersonalPermitVO();
            if (data != null)
            {
                Vo.PersonalPermitID = data.PersonalPermitID;
                Vo.PermitRequestID = data.PermitRequestID;
                Vo.PermitCategoryCode = data.PermitCategoryCode;
                if (data.AllNPASites != null)
                {
                    if (data.AllNPASites == "Y")
                    {
                        Vo.AllNPASites = "True";
                    }
                    //else if (data.AllNPASites == "N")
                    //{
                    //    Vo.AllNPASites = "False";
                    //}
                }
                if (data.SpecificNPASites != null)
                {
                    if (data.SpecificNPASites == "Y")
                    {
                        Vo.SpecificNPASites = "True";
                    }
                    //else if (data.SpecificNPASites == "N")
                    //{
                    //    Vo.SpecificNPASites = "False";
                    //}
                }

                Vo.SpecifyArea = data.SpecifyArea;
                Vo.LeaseholdSite = data.LeaseholdSite;
                Vo.PhysicalAddress = data.PhysicalAddress;
                Vo.AdhocPermits = data.AdhocPermits;
                Vo.TemporaryPermits = data.TemporaryPermits;
                if (data.AllPorts != null)
                {
                    if (data.AllPorts == "Y")
                    {
                        Vo.AllPorts = "True";
                    }
                    //else if (data.AllPorts == "N")
                    //{
                    //    Vo.AllPorts = "False";
                    //}
                }

                Vo.ConstructionArea = data.ConstructionArea;
                Vo.PermanentPermits = data.PermanentPermits;
                Vo.Reason = data.Reason;
                Vo.RecordStatus = data.RecordStatus;
                Vo.CreatedBy = data.CreatedBy;
                Vo.CreatedDate = data.CreatedDate;
                Vo.ModifiedBy = data.ModifiedBy;
                Vo.ModifiedDate = data.ModifiedDate;
                Vo.permittype = data.permittype;
               

            }
            
            return Vo;
        }
        public static PersonalPermit MapToEntity(this PersonalPermitVO VO)
        {
            PersonalPermit data = new PersonalPermit();
            if (VO != null)
            {
                data.PersonalPermitID = VO.PersonalPermitID;
                data.PermitRequestID = VO.PermitRequestID;
                data.PermitCategoryCode = VO.PermitCategoryCode;
                data.SpecificNPASites = VO.SpecificNPASites;
                if (VO.SpecificNPASites != null)
                {
                    if (VO.SpecificNPASites == "T")
                    {
                        data.SpecificNPASites = "Y";
                    }
                    else if (VO.SpecificNPASites == "F")
                    {
                        data.SpecificNPASites = "N";
                    }
                }
                if (VO.AllNPASites != null)
                {
                    if (VO.AllNPASites == "T")
                    {
                        data.AllNPASites = "Y";
                    }
                    else if (VO.AllNPASites == "F")
                    {
                        data.AllNPASites = "N";
                    }
                }
                data.SpecifyArea = VO.SpecifyArea;
                data.LeaseholdSite = VO.LeaseholdSite;
                data.PhysicalAddress = VO.PhysicalAddress;
                data.AdhocPermits = VO.AdhocPermits;
                data.TemporaryPermits = VO.TemporaryPermits;

                if (VO.AllPorts != null)
                {
                    if (VO.AllPorts == "T")
                    {
                        data.AllPorts = "Y";
                    }
                    else if (VO.AllPorts == "F")
                    {
                        data.AllPorts = "N";
                    }
                }
                data.permittype = VO.permittype;
                data.ConstructionArea = VO.ConstructionArea;
                data.PermanentPermits = VO.PermanentPermits;
                data.Reason = VO.Reason;
                data.RecordStatus = VO.RecordStatus;
                data.CreatedBy = VO.CreatedBy;
                data.CreatedDate = VO.CreatedDate;
                data.ModifiedBy = VO.ModifiedBy;
                data.ModifiedDate = VO.ModifiedDate;
              
            }
            return data;
        }

        public static PersonalPermitVO MapToDTOObj(this IEnumerable<PersonalPermit> PermitRequestContractor)
        {
            var PersonalPermitVOList = new PersonalPermitVO();
            if (PermitRequestContractor != null)
            {
                foreach (var data in PermitRequestContractor)
                {
                    PersonalPermitVOList.PersonalPermitID = data.PersonalPermitID;
                    PersonalPermitVOList.PermitRequestID = data.PermitRequestID;
                    PersonalPermitVOList.PermitCategoryCode = data.PermitCategoryCode;
                    if (data.SpecificNPASites != null)
                    {
                        if (data.SpecificNPASites == "Y")
                        {
                            PersonalPermitVOList.SpecificNPASites = "True";
                        }
                        //else if (data.SpecificNPASites == "N") { PersonalPermitVOList.SpecificNPASites = "False"; }
                    }
                    if (data.AllNPASites != null)
                    {
                        if (data.AllNPASites == "Y")
                        {
                            PersonalPermitVOList.AllNPASites = "True";
                        }
                        //else if (data.AllNPASites == "N") { PersonalPermitVOList.AllNPASites = "False"; }
                    }
                    PersonalPermitVOList.SpecifyArea = data.SpecifyArea;
                    PersonalPermitVOList.LeaseholdSite = data.LeaseholdSite;
                    PersonalPermitVOList.PhysicalAddress = data.PhysicalAddress;
                    PersonalPermitVOList.AdhocPermits = data.AdhocPermits;
                    PersonalPermitVOList.TemporaryPermits = data.TemporaryPermits;
                    if (data.AllPorts != null)
                    {
                        if (data.AllPorts == "Y")
                        {
                            PersonalPermitVOList.AllPorts = "True";
                        }
                        //else if (data.AllPorts == "N") { PersonalPermitVOList.AllPorts = "False"; }
                    }
                    PersonalPermitVOList.permittype = data.permittype;
                    PersonalPermitVOList.ConstructionArea = data.ConstructionArea;
                    PersonalPermitVOList.PermanentPermits = data.PermanentPermits;
                    PersonalPermitVOList.Reason = data.Reason;
                    PersonalPermitVOList.RecordStatus = data.RecordStatus;
                    PersonalPermitVOList.CreatedBy = data.CreatedBy;
                    PersonalPermitVOList.CreatedDate = data.CreatedDate;
                    PersonalPermitVOList.ModifiedBy = data.ModifiedBy;
                    PersonalPermitVOList.ModifiedDate = data.ModifiedDate;
                 
                }
            }
            return PersonalPermitVOList;
        }
    
    
    }
}
