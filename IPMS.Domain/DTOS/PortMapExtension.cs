using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;

namespace IPMS.Domain.DTOS
{
    public static class PortMapExtension
    {

        public static List<PortVO> MapToDTO(this IEnumerable<Port> portList)
        {
            List<PortVO> portvoList = new List<PortVO>();
            foreach (var data in portList)
            {
                portvoList.Add(data.MapToDTO());
               
            }
            return portvoList;
        }

        public static List<Port> MapToEntity(this IEnumerable<PortVO> PortVoList)
        {
            List<Port> portList = new List<Port>();
            foreach (var data in PortVoList)
            {
                portList.Add(data.MapToEntity());
              
            }
            return portList;
        }


        public static PortVO MapToDTO(this Port data)
        {
            PortVO portvo = new PortVO();

            portvo.PortCode = data.PortCode;
            portvo.PortName = data.PortName;
            portvo.InternationalCharacter = data.InternationalCharacter;
            portvo.GeographicLocation = data.GeographicLocation;
            portvo.ContactNo = data.ContactNo;
            portvo.Email = data.Email;
            portvo.Fax = data.Fax;
            portvo.Website = data.Website;
            portvo.Description = data.Description;
            portvo.RecordStatus = data.RecordStatus;
            portvo.CreatedBy = data.CreatedBy;
            portvo.CreatedDate = data.CreatedDate;
            portvo.ModifiedBy = data.ModifiedBy;
            portvo.ModifiedDate = data.ModifiedDate;


            return portvo;

        }
        public static Port MapToEntity(this PortVO portvo)
        {
            Port port = new Port();
            port.PortCode = portvo.PortCode;
            port.PortName = portvo.PortName;
            port.InternationalCharacter = portvo.InternationalCharacter;
            port.GeographicLocation = portvo.GeographicLocation;
            port.ContactNo = portvo.ContactNo;
            port.Email = portvo.Email;
            port.Fax = portvo.Fax;
            port.Website = portvo.Website;
            port.Description = portvo.Description;
            port.RecordStatus = portvo.RecordStatus;
            port.CreatedBy = portvo.CreatedBy;
            port.CreatedDate = portvo.CreatedDate;
            port.ModifiedBy = portvo.ModifiedBy;
            port.ModifiedDate = portvo.ModifiedDate;
        
            return port;
        }
    }
}
