﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "test site for the .NET Asana API.";

			return View();
		}
	}
}
