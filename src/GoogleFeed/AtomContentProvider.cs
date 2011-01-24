using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GoogleFeed
{
    public class AtomContentProvider : ContentProvider
    {
        private string _feedTitle;
        private string _feedLink;
        private string _feedAuthor;
        private string _feedId;

        public AtomContentProvider(IFileProvider fileProvider, Func<IEnumerable<Dictionary<string, string>>> getContentData, string feedTitle, string feedLink, string feedAuthor, string feedId)
            : base(fileProvider, getContentData)
        {
            this._feedTitle = feedTitle;
            this._feedAuthor = feedAuthor;
            this._feedLink = feedLink;
            this._feedId = feedId;
        }

        protected override string ItemName
        {
            get
            {
                return "entry";
            }
        }

        protected override void WriteXmlHeader(Stream streamToFill)
        {
            streamToFill.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            streamToFill.WriteLine("<feed xmlns=\"http://www.w3.org/2005/Atom\" xmlns:g=\"http://base.google.com/ns/1.0\">");
            streamToFill.WriteLine(String.Format("<title>{0}</title>", _feedTitle));
            streamToFill.WriteLine(String.Format("<link rel=\"self\" href=\"{0}\"/>", _feedLink));
            streamToFill.WriteLine(String.Format("<updated>{0}</updated>", getFormatedDate(DateTime.Now)));
            streamToFill.WriteLine("<author>");
            streamToFill.WriteLine(String.Format("<name>{0}</name>", _feedAuthor));
            streamToFill.WriteLine("</author>");
            streamToFill.WriteLine(String.Format("<id>{0}</id>", _feedId));
        }

        private string getFormatedDate(DateTime value)
        {
            return String.Format("{0}-{1}-{2}T{3}:{4}:{5}Z",
                value.Year,
                value.Month.ToString().PadLeft(2, '0'),
                value.Day.ToString().PadLeft(2, '0'),
                value.Hour,
                value.Minute,
                value.Second);
        }

        protected override void WriteXmlFooter(Stream streamToFill)
        {
            streamToFill.WriteLine("</feed>");
        }
    }
}
