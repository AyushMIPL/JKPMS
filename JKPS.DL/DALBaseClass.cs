using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Data.SqlClient;
using Autofac.Core;

namespace JKPS.DL
{
    public abstract class DALBaseClass
    {
        //public delegate CollectionBase GenerateCollectionFromReader(IDataReader returnData);

        /// <summary>
        /// Return Connection String(defined in app.config file), used in the application
        /// </summary>
        public abstract string ConnectionString
        {
            get;
            set;
        }

        /// constructor
        public DALBaseClass()
        {
        }

        /// <summary>
        /// To Close Application forcefully
        /// </summary>
        /// <param name="ex"></param>
        public static void CloseApplication(Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message, "Satya Pay");
            Process[] pro = Process.GetProcessesByName("JKPS");
            try { pro[0].Kill(); }
            catch { pro[0].WaitForExit(); }
        }

        public abstract DataSet GetAlldatabases(object[] parameters, Type bType);
        public abstract DataSet Authentication(ref object[] parameters, Type bType);
        public abstract DataSet GetData(ref object[] parameters, Type bType);

        public abstract IDataReader GetDataByReader(ref object[] parameters, Type bType);
        public abstract IDataReader GetDataByReader(string QueryAsProperty);
        /// <summary>
        /// Created to get data through DataReader. Created By Shrishanshu
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="bType"></param>
        /// <param name="ProcedureNameAsProperty"></param>
        /// <returns></returns>
        public abstract IDataReader GetDataByReader(ref object[] parameters, string ProcedureNameAsProperty);

        public abstract DataSet GetData(Type bType, string ProcedureNameAsProperty);


        /// <summary>
        /// Get data from procedure which is not related to that particular object's GET,INSERT,DELETE & UPDATE procedures
        /// </summary>
        /// <param name="parameters">object array of parameters of procedure</param>
        /// <param name="bType">type of object</param>
        /// <param name="ProcedureNameAsProperty">name of property which have procedure's name to execute</param>
        /// <returns></returns>
        public abstract DataSet GetData(ref object[] parameters, Type bType, string ProcedureNameAsProperty);

        /// <summary>
        /// Get data using query which is not related to that particular object's GET,INSERT,DELETE & UPDATE procedures
        /// </summary>
        /// <param name="bType">object, for which you want to execute query</param>
        /// <param name="QueryAsProperty">property of object which has sql query as string</param>
        /// <returns></returns>
        public abstract DataSet GetData(string QueryAsProperty);
        public abstract DataSet GetData_ByTransaction(ref object TransactionObject, string QueryAsProperty);
        public abstract DataSet GetData_ByTransaction(ref object TransactionObject, ref object[] parameters, string ProcedureNameAsProperty);
        //    public abstract DataSet ConnectCSV(string file,string Path);
        public abstract DataSet ExecuteQuery_ByTransaction(ref object TransactionObject, string QueryAsProperty);
        public abstract DataTable GetDataTable(ref object[] parameters, Type bType);
        public abstract DataSet IsDataExisted(SqlParameter[] parameters);
        public abstract DataSet GetAllData(Type bType);
        public abstract DataSet GetPaymentCount(SqlParameter[] Parameter);
        public abstract DataSet GetArearData(SqlParameter[] parameters);
        public abstract DataSet GetDuplicateAccountData(SqlParameter[] parameters);
        public abstract DataSet GetDuplicateAcrossDatabases(SqlParameter[] parameters);
        public abstract DataSet GetAllData(Type bType, SqlParameter[] Parameters);
        public abstract DataSet GetPaymentHistory(SqlParameter[] Parameters);
        public abstract int InsertData(ref object[] parameters, Type bType);
        public abstract object InsertData(ref object[] parameters, Type bType, string ProcedureNameAsProperty);
        public abstract object InsertData(ref object[] parameters, Type bType, bool isScalar);
        public abstract int UpdateData(ref object[] parameters, Type bType);
        public abstract object UpdateData(ref object[] parameters, Type bType, bool isScalar);
        public abstract int DeleteData(ref object[] parameters, Type bType);
        public abstract object DeleteData(ref object[] parameters, Type bType, bool isScalar);
        public abstract object DeleteData(ref object[] parameters, Type bType, string ProcedureNameAsProperty);
        public abstract DataSet GetData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType);
        public abstract DataSet GetAllData_ByTransaction(ref object TransactionObject, Type bType);
        public abstract int InsertData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType);
        public abstract object InsertData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType, string ProcedureNameAsProperty);
        public abstract object InsertData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType, bool isScalar);
        public abstract DataSet InsertAndGetData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType);
        public abstract int UpdateData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType);
        public abstract object UpdateData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType, bool isScalar);
        public abstract object UpdateData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType, string ProcedureNameAsProperty);
        public abstract int DeleteData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType);
        public abstract object DeleteData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType, bool isScalar);
        public abstract object DeleteData_ByTransaction(ref object TransactionObject, ref object[] parameters, Type bType, string ProcedureNameAsProperty);
        public abstract void BulkCopy(DataTable objDataTable, string srcTableName, string ForeignKeyName, object ForeignKeyValue);
        public abstract int ForceLock(ref object objTransaction, ref object[] parameters, Type bType);
        public abstract object ExecuteProcedure_ByTransaction(ref object TransactionObject, ref object[] parameters, string ProcedureNameAsPropertyOfDVOObject, bool isScalar);

        public abstract int ExecuteProcedure_ByTransaction(ref object TransactionObject, ref object[] parameters, string ProcedureNameAsPropertyOfDVOObject);
        public abstract DataSet ExecuteDataSet_ByTransaction(ref object TransactionObject, ref object[] parameters, string ProcedureNameAsPropertyOfDVOObject);
        public abstract int ExecuteProcedure(ref object[] parameters, string ProcedureNameAsPropertyOfDVOObject);
        public abstract int ExecuteStoredProcedure(string ProcedureNameAsPropertyOfDVOObject);

        public abstract object ExecuteScalar(ref object[] parameters, string ProcedureNameAsPropertyOfDVOObject);
        public abstract object ExecuteScalar_ByTransaction(ref object TransactionObject, ref object[] parameters, string ProcedureNameAsPropertyOfDVOObject);
        public abstract object ExecuteScalar(string QueryAsPropertyOfDVOObject);
        public abstract object ExecuteScalar_ByTransaction(ref object TransactionObject, string QueryAsPropertyOfDVOObject);
        public abstract DataTable ExecuteReader(ref object[] parameters, Type bType);
    }
}
