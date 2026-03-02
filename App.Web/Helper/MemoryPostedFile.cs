using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Web.Helper
{
    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] _fileBytes;
        private readonly string _fileName;
        private readonly string _contentType;

        public MemoryPostedFile(byte[] fileBytes, string fileName, string contentType)
        {
            _fileBytes = fileBytes;
            _fileName = fileName;
            _contentType = contentType;
        }

        public override int ContentLength
        {
            get { return _fileBytes.Length; }
        }

        public override string FileName
        {
            get { return _fileName; }
        }

        public override Stream InputStream
        {
            get { return new MemoryStream(_fileBytes); }
        }

        public override string ContentType
        {
            get { return _contentType; }
        }
    }

}
