using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class SiteMapGetter
    {
        private string mainSiteUrl;
        private List<UrlModel> siteMap;


        public SiteMapGetter()
        {
            siteMap = new List<UrlModel>();
        }


        public List<UrlModel> GetSiteMap(string url)
        {
            mainSiteUrl = GetSiteMainUrl(url);

            //waiting for refult
            IDocument document = Task.Run(async () => await GetDocument(url)).Result;

            var items =

                //query to select all hrefs
                from item in document.QuerySelectorAll("a")
                select item.GetAttribute("href");


            // add to list all items, which start with '/', not added yet
            foreach (var item in items)
            {
                if ((item != null) && (item.Length > 1) && (item[0] == '/') && (!siteMap.Any(obj => obj.url == item)))
                {
                    siteMap.Add(new UrlModel(mainSiteUrl, item));
                }
            }

            return siteMap;
        }


        private async Task<IDocument> GetDocument(string link)
        {
            // Setup the configuration to support document loading
            IConfiguration config = Configuration.Default.WithDefaultLoader();

            // Asynchronously get the document in a new context using the configuration
            return await BrowsingContext.New(config).OpenAsync(link);
        }


        private string GetSiteMainUrl(string url)
        {
            return url.Substring(0, url.IndexOf('/', url.IndexOf('/', url.IndexOf('/') + 1) + 1));
        }
        
        private string GetSiteSubUrl(string url)
        {
            return url.Substring(url.IndexOf('/', url.IndexOf('/', url.IndexOf('/') + 1) + 1));
        }
    }
}