using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core.Repository
{
    [DataContract(IsReference = true)]
    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}