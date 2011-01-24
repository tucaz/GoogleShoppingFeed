using System;
using System.IO;
using System.Text;

namespace GoogleFeed
{
    public static class StreamExtensions
    {
        public static void WriteLine(this Stream stream, string value)
        {
            var buffer = Encoding.UTF8.GetBytes(String.Concat(value, Environment.NewLine));
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
