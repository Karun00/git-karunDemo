using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ReportQueryOperatorMapExtension
    {
        public static ReportQueryOperatorVO MapToDTO(this ReportQueryOperator data)
        {
            ReportQueryOperatorVO rbVo = new ReportQueryOperatorVO();
            rbVo.OperatorId=data.OperatorId;
            rbVo.OperatorName=data.OperatorName;
            rbVo.OperatorValue=data.OperatorValue;

            return rbVo;
        }

        public static List<ReportQueryOperatorVO> MapToDTO(this List<ReportQueryOperator> data)
        {
             List<ReportQueryOperatorVO> rbVo = new  List<ReportQueryOperatorVO>();

             foreach (var vo in data)
             {
                 rbVo.Add(vo.MapToDTO());   
             }
             return rbVo;
        }
    }
}


