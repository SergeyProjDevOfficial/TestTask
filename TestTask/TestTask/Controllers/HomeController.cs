using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTask.Models;
using TestTask.Models.Core;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        // ViewBag.url - string url
        // ViewBag.error - string if url is invalid
        // ViewBag.siteMap - List<string>
        // ViewBag.scans - count of history


        public ActionResult Index()
        {
            ViewBag.url = "";
            ViewBag.error = "";
            ViewBag.siteMap = "";

            DbHelper Db = new DbHelper();
            ViewBag.scans = Db.GetScans();
            

            return View();
        }


        [HttpPost]
        public ActionResult Index(string url)
        {
            ViewBag.url = "";
            ViewBag.siteMap = "";

            SiteMapGetter SMgetter = new SiteMapGetter();
            UrlValidator UrlValidator = new UrlValidator();
            DbHelper Db = new DbHelper();

            if (!UrlValidator.IsUrlValid(url)){
                ViewBag.error = "URL is invalid!";
                ViewBag.scans = Db.GetScans();
                return View();
            }

            ViewBag.url = url;

            // init siteMap
            List<UrlModel> siteMap = SMgetter.GetSiteMap(url);
            ViewBag.siteMap = siteMap;

            // add new to DB, get them
            Db.Add(url, siteMap);
            ViewBag.scans = Db.GetScans();

            return View();
        }

        [HttpPost]
        public ActionResult History(string history) 
        {
            ViewData["url-time"] = history;

            string historyUrl = history.Substring(0, history.IndexOf(" - "));
            string historyTime = history.Substring( history.IndexOf(" - ") + 3);

            DbHelper Db = new DbHelper();
            int index = Db.GetScanNumber(historyUrl, historyTime);
            ViewBag.siteMap = Db.GetSiteMap(index);

            return View();
        }
    }
}