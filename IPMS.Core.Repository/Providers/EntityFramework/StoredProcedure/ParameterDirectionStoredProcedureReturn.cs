using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Providers.EntityFramework
{
    public class ParameterDirectionStoredProcedureReturn
    {
        public String ParameterDirectionDefault { get; set; }

        public String ParameterDirectionInput { get; set; }

        public String ParameterDirectionInputOutput { get; set; }

        public String ParameterDirectionOutput { get; set; }

        public String ParameterDirectionReturnValue { get; set; }

    }
}
