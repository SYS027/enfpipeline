using EDF.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITASSET.Controllers.WebControllers.Common
{
    
    public class MainController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
    }
}