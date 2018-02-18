using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using btfb.Models.DbAccessModel;
using btfb.Models.DataAccessClasses;

namespace btfb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = @"BTFB, Inc. is a family owned company. Our family has been servicing the nations south transporting vehicles from coast to coast since 2011 as Yes Auto Transport, Inc., our sister car carrier company.
                            After hearing so many stories about the business itself and what it’s like out there, we realized it was time for a better broker company and decided to start one. With the expertise and experience Yes Auto Transport, Inc. offers and our know-how of the business, we offer the best broker service possible. We are the best in terms of price, our rapid service and our amazing customer service!
                            We service the 48 contiguous states as brokers for carriers, flat beds, drop decks and refrigerated cargo.";

            return View();
        }

        public ActionResult Contact()
        {
           // ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}