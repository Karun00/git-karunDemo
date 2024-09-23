using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class UserReferenceDataVO
    {
        [DataMember]
        public ICollection<SubCategory> ListofUserType { get; set; }
        [DataMember]
        public ICollection<Role> ListofRole { get; set; }
    }
}
