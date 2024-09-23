using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;

namespace IPMS.Repository
{
    public class PortRepository : IPortRepository
    {
        private IUnitOfWork _unitOfWork;

        public PortRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetLoginPort details
        public List<PortVO> GetLoginPort(string loginPort)
        {
            var ports = (from portvo in _unitOfWork.Repository<Port>().Queryable()
                         where portvo.PortCode == loginPort
                         //  orderby portvo.PortName ascending
                         select new PortVO
                         {
                             PortCode = portvo.PortCode,
                             PortName = portvo.PortName,
                             InternationalCharacter = portvo.InternationalCharacter,
                             GeographicLocation = portvo.GeographicLocation,
                             ContactNo = portvo.ContactNo,
                             Email = portvo.Email,
                             Fax = portvo.Fax,
                             Website = portvo.Website,
                             Description = portvo.Description,
                             RecordStatus = portvo.RecordStatus,
                         });
            return ports.ToList();
        }
        #endregion


        #region GetPortzs
        public List<PortVO> GetPortzs()
        {
            var ports = (from portvo in _unitOfWork.Repository<Port>().Queryable()
                         //orderby portvo.CreatedDate descending
                         //Chandrima Das
                         where portvo.RecordStatus == "A"
                         select new PortVO
                         {
                             PortCode = portvo.PortCode,
                             PortName = portvo.PortName                            
                         });
            return ports.OrderBy(x=>x.PortName).ToList();
        }
        #endregion


    //    public List<PlannedMovementsDtlsVO> GetPlannedMovementsCount(int UserID, string fromDate, string toDate, string PortCode)
    //    {
    //        var userid = new SqlParameter("@userid", UserID);
    //        var fromdate = new SqlParameter("@fromdate", fromDate);
    //        var todate = new SqlParameter("@todate", toDate);
    //        var portcode = new SqlParameter("@portcode", PortCode);

    //        var pt1 = _unitOfWork.SqlQuery<PlannedMovementsDtlsVO>("Select * from  dbo.udf_GetDashboardDetails(@userid,@fromdate,@todate,@portcode)", userid, fromdate, todate, portcode).ToList();

    //        pt1.FirstOrDefault().fromDate = Convert.ToDateTime(fromDate, CultureInfo.InvariantCulture);
    //        pt1.FirstOrDefault().toDate = Convert.ToDateTime(toDate, CultureInfo.InvariantCulture);

    //        return pt1;
    

    //     //var ports = (from p in _unitOfWork.Repository<VESSELCALLMOVEMENT>().Query().Select()
    //     //                join up in _unitOfWork.Repository<Arrivalnotification>().Query().Select() on up.vesselid  = p.Vesselid
    //     //                join s in _unitOfWork.Repository<Vessel>().Query().Select() on 
    //     //                join t in _unitOfWork.Repository<Servicerequest>
    //     //                on p.PortCode equals up.PortCode
    //     //                where up.UserID == userId
    //     //                orderby p.PortName
    //     //                select new PortVO
    //     //                {
    //     //                    PortCode = p.PortCode,
    //     //                    PortName = p.PortName,
    //     //                    InternationalCharacter = p.InternationalCharacter,
    //     //                    GeographicLocation = p.GeographicLocation,
    //     //                    ContactNo = p.ContactNo,
    //     //                    Email = p.Email,
    //     //                    Fax = p.Fax,
    //     //                    Website = p.Website,
    //     //                    Description = p.Description,
    //     //                    RecordStatus = p.RecordStatus,
    //     //                });

    //     //   return ports.ToList();
    ////        var query = (from v in _unitOfWork.Repository<VESSELCALLMOVEMENT>() .Query().Select()
    ////join a in _unitOfWork.Repository<Arrivalnotification>().Query().Select()
    ////on v.vcn equals a.vcn where v.v.MovementType == "ARMV" select v).count();
    ////select v).Count(v.MovementType in ('ARMV'));


    //        //var a=(from c in tbl1 join a in tbl2 on c.col=a.col where c.col.equals("ARMV")).count();
    //    }


//        select count(v.MovementType)
//from VESSELCALLMOVEMENT  V
//inner join Arrivalnotification A on A.vcn = v.vcn
////inner join Vessel VE on A.vesselid  = VE.Vesselid 
//where 
//v.MovementType in ('ARMV')





       

        #region GetPorts
        public List<PortVO> GetPorts()
        {
            var ports = (from portvo in _unitOfWork.Repository<Port>().Queryable()
                         orderby portvo.CreatedDate descending
                         ///Chandrima Das
                         //where portvo.RecordStatus == "A"
                         //orderby portvo.PortName ascending
                         select new PortVO
                         {
                             PortCode = portvo.PortCode,
                             PortName = portvo.PortName,
                             InternationalCharacter = portvo.InternationalCharacter,
                             GeographicLocation = portvo.GeographicLocation,
                             ContactNo = portvo.ContactNo,
                             Email = portvo.Email,
                             Fax = portvo.Fax,
                             Website = portvo.Website,
                             Description = portvo.Description,
                             RecordStatus = portvo.RecordStatus,
                         });
            return ports.ToList();
        }
        #endregion

        #region GetPortsByUser
        public List<PortVO> GetPortsByUser(int userId)
        {
            var ports = (from p in _unitOfWork.Repository<Port>().Query().Select()
                         join up in _unitOfWork.Repository<UserPort>().Query().Select()
                         on p.PortCode equals up.PortCode
                         where up.UserID == userId
                         orderby p.PortName
                         select new PortVO
                         {
                             PortCode = p.PortCode,
                             PortName = p.PortName,
                             InternationalCharacter = p.InternationalCharacter,
                             GeographicLocation = p.GeographicLocation,
                             ContactNo = p.ContactNo,
                             Email = p.Email,
                             Fax = p.Fax,
                             Website = p.Website,
                             Description = p.Description,
                             RecordStatus = p.RecordStatus,
                         });

            return ports.ToList();
        }
        #endregion

        #region GetAllExceptLoginport
        public List<PortCodeNameVO> GetAllExceptLoginPort(string loginPort)
        {
            //var ports = (from p in _unitOfWork.Repository<Port>().Query().Select()
            //             where p.RecordStatus == "A" && p.PortCode != LoginPort
            //             orderby p.PortName ascending
            //             select new PortCodeNameVO
            //             {
            //                 PortCode = p.PortCode,
            //                 PortName = p.PortName,

            //             }).ToList<PortCodeNameVO>();
            //return ports;

            var ports = (from p in _unitOfWork.Repository<PortRegistry>().Queryable()
                         where p.RecordStatus == "A" && p.PortCode != loginPort
                         select new PortCodeNameVO
                         {
                             PortCode = p.PortCode,
                             PortName = p.PortName,

                         }).OrderBy(x=>x.PortName).ToList<PortCodeNameVO>();
            return ports;

        }
        #endregion

        #region GetPortDetailsByPortCode
        public Port GetPortDetailsByPortCode(string portCode)
        {
            var portData = (from re in _unitOfWork.Repository<Port>().Queryable().Where(re => re.PortCode.Equals(portCode)).OrderByDescending(re => re.CreatedDate)
                            select re).FirstOrDefault();
            return portData;
        }
        #endregion
    }
}
