using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumTransApp.Models;
using NumTransApp.Logics;

namespace NumTransApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include="Number")] NumberModel m)
        {
            try
            {
                m.TransformedNumber = NumberTransfomer.transform(m.Number);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Number", ex.Message); 
            } 

            return View(m);
        }
    }
}
