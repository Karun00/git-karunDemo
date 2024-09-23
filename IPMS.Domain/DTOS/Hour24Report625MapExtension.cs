using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Hour24Report625MapExtension
    {
        public static List<Hour24Report625VO> MapToDTO(this IEnumerable<Hour24Report625> Hourreportentities)
        {
            List<Hour24Report625VO> HourreportsVos = new List<Hour24Report625VO>();
            if (Hourreportentities != null)
            {
                foreach (var item in Hourreportentities)
                {
                    HourreportsVos.Add(item.MapToDTO());
                }
            }
            return HourreportsVos;
        }
        public static List<Hour24Report625> MapToEntity(this IEnumerable<Hour24Report625VO> HourreportsVos)
        {
            List<Hour24Report625> Hourreportentities = new List<Hour24Report625>();
            if (HourreportsVos != null)
            {
                foreach (var item in HourreportsVos)
                {
                    Hourreportentities.Add(item.MapToEntity());
                }
            }
            return Hourreportentities;
        }
        public static Hour24Report625VO MapToDTO(this Hour24Report625 data)
        {
            Hour24Report625VO HourreportVo = new Hour24Report625VO();
            if (data != null)
            {
                HourreportVo.Hour24Report625ID = data.Hour24Report625ID;
                HourreportVo.OperatorName = data.OperatorName;
                HourreportVo.LincseNumber = data.LincseNumber;
                HourreportVo.CDName = data.CDName;
                HourreportVo.CDDesignation = data.CDDesignation;
                HourreportVo.CDContactNumber = data.CDContactNumber;
                HourreportVo.CDMobileNumber = data.CDMobileNumber;
                HourreportVo.CDEmailID = data.CDEmailID;
                HourreportVo.Timeperiod = data.Timeperiod;
                HourreportVo.CDAddress = data.CDAddress;
                HourreportVo.NONatureCode = data.NONatureCode;
                HourreportVo.IODOccuranceDateTime = data.IODOccuranceDateTime;
                HourreportVo.IODSpecificLocation = data.IODSpecificLocation;
                HourreportVo.IODOccuranceBriefDescription = data.IODOccuranceBriefDescription;
                HourreportVo.PortCode = data.PortCode;
                if (data.SubCategory != null)
                {
                    HourreportVo.NatureofOccuranceName = data.SubCategory.SubCatName;
                }
                HourreportVo.RecordStatus = data.RecordStatus;
                HourreportVo.CreatedBy = data.CreatedBy;
                HourreportVo.CreatedDate = data.CreatedDate;
                HourreportVo.ModifiedBy = data.ModifiedBy;
                HourreportVo.ModifiedDate = data.ModifiedDate;
                if (data.Section625ABCD != null)
                {
                    HourreportVo.section625abcd = data.Section625ABCD.First().MapToDTO();
                }
                if (data.Section625B != null)
                {
                    if (data.Section625B.Count > 0)
                    {
                        HourreportVo.section625b = data.Section625B.First().MapToDTO();
                    }
                }
                if (data.Section625C != null)
                {
                    if (data.Section625C.Count > 0)
                    {
                        HourreportVo.section625c = data.Section625C.First().MapToDTO();
                    }
                }
                if (data.Section625D != null)
                {
                    if (data.Section625D.Count > 0)
                    {
                        HourreportVo.section625D = data.Section625D.First().MapToDTO();
                    }
                }
                if (data.Section625G != null)
                {
                    if (data.Section625G.Count > 0)
                    {
                        HourreportVo.section625G = data.Section625G.First().MapToDTO();
                    }
                }
                if (data.Section625E != null)
                {
                    if (data.Section625E.Count > 0)
                    {
                        HourreportVo.section625E = data.Section625E.First().MapToDTO();
                    }
                }
                if (data.Section625CDetail.Count > 0)
                {
                    HourreportVo.selectedrecordingofIncidentdetails =
                        data.Section625CDetail.MapToRecordingofIncidentArray();
                    HourreportVo.selectedgeneralagenciesdetails = data.Section625CDetail.MapTogeneralagenciesArray();
                    HourreportVo.selectedTypeofContact = data.Section625CDetail.MapToTypeofContactArray();
                    HourreportVo.selectedOccupationalHygieneAgencies =
                        data.Section625CDetail.MapToOccupationalHygieneAgenciesArray();
                    HourreportVo.selectedStandardAct = data.Section625CDetail.MapToStandardActArray();
                    HourreportVo.selectedSubstandardCondition = data.Section625CDetail.MapToSubstandardConditionArray();
                    HourreportVo.selectedPersonalFactors = data.Section625CDetail.MapToPersonalFactorsArray();
                    HourreportVo.selectedJobFactors = data.Section625CDetail.MapToJobFactorsArray();
                    HourreportVo.selectedControlStepsToPreventRecurrence =
                        data.Section625CDetail.MapToControlStepsToPreventRecurrenceArray();
                    HourreportVo.Section625CDetail = data.Section625CDetail.MapToDTO();
                }
                if (data.Section625DDetail.Count > 0)
                {
                    HourreportVo.Section625DDetail = data.Section625DDetail.MapToDTO();
                    HourreportVo.selectedFireDepartment = data.Section625DDetail.MapToFireDepartmentArray();
                    HourreportVo.selectedIncidentClassification =
                        data.Section625DDetail.MapToIncidentClassificationArray();
                    HourreportVo.selectedDiscriptionofExposedRisk =
                        data.Section625DDetail.MapToDiscriptionofExposedRiskArray();

                }
                if (data.Section625BUnion != null)
                {
                    if (data.Section625BUnion.Count > 0)
                    {
                        HourreportVo.Section625BUnion = data.Section625BUnion.MapToDTO();
                    }
                }
                if (data.Section625CPrevent != null)
                {
                    if (data.Section625CPrevent.Count > 0)
                    {
                        HourreportVo.Section625CPrevent = data.Section625CPrevent.MapToDTO();
                    }
                }
                if (data.Section625CRecommended != null)
                {
                    if (data.Section625CRecommended.Count > 0)
                    {
                        HourreportVo.Section625CRecommended = data.Section625CRecommended.MapToDTO();
                    }
                }
                if (data.Section625EDetail != null)
                {
                    if (data.Section625EDetail.Count > 0)
                    {
                        HourreportVo.Section625EDetail = data.Section625EDetail.MapToDTO();
                    }
                }
                if (data.Section625GDetail1.Count > 0)
                {
                    HourreportVo.selectedRecordingofIncident = data.Section625GDetail1.MapToRecordingofIncidentArray();
                }
                if (data.Section625GDetail2 != null)
                {
                    if (data.Section625GDetail2.Count > 0)
                    {
                        HourreportVo.Section625GDetail2 = data.Section625GDetail2.MapToDTO();
                    }
                }
            }
            return HourreportVo;

        }
        public static Hour24Report625 MapToEntity(this Hour24Report625VO VO)
        {
            Hour24Report625 Hourreportentity = new Hour24Report625();
            if (VO != null)
            {
                Hourreportentity.Hour24Report625ID = VO.Hour24Report625ID;
                Hourreportentity.OperatorName = VO.OperatorName;
                Hourreportentity.LincseNumber = VO.LincseNumber;
                Hourreportentity.CDName = VO.CDName;
                Hourreportentity.CDDesignation = VO.CDDesignation;
                Hourreportentity.CDContactNumber = VO.CDContactNumber;
                Hourreportentity.CDMobileNumber = VO.CDMobileNumber;
                Hourreportentity.CDEmailID = VO.CDEmailID;
                Hourreportentity.Timeperiod = VO.Timeperiod;
                Hourreportentity.CDAddress = VO.CDAddress;
                Hourreportentity.NONatureCode = VO.NONatureCode;
                Hourreportentity.IODOccuranceDateTime = VO.IODOccuranceDateTime;
                Hourreportentity.IODSpecificLocation = VO.IODSpecificLocation;
                Hourreportentity.IODOccuranceBriefDescription = VO.IODOccuranceBriefDescription;
                Hourreportentity.RecordStatus = VO.RecordStatus;
                Hourreportentity.CreatedBy = VO.CreatedBy;
                Hourreportentity.CreatedDate = VO.CreatedDate;
                Hourreportentity.ModifiedBy = VO.ModifiedBy;
                Hourreportentity.ModifiedDate = VO.ModifiedDate;
                Hourreportentity.PortCode = VO.PortCode;
                //Hourreportentity.se = vo.OperationalCargotypes.MapToEntityTerminalOperatorCargoes(vo.TerminalOperatorID);
                if (VO.section625abcd != null)
                {
                    Hourreportentity.Section625ABCD.Add(VO.section625abcd.MapToEntity());
                }

                if (VO.section625b != null)
                {

                    Hourreportentity.Section625B.Add(VO.section625b.MapToEntity());
                }

                if (VO.section625c != null)
                {
                    Hourreportentity.Section625C.Add(VO.section625c.MapToEntity());
                }

                if (VO.section625D != null)
                {
                    Hourreportentity.Section625D.Add(VO.section625D.MapToEntity());
                }

                if (VO.section625G != null)
                {
                    Hourreportentity.Section625G.Add(VO.section625G.MapToEntity());
                }
                if (VO.section625E != null)
                {
                    Hourreportentity.Section625E.Add(VO.section625E.MapToEntity());
                }
                if (VO.Section625BUnion != null)
                {
                    Hourreportentity.Section625BUnion = VO.Section625BUnion.MapToEntity();
                }

                if (VO.Section625DDetail != null)
                {
                    Hourreportentity.Section625DDetail = VO.Section625DDetail.MapToEntity();
                }

                if (VO.Section625CDetail != null)
                {
                    Hourreportentity.Section625CDetail = VO.Section625CDetail.MapToEntity();
                }
                if (VO.Section625CPrevent != null)
                {
                    Hourreportentity.Section625CPrevent = VO.Section625CPrevent.MapToEntity();
                }

                if (VO.Section625CRecommended != null)
                {
                    Hourreportentity.Section625CRecommended = VO.Section625CRecommended.MapToEntity();
                }
            }
            return Hourreportentity;

        }
    
    }
}
