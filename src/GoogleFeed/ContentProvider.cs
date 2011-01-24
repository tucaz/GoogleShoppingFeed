using System;
using System.Collections.Generic;
using System.IO;

namespace GoogleFeed
{
    public class ContentProvider
    {
        private IFileProvider _fileProvider;        
        private Func<IEnumerable<Dictionary<string, string>>> _getContentData;

        public ContentProvider(IFileProvider fileProvider, Func<IEnumerable<Dictionary<string, string>>> getContentData)
        {
            this._fileProvider = fileProvider;
            this._getContentData = getContentData;
        }

        protected virtual string ItemName
        {
            get
            {
                return "item";
            }
        }

        public Stream GetFilledFile()
        {
            bool isFilewNew = true;

            var streamToFill = _fileProvider.GetWriter(out isFilewNew);

            if (isFilewNew)
            {
                WriteXmlHeader(streamToFill);
                
                var content = _getContentData();

                foreach (var item in content)
                {
                    streamToFill.WriteLine(String.Format("<{0}>", ItemName));

                    foreach (var prop in item.Keys)
                    {
                        streamToFill.WriteLine(String.Format("<{0}>{1}</{0}>", prop, item[prop]));
                    }

                    streamToFill.WriteLine(String.Format("</{0}>", ItemName));
                }

                WriteXmlFooter(streamToFill);
            }

            return streamToFill;
        }

        protected virtual void WriteXmlHeader(Stream streamToFill)
        {
            streamToFill.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            streamToFill.WriteLine("<root>");
        }

        protected virtual void WriteXmlFooter(Stream streamToFill)
        {
            streamToFill.WriteLine("</root>");
        }       
    }
}
