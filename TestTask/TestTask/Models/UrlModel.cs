using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace TestTask.Models
{
    public class UrlModel
    {
        public string url;
        public string requestTimeMin;
        public string requestTimeMax;
        public string requestTimeMid;


        public UrlModel(string url)
        {
            this.url = url;

            List<TimeSpan> times = GetRequestsTimes(url);

            requestTimeMin = GetMin(times);
            requestTimeMax = GetMax(times);
            requestTimeMid = GetMid(times);

            return;
        }


        // 10 iterations
        private List<TimeSpan> GetRequestsTimes(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            Stopwatch timer = new Stopwatch();
            List<TimeSpan> times = new List<TimeSpan>();

            for (int i = 0; i < 10; i++)
            {
                timer.Start();

                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    response.Close();
                }
                catch // no respond
                {
                    times.Add(TimeSpan.Parse("00:00:00"));
                    return times;
                }

                timer.Stop();

                times.Add(timer.Elapsed);
            }

            return times;
        }
   

        private string GetMin(List<TimeSpan> times)
        {
            string time = times.Min<TimeSpan>().ToString();
            if (time == "00:00:00")
                time = "No Respond";
            else
                time = time.Substring(time.LastIndexOf(':')+2);
            return time;
        }
        private string GetMax(List<TimeSpan> times)
        {
            string time = times.Max<TimeSpan>().ToString();
            if (time == "00:00:00") time = 
                    "No Respond";
            else
                time = time.Substring(time.LastIndexOf(':')+2);
            return time;
        }
        private string GetMid(List<TimeSpan> times)
        {
            double doubleAverageTicks = times.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            string time = new TimeSpan(longAverageTicks).ToString();

            if (time == "00:00:00") 
                time = "No Respond";
            else
                time = time.Substring(time.LastIndexOf(':')+2);
            return time;
        }
    }
}