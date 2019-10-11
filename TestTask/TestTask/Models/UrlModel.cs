using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;

namespace TestTask.Models
{
    public class UrlModel
    {
        public string url;
        public string requestTime;

        public UrlModel(string domain, string sufix)
        {
            url = sufix;
            //requestTime = todo time of respond 00:00:00
        }
    }
}