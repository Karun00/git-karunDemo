using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class DockingPlanDocumentMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<DockingPlanDocumentVO> MapToDto(this IEnumerable<DockingPlanDocument> dockingplandocument)
        {
            var dockingplanDocumentVOList = new List<DockingPlanDocumentVO>();
            if (dockingplandocument != null)
            {
                foreach (var item in dockingplandocument)
                {
                    dockingplanDocumentVOList.Add(item.MapToDto());
                }
            }
            return dockingplanDocumentVOList;
        }


        public static List<DockingPlanDocument> MapToEntity(this List<DockingPlanDocumentVO> dockingplandocumentsvo)
        {
            List<DockingPlanDocument> lstDockingPlanDocumentDocument = new List<DockingPlanDocument>();
            if (dockingplandocumentsvo != null)
            {
            foreach (DockingPlanDocumentVO dockingplanDocumentvo in dockingplandocumentsvo)
            {
                lstDockingPlanDocumentDocument.Add(dockingplanDocumentvo.MapToEntity());
            }
            }

            return lstDockingPlanDocumentDocument;
        }

        public static DockingPlanDocumentVO MapToDto(this DockingPlanDocument data)
        {
            DockingPlanDocumentVO VO = new DockingPlanDocumentVO();
            if (data != null)
            {
                VO.DockingPlanDocumentID = data.DockingPlanDocumentID;
                VO.DockingPlanID = data.DockingPlanID;
                VO.DocumentID = data.DocumentID;
                VO.RecordStatus = data.RecordStatus;
                VO.FileName = data.Document.FileName;
                VO.DocumentTypeName = data.Document.SubCategory1!=null?data.Document.SubCategory1.SubCatName:"";
                //VO.CreatedBy = data.CreatedBy;
                //VO.CreatedDate = data.CreatedDate;
                //VO.ModifiedBy = data.ModifiedBy;
                //VO.ModifiedDate = data.ModifiedDate;
            }
            return VO;    

        }
        public static DockingPlanDocument MapToEntity(this DockingPlanDocumentVO vo)
        {
            DockingPlanDocument data = new DockingPlanDocument();
            if (vo != null)
            {
                data.DockingPlanDocumentID = vo.DockingPlanDocumentID;
                data.DockingPlanID = vo.DockingPlanID;
                data.DocumentID = vo.DocumentID;
                data.RecordStatus = "A";
                //data.CreatedBy = VO.CreatedBy;
                //data.CreatedDate = VO.CreatedDate;
                //data.ModifiedBy = VO.ModifiedBy;
                //data.ModifiedDate = VO.ModifiedDate;
            }
            return data;
        }
    }
}
