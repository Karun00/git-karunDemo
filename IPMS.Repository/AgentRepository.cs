using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain;
using System.Globalization;

namespace IPMS.Repository
{
    public class AgentRepository : IAgentRepository
    {
        private IUnitOfWork _unitOfWork;

        public AgentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // Get Agent Details By Agent Id * changed by Nivedita
        public AgentDetailsVO GetAgent(int agentid)
        {
            var agent = (from a in _unitOfWork.Repository<Agent>().Query().Select()
                         join ac in _unitOfWork.Repository<AuthorizedContactPerson>().Query().Select()
                         on a.AuthorizedContactPersonID equals ac.AuthorizedContactPersonID
                         where a.AgentID == agentid
                         select new AgentDetailsVO
                         {
                             AgentID = a.AgentID,
                             RegisteredName = a.RegisteredName,
                             TelephoneNo1 = a.TelephoneNo1,
                             FaxNo = a.FaxNo,
                             FirstName = ac.FirstName,
                             SurName = ac.SurName,
                             EmailID = ac.EmailID,
                             CellularNo = ac.CellularNo,
                             TradingName = a.TradingName,
                             ReferenceNo = a.ReferenceNo,
                             RegistrationNumber = a.RegistrationNumber,
                             VATNumber = a.VATNumber,
                             IncomeTaxNumber = a.IncomeTaxNumber,
                             //FromDate=a.FromDate,
                             //ToDate=a.ToDate



                         }).FirstOrDefault<AgentDetailsVO>();

            //a).FirstOrDefault<Agent>();
            return agent;
        }
        //To get View Agent registration 
        public AgentDetailsVO GetzAgent(string vcn)
        {
            int AgentID = 0;
            if (!string.IsNullOrEmpty(vcn))
            {
                AgentID = Convert.ToInt32(vcn, CultureInfo.InvariantCulture);
            }

            var agent = (from a in _unitOfWork.Repository<Agent>().Query().Select()
                         join ac in _unitOfWork.Repository<AuthorizedContactPerson>().Query().Select()
                         on a.AuthorizedContactPersonID equals ac.AuthorizedContactPersonID
                         where a.AgentID == AgentID
                         select new AgentDetailsVO
                         {
                             AgentID = a.AgentID,
                             RegisteredName = a.RegisteredName,
                             TelephoneNo1 = a.TelephoneNo1,
                             FaxNo = a.FaxNo,
                             FirstName = ac.FirstName,
                             SurName = ac.SurName,
                             EmailID = ac.EmailID,
                             CellularNo = ac.CellularNo
                         }).FirstOrDefault<AgentDetailsVO>();

            //a).FirstOrDefault<Agent>();
            return agent;
        }

        // Get Agent Details By User ID  * Added by Nivedita
        public AgentDetailsVO GetAgentForUser(int userid)
        {
            var agent = (from a in _unitOfWork.Repository<Agent>().Queryable()
                         join ac in _unitOfWork.Repository<AuthorizedContactPerson>().Queryable()
                         on a.AuthorizedContactPersonID equals ac.AuthorizedContactPersonID
                         join user in _unitOfWork.Repository<User>().Queryable().Where(user => user.UserID == userid)
                         on a.AgentID equals user.UserTypeID
                         //where user.UserID == userid
                         select new AgentDetailsVO
                         {
                             AgentID = a.AgentID,
                             RegisteredName = a.RegisteredName,
                             TelephoneNo1 = a.TelephoneNo1,
                             FaxNo = a.FaxNo,
                             FirstName = ac.FirstName,
                             SurName = ac.SurName,
                             EmailID = ac.EmailID,
                             CellularNo = ac.CellularNo
                         }).FirstOrDefault<AgentDetailsVO>();

            return agent;

        }

        public List<UserMasterVO> GetAllAgents(string portcode)
        {
            List<UserMasterVO> agentlist = new List<UserMasterVO>();
            if (!string.IsNullOrWhiteSpace(portcode))
            {
                agentlist = (from e in _unitOfWork.Repository<Agent>().Query().Select()
                             join ap in _unitOfWork.Repository<AgentPort>().Query().Select()
                             on e.AgentID equals ap.AgentID
                             where e.RecordStatus == RecordStatus.Active && ap.WFStatus == WFStatus.Approved && ap.PortCode == portcode
                             orderby e.RegisteredName ascending
                             select new UserMasterVO
                             {
                                 // Srinivas Malepati, as we need to validate Ref no instead of Reg No
                                 //UserTypeID = e.AgentID,
                                 //Name = e.RegisteredName,
                                 //ReferenceNo = e.RegistrationNumber,
                                 //PortCode = ap.PortCode

                                 UserTypeID = e.AgentID,
                                 Name = e.RegisteredName,
                                 Designation = e.RegisteredName,

                                 //ReferenceNo = e.RegistrationNumber,
                                 ReferenceNo = e.ReferenceNo,
                                 PortCode = ap.PortCode
                             }).ToList();

            }
            else
            {
                agentlist = (from e in _unitOfWork.Repository<Agent>().Query().Select()
                             join ap in _unitOfWork.Repository<AgentPort>().Query().Select()
                             on e.AgentID equals ap.AgentID
                             where e.RecordStatus == RecordStatus.Active && ap.WFStatus == WFStatus.Approved
                             orderby e.RegisteredName ascending
                             select new UserMasterVO
                             {
                                 // Srinivas Malepati, as we need to validate Ref no instead of Reg No
                                 //UserTypeID = e.AgentID,
                                 //Name = e.RegisteredName,
                                 //ReferenceNo = e.RegistrationNumber,
                                 //PortCode = ap.PortCode

                                 UserTypeID = e.AgentID,
                                 Name = e.RegisteredName,
                                 Designation = e.RegisteredName,

                                 //ReferenceNo = e.RegistrationNumber,
                                 ReferenceNo = e.ReferenceNo,
                                 PortCode = ap.PortCode
                             }).ToList();
            }

            return agentlist;

        }

