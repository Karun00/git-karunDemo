using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain;
using System.Globalization;

namespace IPMS.Services.Business
{
    public class CodeGenerator
    {
        private IUnitOfWork _unitOfWork;
        public CodeGenerator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GenerateVCN(string portCode)
        {
            return GenerateCodeforVCN("VCN", portCode);
        }
        public string ReferenceNo(string portCode)
        {
            return GenerateReferenceNumber("VCN", portCode);
        }

        public string GenerateDraft(string portCode)
        {
            return GenerateCodeDraft("DRFT", portCode);
        }
        
        public string GenerateLicenseefno(string codeName, string portCode)
        {

            int year = DateTime.Now.Year;
            string pchars = "00000";
            StringBuilder LicRefno = new StringBuilder();
            var result = (from u in _unitOfWork.Repository<Code>().Query().Select()
                          where u.PortCode == portCode && u.CodeYear == year && u.RecordStatus == "A" && u.CodeName == codeName
                          select u).FirstOrDefault<Code>();

            LicRefno.Append(result.CodeName.ToString() + result.CodeYear.ToString(CultureInfo.InvariantCulture) + (pchars.Remove(pchars.Length - result.CurValue.ToString(CultureInfo.InvariantCulture).Length)) + result.CurValue);

            //var brt = 
                _unitOfWork.ExecuteSqlCommand(" update dbo.Code SET CurValue = ( select CurValue+1 from dbo.Code " +
                          " where PortCode = @p0 and  CodeYear = @p1 and RecordStatus = @p2 and CodeName = @p3 ) " +
                      " WHERE  PortCode = @p0 and  CodeYear = @p1 and RecordStatus = @p2 and CodeName = @p3", portCode, year, "A", codeName);

            //            var brt = _unitOfWork.ExecuteSqlCommand(" delete dbo.LicenseRequestPort where LicenseRequestID = @p0", licenseRequest.LicenseRequestID);

            return LicRefno.ToString();
        }

        public string GenerateServiceRequestCode(string portCode)
        {
            return GenerateCode(EntityCodes.Service_Request, portCode);
        }
        public string GenerateCodeDraft(string codeName, string portCode)
        {
            int year = DateTime.Now.Year;
            string pchars = "0000";
            StringBuilder VCNNO = new StringBuilder();
            var result = (from u in _unitOfWork.Repository<Code>().Query().Select()
                          where u.PortCode == portCode && u.RecordStatus == "A" && u.CodeName == codeName
                          select u).FirstOrDefault<Code>();

            VCNNO.Append(result.CodeName.ToString(CultureInfo.InvariantCulture) + result.PortCode + result.CodeYear.ToString(CultureInfo.InvariantCulture) + (pchars.Remove(pchars.Length - result.CurValue.ToString(CultureInfo.InvariantCulture).Length)) + result.CurValue);

            //TODO:: Write code here to increment current value.
            //DONE : Incremented the value after successfull insertion by calling UpdateCode

            return VCNNO.ToString();
        }

        public string GenerateCode(string codeName, string portCode)
        {
            int year = DateTime.Now.Year;
            string pchars = "000";
            StringBuilder VCNNO = new StringBuilder();
            var result = (from u in _unitOfWork.Repository<Code>().Query().Select()
                          where u.PortCode == portCode && u.CodeYear == year && u.RecordStatus == "A" && u.CodeName == codeName
                          select u).FirstOrDefault<Code>();

            VCNNO.Append(result.CodeName.ToString(CultureInfo.InvariantCulture) + result.PortCode + result.CodeYear.ToString(CultureInfo.InvariantCulture) + (pchars.Remove(pchars.Length - result.CurValue.ToString(CultureInfo.InvariantCulture).Length)) + result.CurValue);

            //TODO:: Write code here to increment current value.
            //DONE : Incremented the value after successfull insertion by calling UpdateCode

            return VCNNO.ToString();
        }

