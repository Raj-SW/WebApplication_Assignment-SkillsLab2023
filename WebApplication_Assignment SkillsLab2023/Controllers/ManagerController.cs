﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.Custom;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        //[CustomAuthorizationAttribute("Manager")]
        public ActionResult Index()
        {
            return View();
        }
    }
}