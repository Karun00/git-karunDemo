using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Data.SqlClient;

namespace IPMS.Repository
{
    public class RevenueStopListRepository : IRevenueStopListRepository
    {
        private IUnitOfWork _unitOfWork;

        public RevenueStopListRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<RevenueStopListVO> GetAllAgents(string portcode, string ag)
        {

            var serchstring = new SqlParameter("@Serchstring", ag);
            var ptcode = new SqlParameter("@portcode", portcode);

            List<RevenueStopListVO> agentlist = _unitOfWork.SqlQuery<RevenueStopListVO>("dbo.usp_GetStopListAccounts @Serchstring, @portcode ", serchstring, ptcode).ToList();
            return agentlist;

        }


        public List<RevenueStopListVO> GetAllAgentsforgrid(string portcode)
        {

            var vesregdata = (from rs in _unitOfWork.Repository<RevenueStopList>().Query().Tracking(true).Select()
                              join ras in _unitOfWork.Repository<RevenueAccountStatus>().Query().Tracking(true).Select()
                              on rs.RevenueStopListID equals ras.RevenueStopListID
                              join ag in _unitOfWork.Repository<Agent>().Query().Tracking(true).Select()
                              on rs.AgentID equals ag.AgentID
                              join aga in _unitOfWork.Repository<AgentAccount>().Query().Tracking(true).Select()
                              on rs.AgentAccountID equals aga.AgentAccountID
                              join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                              on ras.AccountStatusCode equals sc.SubCatCode                             
                              where rs.PortCode == portcode
                              orderby rs.CreatedDate descending
                              select new RevenueStopListVO
                              {
                                  RevenueStopListID = rs.RevenueStopListID,
                                  RevenueAccountStatusID = ras.RevenueAccountStatusID,
                                  RegistrationNumber = ag.RegistrationNumber,
                                  AccountNo = aga.AccountNo,
                                  AgentID = ag.AgentID,
                                  AgentAccountID = aga.AgentAccountID,
                                  PortCode = rs.PortCode,
                                  RegisteredName = ag.RegisteredName,
                                  AccountStatus = sc.SubCatCode,
                                  AccountStatusName = sc.SubCatName,
                                  StopDate = rs.StopDate != null ? rs.StopDate.ToString() : ""
                              }).ToList<RevenueStopListVO>();
            return vesregdata;
        }


        public List<RevenueStopListVO> GetSearchAgentData(string AgentID, string agentname, string horbaraccountno, string accountstatus, string portcode)
        {
            string searchstr = " 1=1  ";

            if (AgentID != "ALL")
            {
                searchstr += " and AG.RegistrationNumber ='" + AgentID + "'";
            }

            if (agentname != "ALL")
                searchstr += " and (AG.RegisteredName Like '%" + agentname + "%' )  ";

            if (horbaraccountno != "ALL")
                searchstr += " and AC.AccountNo ='" + horbaraccountno + "' ";

            if (accountstatus != "ALL")
                searchstr += " and RAS.AccountStatusCode  ='" + accountstatus + "' ";

            List<RevenueStopListVO> vesregdata = _unitOfWork.SqlQuery<RevenueStopListVO>("select  RS.RevenueStopListID,RAS.RevenueAccountStatusID,ag.RegistrationNumber,AC.AccountNo,ag.AgentID,ac.AgentAccountID,Rs.PortCode,ag.RegisteredName,sub.subcatcode as AccountStatus,sub.SubCatName as AccountStatusName,rs.StopDate from RevenueStopList RS inner join RevenueAccountStatus RAS on RS.RevenueStopListID = RAS.RevenueStopListID inner join Agent AG on AG.AgentID = RS.AgentID inner join AgentAccount AC on RS.AgentAccountID = AC.AgentAccountID inner join SubCategory SUB on SUB.subcatcode = RAS.AccountStatusCode inner join AgentPort AP on AP.AgentID = RS.AgentID Where " + searchstr + " and AP.PortCode='" + portcode + "' order by RS.AgentID desc ").ToList();

            return vesregdata;
        }


    }
}

