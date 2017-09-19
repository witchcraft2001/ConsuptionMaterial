using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace ConsuptionMaterials.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Учет расхода материалов";

            string[] verinfo = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
            DateTime date = new DateTime(2000, 1, 1);
            bool check = false;
            try
            {
                //int days = Convert.ToInt32(verinfo[2]);
                date = date.AddDays(Convert.ToInt32(verinfo[2]));
                date = date.AddSeconds(Convert.ToInt32(verinfo[3]) * 2);
                check = true;
            }
            catch { }

            ViewBag.Version = verinfo[0] + "." + verinfo[1] + (check ? (" от " + date.ToString()) : "");

            return View();
        }

    }
}