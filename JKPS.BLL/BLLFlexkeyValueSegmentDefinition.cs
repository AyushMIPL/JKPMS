using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;

namespace JKPS.BLL
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.) BLL For FlexKeyValueDefinition                    Rajeev(D)                                    05/10/2008(DD)
    ///2.) 
    ///<summery>
    public class BLLFlexkeyValueSegmentDefinition
    {

        public static List<DVOFlexKeySegmentDefinition> GetFlexValueDefinition(ref DVOFlexSegment objFlexSegmentDef, ref DataSet dsSegVal)
        {
            object[] parameters = new object[1];
            parameters[0] = objFlexSegmentDef.id;
            DVOFlexKeySegmentDefinition objFlexValueDef = new DVOFlexKeySegmentDefinition();
            List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexValueDef.FIND_SPNAME.ToString());
            //return ds;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexValueDef.FIND_SPNAME.ToString()))
            using (DataSet ds = objDalBaseClass.GetData(objFlexValueDef.FIND_SEGMENTDETAIL(ref parameters)))
            {
                dsSegVal = ds;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOFlexKeySegmentDefinition tempobj_FlexKeySegmentDefinition = new DVOFlexKeySegmentDefinition();
                    //tempobj_FlexKeySegmentDefinition.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);//RowID
                    tempobj_FlexKeySegmentDefinition.segmentid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);//segmentID
                    tempobj_FlexKeySegmentDefinition.id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);//id
                    tempobj_FlexKeySegmentDefinition.keyvalue = (dr[2] != DBNull.Value ? dr[2].ToString() : string.Empty); //KeyValue
                    tempobj_FlexKeySegmentDefinition.desc = (dr[3] != DBNull.Value ? dr[3].ToString() : string.Empty); //Desc
                    tempobj_FlexKeySegmentDefinition.printsafter = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0); //Printsafter
                    tempobj_FlexKeySegmentDefinition.issubto = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0);//issuebto
                    tempobj_FlexKeySegmentDefinition.abbreviation = (dr[6] != DBNull.Value ? dr[6].ToString() : string.Empty); ;//abbreviation



                    objFlexKeyValueSegmentDefinition.Add(tempobj_FlexKeySegmentDefinition);

                }
            }
            return objFlexKeyValueSegmentDefinition;
        }


        public static int InsertFlxValueseg(ref object objTransaction, ref DVOFlexKeySegmentDefinition objflxKeyValseg)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                int success = 0;
                object[] parameter = new object[13];
                parameter[0] = objflxKeyValseg.segmentid;
                parameter[1] = objflxKeyValseg.id;
                parameter[2] = objflxKeyValseg.keyvalue;
                parameter[3] = objflxKeyValseg.desc;
                parameter[4] = objflxKeyValseg.printsafter;

                parameter[5] = objflxKeyValseg.issubto;
                parameter[6] = objflxKeyValseg.abbreviation;
                parameter[7] = objflxKeyValseg.InsertMachineInfo;
                parameter[8] = objflxKeyValseg.InsertDate;
                parameter[9] = objflxKeyValseg.InsertBy;

                parameter[10] = objflxKeyValseg.UpdateMachineInfo;
                parameter[11] = objflxKeyValseg.UpdateDate;
                parameter[12] = objflxKeyValseg.UpdateBy;

                DataSet ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameter, typeof(DVOFlexKeySegmentDefinition));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        success = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    }
                }
                parameter = null;
                objflxKeyValseg = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                if (success > 0)
                    return 1;
                // success = objDalBaseClass.InsertData(ref parameter, typeof(DVOFlexKeySegmentDefinition));
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
        public static List<DVOFlexKeySegmentDefinition> GetPopUpFlexKeyValueDefinition(ref DVOFlexKeySegmentDefinition objFlexKeyValueSegmentDef, ref DataSet dsSegVal)
        {

            object[] parameters = new object[2];
            parameters[0] = objFlexKeyValueSegmentDef.segmentid;
            parameters[1] = objFlexKeyValueSegmentDef.issubto;

            List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition),objFlexKeyValueSegmentDef.FIND_POPUPSPNAME);
            //return ds;

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexKeyValueSegmentDef.FIND_POPUPSPNAME))
            {
                dsSegVal = ds;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOFlexKeySegmentDefinition tempobj_FlexKeySegmentDefinition = new DVOFlexKeySegmentDefinition();
                    //tempobj_FlexKeySegmentDefinition.RowID = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);//RowID
                    tempobj_FlexKeySegmentDefinition.segmentid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);//segmentID
                    tempobj_FlexKeySegmentDefinition.id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);//id
                    tempobj_FlexKeySegmentDefinition.keyvalue = (dr[2] != DBNull.Value ? dr[2].ToString() : string.Empty); //KeyValue
                    tempobj_FlexKeySegmentDefinition.desc = (dr[3] != DBNull.Value ? dr[3].ToString() : string.Empty); //Desc
                    tempobj_FlexKeySegmentDefinition.printsafter = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0); //Printsafter
                    tempobj_FlexKeySegmentDefinition.issubto = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0);//issuebto
                    tempobj_FlexKeySegmentDefinition.abbreviation = (dr[6] != DBNull.Value ? dr[6].ToString() : string.Empty); ;//abbreviation

                    objFlexKeyValueSegmentDefinition.Add(tempobj_FlexKeySegmentDefinition);

                }
            }
            return objFlexKeyValueSegmentDefinition;

        }

        //Added By Rajeev
        // To Get all object Description 
        public static List<DVOFlexKeySegmentDefinition> GetObjectDefinition(string acct_type)
        {

            object[] parameters = new object[1];
            parameters[0] = acct_type;

            DVOFlexKeySegmentDefinition objFlexKeyValueSegmentDef = new DVOFlexKeySegmentDefinition();
            List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexKeyValueSegmentDef.FIND_OBJECTDESCRIPTION))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOFlexKeySegmentDefinition tempobj_FlexKeySegmentDefinition = new DVOFlexKeySegmentDefinition();
                    tempobj_FlexKeySegmentDefinition.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty); //KeyValue
                    tempobj_FlexKeySegmentDefinition.desc = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty); //Desc
                    objFlexKeyValueSegmentDefinition.Add(tempobj_FlexKeySegmentDefinition);
                }
            }
            return objFlexKeyValueSegmentDefinition;

        }


        public static List<DVOFlexKeySegmentDefinition> GetObjectDetailDefinition(string acct_type, string ObjectCode)
        {

            object[] parameters = new object[2];
            parameters[0] = acct_type;
            parameters[1] = ObjectCode;

            DVOFlexKeySegmentDefinition objFlexKeyValueSegmentDef = new DVOFlexKeySegmentDefinition();
            List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexKeyValueSegmentDef.FIND_OBJECT_DETAIL_DESCRIPTION))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOFlexKeySegmentDefinition tempobj_FlexKeySegmentDefinition = new DVOFlexKeySegmentDefinition();
                    tempobj_FlexKeySegmentDefinition.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty); //KeyValue
                    tempobj_FlexKeySegmentDefinition.desc = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty); //Desc
                    objFlexKeyValueSegmentDefinition.Add(tempobj_FlexKeySegmentDefinition);
                }
            }
            return objFlexKeyValueSegmentDefinition;

        }



        //****************************By sanjay******************
        //For SigbudExportUtility step 1
        public static DataSet GetDataSegUtiStep1(ref DVOFlexSegment objFlexSegmentDef)
        {
            object[] parameters = new object[1];
            parameters[0] = objFlexSegmentDef.id;
            DVOFlexKeySegmentDefinition objFlexValueDef = new DVOFlexKeySegmentDefinition();
            List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexValueDef.SigbudUtilityExpStep1);
            return ds;
        }
        //For SigbudExportUtility step 2
        public static DataSet GetDataSegUtiStep2(ref DVOFlexKeySegmentDefinition objFlexKeyValueSegmentDef)
        {
            object[] parameters = new object[0];

            //List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexKeyValueSegmentDef.SigbudUtilityExpStep2);
            return ds;

        }
        //For SigbudExportUtility step 3
        public static DataSet GetDataSegUtiStep3(ref DVOFlexKeySegmentDefinition objFlexKeyValueSegmentDef)
        {
            object[] parameters = new object[2];
            parameters[0] = objFlexKeyValueSegmentDef.period;
            parameters[1] = objFlexKeyValueSegmentDef.year;

            //List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexKeyValueSegmentDef.SigbudUtilityExpStep3);
            DataTable dtt = new DataTable();
            dtt.TableName = "Accounts";
            dtt.Columns.Add("AccountType", typeof(String));
            dtt.Columns.Add("Keyvalue", typeof(String));
            dtt.Columns.Add("Balance", typeof(Decimal));
            if (ds != null && ds.Tables.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                    dtt.Rows.Add(dr.ItemArray);
            if (ds == null)
                ds = new DataSet();
            ds.Tables.Add(dtt);
            return ds;

        }

        public static DataSet GetDataSegUtiExp(ref DVOFlexKeySegmentDefinition objFlexKeyValueSegmentDef)
        {
            object[] parameters = new object[3];
            parameters[0] = objFlexKeyValueSegmentDef.year;
            parameters[1] = objFlexKeyValueSegmentDef.desc;
            parameters[2] = objFlexKeyValueSegmentDef.chk;
            //List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexKeyValueSegmentDef.GetDataSegUtiExp);
            return ds;

        }
        /// <summary>
        /// Check account number in PayrollGLAccounts table for a given keyvalue as a parameter
        /// </summary>
        /// <param name="objFlexKeyValueSegmentDef">reference of DVOFlexKeySegmentDefinition type object as a collection of parameters of search criteria.</param>
        /// <returns>return a dataset as a resultset</returns>
        public static DataSet GetAccountNumber(ref DVOFlexKeySegmentDefinition objFlexKeyValueSegmentDef)
        {
            object[] parameters = new object[1];
            parameters[0] = objFlexKeyValueSegmentDef.keyvalue;

            //List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexKeyValueSegmentDef.GetAccountNumber);
            return ds;

        }

        /// <summary>
        /// This method is use to insert new account type into database
        /// </summary>
        /// <param name="objGLAccountType">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
        public static int InsertExportInfo(ref DVOFlexKeySegmentDefinition objFlexKeySegment)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object[] c = new object[1];
            try
            {
                object[] parameters = new object[5];
                parameters[0] = objFlexKeySegment.year;
                parameters[1] = objFlexKeySegment.desc;
                parameters[2] = objFlexKeySegment.InsertMachineInfo;
                parameters[3] = objFlexKeySegment.InsertDate;
                parameters[4] = objFlexKeySegment.InsertBy;
                c[0] = objDALBaseClass.InsertData(ref parameters, typeof(DVOFlexKeySegmentDefinition), objFlexKeySegment.InsertDataSegUtiExp);
                parameters = null;
                objDALBaseClass = null;
                return Convert.ToInt32(c[0].ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
            return Convert.ToInt32(c[0].ToString());


        }
        public static int DeleteFlexValueDefinition(ref object objTransaction, ref DVOFlexKeySegmentDefinition objflxKeyValseg)
        {
            object success = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {
                object[] parameter = new object[2];
                parameter[0] = objflxKeyValseg.id;
                parameter[1] = objflxKeyValseg.segmentid;
                success = objDalBaseClass.DeleteData_ByTransaction(ref objTransaction,  ref parameter, typeof(DVOFlexKeySegmentDefinition),true);
                if (success == DBNull.Value)
                    throw new Exception();
                else if (Convert.ToInt32(success) != 1)
                    throw new Exception();
                parameter = null;
                objDalBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (objTransaction != null && !statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 1;
        }
    }
}
