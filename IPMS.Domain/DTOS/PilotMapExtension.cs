using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System.Globalization;
namespace IPMS.Domain.DTOS
{
    public static class PilotMapExtension
    {
        public static PioltVO MapToDTO(this Pilot data)
        {
            PioltVO pioltvo = new PioltVO();

            pioltvo.PilotID = data.PilotID;
            pioltvo.FirstName = data.FirstName;
            pioltvo.PortCode = data.PortCode;
            pioltvo.Surname = data.Surname;
            pioltvo.LastName = data.LastName;
            pioltvo.DateofBirth = data.DateofBirth.ToString();
            pioltvo.IDNo = data.IDNo;
            pioltvo.NationalityCode = data.NationalityCode;

            //if (data.IssuedDate != null)
            //{
            //    int month = (data.IssuedDate).Month;

            //    if (month <= 3)
            //    {
            //        int year = (data.IssuedDate).Year;
            //        DateTime lastDay = new DateTime(year, 3, 31);
            //        pioltvo.ExpiryDate = lastDay;
            //    }
            //    else if (month > 3)
            //    {
            //        int year = (data.IssuedDate.AddYears(1)).Year;
            //        DateTime lastDay = new DateTime(year, 3, 31);
            //        pioltvo.ExpiryDate = lastDay;
            //    }
            //}

            pioltvo.IssuedDate = data.IssuedDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            pioltvo.RenewalDate = data.RenewalDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            pioltvo.IssuingAuthority = data.IssuingAuthority;
            pioltvo.InvoiceRecipient = data.InvoiceRecipient;
            pioltvo.LicenseRecipient = data.LicenseRecipient;
            pioltvo.PostalAddressID = data.PostalAddressID;
            pioltvo.ResidentialAddressID = data.ResidentialAddressID;
            pioltvo.ContactNo = data.ContactNo;
            pioltvo.CellNo = data.CellNo;
            pioltvo.EmailID = data.EmailID;
            pioltvo.WorkflowInstanceId = data.WorkflowInstanceId;
            pioltvo.RecordStatus = data.RecordStatus;
            pioltvo.CreatedBy = data.CreatedBy;
            pioltvo.CreatedDate = data.CreatedDate;
            pioltvo.ModifiedBy = data.ModifiedBy;
            pioltvo.ModifiedDate = data.ModifiedDate;
            pioltvo.IssuedApprovedDate = data.IssuedApprovedDate;
            pioltvo.FullName = data.FirstName + " " + data.LastName + " " + data.Surname;
            pioltvo.ExpiryDate = data.ExpiryDate;
            pioltvo.Certificate_of_Competency = data.Certificate_of_Competency;


            if (data.WorkflowInstance != null)
            {
                pioltvo.Workflowinstance = data.WorkflowInstance.MapToDTO();
            }
            pioltvo.ResidentialAddress = data.ResidentialAddress.MapToDTO();
            pioltvo.PostalAddress = data.PostalAddress.MapToDTO();
            pioltvo.AddressCheckbox = CheckUncheck(pioltvo.ResidentialAddress, pioltvo.PostalAddress);
            pioltvo.PilotExemptionRequest = data.PilotExemptionRequests.ToList().MapToDTO();
            pioltvo.PilotExemptionRequestdocument = data.PilotExemptionRequestDocuments.ToList().MapToDTO();
            return pioltvo;
        }

