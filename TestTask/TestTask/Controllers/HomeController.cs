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
        // ViewBag.scans - count of history

        ScanContext db;


        public ActionResult Index()
        {
            ViewBag.url = "";
            ViewBag.error = "";
            ViewBag.siteMap = "";
            ViewBag.scans = "";

            return View();
        }


        [HttpPost]
        public ActionResult Index(string url)
        {
            db = new ScanContext();

            ViewBag.url = "";
            ViewBag.siteMap = "";
            ViewBag.scans = "";

            SiteMapGetter SMgetter = new SiteMapGetter();
            UrlValidator urlValidator = new UrlValidator();

            if (!urlValidator.IsUrlValid(url)){
                ViewBag.error = "URL is invalid!";
                return View();
            }

            // init url
            ViewBag.url = url;

            // init siteMap
            List<UrlModel> siteMap = SMgetter.GetSiteMap(url);
            ViewBag.siteMap = siteMap;

            // addind new values to db
            int Id = 0;
            try { Id = db.scans.Max(u => u.idUrl)+1;} catch { Id = 0; }

            db.scans.Add(new Scan {idUrl = Id+1, ScanningUrl = url, Date = DateTime.Now.ToString() }) ;

            for (int i = 0; i < siteMap.Count; i++)
            {
                db.scansResults.Add(new ScanResult { 
                    id = Id,
                    idUrl = Id,
                    SubUrl = siteMap[i].url,
                    MinResponseTime = siteMap[i].requestTimeMin,
                    MidResponseTime = siteMap[i].requestTimeMid,
                    MaxResponseTime = siteMap[i].requestTimeMax,
                });
            }
            db.SaveChanges();
            db.Dispose();

            ViewBag.scans = db.scans;

            return View();
        }
    }
}