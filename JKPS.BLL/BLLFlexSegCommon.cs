using System;
using System.Collections.Generic;
using System.Text;

using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;
using System.Data.SqlClient;
namespace JKPS.BLL
{
  ///<Development and modification Details>

  ///1.) BLL For Flex Segment Common Use                                                 
  ///2.) 
  ///<summery>
  public class BLLFlexSegCommon
  {
    //DVOFlexSegCommon tempDvoFexprm_entity_code_accType = new DVOFlexSegCommon();

    //**********************************************************************************************************************
    // The Function in the file are used to manupulate the content of the table Flex_Segment_Reference. This table contain three column  *
    //        Flex_Segment_Reference.entity_type --> Determine which programe the row is releventto and is a qualified for Flex_Segment_Reference.code *
    //        Flex_Segment_Reference.code --> Identifies which entity in another table(Determine by the entity type) this row concern    * 
    //        Flex_Segment_Reference.segvd_id --> The Segment value id(Master_Segment.id),used to build a (partial or whole) keyvalue          *
    //                                                                                                                     *
    //**********************************************************************************************************************

    #region FunctionFlexSeg_Load
    //To Call this function 
    //Pass Parameter : EntityType , Code  And Account Type
    //Set EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
    //This Function calculate the keyvalue and return it as a string value     
    public static string Flexseg_Load(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      int locPosition, locLength;
      //DVOFlexSegCommon objDvoFlexSegVal = new DVOFlexSegCommon();
      List<DVOFlexSegCommon> objDVOFlexSegCommonlist = new List<DVOFlexSegCommon>();
      int locKeyLength;
      string ret_keyvalue = string.Empty;
      string copyKeyval = string.Empty;
      DataSet loadFlexFeptPrepDS = BLLFlexSegCommon.GetSegmentValInformation(ref tempDvoFexprm_entity_code_accType);
      int KeyLengthPrep = BLLFlexSegCommon.GetKeyLengthInformation(ref tempDvoFexprm_entity_code_accType);

      //Getting KeyLength value
      if (KeyLengthPrep > 0)
      {
        locKeyLength = Convert.ToInt32(KeyLengthPrep);
        if (locKeyLength > 0)
        {
          //Concatination the ret_value with #, Uptill the length of locKeyvalue
          for (int i = 1; i <= locKeyLength; i++)
          {
            ret_keyvalue = ret_keyvalue + "#";
          }
        }
      }
      if (loadFlexFeptPrepDS.Tables[0].Rows.Count > 0)
      {
        foreach (DataRow dr in loadFlexFeptPrepDS.Tables[0].Rows)
        {
          DVOFlexSegCommon tempobjDvoFlexSegVal = new DVOFlexSegCommon();
          tempobjDvoFlexSegVal.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);  //locSegment
          tempobjDvoFlexSegVal.position = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0); //locPosition
          tempobjDvoFlexSegVal.length = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0); //locLength
          tempobjDvoFlexSegVal.abbreviation = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty); //locAbbrev

