﻿using System.Web;
using System.Web.Mvc;
using EDF.Web.Models;

namespace EDF.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           // filters.Add(new CustomAuthorizeAttribute());
        }
    }
}
