using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace GoogleFeed.UI.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/
        public ActionResult Index()
        {
            var productsSample = SampleProductsGenerator.Generate(400000);
            Func<IEnumerable<Dictionary<string, string>>> content = () => new SampleProductToDictionaryTransformer().Transform(productsSample);

            RSS20ContentProvider c = new RSS20ContentProvider(new FileProvider(), content, "My Sample Feed", "Sample Feed Description for Google Shopping", "http://my.feed.sample.com/feed/products");
            var file = c.GetFilledFile();

            using (StreamReader sr = new StreamReader(file))
            {
                var s = sr.ReadToEnd();
                return this.Content(s, @"text/xml");
            }
        }
    }
}
