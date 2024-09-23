using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ReportQueryTemplateMapExtension
    {
        public static ReportQueryTemplateVO MapToDTO(this ReportQueryTemplate data)
        {
            ReportQueryTemplateVO rbVo = new ReportQueryTemplateVO();
            rbVo.QueryTemplateId = data.QueryTemplateId;
            rbVo.QueryTemplateName = data.QueryTemplateName;
            rbVo.ReportHeader = data.ReportHeader;
            rbVo.UserQuery = data.UserQuery;
            rbVo.RecordStatus = data.RecordStatus;
            rbVo.CreatedBy = data.CreatedBy;
            rbVo.CreatedDate = data.CreatedDate;
            rbVo.ModifiedBy = data.ModifiedBy;
            rbVo.ModifiedDate = data.ModifiedDate;
            rbVo.ReportbuilderId = data.ReportbuilderId;
            rbVo.ReportBuilder = data.ReportBuilder.MapToDTO();
            
            return rbVo;
        }
        public static ReportQueryTemplate MapToEntity(this ReportQueryTemplateVO vo)
        {
            ReportQueryTemplate rb = new ReportQueryTemplate();
            rb.QueryTemplateId = vo.QueryTemplateId;
            rb.QueryTemplateName = vo.QueryTemplateName;
            rb.ReportHeader = vo.ReportHeader;
            rb.UserQuery = vo.UserQuery;
            rb.RecordStatus = vo.RecordStatus;
            rb.CreatedBy = vo.CreatedBy;
            rb.CreatedDate = vo.CreatedDate;
            rb.ModifiedBy = vo.ModifiedBy;
            rb.ModifiedDate = vo.ModifiedDate;
            rb.ReportbuilderId = vo.ReportbuilderId;
            return rb;
        }

        public static List<ReportQueryTemplateVO> MapToDTO(this List<ReportQueryTemplate> data)
        {
            List<ReportQueryTemplateVO> rbVo = new List<ReportQueryTemplateVO>();

            foreach (var vo in data)
            {
                rbVo.Add(vo.MapToDTO());
            }
            return rbVo;
        }
    }
}