          //Logic to find the starting index and end index from the ret_keyvalue and replace with the new keyvalue
          locPosition = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0);
          locLength = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0);
          copyKeyval = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);

          StringBuilder retKeyValueBuilder = new StringBuilder(ret_keyvalue);

          // Replace characters using a foreach loop
          int startIndex = locPosition - 1; // Adjust for zero-based index
          foreach (char c in copyKeyval)
          {
            if (startIndex < retKeyValueBuilder.Length) // Check if the index is within bounds
            {
              retKeyValueBuilder[startIndex] = c; // Replace character at the startIndex
              startIndex++;
            }
          }

          // Convert StringBuilder back to string
          ret_keyvalue = retKeyValueBuilder.ToString();

          //tempobjDvoFlexSegVal.Ret_Keyvalue = ret_keyvalue;
          objDVOFlexSegCommonlist.Add(tempobjDvoFlexSegVal);
        }

      }
      return ret_keyvalue;

    }


    public static string Flexseg_Name(String SegmentCode, Int32 Segment)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      DVOFlexSegCommon flexsegmentsdefinition = new DVOFlexSegCommon();
      String SegmentName = "";
      object[] parameters = new object[2];
      parameters[0] = SegmentCode;
      parameters[1] = Segment;

      object obj = objDALBaseClass.ExecuteScalar(ref parameters, flexsegmentsdefinition.FIND_SegmentName);

      if (obj != null && obj != DBNull.Value)
        SegmentName = Convert.ToString(obj);
      return SegmentName;
    }


    //To Call this function 
    //Pass Parameter : EntityType , Code  And Account Type
    //Set EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
    //This Function calculate the keyvalue and return it as a string value     
    public static string Flexseg_Load_UsingDataReader(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      int locPosition, locLength;
      //DVOFlexSegCommon objDvoFlexSegVal = new DVOFlexSegCommon();
      List<DVOFlexSegCommon> objDVOFlexSegCommonlist = new List<DVOFlexSegCommon>();
      int locKeyLength;
      string ret_keyvalue = string.Empty;
      string copyKeyval = string.Empty;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

      int KeyLengthPrep = 0;
      object[] parameters = new object[1];
      parameters[0] = tempDvoFexprm_entity_code_accType.AccountType;
      object obj = objDALBaseClass.ExecuteScalar(ref parameters, tempDvoFexprm_entity_code_accType.FIND_KEYLEN);
      if (obj != null && obj != DBNull.Value)
        KeyLengthPrep = Convert.ToInt32(obj);
      //Getting KeyLength value
      if (KeyLengthPrep > 0)
      {
        locKeyLength = KeyLengthPrep;
        if (locKeyLength > 0)
          //Concatination the ret_value with #, Uptill the length of locKeyvalue
          for (int i = 1; i <= locKeyLength; i++)
            ret_keyvalue = ret_keyvalue + "#";
      }

      parameters = new object[3];
      parameters[0] = tempDvoFexprm_entity_code_accType.EntityType;
      parameters[1] = tempDvoFexprm_entity_code_accType.Code;
      parameters[2] = tempDvoFexprm_entity_code_accType.AccountType;
      IDataReader dr = objDALBaseClass.GetDataByReader(ref parameters, tempDvoFexprm_entity_code_accType.FIND_SPNAME);


      while (dr.Read())
      {
        using (DVOFlexSegCommon tempobjDvoFlexSegVal = new DVOFlexSegCommon())
        {
          tempobjDvoFlexSegVal.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);  //locSegment
          tempobjDvoFlexSegVal.position = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0); //locPosition
          tempobjDvoFlexSegVal.length = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0); //locLength
          tempobjDvoFlexSegVal.abbreviation = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty); //locAbbrev

          //Logic to find the starting index and end index from the ret_keyvalue and replace with the new keyvalue
          locPosition = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0);
          locLength = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0);
          copyKeyval = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);

          StringBuilder retKeyValueBuilder = new StringBuilder(ret_keyvalue);

          // Replace characters using a foreach loop
          int startIndex = locPosition - 1; // Adjust for zero-based index
          foreach (char c in copyKeyval)
          {
            if (startIndex < retKeyValueBuilder.Length) // Check if the index is within bounds
            {
              retKeyValueBuilder[startIndex] = c; // Replace character at the startIndex
              startIndex++;
            }
          }

          // Convert StringBuilder back to string
          ret_keyvalue = retKeyValueBuilder.ToString();

          //tempobjDvoFlexSegVal.Ret_Keyvalue = ret_keyvalue;
          objDVOFlexSegCommonlist.Add(tempobjDvoFlexSegVal);
        }
      }
      dr.Close();
      return ret_keyvalue;
    }

    //To Call this function 
    //Pass Parameter : EntityType , Code  And Account Type
    //Set EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
    //This Function calculate the keyvalue and return it as a string value     
    public static string Flexseg_Load_UsingTransaction(ref Object objTransaction, ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      bool statusObjTransaction = true;
      if (objTransaction == null)
      {
        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        statusObjTransaction = false;
      }
      string ret_keyvalue = string.Empty;

      try
      {
        int locPosition, locLength;
        //DVOFlexSegCommon objDvoFlexSegVal = new DVOFlexSegCommon();
        List<DVOFlexSegCommon> objDVOFlexSegCommonlist = new List<DVOFlexSegCommon>();
        int locKeyLength;
        string copyKeyval = string.Empty;

        int KeyLengthPrep = 0;
        object[] parameters = new object[1];
        parameters[0] = tempDvoFexprm_entity_code_accType.AccountType;
        object obj = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, tempDvoFexprm_entity_code_accType.FIND_KEYLEN);
        if (obj != null && obj != DBNull.Value)
          KeyLengthPrep = Convert.ToInt32(obj);
        //Getting KeyLength value
        if (KeyLengthPrep > 0)
        {
          locKeyLength = KeyLengthPrep;
          if (locKeyLength > 0)
            //Concatination the ret_value with #, Uptill the length of locKeyvalue
            for (int i = 1; i <= locKeyLength; i++)
              ret_keyvalue = ret_keyvalue + "#";
        }

        parameters = new object[3];
        parameters[0] = tempDvoFexprm_entity_code_accType.EntityType;
        parameters[1] = tempDvoFexprm_entity_code_accType.Code;
        parameters[2] = tempDvoFexprm_entity_code_accType.AccountType;
        using (DataSet ds = objDALBaseClass.GetData_ByTransaction(ref objTransaction, ref parameters, tempDvoFexprm_entity_code_accType.FIND_SPNAME))
        {
          if (ds != null && ds.Tables.Count > 0)
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
              using (DVOFlexSegCommon tempobjDvoFlexSegVal = new DVOFlexSegCommon())
              {
                tempobjDvoFlexSegVal.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);  //locSegment
                tempobjDvoFlexSegVal.position = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0); //locPosition
                tempobjDvoFlexSegVal.length = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0); //locLength
                tempobjDvoFlexSegVal.abbreviation = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty); //locAbbrev

                //Logic to find the starting index and end index from the ret_keyvalue and replace with the new keyvalue
                locPosition = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0);
                locLength = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0);
                copyKeyval = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);

                StringBuilder retKeyValueBuilder = new StringBuilder(ret_keyvalue);

                // Replace characters using a foreach loop
                int startIndex = locPosition - 1; // Adjust for zero-based index
                foreach (char c in copyKeyval)
                {
                  if (startIndex < retKeyValueBuilder.Length) // Check if the index is within bounds
                  {
                    retKeyValueBuilder[startIndex] = c; // Replace character at the startIndex
                    startIndex++;
                  }
                }

                // Convert StringBuilder back to string
                ret_keyvalue = retKeyValueBuilder.ToString();

                //tempobjDvoFlexSegVal.Ret_Keyvalue = ret_keyvalue;
                objDVOFlexSegCommonlist.Add(tempobjDvoFlexSegVal);
              }
            }
        }
        if (!statusObjTransaction && objTransaction != null)
          objDALBaseClassHelper.CommitTransaction(ref objTransaction);
      }
      catch (Exception ex)
      {
        if (!statusObjTransaction && objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return ret_keyvalue;
    }

    //Function used to get the information of segment value
    //Filter Criteria "Entity_Type,Account_Type and Code"
    // Value Return Keyvalue,Position,Length,Abbreviation
    private static DataSet GetSegmentValInformation(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      object[] parameters = new object[3];
      try
      {

        parameters[0] = tempDvoFexprm_entity_code_accType.EntityType;
        parameters[1] = tempDvoFexprm_entity_code_accType.Code;
        parameters[2] = tempDvoFexprm_entity_code_accType.AccountType;

        ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), tempDvoFexprm_entity_code_accType.FIND_SPNAME);


        if (ds.Tables[0].Rows.Count > 0)
        {
          parameters = null;
          objDALBaseClass = null;
          return ds;
        }
      }
      catch (Exception ex)
      {
        parameters = null;
        objDALBaseClass = null;
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }

    //Function used to get the length of the keyvalue, According to account type.
    //Table used ingflxkh
    private static int GetKeyLengthInformation(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      int RetKeyval = 0;
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[1];
        parameters[0] = tempDvoFexprm_entity_code_accType.AccountType;

        ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), tempDvoFexprm_entity_code_accType.FIND_KEYLEN);
        {
          if (ds.Tables.Count > 0)
            if (ds.Tables[0].Rows.Count > 0)
            {
              RetKeyval = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        parameters = null;
        objDALBaseClass = null;
      }
      catch (Exception ex)
      {

        objDALBaseClass = null;
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return RetKeyval;
    }

    #endregion FunctionFlexSeg_Load

    #region FunctionFlexSeg_Delete
    //To Call this function 
    //Pass Parameter : EntityType And Code
    //Set EntityType And Code value in DVOFlexSegCommon and then call this function

    //********  Modified by Bharat Dhall [21 December, 2008]  *******************
    //****   this function made to public  ******************************************
    public static int Flexseg_delete(ref DVOFlexSegCommon tempDvoFexprm_entity_code)
    {
      int success = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        //int success = 0;
        object[] parameters = new object[2];
        parameters[0] = tempDvoFexprm_entity_code.EntityType;
        parameters[1] = tempDvoFexprm_entity_code.Code;
        object obj = new object();
        obj = objDalBaseClass.DeleteData(ref parameters, typeof(DVOFlexSegCommon), true);
        if (obj != null)
        {
          success = Convert.ToInt32(obj.ToString());
          parameters = null;
          objDalBaseClass = null;
          return success;
        }
      }
      catch (Exception ex)
      {

        objDalBaseClass = null;
        ExceptionManager.Publish(ex);
        return success;
      }
      return success;
    }

    #endregion FunctionFlexSeg_Delete


    #region FunctionFlexSeg_Add
    //To Call this function 
    //Pass Parameter : EntityType , Code , AccountType and KeyValue
    //Set EntityType And Code value in DVOFlexSegCommon and then call this function 
    public static int FunctionFlexSeg_Add(ref DVOFlexSegCommon objDVOFlexSegCommonAdd)
    {
      int retVal = -1;
      int locPosition, locLength;
      int locParentId;
      int locSegvdId = 0;
      string locSegKeyvalue;
      int deleteChk = -1;
      try
      {
        //Checking whether Segment id Exist in the table Flex_Segment_Reference
        //If it Exist then Delete the entire rows according to parameter
        //Entity Type And Code
        DataSet DSExistSegIDCheck = BLLFlexSegCommon.GetCheckSegIDExist(ref objDVOFlexSegCommonAdd);
        if (DSExistSegIDCheck.Tables[0].Rows.Count > 0)
        {
          //Found SegID is Exist
          //First Delete Operation perform
          //Below Line is commented by Neeraj on date 24/10/2015
          deleteChk = BLLFlexSegCommon.Flexseg_delete(ref objDVOFlexSegCommonAdd);
        }
        //Employee code found and deleted


        //Now insertion can be perform
        List<DVOFlexSegCommon> objinxflxacdList = new List<DVOFlexSegCommon>();
        int locSegcnt = 0;
        DataSet DSSegvdid = BLLFlexSegCommon.GetInfoToCalculate_SegvdID_ADD(ref objDVOFlexSegCommonAdd);
        if (DSSegvdid.Tables[0].Rows.Count > 0)
        {
          DataRow drlast = DSSegvdid.Tables[0].Rows[DSSegvdid.Tables[0].Rows.Count - 1];
          int _position = (drlast[2] != DBNull.Value ? Convert.ToInt32(drlast[2]) : 0);
          int _length = (drlast[3] != DBNull.Value ? Convert.ToInt32(drlast[3]) : 0);
          int keyvaluelength = _position + _length + 1;

          if (objDVOFlexSegCommonAdd.keyvalue.Trim().Length == keyvaluelength)
          {
            foreach (DataRow dr in DSSegvdid.Tables[0].Rows)
            {
              DVOFlexSegCommon tempobjDvoFlexSegVal = new DVOFlexSegCommon();
              tempobjDvoFlexSegVal.strucid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
              tempobjDvoFlexSegVal.flexsegid = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
              tempobjDvoFlexSegVal.position = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
              tempobjDvoFlexSegVal.length = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
              tempobjDvoFlexSegVal.required = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
              tempobjDvoFlexSegVal.subtotal = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
              tempobjDvoFlexSegVal.subdivides = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);

              locParentId = 0;

              // Logic Start here to get the SegVDId
              locSegKeyvalue = objDVOFlexSegCommonAdd.keyvalue.Substring(tempobjDvoFlexSegVal.position - 1, tempobjDvoFlexSegVal.length);
              if (!locSegKeyvalue.Contains("#"))
              {
                if (tempobjDvoFlexSegVal.subdivides != 0)
                {
                  //It means this segment subdivides another, So get its parent segment value ID
                  if (tempobjDvoFlexSegVal.subdivides == objinxflxacdList[locSegcnt - 1].flexsegid)
                  {
                    locParentId = locSegvdId;
                  }

                }
              }
              locSegcnt += 1;
              locSegvdId = BLLFlexSegCommon.GetSegValIDInformation(tempobjDvoFlexSegVal.flexsegid, locSegKeyvalue, locParentId);
              tempobjDvoFlexSegVal.segvd_id = locSegvdId;
              tempobjDvoFlexSegVal.EntityType = objDVOFlexSegCommonAdd.EntityType;
              tempobjDvoFlexSegVal.Code = objDVOFlexSegCommonAdd.Code;
              objinxflxacdList.Add(tempobjDvoFlexSegVal);
              if (tempobjDvoFlexSegVal.segvd_id > 0)
                retVal = BLLFlexSegCommon.FlexSegValue_Add(ref tempobjDvoFlexSegVal);
              if (retVal == 0)
                return retVal;
            }
          }
        }
        return retVal;
      }
      catch (Exception ex)
      {
        return 0;
      }

    }

    private static DataSet GetCheckSegIDExist(ref DVOFlexSegCommon objDVOFlexSegCommonChk)
    {
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[2];
        parameters[0] = objDVOFlexSegCommonChk.EntityType;
        parameters[1] = objDVOFlexSegCommonChk.Code;

        ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), objDVOFlexSegCommonChk.FIND_SegvdIDCheck);
        {
          if (ds.Tables.Count > 0)
            if (ds.Tables[0].Rows.Count > 0)
            {
              parameters = null;
              objDALBaseClass = null;
              return ds;
            }
        }

      }
      catch (Exception ex)
      {

        objDALBaseClass = null;
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }

    private static DataSet GetInfoToCalculate_SegvdID_ADD(ref DVOFlexSegCommon objDVOFlexSegCommonAdd)
    {
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[1];
        parameters[0] = objDVOFlexSegCommonAdd.AccountType;

        ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), objDVOFlexSegCommonAdd.FIND_SegvdIDADD);
        {
          if (ds.Tables.Count > 0)
            if (ds.Tables[0].Rows.Count > 0)
            {
              parameters = null;
              objDALBaseClass = null;
              return ds;
            }
        }

      }
      catch (Exception ex)
      {

        objDALBaseClass = null;
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return ds;
    }

    private static int GetSegValIDInformation(int Segid, string locSegKeyval, int locParentID)
    {
      DVOFlexSegCommon objFlexsegCom = new DVOFlexSegCommon();
      int RetKeyval = 0;
      DataSet ds = null;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[3];
        parameters[0] = Segid;
        parameters[1] = locSegKeyval;
        parameters[2] = locParentID;

        ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), objFlexsegCom.FIND_SegvdIDGet);
        {
          if (ds.Tables.Count > 0)
            if (ds.Tables[0].Rows.Count > 0)
            {
              RetKeyval = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            }
        }
        parameters = null;
        objDALBaseClass = null;

      }
      catch (Exception ex)
      {

        objDALBaseClass = null;
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return RetKeyval;
    }

    private static int FlexSegValue_Add(ref DVOFlexSegCommon objDVOFlexSegCommonInsPrm)
    {
      int success = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object objTransection = objDALBaseClassHelper.GetTransactionObject();
      try
      {
        object[] parameters = new object[3];
        parameters[0] = objDVOFlexSegCommonInsPrm.EntityType;
        parameters[1] = objDVOFlexSegCommonInsPrm.Code;
        parameters[2] = objDVOFlexSegCommonInsPrm.segvd_id;


        //Parameters used For Only SQL Server
        //parameters[6] = objDVONssTrnRecord.InsertMachineInfo;
        //parameters[7] = objDVONssTrnRecord.InsertDate;
        //parameters[8] = objDVONssTrnRecord.InsertBy;
        //parameters[9] = objDVONssTrnRecord.UpdateMachineInfo;
        //parameters[10] = objDVONssTrnRecord.UpdateDate;
        //parameters[11] = objDVONssTrnRecord.UpdateBy;

        DataSet Ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameters, typeof(DVOFlexSegCommon));
        if (Ds.Tables.Count > 0)
          if (Ds.Tables[0].Rows.Count > 0)
          {
            success = Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString());
            objDALBaseClassHelper.CommitTransaction(ref objTransection);
          }
        // Return when Contact Details Inserted Successfully
        if (success > 0)
        {
          return success;
        }
        // Return when Exception Occured During the insert Statement
        else
        {
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
          return 0;
        }
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        return 0;
      }
    }


    #endregion FunctionFlexSeg_Add

    // ********************

    public static DataTable PrintOrgStr()
    {
      DataTable objDataTable = new DataTable();
      objDataTable.Columns.Add("L1");
      objDataTable.Columns.Add("L2");
      objDataTable.Columns.Add("L3");
      objDataTable.Columns.Add("L4");
      objDataTable.Columns.Add("L5");
      objDataTable.Columns.Add("L6");
      objDataTable.Columns.Add("L7");
      objDataTable.Columns.Add("D1");
      objDataTable.Columns.Add("D2");
      objDataTable.Columns.Add("D3");
      objDataTable.Columns.Add("D4");
      objDataTable.Columns.Add("D5");
      objDataTable.Columns.Add("D6");
      objDataTable.Columns.Add("D7");
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        DataSet dsSegvd = BLLGLTrialBalance.GetMonthEndSegmentDetails();
        DataRow[] draMin = dsSegvd.Tables[0].Select("segmentid=23");
        bool IsFirst = true;
        int id = 0;
        int nextId = 0;
        foreach (DataRow drM in draMin)
        {

          if (IsFirst)
          {
            id = Convert.ToInt32(objDALBaseClass.ExecuteScalar((new DVOFlexSegCommon()).GETFIRSTID()));
            nextId = id;
            SetStructure(id, nextId, 1, ref dsSegvd, ref objDataTable, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            IsFirst = false;
          }
          else
          {
            DataRow[] draNextSegvd = dsSegvd.Tables[0].Select("printsafter=" + nextId);
            if (draNextSegvd.Length > 0)
            {
              id = Convert.ToInt32(draNextSegvd[0]["id"]);
              nextId = id;
              //s1 = string.Empty; s2 = string.Empty; s3 = string.Empty; s4 = string.Empty; s5 = string.Empty; s6 = string.Empty; s7 = string.Empty;
              SetStructure(id, nextId, 1, ref dsSegvd, ref objDataTable, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                  string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
          }
        }
        objDALBaseClass = null;
        dsSegvd = null;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return objDataTable;
    }

    private static void SetStructure(int id, int nextId, int level, ref DataSet dsSegvd, ref DataTable objDataTable,
        string s1, string s2, string s3, string s4, string s5, string s6, string s7, string d1, string d2, string d3, string d4, string d5, string d6, string d7)
    {
      if (level <= 7)
      {
        DataRow[] draSegvd = dsSegvd.Tables[0].Select("id =" + id);

        switch (level)
        {
          case 1: s1 = draSegvd[0]["keyvalue"].ToString(); d1 = draSegvd[0]["desc"].ToString(); break;
          case 2: s2 = draSegvd[0]["keyvalue"].ToString(); d2 = draSegvd[0]["desc"].ToString(); break;
          case 3: s3 = draSegvd[0]["keyvalue"].ToString(); d3 = draSegvd[0]["desc"].ToString(); break;
          case 4: s4 = draSegvd[0]["keyvalue"].ToString(); d4 = draSegvd[0]["desc"].ToString(); break;
          case 5: s5 = draSegvd[0]["keyvalue"].ToString(); d5 = draSegvd[0]["desc"].ToString(); break;
          case 6: s6 = draSegvd[0]["keyvalue"].ToString(); d6 = draSegvd[0]["desc"].ToString(); break;
          case 7: s7 = draSegvd[0]["keyvalue"].ToString(); d7 = draSegvd[0]["desc"].ToString(); break;
        }
        DataRow[] draSubSegvd = dsSegvd.Tables[0].Select("issubto =" + id);//printsafter = 0 and
        if (draSubSegvd.Length > 0)
        {
          foreach (DataRow drsubsug in draSubSegvd)
          {
            SetStructure(Convert.ToInt32(drsubsug["id"]), nextId, level + 1, ref dsSegvd, ref objDataTable, s1, s2, s3, s4, s5, s6, s7, d1, d2, d3, d4, d5, d6, d7);
          }
        }
        else
        {
          DataRow dr = objDataTable.NewRow();
          dr["L1"] = s1;
          dr["L2"] = s2;
          dr["L3"] = s3;
          dr["L4"] = s4;
          dr["L5"] = s5;
          dr["L6"] = s6;
          dr["L7"] = s7;
          dr["D1"] = d1;
          dr["D2"] = d2;
          dr["D3"] = d3;
          dr["D4"] = d4;
          dr["D5"] = d5;
          dr["D6"] = d6;
          dr["D7"] = d7;
          objDataTable.Rows.Add(dr);
          switch (level)
          {
            case 1: s1 = string.Empty; d1 = string.Empty; break;
            case 2: s2 = string.Empty; d1 = string.Empty; break;
            case 3: s3 = string.Empty; d1 = string.Empty; break;
            case 4: s4 = string.Empty; d1 = string.Empty; break;
            case 5: s5 = string.Empty; d1 = string.Empty; break;
            case 6: s6 = string.Empty; d1 = string.Empty; break;
            case 7: s7 = string.Empty; d1 = string.Empty; break;
          }
        }
      }
      else
        return;
    }

    //**************************************************************************************************



    public static string GetKeyValueForRequisition(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      int locPosition, locLength;
      //DVOFlexSegCommon objDvoFlexSegVal = new DVOFlexSegCommon();
      List<DVOFlexSegCommon> objDVOFlexSegCommonlist = new List<DVOFlexSegCommon>();
      int locKeyLength;
      string ret_keyvalue = string.Empty;
      string copyKeyval = string.Empty;
      DataSet loadFlexFeptPrepDS = BLLFlexSegCommon.GetSegmentValInformation(ref tempDvoFexprm_entity_code_accType);
      if (loadFlexFeptPrepDS.Tables[0].Rows.Count > 0)
      {
        foreach (DataRow dr in loadFlexFeptPrepDS.Tables[0].Rows)
        {
          ret_keyvalue = ret_keyvalue + (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
        }
      }
      return ret_keyvalue.ToString().Trim();
    }
  }

}
