using IPMS.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;

namespace IPMS.Web.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterHttpFilters(HttpFilterCollection filters)
        {
            if (filters != null)
            {
                filters.Add(new LogUserActivityAttribute());
            }
        }
    }
}