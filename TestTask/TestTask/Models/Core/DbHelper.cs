using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace TestTask.Models.Core
{
    public class DbHelper
    {
        ScanContext db;
        public DbHelper()
        {
            db = new ScanContext();
        }

        public void Add(string url, List<UrlModel> siteMap)
        {
            // addind new values to db
            int Id = 1;
            try { Id = db.scans.Max(u => u.idUrl) + 1; } catch { Id = 1; }

            db.scans.Add(new Scan { idUrl = Id, ScanningUrl = url, Date = DateTime.Now.ToString() });

            for (int i = 0; i < siteMap.Count; i++)
            {
                db.scansResults.Add(new ScanResult
                {
                    idUrl = Id,
                    SubUrl = siteMap[i].url,
                    MinResponseTime = siteMap[i].requestTimeMin,
                    MidResponseTime = siteMap[i].requestTimeMid,
                    MaxResponseTime = siteMap[i].requestTimeMax,
                });
            }
            db.SaveChanges();
            db.Database.Connection.Close();
        }

        public DbSet<Scan> GetScans()
        {
            return db.scans;
        }

        public IEnumerable<ScanResult> GetSiteMap(int index)
        {
            return
                from b in db.scansResults
                where b.idUrl == index
                select b;
        }

        public int GetScanNumber(string url, string time)
        {
            var id =
                (from b in db.scans
                where (b.ScanningUrl.Contains(url) && b.Date.Contains(time))
                select b.idUrl);

            return Convert.ToInt32(id.FirstOrDefault());
        }
    }
}