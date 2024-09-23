using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class IdNameVO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public long DeadWeightTonnage { get; set; }
        public long GrossWeightTonnage { get; set; }
        public int CraftID { get; set; }
        public string CraftName { get; set; }
        public Nullable<DateTime> OCTime { get; set; }
        public Nullable<DateTime> OCBackTime { get; set; }
    }
}
