using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionManagement
{
    public class Logger
    {              
        public string ActivityID { get; set; }

         public log4net.ILog Log { get; set; }

         public Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        public void WriteError(object msg)
        {
            Log.Error(string.Format("{0} : {1}",ActivityID ,msg));
        }

        public void WriteError(object msg, Exception ex)
        {
            Log.Error(string.Format("{0} : {1}", ActivityID, msg), ex);
        }

        public void WriteError(string msg, Exception ex)
        {
            Log.Error(string.Format("{0} : {1}", ActivityID, msg), ex);
        }

        public void WriteInformation(object msg)
        {
            Log.Info(string.Format("{0} : {1}", ActivityID, msg));
        }

        public void WriteWarning(object msg)
        {
            Log.Warn(string.Format("{0} : {1}", ActivityID, msg));
        }
        public void WriteFatal(object msg)
        {
            Log.Fatal(string.Format("{0} : {1}", ActivityID, msg));
        }
        public void WriteFatal(object msg, Exception ex)
        {
            Log.Fatal(string.Format("{0} : {1}", ActivityID, msg), ex);
        }
    }
}
