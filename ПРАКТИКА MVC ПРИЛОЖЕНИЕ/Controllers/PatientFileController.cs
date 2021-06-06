using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class PatientFileController : Controller
    {
        Entities dbModel = new Entities();
        public ActionResult Index()
        {
            return View(dbModel.Электронная_амбулаторная_карта.ToList());
        }

        // GET: PatientFile/Details/5
        public ActionResult Details(int id)
        {
            return View(dbModel.Электронная_амбулаторная_карта.Where(x=>x.Номер_амбулаторной_карты==id).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientFile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(dbModel.Электронная_амбулаторная_карта.Where(x => x.Номер_амбулаторной_карты == id).FirstOrDefault());
        }

        // POST: PatientFile/Edit/5
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

        // GET: PatientFile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientFile/Delete/5
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
