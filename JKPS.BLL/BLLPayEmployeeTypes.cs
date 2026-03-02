using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
using Autofac.Core;
using System.Data.SqlClient;

namespace JKPS.BLL
{
  /// <summary>
  /// Implemented by : sanjay chawla
  /// Date : 11 DEC 2008
  /// Description :Form is use to Update Employee Types
  /// Modified by: 
  /// Modified Date : 
  /// Description : 
  /// </summary>
  public class BLLPayEmployeeTypes
  {/// <summary>
   /// This method is use to get Employee types from database 
   /// </summary>
   /// <param name="objEmployeeTypes">reference of DVOMasterEmpTypes type object as a collection of parameters of search criteria.</param>
   /// <returns>return a list having employee types information</returns>
    public static List<DVOMasterEmpTypes> GetEmployeeTypes(ref DVOMasterEmpTypes objEmployeeTypes)
    {
      object[] parameters = new object[14];
      parameters[0] = objEmployeeTypes.type_code;
      parameters[1] = objEmployeeTypes.description;
      parameters[2] = objEmployeeTypes.empl_status;
      parameters[3] = objEmployeeTypes.pay_period;
      parameters[4] = objEmployeeTypes.vac_code;
      parameters[5] = objEmployeeTypes.vac_allowed;
      parameters[6] = objEmployeeTypes.sick_code;
      parameters[7] = objEmployeeTypes.sick_allowed;
      parameters[8] = objEmployeeTypes.hold_pymnt;
      parameters[9] = objEmployeeTypes.statax_code;
      parameters[10] = objEmployeeTypes.loctax_code;
      parameters[11] = objEmployeeTypes.sick_accr_code;
      parameters[12] = objEmployeeTypes.vac_accr_code;
      parameters[13] = objEmployeeTypes.Rowid;

      List<DVOMasterEmpTypes> objEmployeeTypeslist = new List<DVOMasterEmpTypes>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmpTypes)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOMasterEmpTypes objPayEmployeeTypes = new DVOMasterEmpTypes();
          objPayEmployeeTypes.type_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);//type_code
          objPayEmployeeTypes.description = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty); //description
                                                                                                              //objEmployeeTypes.cash_acct = dr[2].ToString();//description
                                                                                                              //objEmployeeTypes.department = dr[3].ToString();//description
          objPayEmployeeTypes.empl_status = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);//empl_status
          objPayEmployeeTypes.pay_period = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//pay_period
          objPayEmployeeTypes.vac_code = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);//vac_code
          objPayEmployeeTypes.vac_allowed = (dr[7] != DBNull.Value ? (decimal?)(dr[7]) : null);//vac_allowed
          objPayEmployeeTypes.sick_code = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//sick_code
          objPayEmployeeTypes.sick_allowed = (dr[9] != DBNull.Value ? (decimal?)dr[9] : null);//sick_allowed
          objPayEmployeeTypes.hold_pymnt = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);//hold_pymnt
          objPayEmployeeTypes.statax_code = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);//statax_code
          objPayEmployeeTypes.loctax_code = (dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty);//loctax_code
          objPayEmployeeTypes.sick_accr_code = (dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty);//sick_accr_code
          objPayEmployeeTypes.vac_accr_code = (dr[14] != DBNull.Value ? dr[14].ToString().Trim() : string.Empty);//vac_accr_code                                 
          objPayEmployeeTypes.Rowid = Convert.ToInt32(dr[15].ToString());//Rowid 
          objPayEmployeeTypes.keyvalue = (dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty); //Keyvalue          
          objEmployeeTypeslist.Add(objPayEmployeeTypes);
        }
      }
      return objEmployeeTypeslist;
    }

    /// <summary>
    /// This method is use to Insert new employee type information into database
    /// </summary>
    /// <param name="objEmployeeTypes">reference of DVOMasterEmpTypes type object as a collection of parameters of search criteria.</param>
    /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
    public static int InsertEmployeeTypes(ref object objTransaction, ref DVOMasterEmpTypes objPayEmployeeTypes,
         ref List<DVOMasterEmpTypeIncomes> listDVOMasterEmpTypeIncomes,
         ref List<DVOMasterEmpTypeDeductions> listDVOMasterEmpTypeDeductions,
         ref List<DVOMasterEmpTypeObligations> listDVOMasterEmpTypeObligations)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      try
      {

        object[] parameters = new object[14];
        parameters[0] = objPayEmployeeTypes.type_code;
        parameters[1] = objPayEmployeeTypes.description;
        parameters[2] = objPayEmployeeTypes.keyvalue;
        parameters[3] = objPayEmployeeTypes.statax_code;
        parameters[4] = objPayEmployeeTypes.sick_accr_code;
        parameters[5] = objPayEmployeeTypes.loctax_code;
        parameters[6] = objPayEmployeeTypes.vac_accr_code;
        parameters[7] = objPayEmployeeTypes.sick_code;
        parameters[8] = objPayEmployeeTypes.sick_allowed;
        parameters[9] = objPayEmployeeTypes.vac_code;
        parameters[10] = objPayEmployeeTypes.vac_allowed;
        parameters[11] = objPayEmployeeTypes.pay_period;
        parameters[12] = objPayEmployeeTypes.empl_status;
        parameters[13] = objPayEmployeeTypes.hold_pymnt;

        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        object obj = objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOMasterEmpTypes), true);
        if (obj == null)
          throw new Exception();
        else if (Convert.ToInt32(obj) < 1)
          throw new Exception();
        parameters = null;
        objDalBaseClass = null;

        BLLPREmpTypeIncomeStytypid.InsertData(ref objTransaction, ref listDVOMasterEmpTypeIncomes);
        BLLPREmpTypeDeductionStytypdd.InsertData(ref objTransaction, ref listDVOMasterEmpTypeDeductions);
        BLLPREmpTypeObligationStytypod.InsertData(ref objTransaction, ref listDVOMasterEmpTypeObligations);



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




      //return obj;

    }
    /// <summary>
    /// This method is use to update employee type information into database
    /// </summary>
    /// <param name="objEmployeeTypes">reference of DVOMasterEmpTypes type object as a collection of parameters of search criteria.</param>
    /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
    public static object UpdateEmployeeTypes(ref DVOMasterEmpTypes objPayEmployeeTypes)
    {

      object[] parameters = new object[14];
      parameters[0] = objPayEmployeeTypes.type_code;
      parameters[1] = objPayEmployeeTypes.description;
      parameters[2] = objPayEmployeeTypes.keyvalue;
      parameters[3] = objPayEmployeeTypes.statax_code;
      parameters[4] = objPayEmployeeTypes.sick_accr_code;
      parameters[5] = objPayEmployeeTypes.loctax_code;
      parameters[6] = objPayEmployeeTypes.vac_accr_code;
      parameters[7] = objPayEmployeeTypes.sick_code;
      parameters[8] = objPayEmployeeTypes.sick_allowed;
      parameters[9] = objPayEmployeeTypes.vac_code;
      parameters[10] = objPayEmployeeTypes.vac_allowed;
      parameters[11] = objPayEmployeeTypes.pay_period;
      parameters[12] = objPayEmployeeTypes.empl_status;
      parameters[13] = objPayEmployeeTypes.hold_pymnt;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.UpdateData(ref parameters, typeof(DVOMasterEmpTypes), true);
      return obj;

    }
    /// <summary>
    /// This method is use to delete employee type information into database
    /// </summary>
    /// <param name="objEmployeeTypes">reference of DVOMasterEmpTypes type object as a collection of parameters of search criteria.</param>
    /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
    public static object DeleteEmployeeTypes(ref DVOMasterEmpTypes objPayEmployeeTypes)
    {

      object[] parameters = new object[1];
      parameters[0] = objPayEmployeeTypes.type_code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.DeleteData(ref parameters, typeof(DVOMasterEmpTypes), true);
      return obj;

    }
    /// <summary>
    /// This method is use to get Employee type from database ,To check either employee code exist or not
    /// </summary>
    /// <param name="objEmployeeTypes">reference of DVOMasterEmpTypes type object as a collection of parameters of search criteria.</param>
    /// <returns>return a list having employee types information</returns>
    /// </summary>
    /// <param name="objFlexKeyValueSegmentDef"></param>
    /// <returns></returns>

    public static DataSet GetEmployeeType(ref DVOMasterEmpTypes objEmployeeTypes)
    {
      object[] parameters = new object[1];
      parameters[0] = objEmployeeTypes.type_code;
      //List<DVOFlexKeySegmentDefinition> objFlexKeyValueSegmentDefinition = new List<DVOFlexKeySegmentDefinition>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmpTypes), objEmployeeTypes.GetEmployeeType);
      return ds;
    }

    public static object UpdateEmployeeTypeInfo(ref object objTransaction, ref DVOMasterEmpTypes objPayEmployeeTypes,
         ref List<DVOMasterEmpTypeIncomes> listDVOMasterEmpTypeIncomes,
         ref List<DVOMasterEmpTypeIncomes> listNewDVOMasterEmpTypeIncomes,
         ref List<DVOMasterEmpTypeIncomes> listDeleteDVOMasterEmpTypeIncomes,

        ref List<DVOMasterEmpTypeDeductions> listDVOMasterEmpTypeDeductions,
        ref List<DVOMasterEmpTypeDeductions> listDeleteDVOMasterEmpTypeDeductions,
        ref List<DVOMasterEmpTypeDeductions> listNewDVOMasterEmpTypeDeductions,
        ref List<DVOMasterEmpTypeObligations> listDVOMasterEmpTypeObligations,
        ref List<DVOMasterEmpTypeObligations> listDeleteDVOMasterEmpTypeObligations,
        ref List<DVOMasterEmpTypeObligations> listNewDVOMasterEmpTypeObligations

        )
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      int success = 0;

      try
      {
        object[] parameters = new object[14];
        parameters[0] = objPayEmployeeTypes.type_code;
        parameters[1] = objPayEmployeeTypes.description;
        parameters[2] = objPayEmployeeTypes.keyvalue;
        parameters[3] = objPayEmployeeTypes.statax_code;
        parameters[4] = objPayEmployeeTypes.sick_accr_code;
        parameters[5] = objPayEmployeeTypes.loctax_code;
        parameters[6] = objPayEmployeeTypes.vac_accr_code;
        parameters[7] = objPayEmployeeTypes.sick_code;
        parameters[8] = objPayEmployeeTypes.sick_allowed;
        parameters[9] = objPayEmployeeTypes.vac_code;
        parameters[10] = objPayEmployeeTypes.vac_allowed;
        parameters[11] = objPayEmployeeTypes.pay_period;
        parameters[12] = objPayEmployeeTypes.empl_status;
        parameters[13] = objPayEmployeeTypes.hold_pymnt;
        object obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOMasterEmpTypes), true);
        if (obj == null)
          throw new Exception();
        else if (Convert.ToInt32(obj) < 1)
          throw new Exception();

        BLLPREmpTypeIncomeStytypid.DeleteData(ref objTransaction, ref listDeleteDVOMasterEmpTypeIncomes);
        BLLPREmpTypeDeductionStytypdd.DeleteData(ref objTransaction, ref listDeleteDVOMasterEmpTypeDeductions);
        BLLPREmpTypeObligationStytypod.DeleteData(ref objTransaction, ref listDeleteDVOMasterEmpTypeObligations);

        BLLPREmpTypeIncomeStytypid.updateData(ref objTransaction, ref listDVOMasterEmpTypeIncomes);
        BLLPREmpTypeDeductionStytypdd.updateData(ref objTransaction, ref listDVOMasterEmpTypeDeductions);
        BLLPREmpTypeObligationStytypod.updateData(ref objTransaction, ref listDVOMasterEmpTypeObligations);

        BLLPREmpTypeIncomeStytypid.InsertData(ref objTransaction, ref listNewDVOMasterEmpTypeIncomes);
        BLLPREmpTypeDeductionStytypdd.InsertData(ref objTransaction, ref listNewDVOMasterEmpTypeDeductions);
        BLLPREmpTypeObligationStytypod.InsertData(ref objTransaction, ref listNewDVOMasterEmpTypeObligations);


        if (Convert.ToInt32(obj) > 0)
          success = 1;

        if (!statusObjTransaction)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        return success;
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return success;
    }

    public static List<DVOMasterEmpTypes> GetEmployee(ref DVOMasterEmpTypes objDVOMasterEmpTypes)
    {
      // object[] GetParameter = new object[0];
      //GetParameter[0] = objDVOMasterEmpTypes.type_code;

      List<DVOMasterEmpTypes> objEmployeeTypeslist = new List<DVOMasterEmpTypes>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOMasterEmpTypes)))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOMasterEmpTypes objPayEmployeeTypes = new DVOMasterEmpTypes();
          objPayEmployeeTypes.type_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);//type_code

          objEmployeeTypeslist.Add(objPayEmployeeTypes);
        }
      }
      return objEmployeeTypeslist;

    }
    public static List<string> CHeckExistingRecord(string FilePath, int UserId, int RoleId)
    {
      List<string> list =  new List<string>();
      SqlParameter[] Params = new SqlParameter[]
      {
        new SqlParameter("@filePathWithName",FilePath),
        new SqlParameter("@RoleId",RoleId),
        new SqlParameter("@UserId",UserId)
      };
      List<DVOMasterEmpTypes> objEmployeeTypeslist = new List<DVOMasterEmpTypes>();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.IsDataExisted(Params))
      {
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            list.Add(dr[0].ToString());
          }
        }
      } 
      return list;
    }
  }
}
