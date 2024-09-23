using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IHour24AndSection625Repository
    {
        List<Hour24Report625> Gethoursreportlist(string portcode);
        Hour24Report625VO Gethoursreportdetailsbyid(string value, int id);
        Entity GetEntities(string entityCode);
        CompanyVO Getuserdetails(int userid);
        Hour24Report625 Get24HoursreportDetailsForNotification(string portcode, int id);
    }
}