        public string GenerateCodeforVCN(string codeName, string portCode)
        {
            int year = DateTime.Now.Year;
            string pchars = "00000";
            StringBuilder VCNNO = new StringBuilder();
            var result = (from u in _unitOfWork.Repository<Code>().Query().Select()
                          where u.PortCode == portCode && u.RecordStatus == "A" && u.CodeName == codeName
                          select u).FirstOrDefault<Code>();
            VCNNO.Append(result.CodeName.ToString() + result.PortCode + result.CodeYear.ToString(CultureInfo.InvariantCulture) + (pchars.Remove(pchars.Length - result.CurValue.ToString(CultureInfo.InvariantCulture).Length)) + result.CurValue);
            return VCNNO.ToString();
        }

        public string GenerateReferenceNumber(string codeName, string portCode)
        {
            int year = DateTime.Now.Year;
            string pchars = "00000";
            StringBuilder VCNNO = new StringBuilder();
            var result = (from u in _unitOfWork.Repository<Code>().Query().Select()
                          where u.PortCode == portCode && u.RecordStatus == "A" && u.CodeName == codeName
                          select u).FirstOrDefault<Code>();
            VCNNO.Append(result.CodeName.ToString() + result.PortCode + result.CodeYear.ToString(CultureInfo.InvariantCulture) + (pchars.Remove(pchars.Length - result.CurValue.ToString(CultureInfo.InvariantCulture).Length)) + result.CurValue);
            return VCNNO.ToString();
        }
        public int UpdateCode(string codeName, string portCode)
        {
            return _unitOfWork.ExecuteSqlCommand("update Code  set CurValue = CurValue+1 where CodeName = @p0 and PortCode=@p1", codeName, portCode);
        }

        //updated by prasad
        public string GenerateRefNum()
        {

            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int Day = DateTime.Now.Day;
            //string code = "AN";
            // string pchars = "00000";
            StringBuilder RefNO = new StringBuilder();

            int count = (from a in _unitOfWork.Repository<Agent>().Query().Select()
                         //where  a.RecordStatus == "A"
                         select a).Count();

            count = count + 1;

            string stMonth = Month.ToString(CultureInfo.InvariantCulture);
            string stDay = Day.ToString(CultureInfo.InvariantCulture);

            if (Month <= 9)
                stMonth = "0" + Month.ToString(CultureInfo.InvariantCulture);
            if (Day <= 9)
                stDay = "0" + Day.ToString(CultureInfo.InvariantCulture);

            //select @count =max(convert(numeric(5),substring(convert(varchar(7),cust_id),3,len(cust_id)))) from dbo.mst_users where substring(convert(varchar(7),cust_id),0,3) = substring(@year,3,4)


            //int randomNumber = random.Next(0,10000000);

            RefNO.Append(Year.ToString(CultureInfo.InvariantCulture) + stMonth + stDay + count.ToString(CultureInfo.InvariantCulture));


            return RefNO.ToString();
        }


        //updated by venkataramana
        public string GenerateReferenceNumber()
        {

            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int Day = DateTime.Now.Day;
            //string code = "AN";
            // string pchars = "00000";
            StringBuilder RefNO = new StringBuilder();

            int count = (from a in _unitOfWork.Repository<LicenseRequest>().Query().Select()
                         //where  a.RecordStatus == "A"
                         select a).Count();

            count = count + 1;

            string stMonth = Month.ToString(CultureInfo.InvariantCulture);
            string stDay = Day.ToString(CultureInfo.InvariantCulture);

            if (Month <= 9)
                stMonth = "0" + Month.ToString(CultureInfo.InvariantCulture);
            if (Day <= 9)
                stDay = "0" + Day.ToString(CultureInfo.InvariantCulture);

            //select @count =max(convert(numeric(5),substring(convert(varchar(7),cust_id),3,len(cust_id)))) from dbo.mst_users where substring(convert(varchar(7),cust_id),0,3) = substring(@year,3,4)


            //int randomNumber = random.Next(0,10000000);

            RefNO.Append(Year.ToString(CultureInfo.InvariantCulture) + stMonth + stDay + count.ToString(CultureInfo.InvariantCulture));


            return RefNO.ToString();
        }
    
    
    
    }
}
