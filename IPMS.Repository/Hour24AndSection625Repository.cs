using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public class Hour24AndSection625Repository : IHour24AndSection625Repository
    {
        private IUnitOfWork _unitOfWork;

        public Hour24AndSection625Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<Hour24Report625> Gethoursreportlist(string portcode)
        {
            var hoursreportlistlist = new List<Hour24Report625>();

            hoursreportlistlist = (from t in _unitOfWork.Repository<Hour24Report625>().Query().Include(t => t.SubCategory).Include(t => t.Section625ABCD).Select()
                                           where t.PortCode == portcode
                                       orderby t.Hour24Report625ID descending
                                       select t).ToList<Hour24Report625>();

                    return hoursreportlistlist;
            
        }
        public Hour24Report625VO Gethoursreportdetailsbyid(string value, int id)
        {

            var hoursreportlist = new Hour24Report625();
            var hoursreportVOlist = new Hour24Report625VO();
           if (value == "625A")
           {
               hoursreportlist = (from H in _unitOfWork.Repository<Hour24Report625>().Query().Include(H => H.Section625ABCD).Select()
                                      where H.Hour24Report625ID == id
                                      select H).FirstOrDefault<Hour24Report625>();

           }
           if (value == "625B")
           {
               hoursreportlist = (from H in _unitOfWork.Repository<Hour24Report625>().Query().Include(H => H.Section625ABCD).Include(H => H.Section625B).Include(I => I.Section625BUnion).Select()
                                      where H.Hour24Report625ID == id
                                      select H).FirstOrDefault<Hour24Report625>();

           }
           if (value == "625C")
           {
               hoursreportlist = (from H in _unitOfWork.Repository<Hour24Report625>().Query().Include(H => H.Section625ABCD).Include(H => H.Section625C).Include(H => H.Section625CDetail).Include(H => H.Section625CPrevent).Include(H => H.Section625CRecommended).Select()
                                      where H.Hour24Report625ID == id
                                      select H).FirstOrDefault<Hour24Report625>();
           }
           if (value == "625D")
           {
               hoursreportlist = (from H in _unitOfWork.Repository<Hour24Report625>().Query().Include(H => H.Section625ABCD).Include(H => H.Section625D).Include(H => H.Section625DDetail).Select()
                                      where H.Hour24Report625ID == id
                                      select H).FirstOrDefault<Hour24Report625>();
           }
           if (value == "625E")
           {
               hoursreportlist = (from H in _unitOfWork.Repository<Hour24Report625>().Query().Include(H => H.Section625ABCD).Include(H => H.Section625E).Include(H => H.Section625EDetail).Select()
                                      where H.Hour24Report625ID == id
                                      select H).FirstOrDefault<Hour24Report625>();
           }
           if (value == "625F")
           {
               hoursreportlist = (from H in _unitOfWork.Repository<Hour24Report625>().Query().Include(H => H.Section625ABCD).Select()
                                  where H.Hour24Report625ID == id
                                  select H).FirstOrDefault<Hour24Report625>();
           }
           if (value == "625G")
           {
               hoursreportlist = (from H in _unitOfWork.Repository<Hour24Report625>().Query().Include(H => H.Section625ABCD).Include(H => H.Section625G).Include(H => H.Section625GDetail1).Include(H => H.Section625GDetail2).Select()
                                      where H.Hour24Report625ID == id
                                      select H).FirstOrDefault<Hour24Report625>();
           }
           hoursreportVOlist = hoursreportlist.MapToDTO();
           return hoursreportVOlist;
        }



        public Hour24Report625 Get24HoursreportDetailsForNotification(string portcode,int id)
        {

            var hoursreportlistlist = (from t in _unitOfWork.Repository<Hour24Report625>().Query().Include(t => t.Port24).Include(t => t.SubCategory).Include(t => t.Section625ABCD).Select()
                                   where t.PortCode == portcode && t.Hour24Report625ID==id
                                   orderby t.Hour24Report625ID descending
                                      select new Hour24Report625
                                      {
                                          CDName = t.CDName,                                 
                                          CDEmailID = t.CDEmailID,
                                          CDMobileNumber = t.CDMobileNumber,
                                          IODOccuranceDateTime = t.IODOccuranceDateTime,
                                          IODSpecificLocation = t.IODSpecificLocation,
                                          CreatedDate = t.CreatedDate,
                                          PortCode = t.Port24.PortCode,
                                          PortName = t.Port24.PortName,
                                          NONatureCode = t.SubCategory.SubCatCode,
                                          NONatureCodeType=t.SubCategory.SubCatName,
                                          IODOccuranceBriefDescription = t.IODOccuranceBriefDescription,
                                          Hour24Report625ID = t.Hour24Report625ID,                                       
                                      }).FirstOrDefault<Hour24Report625>();

            return hoursreportlistlist;

        }

        /// <summary>
        /// To Get Entity Details Based on EntitiyCode
        /// </summary>
        /// <param name="_entityCode"></param>
        /// <returns></returns>
        public Entity GetEntities(string entityCode)
        {
            var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                          where e.EntityCode == entityCode
                          select e).FirstOrDefault<Entity>();
            return entity;
        }

        /// <summary>
        /// To Get User Details by UserId
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CompanyVO Getuserdetails(int userid)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == userid
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }
    
    }
}
