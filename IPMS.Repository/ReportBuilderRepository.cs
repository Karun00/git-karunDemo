using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace IPMS.Repository
{
    public class ReportBuilderRepository : IReportBuilderRepository
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILog log;
       // private IAccountRepository _accountRepository;

        public ReportBuilderRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(ArrivalNotificationRepository));
           // _accountRepository = new AccountRepository(_unitOfWork);
        }

        public IEnumerable<ReportBuilderVO> GetReportCategory()
        {
            var reportbuilder = (from rb in _unitOfWork.Repository<ReportBuilder>().Queryable()
                                 where rb.RecordStatus == "A"
                                 select rb).ToList();
            return reportbuilder.MapToDTO();

        }

        public List<ReportQueryOperator> GetReportQueryOperator(string datatype)
        {
            var reportoperator = (from ro in _unitOfWork.Repository<ReportQueryOperator>().Query().Select()
                                  join rd in _unitOfWork.Repository<ReportQueryDataTypeOperator>().Query().Select()
                                  on ro.OperatorId equals rd.OperatorId where rd.ApplicableDataType == datatype
                                  select ro).ToList();
            return reportoperator;
        }

        public IEnumerable<ReportCategoryColumnVO> GetReportCategoryColumn(int reportbilderid)
         {
            var reportbuilder = (from rb in _unitOfWork.Repository<ReportBuilder>().Query().Select()
                                 where rb.ReportbuilderId == reportbilderid
                                 select rb);
            
            List<ReportCategoryColumnVO> rccolumn = new List<ReportCategoryColumnVO>();
            if (reportbilderid != 3)
            {
                 rccolumn = _unitOfWork.SqlQuery<ReportCategoryColumnVO>("select * from dbo.udf_get_columnnames('" + reportbuilder.FirstOrDefault().ReportObjectName + "')").ToList<ReportCategoryColumnVO>();
            }
            else
            {
                 rccolumn = _unitOfWork.SqlQuery<ReportCategoryColumnVO>("select * from rbuild.dbo.udf_get_columnnames('" + reportbuilder.FirstOrDefault().ReportObjectName + "')").ToList<ReportCategoryColumnVO>();
            }
            
            return rccolumn;

        }

        public List<ReportQueryTemplate> GetReportFilterData(int userid)
        {

            var reportquerytemp = (from t in _unitOfWork.Repository<ReportQueryTemplate>().Query().Include(t => t.ReportBuilder).Select()
                                   where (t.CreatedBy == userid &&  t.RecordStatus==RecordStatus.Active)
                                   select t).ToList<ReportQueryTemplate>();
            return reportquerytemp;

        }

        public ReportQueryTemplate DeleteReportQueryTemplate(int ID)
        {
            var Obj = _unitOfWork.Repository<ReportQueryTemplate>().Find(ID);
            Obj.RecordStatus = RecordStatus.InActive;
            Obj.ObjectState = ObjectState.Modified;
            Obj.ModifiedDate = DateTime.Now;
            _unitOfWork.Repository<ReportQueryTemplate>().Update(Obj);
            _unitOfWork.SaveChanges();
            return Obj;          

        }

        public IEnumerable<ReportBuilderVO> GetReportBuilderGridData(ReportBuilderVO rbitem)
        {
            var reportbuilder = (from rb in _unitOfWork.Repository<ReportBuilder>().Query().Select()
                                 where rb.ReportbuilderId == rbitem.ReportbuilderId
                                 select rb);
            var reportbuilderdata = new List<ReportBuilderVO>();
            if (rbitem.ReportbuilderId != 3)
            {
                var ReportObjectName = new SqlParameter("@p_ReportObjectName", reportbuilder.FirstOrDefault().ReportObjectName);
                var ReportObjectType = new SqlParameter("@p_ReportObjectType", rbitem.ReportObjectType);
                var display_columns = new SqlParameter("@p_display_columns", rbitem.DisplayColumns);
                var obj_Parameters = new SqlParameter("@p_obj_Parameters", rbitem.Parameters);
                var srch_filter = new SqlParameter("@p_srch_filter", rbitem.SearchFilter);
                var orderby = new SqlParameter("@p_orderby", rbitem.OrderBy + " " + rbitem.OrderByAD);
                reportbuilderdata = _unitOfWork.SqlQuery<ReportBuilderVO>("dbo.usp_getReportBuilderData @p_ReportObjectName, @p_ReportObjectType,@p_display_columns,@p_obj_Parameters,@p_srch_filter,@p_orderby", ReportObjectName, ReportObjectType, display_columns, obj_Parameters, srch_filter, orderby).ToList();
                log.Debug("dbo.usp_getReportBuilderData '" + ReportObjectName.Value + "', '" + ReportObjectType.Value + "','" + display_columns.Value + "','" + obj_Parameters.Value + "','" + srch_filter.Value + "','" + orderby.Value + "','Y'");
            }
            else
            {
                //INJSON
                var ReportObjectName = new SqlParameter("@p_ReportObjectName", reportbuilder.FirstOrDefault().ReportObjectName);
                var ReportObjectType = new SqlParameter("@p_ReportObjectType", rbitem.ReportObjectType);
                var display_columns = new SqlParameter("@p_display_columns", rbitem.DisplayColumns);
                var obj_Parameters = new SqlParameter("@p_obj_Parameters", rbitem.Parameters);
                var srch_filter = new SqlParameter("@p_srch_filter", rbitem.SearchFilter);
                var orderby = new SqlParameter("@p_orderby", rbitem.OrderBy + " " + rbitem.OrderByAD);
                reportbuilderdata = _unitOfWork.SqlQuery<ReportBuilderVO>("RBUILD.dbo.getReportBuilderData @p_ReportObjectName, @p_ReportObjectType,@p_display_columns,@p_obj_Parameters,@p_srch_filter,@p_orderby", ReportObjectName, ReportObjectType, display_columns, obj_Parameters, srch_filter, orderby).ToList();
                log.Debug("RBUILD.dbo.usp_getReportBuilderData '" + ReportObjectName.Value + "', '" + ReportObjectType.Value + "','" + display_columns.Value + "','" + obj_Parameters.Value + "','" + srch_filter.Value + "','" + orderby.Value + "','Y'");
                //string[] cond = rbitem.SearchFilter.Split('=');
                //string str = "'" + cond[1] + "'";
                //string Param = cond[0] + "=" + str;

                //Not IN JSON
                //var ReportObjectName = new SqlParameter("@TableViewName", reportbuilder.FirstOrDefault().ReportObjectName);
                //// var ReportObjectType = new SqlParameter("@p_ReportObjectType", rbitem.ReportObjectType);
                //var display_columns = new SqlParameter("@Columns", rbitem.DisplayColumns);
                ////var obj_Parameters = new SqlParameter("@p_obj_Parameters", rbitem.Parameters);
                //var srch_filter = new SqlParameter("@conditionname1", rbitem.SearchFilter);
                ////var orderby = new SqlParameter("@orededvalues", rbitem.OrderBy + " " + rbitem.OrderByAD);
                //reportbuilderdata = _unitOfWork.SqlQuery<ReportBuilderVO>("RBUILD.dbo.[REPB_DATAEXE_SP_123] @TableViewName,@columns,@conditionname1", ReportObjectName, display_columns, srch_filter).ToList();
                ////log.Debug("RBUILD.dbo.[REPB_DATAEXE_SP_123] '" + ReportObjectName.Value + "', '" + ReportObjectType.Value + "','" + display_columns.Value + "','" + obj_Parameters.Value + "','" + srch_filter.Value + "','" + orderby.Value + "','Y'");
            }
           // var jsonText = new SqlParameter("@p_convertJSON",'N');


            //try
            //{
            //if (rbitem.ReportbuilderId != 3)
            //{
            //    reportbuilderdata = _unitOfWork.SqlQuery<ReportBuilderVO>("dbo.usp_getReportBuilderData @p_ReportObjectName, @p_ReportObjectType,@p_display_columns,@p_obj_Parameters,@p_srch_filter,@p_orderby", ReportObjectName, ReportObjectType, display_columns, obj_Parameters, srch_filter, orderby).ToList();
            //    log.Debug("dbo.usp_getReportBuilderData '" + ReportObjectName.Value + "', '" + ReportObjectType.Value + "','" + display_columns.Value + "','" + obj_Parameters.Value + "','" + srch_filter.Value + "','" + orderby.Value + "','Y'");
            //}
            //else if (rbitem.ReportbuilderId == 3)
            //{
            //    reportbuilderdata = _unitOfWork.SqlQuery<ReportBuilderVO>("RBUILD.dbo.REPB_DATAEXE_SP @p_ReportObjectName, @p_ReportObjectType,@p_display_columns,@p_obj_Parameters,@p_srch_filter,@p_orderby", ReportObjectName, ReportObjectType, display_columns, obj_Parameters, srch_filter, orderby).ToList();
            //   //rajesh //log.Debug("dbo.usp_getReportBuilderData '" + ReportObjectName.Value + "', '" + ReportObjectType.Value + "','" + display_columns.Value + "','" + obj_Parameters.Value + "','" + srch_filter.Value + "','" + orderby.Value + "','Y'");
            //}
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception " + ex.Message);
            //}


            return reportbuilderdata;

        }

        public List<ReportLookUpVO> GetLookUpData(string columnName, string searchValue)
      {
           //// string correctString = query.Replace("#COND#", searchvalue);

           // var rccolumn = _unitOfWork.SqlQuery<ReportLookUpVO>("exec usp_getReportBuilderData  'udf_get_SubCategoryLookupdata', '', 'TOP 10 *', '''VETY''', 'Text Like ''%oi%''','', 'N'").ToList<ReportLookUpVO>();
 
           // return rccolumn;
            if (searchValue == "*")
            {
                searchValue = "%";
            }
            var rccolumn = new List<ReportLookUpVO>();
            var queryColumn = (from t in _unitOfWork.Repository<ReportQueryLookup>().Query().Select()
                               where t.LookupColumnname == columnName
                               select t).ToList<ReportQueryLookup>();

            if (queryColumn.Count != 0)
            {
                string correctString = queryColumn.FirstOrDefault().LookupName.Replace("#COND#", searchValue);

                rccolumn = _unitOfWork.SqlQuery<ReportLookUpVO>(correctString).ToList<ReportLookUpVO>();
            }
            else
            {
              List<ReportLookUpVO> rvo = new List<ReportLookUpVO>();


              rvo.Add(new ReportLookUpVO()
              {
                  ColumnName = "",
                  query="",
                  SearchText="",
                  Value="",
                  Text=searchValue

              });
                rccolumn = rvo;
            }
           
                       
            return rccolumn;
        }
    }
}
