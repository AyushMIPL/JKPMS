using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLTenderInttendr
    {
        /// <summary>
        /// To get Tenders based on some search criteria
        /// </summary>
        /// <param name="objDVOTreasuryBillIssueNumbersIntissur">DVO type object with required search criteria</param>
        /// <returns>list of DVOTenderInttendr type objects, if any</returns>
        public static List<DVOTenderInttendr> GetData(ref DVOTenderInttendr objDVOTenderInttendr)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOTenderInttendr> listDVOTenderInttendr = new List<DVOTenderInttendr>();

            try
            {
                object[] parameters = new object[9];
                parameters[0] = objDVOTenderInttendr.RowID;
                parameters[1] = objDVOTenderInttendr.tend_code;
                parameters[2] = objDVOTenderInttendr.tend_name;
                parameters[3] = objDVOTenderInttendr.address1;
                parameters[4] = objDVOTenderInttendr.address2;
                parameters[5] = objDVOTenderInttendr.contact;
                parameters[6] = objDVOTenderInttendr.phone;
                parameters[7] = objDVOTenderInttendr.fax;
                parameters[8] = objDVOTenderInttendr.class_code;

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOTenderInttendr)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOTenderInttendr tobjDVOTenderInttendr = new DVOTenderInttendr())
                                {
                                    tobjDVOTenderInttendr.RowID = (dr[0] == DBNull.Value) ? 0 : Convert.ToInt32(dr[0]);//p_RowID
                                    tobjDVOTenderInttendr.tend_code = (dr[1] == DBNull.Value) ? string.Empty : dr[1].ToString().Trim();//p_tend_code
                                    tobjDVOTenderInttendr.tend_name = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//p_tend_name
                                    tobjDVOTenderInttendr.address1 = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();//p_address1
                                    tobjDVOTenderInttendr.address2 = (dr[4] == DBNull.Value) ? string.Empty : dr[4].ToString().Trim();//p_address2
                                    tobjDVOTenderInttendr.contact = (dr[5] == DBNull.Value) ? string.Empty : dr[5].ToString().Trim();//p_contact
                                    tobjDVOTenderInttendr.phone = (dr[6] == DBNull.Value) ? string.Empty : dr[6].ToString().Trim();//p_phone
                                    tobjDVOTenderInttendr.fax = (dr[7] == DBNull.Value) ? string.Empty : dr[7].ToString().Trim();//p_fax
                                    tobjDVOTenderInttendr.class_code = (dr[8] == DBNull.Value) ? string.Empty : dr[8].ToString().Trim();//p_class_code

                                    listDVOTenderInttendr.Add(tobjDVOTenderInttendr);
                                }
                            }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOTenderInttendr;
        }

        /// <summary>
        /// To get Tenders information
        /// </summary>
        /// <returns>list of DVOTenderInttendr type objects, if any</returns>
        public static List<DVOTenderInttendr> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOTenderInttendr> listDVOTenderInttendr = new List<DVOTenderInttendr>();

            try
            {
                using (DataSet ds = objDALBaseClass.GetAllData(typeof(DVOTenderInttendr)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOTenderInttendr tobjDVOTenderInttendr = new DVOTenderInttendr())
                                {
                                    tobjDVOTenderInttendr.RowID = (dr[0] == DBNull.Value) ? 0 : Convert.ToInt32(dr[0]);//p_RowID
                                    tobjDVOTenderInttendr.tend_code = (dr[1] == DBNull.Value) ? string.Empty : dr[1].ToString().Trim();//p_tend_code
                                    tobjDVOTenderInttendr.tend_name = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//p_tend_name
                                    tobjDVOTenderInttendr.address1 = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();//p_address1
                                    tobjDVOTenderInttendr.address2 = (dr[4] == DBNull.Value) ? string.Empty : dr[4].ToString().Trim();//p_address2
                                    tobjDVOTenderInttendr.contact = (dr[5] == DBNull.Value) ? string.Empty : dr[5].ToString().Trim();//p_contact
                                    tobjDVOTenderInttendr.phone = (dr[6] == DBNull.Value) ? string.Empty : dr[6].ToString().Trim();//p_phone
                                    tobjDVOTenderInttendr.fax = (dr[7] == DBNull.Value) ? string.Empty : dr[7].ToString().Trim();//p_fax
                                    tobjDVOTenderInttendr.class_code = (dr[8] == DBNull.Value) ? string.Empty : dr[8].ToString().Trim();//p_class_code

                                    listDVOTenderInttendr.Add(tobjDVOTenderInttendr);
                                }
                            }
                }
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOTenderInttendr;
        }
    }
}
