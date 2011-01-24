using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GoogleFeed
{
    public class RSS20ContentProvider : ContentProvider
    {
        private string _feedTitle;
        private string _feedDescription;
        private string _feedLink;
        
        public RSS20ContentProvider(IFileProvider fileProvider, Func<IEnumerable<Dictionary<string, string>>> getContentData, string feedTitle, string feedDescription, string feedLink) : base(fileProvider, getContentData)
        {
            this._feedTitle = feedTitle;
            this._feedDescription = feedDescription;
            this._feedLink = feedLink;
        }

        protected override void WriteXmlHeader(Stream streamToFill)
        {
            streamToFill.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            streamToFill.WriteLine("<rss version =\"2.0\" xmlns:g=\"http://base.google.com/ns/1.0\">");
            streamToFill.WriteLine("<channel>");
            streamToFill.WriteLine(String.Format("<title>{0}</title>", _feedTitle));
            streamToFill.WriteLine(String.Format("<description>{0}</description>", _feedTitle));
            streamToFill.WriteLine(String.Format("<link>{0}</link>", _feedLink));
        }

        protected override void WriteXmlFooter(Stream streamToFill)
        {
            streamToFill.WriteLine("</channel>");
            streamToFill.WriteLine("</rss>");
        }
    }
}
