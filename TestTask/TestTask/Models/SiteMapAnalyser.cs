using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

namespace TestTask.Models
{
    public class SiteMapAnalyser
    {
        
        public bool IsUrlValid(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                string HTMLSource = wc.DownloadString(url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<string> GetSiteMap(string url)
        {
            new NotImplementedException();
        }

    }
}