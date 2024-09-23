
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Repository
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}