        public List<AgentVO> GetAllAgentsExceptLogOnAgent(string portcode, int loginagent, String searchvalue)
        {

            /*var agentlist = (from e in _unitOfWork.Repository<Agent>().Query().Select()
                             join ap in _unitOfWork.Repository<AgentPort>().Query().Select()
                             on e.AgentID equals ap.AgentID
                             where e.RecordStatus == RecordStatus.Active && ap.WFStatus == WFStatus.Approved && ap.PortCode == portcode
                             && e.AgentID != LoginAgent && ( e.RegisteredName.StartsWith(Searchvalue)||e.RegisteredName.Contains(Searchvalue) ||e.RegisteredName.EndsWith(Searchvalue) )
                             orderby e.RegisteredName ascending
                             select new AgentVO
                             {
                                 AgentID = e.AgentID,
                                 RegisteredName = e.RegisteredName +" - "+ e.RegistrationNumber
                             }).ToList();
             */
            var agentlist = (from e in _unitOfWork.Repository<Agent>().Query().Select()
                             join ap in _unitOfWork.Repository<AgentPort>().Query().Select() on new { aa = e.AgentID } equals new { aa = ap.AgentID }
                             join au in _unitOfWork.Repository<User>().Query().Select() on new { ab = ap.AgentID } equals new { ab = au.UserTypeID }
                             join up in _unitOfWork.Repository<UserPort>().Query().Select() on new { ba = au.UserID, bb = ap.PortCode } equals new { ba = up.UserID, bb = up.PortCode }
                             where au.UserType == "AGNT" && ap.PortCode == portcode && e.RecordStatus == "A" && e.AgentID != loginagent && au.RecordStatus == "A"
                             && e.RegisteredName.ToUpper(CultureInfo.InvariantCulture).Contains(searchvalue.ToUpper(CultureInfo.InvariantCulture))
                             orderby e.RegisteredName ascending
                             select new AgentVO
                             {
                                 AgentID = e.AgentID,
                                 RegisteredName = e.RegisteredName + " - " + e.RegistrationNumber
                             }).GroupBy(i => i.AgentID).Select(l => l.Last()).ToList();

            return agentlist;

        }

        // -- Added by sandeep on 12-11-2014
        public AgentVO GetAgentDetailsInVesselCallByVcn(string vcn)
        {
            var agent = (from vc in _unitOfWork.Repository<VesselCall>().Query()
                         .Include(vc => vc.Agent)
                         .Include(vc => vc.Agent.AuthorizedContactPerson)
                         .Select()

                         where vc.VCN == vcn

                         select new AgentVO
                         {
                             RegisteredName = vc.Agent.RegisteredName,
                             TradingName = vc.Agent.TradingName,
                             TelephoneNo1 = vc.Agent.TelephoneNo1,
                             FaxNo = vc.Agent.FaxNo,
                             TelephoneNo2 = vc.Agent.AuthorizedContactPerson.CellularNo

                         }).FirstOrDefault();

            return agent;
        }
        // -- end

        public List<PortCodeNameVO> GetAgentportbasedAccount(int agentid, string portcode)
        {
            var Agentportdtls = (from p in _unitOfWork.Repository<Port>().Query().Select()
                                 join ap in _unitOfWork.Repository<AgentPort>().Query().Select()
                                 on p.PortCode equals ap.PortCode
                                 where ap.WFStatus == WFStatus.Approved && ap.AgentID == agentid && ap.PortCode == portcode
                                 //join wf in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                                 //on ap.WorkflowInstanceId equals wf.WorkflowInstanceId
                                 //where wf.WorkflowTaskCode == "WFSA" && ap.AgentID == AgentID
                                 //where ap.AgentID == AgentID
                                 select new PortCodeNameVO
                                 {
                                     PortCode = p.PortCode,
                                     PortName = p.PortName

                                 }).ToList();


            return Agentportdtls;
        }


        public List<AgentAccount> GetAgentAccountDetailsbyAgentId(int agentid, string portcode)
        {
            var agent = (from a in _unitOfWork.Repository<AgentAccount>().Query().Select()
                         where a.AgentID == agentid && a.PortCode == portcode
                         select a).ToList();

            return agent;
        }

        public List<AgentVO> GetAllAgentswithAccountno(string portcode)
        {


            var agentlist = (from e in _unitOfWork.Repository<Agent>().Query().Select()
                             join ap in _unitOfWork.Repository<AgentPort>().Query().Select()
                             on e.AgentID equals ap.AgentID
                             join ac in _unitOfWork.Repository<AgentAccount>().Query().Select()
                             on e.AgentID equals ac.AgentID
                             where e.RecordStatus == RecordStatus.Active && ap.WFStatus == WFStatus.Approved && ap.PortCode == portcode
                             orderby e.RegisteredName ascending
                             select new AgentVO
                             {
                                 AgentID = e.AgentID,
                                 RegistrationNumber = e.RegistrationNumber,
                                 AgentAccountno = ac.AccountNo,
                                 RegisteredName = e.RegisteredName,
                                 ReferenceNo = e.ReferenceNo,
                             }).ToList();

            return agentlist;

        }

    }
}
