using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class RelationController : Controller
    {
        Entities dbModel = new Entities();

        public static List<SelectListItem> PopulateCategories()
        {
            Entities dbModel = new Entities();
            List<SelectListItem> selectListItems = (from p in dbModel.Родственная_связь.AsEnumerable()
                                                    select new SelectListItem
                                                    { 
                                                      Text=  p.Название_родственной_связи,
                                                      Value = p.Код_родства__свойства.ToString()

                                                    }).ToList() ;
            selectListItems.Insert(0, new SelectListItem { Text = "--ABobA--", Value = "" });
            return selectListItems;
        }

        public static IEnumerable<SelectListItem> PopulateRelations()
        {
            Entities dbModel = new Entities();
            IEnumerable<SelectListItem> selectListItems = (dbModel.Родственная_связь.Select(p=> new SelectListItem
                                                    {
                                                        Text = p.Название_родственной_связи,
                                                        Value = p.Код_родства__свойства.ToString()
                                                    }).ToList());
            return selectListItems;
        }
        public static IEnumerable<SelectListItem> PopulatePatients()
        {
            Entities dbModel = new Entities();
            IEnumerable<SelectListItem> selectListItems = (dbModel.Электронная_амбулаторная_карта.Select(p => new SelectListItem
            {
                Text = "№ " + p.Номер_амбулаторной_карты.ToString() + " | " + p.Фамилия + " " + p.Имя + " " + p.Отчество,
                Value = p.Номер_амбулаторной_карты.ToString()
            }).ToList());
            return selectListItems;
        }

        public static IEnumerable<SelectListItem> PopulateParents()
        {
            Entities dbModel = new Entities();
            IEnumerable<SelectListItem> selectListItems = (dbModel.Карточка_представителя.Select(p => new SelectListItem
            {
                Text = "№ " + p.Код_представителя.ToString() + " | " + p.Фамилия_представителя +" "+ p.Имя_представителя + " " + p.Отчество_представителя,
                Value = p.Код_представителя.ToString()
            }).ToList());
            return selectListItems;
        }
        public ActionResult Index()
        {
            return View(dbModel.Связь_пациента_с_представителем.ToList());
        }

        // GET: Relation/Details/5
        public ActionResult Details(int id)
        {
            return View(dbModel.Связь_пациента_с_представителем.Where(x=>x.Код_родственной_связи==id).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var rel = new Связь_пациента_с_представителем
            {
                CategoryList = PopulateRelations(),
                PatientList= PopulatePatients(),
                ParentList=PopulateParents()
            };
            return View(rel);
        }

        // POST: Relation/Create
        [HttpPost]
        public ActionResult Create(Связь_пациента_с_представителем rel)
        {
            ViewData["Родственная_связь"] = new SelectList(dbModel.Родственная_связь, "Код_родства__свойства", "Название_родственной_связи");

            if (!ModelState.IsValid)
            {
                try
                {
                    //rel.CategoryList = new SelectList(dbModel.Родственная_связь, "Код_родства__свойства", "Название_родственной_связи"); // add this
                    // return View(rel);

                    rel.PatientList = PopulatePatients();
                    return View(rel);
                    //return RedirectToAction("Index");
                }
                catch
                {
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Ошибка! Проверьте данные на дублирование или обратитесь в техническую поддержку!');</script>");
                }

            }
            else
            { 
                //save to db
            }

            return View();

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Связь_пациента_с_представителем pat = new Связь_пациента_с_представителем();
            pat.PatientList = PopulatePatients();
            pat.ParentList = PopulateParents();
            pat.CategoryList = PopulateRelations();
            //ViewBag.Связь_пациента_с_представителем = rel;
            //SelectList pat = new SelectList(dbModel.Электронная_амбулаторная_карта, "Номер_амбулаторной_карты", "Фамилия");
            //SelectList par = new SelectList(dbModel.Карточка_представителя, "", "Фамилия");

            ViewBag.Связь_пациента_с_представителем = pat;
            // return View(dbModel.Связь_пациента_с_представителем.Where(x => x.Код_родственной_связи == id&&x.Код_родственной_связи==pat.Код_родственной_связи).FirstOrDefault());
            return View(pat);
        }

        // POST: Relation/Edit/5
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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Relation/Delete/5
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
