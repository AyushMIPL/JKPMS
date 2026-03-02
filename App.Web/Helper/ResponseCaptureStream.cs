using System.IO;
using System.Text;

namespace App.Web.Helper
{
    public class ResponseCaptureStream : Stream
    {
        private readonly Stream _responseStream;
        private readonly MemoryStream _captureStream;

        public ResponseCaptureStream(Stream responseStream)
        {
            _responseStream = responseStream;
            _captureStream = new MemoryStream();
        }

        public string GetCapturedContent()
        {
            return Encoding.UTF8.GetString(_captureStream.ToArray());
        }

        // Override required Stream methods and properties
        public override bool CanRead => _responseStream.CanRead;
        public override bool CanSeek => _responseStream.CanSeek;
        public override bool CanWrite => _responseStream.CanWrite;
        public override long Length => _responseStream.Length;
        public override long Position { get => _responseStream.Position; set => _responseStream.Position = value; }
        public override void Flush() => _responseStream.Flush();
        public override int Read(byte[] buffer, int offset, int count) => _responseStream.Read(buffer, offset, count);
        public override long Seek(long offset, SeekOrigin origin) => _responseStream.Seek(offset, origin);
        public override void SetLength(long value) => _responseStream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count)
        {
            _captureStream.Write(buffer, 0, count); // Capture the output
            _responseStream.Write(buffer, 0, count); // Write to the original stream
        }
    }

}
