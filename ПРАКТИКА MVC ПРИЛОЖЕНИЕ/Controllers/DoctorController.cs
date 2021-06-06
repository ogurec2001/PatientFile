using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        Entities dbModel = new Entities();
        public static IEnumerable<SelectListItem> PopulateDoctors()
        {
            Entities dbModel = new Entities();
            IEnumerable<SelectListItem> selectListItems = (dbModel.Специализация_врача.Select(p => new SelectListItem
            {
                Text = p.Специальность,
                Value = p.Код_специальности.ToString()
            }).ToList());
            return selectListItems;
        }
        public ActionResult Index()
        {
            return View(dbModel.Идентификатор_врача.ToList());
        }

        public ActionResult Details(int id)
        {
            return View(dbModel.Идентификатор_врача.Where(x => x.Идентификатор_врача1 == id).FirstOrDefault());
        }

        // GET: Doctor/Create
        [HttpGet]
        public ActionResult Create()
        {
            var docList = new Идентификатор_врача
            {
                DoctorList = PopulateDoctors()
            };
            return View(docList);
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(Идентификатор_врача doc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbModel.Идентификатор_врача.Add(doc);
                    dbModel.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Проверьте данные на дублирование или обратитесь в техническую поддержку!');</script>");

                }

            }
            ViewData["Специализация_врача"] = new SelectList(dbModel.Специализация_врача, "Код_специальности", "Специальность");

            return View();
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            Идентификатор_врача docId = new Идентификатор_врача();
            docId.DoctorList = PopulateDoctors();
            ViewBag.Идентификатор_врача = docId;

            return View(docId);
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
