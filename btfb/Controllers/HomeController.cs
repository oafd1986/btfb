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
            ViewBag.Message = @"EasyShip Brokers, Inc. is a family owned company. Our family has been servicing the nations south transporting vehicles from coast     to coast since 2011. Working formerly as car carriers, we decided to switch gears and work the brokers side of the business. This change was a result of hearing so many stories about the business itself and what it’s like out there. We realized it was time for a better broker company and decided to start one. With the expertise and experience of our past endeavors and our know-how of the business, we offer you the best broker service possible. We are the best when it comes to price, with rapid service and amazing customer service! We service the 48 contiguous states as brokers for carriers, flat beds, drop decks and refrigerated cargo.
                                 Our goal continues to be as it always has been, regardless of which side of the business we were focused, to provide the easiest and fastest service for all of your shipping needs. Customer satisfaction guaranteed! ";

            return View();
        }

        public ActionResult Contact()
        {
           // ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}