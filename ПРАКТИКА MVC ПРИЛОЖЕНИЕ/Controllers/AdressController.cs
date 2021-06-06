using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class AdressController : Controller
    {
        Entities context = new Entities();
        // GET: Adress
        public ActionResult Index()
        {
            ViewBag.Номер_амбулаторной_карты = new List<SelectListItem>().GetSelectListItems();

            return View(context.Адрес_пациента.ToList());
        }

        public ActionResult Details(int id)
        {
            return View(context.Адрес_пациента.Where(x=>x.Код_адреса==id).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Adress/Create
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
            return View(context.Адрес_пациента.Where(x => x.Код_адреса == id).FirstOrDefault());
        }

        // POST: Adress/Edit/5
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

        // GET: Adress/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Adress/Delete/5
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

    public static class ExtensionMethod
    {

        public static List<SelectListItem> GetSelectListItems(this List<SelectListItem> list)
        {
            Entities context = new Entities();

            list.Add(new SelectListItem { Text= context.Электронная_амбулаторная_карта.Select(x=> x.Фамилия ).ToString(), Value= context.Электронная_амбулаторная_карта.Select(x => x.Номер_амбулаторной_карты).ToString() });
            //list.Add(new SelectListItem { Text = "2", Value = "2" });
            //list.Add(new SelectListItem { Text = "3", Value = "3" });
            return list;
        }
    }
}
