using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class LoginController : Controller
    {
        Entities dbModel = new Entities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Логин_и_пароль auth)
        {
            var userDetails = dbModel.Логин_и_пароль.Where(x => x.Логин == auth.Логин && x.Пароль == auth.Пароль).FirstOrDefault();
            if (userDetails == null)
            {
                // return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Проверьте правильность ввода логина и/или пароля!');</script>");
                auth.ErrorMsg = "Ошибка! Проверьте правильность ввода логина и/или пароля!";
                return View("Index", auth);
            }
            else
            {
                Session["Код"] = userDetails.Код;
                if (userDetails.Код == 1)
                {
                    return RedirectToAction("Index", "MainAdmin");
                }
                else
                {
                    return RedirectToAction("Index", "MainDoctor");
                }

            }
        }
        public ActionResult Logout()
        {
            int userId = (int)Session["Код"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}