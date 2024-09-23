using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PermitRequestVerifyedDetailMapExtension
    {
        public static List<PermitRequestVerifyedDetail> MapToEntity(this IEnumerable<PermitRequestVerifyedDetailVO> vos)
        {
            List<PermitRequestVerifyedDetail> entities = new List<PermitRequestVerifyedDetail>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<PermitRequestVerifyedDetailVO> MapToDTO(this IEnumerable<PermitRequestVerifyedDetail> entities)
        {
            List<PermitRequestVerifyedDetailVO> vos = new List<PermitRequestVerifyedDetailVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static PermitRequestVerifyedDetailVO MapToDTO(this PermitRequestVerifyedDetail data)
        {
            PermitRequestVerifyedDetailVO Vo = new PermitRequestVerifyedDetailVO();
            if (data != null)
            {

                Vo.PermitRequestverifyedID = data.PermitRequestverifyedID;
                Vo.permitrRequestID = data.permitrRequestID;
                Vo.Comments = data.Comments;
                Vo.CreminalCheck = data.CreminalCheck;
                Vo.Flag = data.Flag;
                Vo.RecordStatus = data.RecordStatus;
                Vo.verifyedUserID = data.verifyedUserID;
                Vo.verifyedDate = data.verifyedDate;
                //Vo.PermitRequestVerifyedDocuments = data.PermitRequestVerifyedDocuments.MapToDTO();
            }
            return Vo;
        }
        public static PermitRequestVerifyedDetail MapToEntity(this PermitRequestVerifyedDetailVO VO)
        {
            PermitRequestVerifyedDetail data = new PermitRequestVerifyedDetail();
            if (VO != null)
            {
                data.PermitRequestverifyedID = VO.PermitRequestverifyedID;
                data.permitrRequestID = VO.permitrRequestID;
                data.Comments = VO.Comments;
                data.CreminalCheck = VO.CreminalCheck;
                data.Flag = VO.Flag;
                data.RecordStatus = VO.RecordStatus;
                data.verifyedUserID = VO.verifyedUserID;
                data.verifyedDate = VO.verifyedDate;
                //data.PermitRequestVerifyedDocuments = VO.PermitRequestVerifyedDocuments.MapToEntity();
            }
            return data;
        }
        public static PermitRequestVerifyedDetailVO MapToDTOverifyedbySSAObj(this IEnumerable<PermitRequestVerifyedDetail> PermitRequestVerifyedDetails, string status)
        {
            var PermitRequestVerifyedDetailVOList = new PermitRequestVerifyedDetailVO();
            if (PermitRequestVerifyedDetails != null)
            {
                foreach (var data in PermitRequestVerifyedDetails)
                {
                    if (data.Flag == status)
                    {
                        PermitRequestVerifyedDetailVOList.permitrRequestID = data.permitrRequestID;
                        PermitRequestVerifyedDetailVOList.CreminalCheck = data.CreminalCheck;
                        //if (data.CreminalCheck != null)
                        //{
                        //    if (data.CreminalCheck == "Y")
                        //    {
                        //        PermitRequestVerifyedDetailVOList.CreminalCheck = "True";
                        //    }
                        //}
                        PermitRequestVerifyedDetailVOList.Flag = data.Flag;
                        PermitRequestVerifyedDetailVOList.PermitRequestverifyedID = data.PermitRequestverifyedID;
                        PermitRequestVerifyedDetailVOList.Comments = data.Comments;
                        PermitRequestVerifyedDetailVOList.RecordStatus = data.RecordStatus;
                        PermitRequestVerifyedDetailVOList.verifyedUserID = data.verifyedUserID;
                        PermitRequestVerifyedDetailVOList.verifyedDate = data.verifyedDate;
                        PermitRequestVerifyedDetailVOList.PermitRequestverifyedbySSADocuments =
                            data.PermitRequestVerifyedDocuments.MapToDTO();
                    }
                }
            }
            return PermitRequestVerifyedDetailVOList;
        }

        public static PermitRequestVerifyedDetailVO MapToDTOverifyedbySAPSObj(this IEnumerable<PermitRequestVerifyedDetail> PermitRequestVerifyedDetails, string status)
        {
            var PermitRequestVerifyedDetailVOList = new PermitRequestVerifyedDetailVO();
            if (PermitRequestVerifyedDetails != null)
            {
                foreach (var data in PermitRequestVerifyedDetails)
                {
                    if (data.Flag == status)
                    {
                        PermitRequestVerifyedDetailVOList.permitrRequestID = data.permitrRequestID;
                        PermitRequestVerifyedDetailVOList.CreminalCheck = data.CreminalCheck;
                        //if (data.CreminalCheck != null)
                        //{
                        //    if (data.CreminalCheck == "Y")
                        //    {
                        //        PermitRequestVerifyedDetailVOList.CreminalCheck = "True";
                        //    }
                        //}
                        PermitRequestVerifyedDetailVOList.Flag = data.Flag;
                        PermitRequestVerifyedDetailVOList.PermitRequestverifyedID = data.PermitRequestverifyedID;
                        PermitRequestVerifyedDetailVOList.Comments = data.Comments;
                        PermitRequestVerifyedDetailVOList.RecordStatus = data.RecordStatus;
                        PermitRequestVerifyedDetailVOList.verifyedUserID = data.verifyedUserID;
                        PermitRequestVerifyedDetailVOList.verifyedDate = data.verifyedDate;
                        PermitRequestVerifyedDetailVOList.PermitRequestverifyedbySAPSDocuments =
                            data.PermitRequestVerifyedDocuments.MapToDTO();
                    }
                }
            }
            return PermitRequestVerifyedDetailVOList;
        }
    
    }
}
