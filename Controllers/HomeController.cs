﻿using DekanatWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DekanatWeb.Controllers
{
    public class HomeController : Controller
    {

        DekanatDBEntities1 db = new DekanatDBEntities1();
        public ActionResult Index()
        {
            List<FacultyViewModel> facultyList = db.факультет.Select(x => new FacultyViewModel {

            FacultyID = x.FacultyID,
            назва = x.назва
            
            }).ToList();
            return View(facultyList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}