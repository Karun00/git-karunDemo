using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625CDetailMapExtension
    {

        public static List<Section625CDetail> MapToEntity(this IEnumerable<Section625CDetailVO> vos)
        {
            List<Section625CDetail> section625cdetailentities = new List<Section625CDetail>();
            foreach (var vo in vos)
            {
                section625cdetailentities.Add(vo.MapToEntity());
            }
            return section625cdetailentities;
        }
        public static List<Section625CDetailVO> MapToDTO(this IEnumerable<Section625CDetail> section625cdetailentities)
        {
            List<Section625CDetailVO> section625cdetailsvos = new List<Section625CDetailVO>();
            foreach (var section625cdetailentity in section625cdetailentities)
            {
                section625cdetailsvos.Add(section625cdetailentity.MapToDTO());
            }
            return section625cdetailsvos;

        }
        public static Section625CDetailVO MapToDTO(this Section625CDetail data)
        {
            Section625CDetailVO Vo = new Section625CDetailVO();
            Vo.Section625CDetailID = data.Section625CDetailID;
            Vo.Section625CID = data.Section625CID;
            Vo.GroupCode = data.GroupCode;
            Vo.DetailCode = data.DetailCode;
            return Vo;
        }

         public static Section625CDetail MapToEntity(this Section625CDetailVO VO)
        {
            Section625CDetail Data = new Section625CDetail();
            Data.Section625CDetailID = VO.Section625CDetailID;
            Data.Section625CID = VO.Section625CID;
            Data.GroupCode = VO.GroupCode;
            Data.DetailCode = VO.DetailCode;
            return Data;
        }

         //public static List<Section625CDetail> MapToEntityrecordingofIncidentdetails(this List<string> selectedrecordingofIncidentdetails, int Section625CID)
         //{


         //    List<Section625CDetail> section625cdetails = new List<Section625CDetail>();
         //    if (selectedrecordingofIncidentdetails != null)
         //    {
         //        foreach (var selectedrecordingofIncident in selectedrecordingofIncidentdetails)
         //        {                  

         //            Section625CDetail section625cdetail = new Section625CDetail();                    
         //            section625cdetail.Section625CID = Section625CID;
         //            section625cdetail.DetailCode = selectedrecordingofIncident;
         //            section625cdetails.Add(section625cdetail);
         //        }
         //    }
         //    return section625cdetails;
         //}



         public static List<string> MapToRecordingofIncidentArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> selectedrecordingofIncidentdetails = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6CRE")
                     {
                         selectedrecordingofIncidentdetails.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return selectedrecordingofIncidentdetails;
         }

         public static List<string> MapTogeneralagenciesArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> selectedgeneralagenciesdetails = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6CGE")
                     {
                         selectedgeneralagenciesdetails.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return selectedgeneralagenciesdetails;
         }

         public static List<string> MapToOccupationalHygieneAgenciesArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> selectedOccupationalHygieneAgencies = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6COC")
                     {
                         selectedOccupationalHygieneAgencies.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return selectedOccupationalHygieneAgencies;
         }

         public static List<string> MapToTypeofContactArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> TypeofContactlist = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6CTC")
                     {
                         TypeofContactlist.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return TypeofContactlist;
         }
        
        public static List<string> MapToStandardActArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> selectedStandardAct = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6CAC")
                     {
                         selectedStandardAct.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return selectedStandardAct;
         }

         public static List<string> MapToSubstandardConditionArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> selectedSubstandardCondition = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6CSC")
                     {
                         selectedSubstandardCondition.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return selectedSubstandardCondition;
         }

         public static List<string> MapToPersonalFactorsArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> selectedPersonalFactors = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6CPF")
                     {
                         selectedPersonalFactors.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return selectedPersonalFactors;
         }

         public static List<string> MapToJobFactorsArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> selectedJobFactors = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6CJF")
                     {
                         selectedJobFactors.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return selectedJobFactors;
         }

         public static List<string> MapToControlStepsToPreventRecurrenceArray(this ICollection<Section625CDetail> section625cdetails)
         {
             List<string> selectedControlStepsToPreventRecurrence = new List<string>();
             if (section625cdetails != null)
             {

                 foreach (var section625cdetail in section625cdetails)
                 {
                     if (section625cdetail.GroupCode == "6CPR")
                     {
                         selectedControlStepsToPreventRecurrence.Add(section625cdetail.DetailCode);
                     }

                 }
             }
             return selectedControlStepsToPreventRecurrence;
         }

       
    }
    }

         
       

