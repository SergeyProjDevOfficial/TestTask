using AngleSharp;
using AngleSharp.Dom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class SiteMapGetter
    {
        private string mainSiteUrl;
        private List<UrlModel> siteMapWithRespondTimes;
        private List<string> urls;


        public SiteMapGetter()
        {
            urls = new List<string>();
            siteMapWithRespondTimes = new List<UrlModel>();
        }


        public List<UrlModel> GetSiteMap(string url)
        {
            mainSiteUrl = GetSiteMainUrl(url);
            
            // Getting all site map (begin)
            /*
                urls.Add(url);

                int counter = 0;
                while (true)
                {
                    try
                    {
                        AddToUrlsListAllHrefsOnPage(urls[counter]);
                    } 
                    catch // out of range => list ended
                    {
                        break;
                    }

                    counter++;
                }
                */
            // Getting all site map (end)

            AddToUrlsListAllHrefsOnPage(url);

            foreach (var item in urls)
                siteMapWithRespondTimes.Add(new UrlModel(item));

            // sort to slowest mid time on top
            return siteMapWithRespondTimes.OrderBy(o => o.requestTimeMid).Reverse().ToList();
        }


        private async Task<IDocument> GetDocument(string link)
        {
            // Setup the configuration to support document loading
            IConfiguration config = Configuration.Default.WithDefaultLoader();

            // Asynchronously get the document in a new context using the configuration
            return await BrowsingContext.New(config).OpenAsync(link);
        }

        private bool AddToUrlsListAllHrefsOnPage(string url)
        {
            // if (1 or more urls added) flag = true 
            bool flag = false;

            // waiting for refult
            IDocument document = Task.Run(async () => await GetDocument(url)).Result;

            var items =

                // query to select all hrefs
                from item in document.QuerySelectorAll("a")
                select item.GetAttribute("href");

            // selecting only current site urls
            foreach (var item in items)
            {
                if ((item != null) && 
                    (item.Length > 1) && 
                    (item[0] == '/') && 
                    (!urls.Contains(mainSiteUrl + item))
                   )
                {
                    urls.Add(mainSiteUrl + item);
                    flag = true;
                }
            }

            return flag;
        }

        private string GetSiteMainUrl(string url)
        {
            try
            {
                return url.Substring(0, url.IndexOf('/', url.IndexOf('/', url.IndexOf('/') + 1) + 1));
            }
            catch
            {
                return url;
            }
        }
    }
}