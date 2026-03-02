using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLScreenNotesStxnoted
    {
        /// <summary>
        /// This method is use to get information from database based on search options
        /// </summary>
        /// <param name="pobjDVOstxnoted">A class object passed as parameter having parameter data</param>
        /// <returns>return a list having searched information</returns>
        public static List<DVOstxnoted> GetData(ref DVOstxnoted pobjDVOstxnoted)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOstxnoted> listDVOstxnoted = new List<DVOstxnoted>();
            try
            {
                object[] parameters = new object[5];
                parameters[0] = pobjDVOstxnoted.Rowid;
                parameters[1] = pobjDVOstxnoted.filename;
                parameters[2] = pobjDVOstxnoted.record_key;
                parameters[3] = pobjDVOstxnoted.line_no;
                parameters[4] = pobjDVOstxnoted.data;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOstxnoted)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOstxnoted objDVOstxnoted = new DVOstxnoted();
                        objDVOstxnoted.Rowid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                        objDVOstxnoted.filename = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
                        objDVOstxnoted.record_key = (dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty);
                        objDVOstxnoted.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                        objDVOstxnoted.data = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                        
                        listDVOstxnoted.Add(objDVOstxnoted);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return listDVOstxnoted;
            }
            return listDVOstxnoted;
        }

        /// <summary>
        /// To Insert Common Notes of screen
        /// </summary>
        /// <param name="listDVOstxnoted">list of DVO object with notes</param>
        /// <returns></returns>
        public static int InsertCommonNotes(ref Object objTransaction, ref List<DVOstxnoted> listDVOstxnoted)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {
                foreach (DVOstxnoted objDVOstxnoted in listDVOstxnoted)
                {
                    DVOstxnoted objtemp = objDVOstxnoted;
                    InsertCommonNotes(ref objTransaction, ref objtemp);
                }
                objDALBaseClass = null;

                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        /// <summary>
        /// To Insert Employee Notes
        /// </summary>
        /// <param name="objDVOstxnoted">DVO object with notes</param>
        /// <returns></returns>
        public static int InsertCommonNotes(ref Object objTransaction, ref DVOstxnoted objDVOstxnoted)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {
                object[] parameters = new object[4];
                #region parameters
                parameters[0] = objDVOstxnoted.filename;
                parameters[1] = objDVOstxnoted.record_key;
                parameters[2] = objDVOstxnoted.line_no;
                parameters[3] = objDVOstxnoted.data;
                #endregion parameters

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOstxnoted.INSERT_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        /// <summary>
        /// To Update Common Notes of screen
        /// </summary>
        /// <param name="listDVOstxnoted">list of DVO objects with notes</param>
        /// <returns></returns>
        public static int UpdateCommonNotes(ref object objTransaction, ref List<DVOstxnoted> listDVOstxnoted)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                foreach (DVOstxnoted objDVOstxnoted in listDVOstxnoted)
                {
                    DVOstxnoted objtemp = objDVOstxnoted;
                    if (objDVOstxnoted.line_no == -1)
                        //DeleteEmployeeNotes(ref objTransaction, ref objtemp);
                        DeleteCommonNotes(ref objTransaction, ref objtemp);
                    if (objDVOstxnoted.Rowid <= 0)
                        InsertCommonNotes(ref objTransaction, ref objtemp);
                    else
                        UpdateCommonNotes(ref objTransaction, ref objtemp);
                }
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        /// <summary>
        /// To Update Common Notes of screen
        /// </summary>
        /// <param name="objDVOstxnoted">DVO object with notes</param>
        /// <returns></returns>
        public static int UpdateCommonNotes(ref object objTransaction, ref DVOstxnoted objDVOstxnoted)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[5];
                parameters[0] = objDVOstxnoted.Rowid;
                parameters[1] = objDVOstxnoted.filename;
                parameters[2] = objDVOstxnoted.record_key;
                parameters[3] = objDVOstxnoted.line_no;
                parameters[4] = objDVOstxnoted.data;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOstxnoted.UPDATE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        /// <summary>
        /// To Delete Common Notes of screen
        /// </summary>
        /// <param name="objDVOstxnoted">DVO object with notes</param>
        /// <returns></returns>
        /// 
        public static int DeleteCommonNotes(ref object objTransaction, ref DVOstxnoted objDVOstxnoted)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVOstxnoted.Rowid;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOstxnoted.DELETE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();
                parameters = null;
                objDALBaseClass = null;

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }


        #region notusing

        ///// <summary>
        ///// To Insert Employee Notes
        ///// </summary>
        ///// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
        ///// <returns></returns>
        //public static int InsertEmployeeNotes(ref Object objTransaction, ref List<DVOstxnoted> listDVOstxnoted)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    try
        //    {
        //        //object[] parameters = new object[4];
        //        foreach (DVOstxnoted objDVOstxnoted in listDVOstxnoted)
        //        {
        //            DVOstxnoted objtemp = objDVOstxnoted;
        //            InsertEmployeeNotes(ref objTransaction, ref objtemp);

        //            //#region parameters
        //            //parameters[0] = objDVOstxnoted.filename;
        //            //parameters[1] = objDVOstxnoted.record_key;
        //            //parameters[2] = objDVOstxnoted.line_no;
        //            //parameters[3] = objDVOstxnoted.data;
        //            //#endregion parameters

        //            //object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOstxnoted.INSERT_SPNAME);
        //            //if (o == null)
        //            //    throw new Exception();
        //            //else if (Convert.ToInt32(o) < 1)
        //            //    throw new Exception();
        //        }
        //        //parameters = null;
        //        objDALBaseClass = null;

        //        if (!statusObjTransaction && objTransaction != null)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!statusObjTransaction && objTransaction != null)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        throw ex;
        //    }
        //    return 0;
        //}

        ///// <summary>
        ///// To Insert Employee Notes
        ///// </summary>
        ///// <param name="objDVOEmployeeStyemplr">DVO object with all information of employee</param>
        ///// <returns></returns>
        //public static int InsertEmployeeNotes(ref Object objTransaction, ref DVOstxnoted objDVOstxnoted)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    try
        //    {
        //        object[] parameters = new object[4];
        //        #region parameters
        //        parameters[0] = objDVOstxnoted.filename;
        //        parameters[1] = objDVOstxnoted.record_key;
        //        parameters[2] = objDVOstxnoted.line_no;
        //        parameters[3] = objDVOstxnoted.data;
        //        #endregion parameters

        //        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOstxnoted.INSERT_SPNAME);
        //        if (o == null)
        //            throw new Exception();
        //        else if (Convert.ToInt32(o) < 1)
        //            throw new Exception();

        //        parameters = null;
        //        objDALBaseClass = null;

        //        if (!statusObjTransaction && objTransaction != null)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!statusObjTransaction && objTransaction != null)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        throw ex;
        //    }
        //    return 0;
        //}

        ///// <summary>
        ///// To Update Employee Notes
        ///// </summary>
        ///// <param name="objDVOstxnoted">DVO object with notes of employee</param>
        ///// <returns></returns>
        //public static int UpdateEmployeeNotes(ref object objTransaction, ref List<DVOstxnoted> listDVOstxnoted)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    try
        //    {
        //        //List<DVOEmployeeInfoLogEmpUpdLog> listDVOEmployeeInfoLogEmpUpdLog = new List<DVOEmployeeInfoLogEmpUpdLog>();

        //        //object[] parameters = new object[5];
        //        foreach (DVOstxnoted objDVOstxnoted in listDVOstxnoted)
        //        {
        //            DVOstxnoted objtemp = objDVOstxnoted;
        //            if (objDVOstxnoted.line_no == -1)
        //                DeleteEmployeeNotes(ref objTransaction, ref objtemp);
        //            if (objDVOstxnoted.Rowid <= 0)
        //                InsertEmployeeNotes(ref objTransaction, ref objtemp);
        //            else
        //                UpdateEmployeeNotes(ref objTransaction, ref objtemp);

        //            //#region parameters
        //            //parameters[0] = objDVOstxnoted.Rowid;
        //            //parameters[1] = objDVOstxnoted.filename;
        //            //parameters[2] = objDVOstxnoted.record_key;
        //            //parameters[3] = objDVOstxnoted.line_no;
        //            //parameters[4] = objDVOstxnoted.data;
        //            //#endregion parameters

        //            ////if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
        //            ////    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

        //            //object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOstxnoted.UPDATE_SPNAME);
        //            //if (o == null)
        //            //    throw new Exception();
        //            //else if (Convert.ToInt32(o) < 1)
        //            //    throw new Exception();
        //        }
        //        //parameters = null;
        //        objDALBaseClass = null;

        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        throw ex;
        //    }
        //    return 0;
        //}

        ///// <summary>
        ///// To Update Employee Notes
        ///// </summary>
        ///// <param name="objDVOstxnoted">DVO object with notes of employee</param>
        ///// <returns></returns>
        //public static int UpdateEmployeeNotes(ref object objTransaction, ref DVOstxnoted objDVOstxnoted)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    try
        //    {
        //        //List<DVOEmployeeInfoLogEmpUpdLog> listDVOEmployeeInfoLogEmpUpdLog = new List<DVOEmployeeInfoLogEmpUpdLog>();

        //        object[] parameters = new object[5];
        //        //foreach (DVOstxnoted objDVOstxnoted in listDVOstxnoted)
        //        //{
        //        #region parameters
        //        parameters[0] = objDVOstxnoted.Rowid;
        //        parameters[1] = objDVOstxnoted.filename;
        //        parameters[2] = objDVOstxnoted.record_key;
        //        parameters[3] = objDVOstxnoted.line_no;
        //        parameters[4] = objDVOstxnoted.data;
        //        #endregion parameters

        //        //if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
        //        //    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

        //        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOstxnoted.UPDATE_SPNAME);
        //        if (o == null)
        //            throw new Exception();
        //        else if (Convert.ToInt32(o) < 1)
        //            throw new Exception();
        //        //}
        //        parameters = null;
        //        objDALBaseClass = null;

        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        throw ex;
        //    }
        //    return 0;
        //}

        ///// <summary>
        ///// To Delete Employee Notes
        ///// </summary>
        ///// <param name="objDVOstxnoted">DVO object with notes of employee</param>
        ///// <returns></returns>
        //public static int DeleteEmployeeNotes(ref object objTransaction, ref DVOstxnoted objDVOstxnoted)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    try
        //    {
        //        //List<DVOEmployeeInfoLogEmpUpdLog> listDVOEmployeeInfoLogEmpUpdLog = new List<DVOEmployeeInfoLogEmpUpdLog>();

        //        object[] parameters = new object[1];
        //        //foreach (DVOstxnoted objDVOstxnoted in listDVOstxnoted)
        //        //{
        //        #region parameters
        //        parameters[0] = objDVOstxnoted.Rowid;
        //        #endregion parameters

        //        //if (listDVOEmployeeInfoLogEmpUpdLog.Count > 0)
        //        //    BLLEmployeeInfoLogEmpupdlog.InsertEmployeeInformationLog(ref objTransaction, ref listDVOEmployeeInfoLogEmpUpdLog);

        //        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOstxnoted.DELETE_SPNAME);
        //        if (o == null)
        //            throw new Exception();
        //        else if (Convert.ToInt32(o) < 1)
        //            throw new Exception();
        //        //}
        //        parameters = null;
        //        objDALBaseClass = null;

        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        throw ex;
        //    }
        //    return 0;
        //}

        #endregion notusing
    }
}
