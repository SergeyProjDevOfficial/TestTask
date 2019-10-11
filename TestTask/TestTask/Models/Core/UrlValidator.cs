using System;
using System.Net;

namespace TestTask.Models
{
    public class UrlValidator
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
    }
}