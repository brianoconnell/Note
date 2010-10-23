using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Note.Filters
{
    public class WhitespaceFilter : Stream
    {

        public WhitespaceFilter(Stream sink)
        {
            this.sink = sink;
        }

        private readonly Stream sink;
        private static readonly Regex reg = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}");

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            sink.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return sink.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return sink.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            sink.SetLength(value);
        }

        public override void Close()
        {
            sink.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            var html = System.Text.Encoding.Default.GetString(buffer);

            html = reg.Replace(html, string.Empty);

            var outdata = System.Text.Encoding.Default.GetBytes(html);
            sink.Write(outdata, 0, outdata.GetLength(0));
        }

    }
}