using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class MainAdminController : Controller
    {
        Entities dbModel = new Entities();
        public ActionResult Index()
        {
            
            return View();
        }
    }
}