        public static Pilot MapToEntity(this PioltVO data)
        {
            Pilot Pilot = new Pilot();
           
            Pilot.PilotID = data.PilotID;
            Pilot.FirstName = data.FirstName;
            Pilot.PortCode = data.PortCode;
            Pilot.Surname = data.Surname;
            Pilot.LastName = data.LastName;
            Pilot.DateofBirth = Convert.ToDateTime(data.DateofBirth, CultureInfo.InvariantCulture);
            Pilot.IDNo = data.IDNo;
            Pilot.NationalityCode = data.NationalityCode;
            Pilot.IssuedDate = DateTime.Parse(data.IssuedDate, CultureInfo.InvariantCulture);
            Pilot.RenewalDate = DateTime.Parse(data.RenewalDate, CultureInfo.InvariantCulture);
            Pilot.IssuingAuthority = data.IssuingAuthority;
            Pilot.InvoiceRecipient = data.InvoiceRecipient;
            Pilot.LicenseRecipient = data.LicenseRecipient;
            Pilot.PostalAddressID = data.PostalAddressID;
            Pilot.ResidentialAddressID = data.ResidentialAddressID;
            Pilot.ContactNo = data.ContactNo;
            Pilot.CellNo = data.CellNo;
            Pilot.EmailID = data.EmailID;
            Pilot.WorkflowInstanceId = data.WorkflowInstanceId;
            Pilot.RecordStatus = data.RecordStatus;
            Pilot.CreatedBy = data.CreatedBy;
            Pilot.CreatedDate = data.CreatedDate;
            Pilot.ModifiedBy = data.ModifiedBy;
            Pilot.ModifiedDate = data.ModifiedDate;
            Pilot.Certificate_of_Competency = data.Certificate_of_Competency;
            Pilot.IssuedApprovedDate = data.IssuedApprovedDate;
            Pilot.FullName = data.FirstName + " " + data.LastName + " " + data.Surname;
            Pilot.ExpiryDate = data.ExpiryDate;
            Pilot.IssueDate = data.IssueDate;
            Pilot.RenewDate = data.RenewDate;

           // data.IssuedDate.ToString
         //   Pilot.IssueDate = DateTime.ParseExact(data.IssuedDate, "yyyy/MM/dd", CultureInfo.CurrentCulture).ToString();
           // Pilot.IssueDate = data.IssuedDate.ToString("yyyy-MM-dd");
                //Convert.ToDateTime(data.IssuedDate.ToString("yyyy/MM/dd"));
                //data.IssuedDate.ToString("yyyy/MM/dd");

            if (data.Workflowinstance != null)
            {
                Pilot.WorkflowInstance = data.Workflowinstance.MapToEntity();
            }
            Pilot.ResidentialAddress = data.ResidentialAddress.MapToEntity();
            Pilot.PilotExemptionRequestDocuments = data.PilotExemptionRequestdocument.ToList().MapToEntity();
            Pilot.PostalAddress = data.PostalAddress.MapToEntity();
            Pilot.PilotExemptionRequests = data.PilotExemptionRequest.MapToEntity();
            return Pilot;
        }

        public static List<PioltVO> MapToListDTO(this IEnumerable<Pilot> pilotList)
        {
            List<PioltVO> pilotvoList = new List<PioltVO>();
            foreach (var data in pilotList)
            {
                pilotvoList.Add(data.MapToDTO());
            }
            return pilotvoList;
        }

        public static List<Pilot> MapToListEntity(this IEnumerable<PioltVO> pilotvoList)
        {
            List<Pilot> pilotList = new List<Pilot>();
            foreach (var data in pilotvoList)
            {
                pilotList.Add(data.MapToEntity());
            }
            return pilotList;
        }
   
    
     public static Boolean CheckUncheck(AddressVO ResidentialAddress, AddressVO PostalAddress)
        {
         int check=4;
         Boolean AddressCheckbox = false;
         if((ResidentialAddress.NumberStreet.ToString()).Trim()==(PostalAddress.NumberStreet.ToString()).Trim())
         {check=check-1;}
       if((ResidentialAddress.Suburb.ToString()).Trim()==(PostalAddress.Suburb.ToString()).Trim())
           {check=check-1;}
             if((ResidentialAddress.TownCity.ToString()).Trim()==(PostalAddress.TownCity.ToString()).Trim())
           {check=check-1;}
              if((ResidentialAddress.PostalCode.ToString()).Trim()==(PostalAddress.PostalCode.ToString()).Trim())
           {check=check-1;}
         if(check<=0)
         {AddressCheckbox=true; }
         else{AddressCheckbox=false;}

         return AddressCheckbox;
        }
    }
}
