using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class IndividualPermitAppliactionDetailsMapExtension
    {
        public static IndividualPermitApplicationDetails MapToEntity(this IndividualPermitAppliactionDetailsVO VO)
        {
            IndividualPermitApplicationDetails data = new IndividualPermitApplicationDetails();
            data.IndividualApplicationID = VO.IndividualApplicationID;
            data.PermitRequestID = VO.PermitRequestID;
            data.Initial = VO.Initial;
            data.SACitizen = VO.SACitizen;
            data.Gender = VO.Gender;
            data.Suburb = VO.Suburb;
            data.City = VO.City;
            data.PostalCode = VO.PostalCode;
            data.CountryOfOrigin = VO.CountryOfOrigin;
            data.DepartmentManager = VO.DepartmentManager;
            data.JobTitle = VO.JobTitle;
            data.Current_Permit_Exists = VO.Current_Permit_Exists;
            data.Reason_Reapplication = VO.Reason_Reapplication;
            data.Port_Induction_Training = VO.Port_Induction_Training;
            data.Training_Date = VO.Training_Date;
            data.Criminal_Bckground = VO.Criminal_Bckground;
            data.Signature = VO.Signature;
            data.Date = VO.Date;
            data.EmployeeNo = VO.EmployeeNo;
            data.EmailAddress = VO.EmailAddress;
            return data;
        }
       
        public static IndividualPermitAppliactionDetailsVO MapToDTO(this IndividualPermitApplicationDetails data)
        {
            IndividualPermitAppliactionDetailsVO VO = new IndividualPermitAppliactionDetailsVO();
            if (data != null)
            {
                data.IndividualApplicationID = data.IndividualApplicationID;
                data.PermitRequestID = data.PermitRequestID;
                data.Initial = VO.Initial;
                data.SACitizen = VO.SACitizen;
                data.Gender = VO.Gender;
                data.Suburb = VO.Suburb;
                data.City = VO.City;
                data.PostalCode = VO.PostalCode;
                data.CountryOfOrigin = VO.CountryOfOrigin;
                data.DepartmentManager = VO.DepartmentManager;
                data.JobTitle = VO.JobTitle;
                data.Current_Permit_Exists = VO.Current_Permit_Exists;
                data.Reason_Reapplication = VO.Reason_Reapplication;
                data.Port_Induction_Training = VO.Port_Induction_Training;
                data.Training_Date = VO.Training_Date;
                data.Criminal_Bckground = VO.Criminal_Bckground;
                data.Signature = VO.Signature;
                data.Date = VO.Date;
                data.EmployeeNo = VO.EmployeeNo;
            }
            return VO;
        }
        public static IndividualPermitAppliactionDetailsVO MapToDTOObj(this IEnumerable<IndividualPermitApplicationDetails> IndividualPermit)
        {
            var IndividualPermitAppliactionDetailsVOList = new IndividualPermitAppliactionDetailsVO();
            if (IndividualPermit != null)
            {
                foreach (var data in IndividualPermit)
                {
                    IndividualPermitAppliactionDetailsVOList.IndividualApplicationID = data.IndividualApplicationID;
                    IndividualPermitAppliactionDetailsVOList.PermitRequestID = data.PermitRequestID;
                    IndividualPermitAppliactionDetailsVOList.Initial = data.Initial;
                    IndividualPermitAppliactionDetailsVOList.SACitizen = data.SACitizen;
                    IndividualPermitAppliactionDetailsVOList.Gender = data.Gender;
                    IndividualPermitAppliactionDetailsVOList.EmailAddress = data.EmailAddress;
                    //if (data.SACitizen != null)
                    //{
                    //    if (data.SACitizen == "Y")
                    //    {
                    //        IndividualPermitAppliactionDetailsVOList.SACitizen = "True";
                    //    }                        
                    //}
                    //if (data.Gender != null)
                    //{
                    //    if (data.Gender == "Y")
                    //    {
                    //        IndividualPermitAppliactionDetailsVOList.Gender = "True";
                    //    }
                    //    //else if (data.AllNPASites == "N") { PersonalPermitVOList.AllNPASites = "False"; }
                    //}
                    IndividualPermitAppliactionDetailsVOList.Suburb = data.Suburb;
                    IndividualPermitAppliactionDetailsVOList.City = data.City;
                    IndividualPermitAppliactionDetailsVOList.PostalCode = data.PostalCode;
                    IndividualPermitAppliactionDetailsVOList.CountryOfOrigin = data.CountryOfOrigin;
                    IndividualPermitAppliactionDetailsVOList.DepartmentManager = data.DepartmentManager;
                    IndividualPermitAppliactionDetailsVOList.JobTitle = data.JobTitle;
                    IndividualPermitAppliactionDetailsVOList.Current_Permit_Exists = data.Current_Permit_Exists;

                    //if (data.Current_Permit_Exists != null)
                    //{
                    //    if (data.Current_Permit_Exists == "Y")
                    //    {
                    //        IndividualPermitAppliactionDetailsVOList.Current_Permit_Exists = "True";
                    //    }
                    //    //else if (data.AllPorts == "N") { PersonalPermitVOList.AllPorts = "False"; }
                    //}
                    IndividualPermitAppliactionDetailsVOList.Reason_Reapplication = data.Reason_Reapplication;
                    IndividualPermitAppliactionDetailsVOList.Port_Induction_Training = data.Port_Induction_Training;
                    //if (data.Port_Induction_Training != null)
                    //{
                    //    if (data.Port_Induction_Training == "Y")
                    //    {
                    //        IndividualPermitAppliactionDetailsVOList.Port_Induction_Training = "True";
                    //    }
                    //    //else if (data.AllPorts == "N") { PersonalPermitVOList.AllPorts = "False"; }
                    //}
                    IndividualPermitAppliactionDetailsVOList.Training_Date = data.Training_Date;
                    IndividualPermitAppliactionDetailsVOList.Criminal_Bckground = data.Criminal_Bckground;
                    //if (data.Criminal_Bckground != null)
                    //{
                    //    if (data.Criminal_Bckground == "Y")
                    //    {
                    //        IndividualPermitAppliactionDetailsVOList.Criminal_Bckground = "True";
                    //    }
                    //    //else if (data.AllPorts == "N") { PersonalPermitVOList.AllPorts = "False"; }
                    //}
                    IndividualPermitAppliactionDetailsVOList.Signature = data.Signature;
                    IndividualPermitAppliactionDetailsVOList.Date = data.Date;
                    IndividualPermitAppliactionDetailsVOList.EmployeeNo = data.EmployeeNo;
                }
            }
            return IndividualPermitAppliactionDetailsVOList;
        }
        //public static PermitRequestArea MapToEntity(this PermitRequestAreaVO VO)
        //{
        //    PermitRequestArea data = new PermitRequestArea();
        //    if (VO != null)
        //    {
        //        data.PermitRequestAreaID = VO.PermitRequestAreaID;
        //        data.PermitRequestID = VO.PermitRequestID;
        //        data.PermitRequestAreaCode = VO.PermitRequestAreaCode;
        //    }
        //    return data;
        //}

        //public static List<string> MapToPermitRequestAreaArray(this ICollection<PermitRequestArea> permitrequestareas)
        //{
        //    List<string> PermitRequestAreas = new List<string>();
        //    if (permitrequestareas != null)
        //    {
        //        foreach (var permitrequestarea in permitrequestareas)
        //        {
        //            PermitRequestAreas.Add(permitrequestarea.PermitRequestAreaCode);
        //        }
        //    }
        //    return PermitRequestAreas;
        //}

        ///// <summary>
        ///// Data List Transfer from Array to TerminalOperatorCargoHandling List 
        ///// </summary>
        ///// <param name="CargoKeys"></param>
        ///// <param name="terminalOperatorId"></param>
        ///// <returns></returns>
        //public static List<PermitRequestArea> MapToEntityPermitRequestArea(this List<string> PermitRequestAreas, int PermitRequestID)
        //{


        //    List<PermitRequestArea> PermitRequestAreaslist = new List<PermitRequestArea>();
        //    if (PermitRequestAreas != null)
        //    {
        //        foreach (var PermitRequestArea in PermitRequestAreas)
        //        {
        //            PermitRequestArea permitrequestarea = new PermitRequestArea();
        //            permitrequestarea.PermitRequestID = PermitRequestID;
        //            permitrequestarea.PermitRequestAreaCode = PermitRequestArea.ToString();

        //            PermitRequestAreaslist.Add(permitrequestarea);
        //        }
        //    }
        //    return PermitRequestAreaslist;
        //    //return berthKeyArray;
        //}


    }
}
