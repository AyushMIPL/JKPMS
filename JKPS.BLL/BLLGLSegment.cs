using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLGLSegment
    {
    
        /// <summary>
        /// This method is use to get Account type information from database on the basses of account type Id
        /// </summary>
        /// <param name="id">Account type is is passed as a parameter</param>
        /// <returns>return a datatable having Account type information</returns>
        public static DataTable GetSegmentDetails(int id)
        {
            object[] parameters = new object[1];
            parameters[0] = id;
            List<DVOGLSegment> objGLSegmentlist = new List<DVOGLSegment>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataTable dt = new DataTable();
            dt = objDalBaseClass.GetDataTable(ref parameters, typeof(DVOGLSegment));
            return dt;
        }
        /// <summary>
        /// Delete record from detail table on the basis of key field value
        /// </summary>
        /// <param name="foreignkeyvalue">key field value</param>
        /// <returns>interger variable for confirmation</returns>
        public static object DeleteAccountType(ref DVOGLSegment objGLSegment)
        {
            object[] parameters = new object[1];
            parameters[0] = objGLSegment.strucid;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOGLSegment> objSecAccountTypeList = new List<DVOGLSegment>();
            object obj = objDalBaseClass.DeleteData(ref parameters, typeof(DVOGLSegment),true);
            return obj;

        }

        /// <summary>
        /// Update record into parent table on the basis of key field value
        /// </summary>
        /// <param name="foreignkeyvalue">key field value</param>
        /// <returns>interger variable for confirmation</returns>
        public static int UpdateAccountType(object foreignkeyvalue)
        {
            object[] parameters = new object[1];
            parameters[0] = foreignkeyvalue;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOGLSegment> objSecAccountTypeList = new List<DVOGLSegment>();
            int c = objDalBaseClass.UpdateData(ref parameters, typeof(DVOGLSegment));
            return c;

        }

    }
}