using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Models;

namespace ПРАКТИКА_MVC_ПРИЛОЖЕНИЕ.Controllers
{
    public class CheckupController : Controller
    {
        public static IEnumerable<SelectListItem> PopulateDoctors()
        {
            Entities dbModel = new Entities();
            IEnumerable<SelectListItem> selectListItems = (dbModel.Идентификатор_врача.Select(p => new SelectListItem
            {
                Text = "№ " + p.Идентификатор_врача1.ToString() + " | " + p.Врач.Фамилия_врача + " " + p.Врач.Имя_врача + " " + p.Врач.Отчество_врача + " " + p.Специализация_врача.Специальность,
                Value = p.Идентификатор_врача1.ToString()
            }).ToList());
            return selectListItems;
        }

        public static IEnumerable<SelectListItem> PopulatePatients()
        {
            Entities dbModel = new Entities();
            IEnumerable<SelectListItem> selectListItems = (dbModel.Электронная_амбулаторная_карта.Select(p => new SelectListItem
            {
                Text = "№ " + p.Номер_амбулаторной_карты.ToString() + " | " + p.Фамилия + " " + p.Имя + " " + p.Отчество+", " + p.Дата_рождения+ ", " + p.Пол,
                Value = p.Номер_амбулаторной_карты.ToString()
            }).ToList());
            return selectListItems;
        }

        Entities dbModel = new Entities();
        public ActionResult Index()
        {

            return View(dbModel.Осмотр_пациента.ToList()) ;
        }

        // GET: Checkup/Details/5
        public ActionResult Details(int id)
        {
            return View(dbModel.Осмотр_пациента.Where(x=>x.Номер_осмотра==id).FirstOrDefault());
        }

        // GET: Checkup/Create
        public ActionResult Create()
        {
            var checkup = new Осмотр_пациента
            {
                PatientList = PopulatePatients(),
                DoctorList = PopulateDoctors()
            };
            return View(checkup);
        }

        // POST: Checkup/Create
        [HttpPost]
        public ActionResult Create(Осмотр_пациента checkup)
        {
            try
            {
                //checkup.Диагнозы_за_осмотр.Select(m => m.Диагноз_по_МКБ_10);
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Checkup/Edit/5
        public ActionResult Edit(int id)
        {
            Осмотр_пациента checkup = new Осмотр_пациента();
            var edit = dbModel.Осмотр_пациента.Where(x => x.Номер_осмотра == id).FirstOrDefault();
            checkup.Электронная_амбулаторная_карта = edit.Электронная_амбулаторная_карта;
            checkup.Идентификатор_врача1 = edit.Идентификатор_врача1;
            checkup.DoctorList = edit.DoctorList;
            checkup.Дата_приема = edit.Дата_приема;
            checkup.Повторный_прием = edit.Повторный_прием;
            checkup.Цель_посещения = edit.Цель_посещения;
            checkup.Диагнозы_за_осмотр = edit.Диагнозы_за_осмотр;
            checkup.Назначения_препаратов = edit.Назначения_препаратов;
           
            //ViewBag.Осмотр_пациента = checkup;
            return View(checkup);
        }

        // POST: Checkup/Edit/5
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

        // GET: Checkup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Checkup/Delete/5
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
