using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.DTOS;

namespace IPMS.Domain.DTOS
{
    public static class PortContentMapExtension
    {
        public static List<PortContentVO> MapToDTO(this IEnumerable<PortContent> porcontentList)
        {
            List<PortContentVO> portcontentvoList = new List<PortContentVO>();

            foreach (var data in porcontentList)
            {
                portcontentvoList.Add(data.MapToDTO());
            }
            return portcontentvoList;
        }

        public static List<PortContent> MapToEntity(this IEnumerable<PortContentVO> PortcontentVoList)
        {
            List<PortContent> portcontentList = new List<PortContent>();

            foreach (var data in PortcontentVoList)
            {
                portcontentList.Add(data.MapToEntity());
            }
            return portcontentList;
        }

        public static PortContentVO MapToDTO(this PortContent data)
        {
            PortContentVO portcontentvo = new PortContentVO();

            portcontentvo.PortContentID = data.PortContentID;
            portcontentvo.ParentPortContentID = data.ParentPortContentID;
            portcontentvo.ContentName = data.ContentName;
            portcontentvo.PortCode = data.PortCode;
            portcontentvo.ContentType = data.ContentType;
            portcontentvo.LinkVisibility = data.LinkVisibility;
            portcontentvo.LinkType = data.LinkType;
            portcontentvo.LinkContent = data.LinkContent;
            portcontentvo.DocumentID = data.DocumentID;
            portcontentvo.RecordStatus = data.RecordStatus;
            portcontentvo.CreatedBy = data.CreatedBy;
            portcontentvo.CreatedDate = data.CreatedDate;
            portcontentvo.ModifiedBy = data.ModifiedBy;
            portcontentvo.ModifiedDate = data.ModifiedDate;

            return portcontentvo;
        }

        public static PortContent MapToEntity(this PortContentVO portcontentvo)
        {
            PortContent portcontent = new PortContent();
            portcontent.PortContentID = portcontentvo.PortContentID;
            portcontent.ParentPortContentID = portcontentvo.ParentPortContentID;
            portcontent.ContentName = portcontentvo.ContentName;
            portcontent.PortCode = portcontentvo.PortCode;
            portcontent.ContentType = portcontentvo.ContentType;
            portcontent.LinkVisibility = portcontentvo.LinkVisibility;
            portcontent.LinkType = portcontentvo.LinkType;
            portcontent.LinkContent = portcontentvo.LinkContent;
            portcontent.DocumentID = portcontentvo.DocumentID;
            portcontent.RecordStatus = portcontentvo.RecordStatus;
            portcontent.CreatedBy = portcontentvo.CreatedBy;
            portcontent.CreatedDate = portcontentvo.CreatedDate;
            portcontent.ModifiedBy = portcontentvo.ModifiedBy;
            portcontent.ModifiedDate = portcontentvo.ModifiedDate;

            return portcontent;
        }
    }
}
