using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class ParentController : Controller
    {
        Entities context = new Entities();

        // GET: Parent
        public ActionResult Index()
        {
            return View(context.Карточка_представителя.ToList());
        }
        public ActionResult DocIndex()
        {
            return View(context.Карточка_представителя.ToList());
        }

        public ActionResult Details(int id)
        {
            return View(context.Карточка_представителя.Where(x => x.Код_представителя == id).FirstOrDefault());
        }
        public ActionResult DocDetails(int id)
        {
            return View(context.Карточка_представителя.Where(x => x.Код_представителя == id).FirstOrDefault());
        }

        // GET: Parent/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Карточка_представителя parent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Карточка_представителя.Add(parent);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                   return Content("<script language='javascript' type='text/javascript'>alert     " +
                       "('Ошибка! Заполните все поля, проверьте данные на дублирование или обратитесь в техническую поддержку!');</script>");

                }

            }
            return View(parent);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(context.Карточка_представителя.Where(x => x.Код_представителя == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, Карточка_представителя parent)
        {
            try
            {
                context.Entry(parent).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("<script language='javascript' type='text/javascript'>alert     " +
                                     "('Ошибка! Проверьте данные на дублирование или обратитесь в техническую поддержку!');</script>");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
             return View(context.Карточка_представителя.Where(x => x.Код_представителя == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, Карточка_представителя parent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    parent = context.Карточка_представителя.Where(x => x.Код_представителя == id).FirstOrDefault();
                    context.Карточка_представителя.Remove(parent);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert    " +
                        " ('Ошибка удаления! Обратитесь в техническую поддержку!');</script>");
                }
            }
            return View(parent);
        }
    }
}
