using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumTransApp;
using NumTransApp.Controllers;
using NumTransApp.Models;

namespace NumTransApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            NumberModel response = (NumberModel) result.Model;

            Assert.IsNull(response);
        }

        [TestMethod]
        public void IndexPost()
        {
            HomeController controller = new HomeController();

            NumberModel request = new NumberModel();
            request.Number = "123.45";

            ViewResult result = controller.Index(request) as ViewResult;

            NumberModel response = (NumberModel) result.Model;

            Assert.AreEqual("123.45", response.Number);
            Assert.AreEqual("One Hundred and Twenty Three Dollars and Fourty Five Cents", response.TransformedNumber);
            Assert.IsFalse(controller.ModelState.ContainsKey("Number"));
        }

        [TestMethod]
        public void IndexPostNegative()
        {
            HomeController controller = new HomeController();

            NumberModel request = new NumberModel();
            request.Number = "-123.45";

            ViewResult result = controller.Index(request) as ViewResult;

            NumberModel response = (NumberModel)result.Model;

            Assert.AreEqual("-123.45", response.Number);
            Assert.IsNull(response.TransformedNumber);
            Assert.IsTrue(controller.ViewData.ModelState.ContainsKey("Number"));
        }

    }
}
