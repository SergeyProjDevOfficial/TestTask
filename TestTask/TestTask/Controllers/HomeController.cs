using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        // ViewBag.url - string url
        // ViewBag.error - string if url is invalid
        // ViewBag.siteMap - List<string>

        public ActionResult Index()
        {
            ViewBag.url = "";
            ViewBag.error = "";
            ViewBag.siteMap = "";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string url)
        {
            ViewBag.url = url;
            ViewBag.siteMap = "";

            SiteMapGetter SMgetter = new SiteMapGetter();
            UrlValidator urlValidator = new UrlValidator();

            if (!urlValidator.IsUrlValid(url)){
                ViewBag.error = "URL is invalid!";
                return View();
            }

            ViewBag.siteMap = SMgetter.GetSiteMap(url);

            return View();
        }
    }
}