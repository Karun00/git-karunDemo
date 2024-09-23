using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ReportBuilderMapExtension
    {
        public static ReportBuilderVO MapToDTO(this ReportBuilder data)
        {
            ReportBuilderVO rbVo = new ReportBuilderVO();
            rbVo.ReportbuilderId = data.ReportbuilderId;
            rbVo.Schemaname = data.Schemaname;
            rbVo.ReportCategory = data.ReportCategory;
            rbVo.ReportObjectType = data.ReportObjectType;
            rbVo.ReportObjectName = data.ReportObjectName;
            rbVo.ReportDescription = data.ReportDescription;
            rbVo.RecordStatus = data.RecordStatus;
            rbVo.CreatedBy = data.CreatedBy;
            rbVo.CreatedDate = data.CreatedDate;
            rbVo.ModifiedBy = data.ModifiedBy;
            rbVo.ModifiedDate = data.ModifiedDate;
            return rbVo;
        }
        public static ReportBuilder MapToEntity(this ReportBuilderVO vo)
        {
            ReportBuilder rb = new ReportBuilder();
            rb.ReportbuilderId = vo.ReportbuilderId;
            rb.Schemaname = vo.Schemaname;
            rb.ReportCategory = vo.ReportCategory;
            rb.ReportObjectType = vo.ReportObjectType;
            rb.ReportObjectName = vo.ReportObjectName;
            rb.ReportDescription = vo.ReportDescription;
            rb.RecordStatus = vo.RecordStatus;
            rb.CreatedBy = vo.CreatedBy;
            rb.CreatedDate = vo.CreatedDate;
            rb.ModifiedBy = vo.ModifiedBy;
            rb.ModifiedDate = vo.ModifiedDate;
            return rb;
        }

        public static List<ReportBuilder> MapToEntity(this List<ReportBuilderVO> vos)
        {
            List<ReportBuilder> rbEntities = new List<ReportBuilder>();
            foreach (var rbvo in vos)
            {
                rbEntities.Add(rbvo.MapToEntity());
            }
            return rbEntities;
        }

        public static List<ReportBuilderVO> MapToDTO(this List<ReportBuilder> data)
        {
             List<ReportBuilderVO> rbVo = new  List<ReportBuilderVO>();

             foreach (var vo in data)
             {
                 rbVo.Add(vo.MapToDTO());   
             }
             return rbVo;
        }
    }
}


