using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;

namespace JKPS.BLL
{
 public   class BLLUpdateSalaryPositions
    {
        //**********************
        //Added by Sunil Pahwa
       //#######################
        public static int InsertSalaryInfo(ref DVOUpdateSalaryPositions objdvoins)
        {
            try
            {

                object success = null;
                object[] InParameters = new object[5];
                InParameters[0] = objdvoins.code;
                InParameters[1] = objdvoins.desc;
                InParameters[2] = objdvoins.dflt_cat_code;
                InParameters[3] = objdvoins.dflt_scale_code;
                InParameters[4] = objdvoins.dflt_py_acct_type;

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

                success = objDalBaseClass.InsertData(ref InParameters, typeof(DVOUpdateSalaryPositions), true);
               
                if (success == null)
                    throw new  Exception();
                else if (Convert.ToInt32(success) < 1)
                    throw new Exception();
                InParameters = null;
                return Convert.ToInt16(success);
               
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return 0;
            
        }

     public static List<DVOUpdateSalaryPositions> GetOnLoad()
     {
         List<DVOUpdateSalaryPositions> oblList = new List<DVOUpdateSalaryPositions>();

         DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
         DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
         using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOUpdateSalaryPositions)))
         {
             foreach (DataRow dr in ds.Tables[0].Rows)
             {
                 //if (!Convert.IsDBNull(dr[0])) ob.empl_code = dr[0].ToString().Trim();
                
                 DVOUpdateSalaryPositions obj = new DVOUpdateSalaryPositions();
                 if (!Convert.IsDBNull(dr[0])) obj.code = dr[0].ToString().Trim();
                 if (!Convert.IsDBNull(dr[1])) obj.desc = dr[1].ToString().Trim();
                 if (!Convert.IsDBNull(dr[2])) obj.mincode = Convert.ToString(dr[2]);
                 if (!Convert.IsDBNull(dr[3])) obj.maxcode = dr[3].ToString().Trim();
                 if (!Convert.IsDBNull(dr[4])) obj.minperannum = Convert.ToDecimal(dr[4]);
                 if (!Convert.IsDBNull(dr[5])) obj.maxperannum = Convert.ToDecimal(dr[5]);
                 if (!Convert.IsDBNull(dr[6])) obj.scalecode = dr[6].ToString().Trim();
                 oblList.Add(obj);

             }
         }

         return oblList;


     }

     //public static List<DVOUpdateSalaryPositions> GetSalPositionInfo(ref DVOUpdateSalaryPositions objUpdSaldef)
     //{
     //    object[] Parameters = new object[4];
     //    Parameters[0] = objUpdSaldef.code;
     //    Parameters[1] = objUpdSaldef.desc;
     //    Parameters[2] = objUpdSaldef.dflt_cat_code;
     //    Parameters[3] = objUpdSaldef.dflt_scale_code;

     //    List<DVOUpdateSalaryPositions> lstUpdSalPos = new List<DVOUpdateSalaryPositions>();
     //   // List<DVOUpdateBudgetControlDefaults> objBudConDefList = new List<DVOUpdateBudgetControlDefaults>();

     //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
     //    using (DataSet ds = objDalBaseClass.GetData(ref Parameters, typeof(DVOUpdateSalaryPositions ),(new DVOUpdateSalaryPositions()).uspUpdSalPosiget))
     //    {
     //        foreach (DataRow dr in ds.Tables[0].Rows)
     //        {
     //           DVOUpdateSalaryPositions obj = new DVOUpdateSalaryPositions();
     //           obj.code = dr[0].ToString().Trim();
     //           obj.desc = dr[1].ToString().Trim();
     //           obj.dflt_cat_code = dr[2].ToString().Trim();
     //           obj.dflt_scale_code = dr[3].ToString().Trim();
     //           obj.dflt_py_acct_type = dr[6].ToString().Trim();
     //           obj.acct_type = dr[7].ToString();
     //           obj.acct_desc = dr[8].ToString(); ;
     //           obj.keyvalue = dr[9].ToString().Trim();
     //           lstUpdSalPos.Add(obj);

     //        }
     //        return lstUpdSalPos;
     //    }
     //}
     /// <summary>
     /// Update the salary position information
     /// </summary>
     /// <param name="objUpdDSalPos"></param>
     /// <returns></returns>
     //public static int UpdateSalPosInfo(ref DVOUpdateSalaryPositions objUpdDSalPos)
     //{
     //    int success = 0;
     //    object[] UpdParameters = new object[5];
     //    UpdParameters[0] = objUpdDSalPos.code;
     //    UpdParameters[1] = objUpdDSalPos.desc;
     //    UpdParameters[2] = objUpdDSalPos.dflt_cat_code;
     //    UpdParameters[3] = objUpdDSalPos.dflt_scale_code ;
     //    UpdParameters[4] = objUpdDSalPos.dflt_py_acct_type;
     //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
     //    try
     //    {
     //        success = objDalBaseClass.UpdateData(ref UpdParameters, typeof(DVOUpdateSalaryPositions ));
     //    }

     //    catch (Exception ex)
     //    {
     //        ExceptionManager.Publish(ex);
     //        System.Windows.Forms.MessageBox.Show(ex.Message);
     //    }
     //    return success;
     //}

     public static List<DVOUpdateSalaryPositions> getDfltSalScale()
     {

         List<DVOUpdateSalaryPositions> listDflt = new List<DVOUpdateSalaryPositions>();
         object[] parameter = new object[0];

         DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
         DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
         using (DataSet ds = objDalBaseClass.GetData(ref parameter ,typeof(DVOUpdateSalaryPositions),(new DVOUpdateSalaryPositions()).uspDfltSalScgetall))
         {
             foreach (DataRow dr in ds.Tables[0].Rows)
             {
                 DVOUpdateSalaryPositions obj1 = new DVOUpdateSalaryPositions();
                 if (!Convert.IsDBNull(dr[0])) obj1.code1 = dr[0].ToString().Trim();
                 if (!Convert.IsDBNull(dr[1])) obj1.per_annum = Convert.ToDecimal(dr[1].ToString().Trim());
                 listDflt.Add(obj1);

             }
         }
         return listDflt;
     }

     public static List<DVOUpdateSalaryPositions> GetSalPosition(ref DVOUpdateSalaryPositions obj)
     {
         List<DVOUpdateSalaryPositions> listDflt = new List<DVOUpdateSalaryPositions>();
         object[] parameter = new object[1];
         parameter[0] = obj.code;
         DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
         DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
         using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdateSalaryPositions), (new DVOUpdateSalaryPositions()).usppaysalposget))
         {
             foreach (DataRow dr in ds.Tables[0].Rows)
             {
                 DVOUpdateSalaryPositions obj1 = new DVOUpdateSalaryPositions();
                 if (!Convert.IsDBNull(dr[0])) obj1.mincode = dr[0].ToString().Trim();
                 if (!Convert.IsDBNull(dr[1])) obj1.maxcode = dr[1].ToString().Trim();
                 if (!Convert.IsDBNull(dr[2])) obj1.minperannum = Convert.ToDecimal(dr[2]);
                 if (!Convert.IsDBNull(dr[3])) obj1.maxperannum = Convert.ToDecimal(dr[3]);

                 listDflt.Add(obj1);

             }
         }
         return listDflt;
     }


     public static int InsertDetailInformation(ref DVOUpdateSalaryPositions objdvoins)
     {
         int success = 0;
         //object success;
         object[] InParameters = new object[7];
         InParameters[0] = objdvoins.code;
         InParameters[1] = objdvoins.description;
         InParameters[2] = objdvoins.mincode;
         InParameters[3] = objdvoins.maxcode;
         InParameters[4] = objdvoins.minperannum;
         InParameters[5] = objdvoins.maxperannum;
         InParameters[6] = objdvoins.keyvalue;

         DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
         DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
         try
         {
             success =Convert.ToInt32 ( objDalBaseClass.InsertData(ref InParameters, typeof(DVOUpdateSalaryPositions), (new DVOUpdateSalaryPositions()).usppaysalDetailIns));
         }

         catch (Exception ex)
         {
             ExceptionManager.Publish(ex);
             System.Windows.Forms.MessageBox.Show(ex.Message);
         }
         return success;
            
        
     }

     public static List<DVOUpdateSalaryPositions> GetAllInfo()
     {
         

         List<DVOUpdateSalaryPositions> lstUpdSalPos = new List<DVOUpdateSalaryPositions>();
         DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
         DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
         using (DataSet ds = objDalBaseClass.GetData(typeof(DVOUpdateSalaryPositions), (new DVOUpdateSalaryPositions()).uspUpdSalgetall))
         {
             foreach (DataRow dr in ds.Tables[0].Rows)
             {
                 DVOUpdateSalaryPositions obj = new DVOUpdateSalaryPositions();
                 if (!Convert.IsDBNull(dr[0])) obj.code = dr[0].ToString().Trim();
                 if (!Convert.IsDBNull(dr[1])) obj.desc = dr[1].ToString().Trim();
                 if (!Convert.IsDBNull(dr[2])) obj.code1 = dr[2].ToString().Trim();
                 if (!Convert.IsDBNull(dr[3])) obj.description = dr[3].ToString().Trim();
                 if (!Convert.IsDBNull(dr[5])) obj.dflt_scale_code = dr[5].ToString().Trim();
                 if (!Convert.IsDBNull(dr[8])) obj.dflt_py_acct_type = dr[8].ToString().Trim();

                 if (!Convert.IsDBNull(dr[9])) obj.mincode = dr[9].ToString().Trim();
                 if (!Convert.IsDBNull(dr[10])) obj.maxcode = dr[10].ToString().Trim();
                 if (!Convert.IsDBNull(dr[11])) obj.minperannum = Convert.ToDecimal(dr[11]);
                 if (!Convert.IsDBNull(dr[12])) obj.maxperannum = Convert.ToDecimal(dr[12]);

                 lstUpdSalPos.Add(obj);

             }
             return lstUpdSalPos;
         }
     }

     public static List<DVOUpdateSalaryPositions> GetInfo(ref DVOUpdateSalaryPositions pobjDVOUpdateSalaryPositions)
     {
         DataTable dt = null;

         List<DVOUpdateSalaryPositions> lstUpdSalPos = new List<DVOUpdateSalaryPositions>();
         DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
         DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
         using (DataSet ds = objDalBaseClass.GetData(typeof(DVOUpdateSalaryPositions), pobjDVOUpdateSalaryPositions.uspUpdSalgetall))
         {
             if(ds!=null)
                 if(ds.Tables.Count>0)
                     if (ds.Tables[0].Rows.Count > 0)
                     {
                        
                         ds.Tables[0].Columns[0].ColumnName = "code";
                         ds.Tables[0].Columns[1].ColumnName = "desc";
                         ds.Tables[0].Columns[4].ColumnName = "dflt_cat_code";
                         ds.Tables[0].Columns[5].ColumnName = "dflt_scale_code";
                         ds.Tables[0].Columns[13].ColumnName = "rowid";
                         //ds.Tables[0].DefaultView.Sort = "rowid";
                         //dt = ds.Tables[0].DefaultView.ToTable();
                         //DataView dv = ds.Tables[0].DefaultView;
                        // dv.Sort = "rowid";
                         StringBuilder _searchCondition = new StringBuilder();
                         if (pobjDVOUpdateSalaryPositions.code != null)
                             if (pobjDVOUpdateSalaryPositions.code != string.Empty)
                                 _searchCondition.Append(" code='" + pobjDVOUpdateSalaryPositions.code.Trim() + "'");
                         if (pobjDVOUpdateSalaryPositions.desc != null)
                             if (pobjDVOUpdateSalaryPositions.desc != string.Empty)
                             {
                                 if (_searchCondition.ToString() != string.Empty) _searchCondition.Append(" AND");
                                 _searchCondition.Append(" desc='" + pobjDVOUpdateSalaryPositions.desc.Trim() + "'");
                             }
                         if (pobjDVOUpdateSalaryPositions.dflt_cat_code != null)
                             if (pobjDVOUpdateSalaryPositions.dflt_cat_code != string.Empty)
                             {
                                 if (_searchCondition.ToString() != string.Empty) _searchCondition.Append(" AND");
                                 _searchCondition.Append(" dflt_cat_code='" + pobjDVOUpdateSalaryPositions.dflt_cat_code.Trim() + "'");
                             }
                         if (pobjDVOUpdateSalaryPositions.dflt_scale_code != null)
                             if (pobjDVOUpdateSalaryPositions.dflt_scale_code != string.Empty)
                             {
                                 if (_searchCondition.ToString() != string.Empty) _searchCondition.Append(" AND");
                                 _searchCondition.Append(" dflt_scale_code='" + pobjDVOUpdateSalaryPositions.dflt_scale_code.Trim() + "'");
                             }

                       //  if (pobjDVOUpdateSalaryPositions.Rowid  != null)
                             if (pobjDVOUpdateSalaryPositions.Rowid  != 0)
                             {
                                 if (_searchCondition.ToString() != string.Empty) _searchCondition.Append(" AND");
                                 _searchCondition.Append(" rowid='" + pobjDVOUpdateSalaryPositions.Rowid + "'");
                             }
                             //ds.Tables[0].DefaultView.Sort = "rowid";
                         DataRow[] datarows = ds.Tables[0].Select(_searchCondition.ToString());
                         //DataRow[] datarows = dt.Select(_searchCondition.ToString());
                         
                    try
                    {
                        foreach (DataRow dr in datarows)
                        {
                            DVOUpdateSalaryPositions obj = new DVOUpdateSalaryPositions();
                            if (!Convert.IsDBNull(dr[0])) obj.code = dr[0].ToString().Trim();
                            if (!Convert.IsDBNull(dr[1])) obj.desc = dr[1].ToString().Trim();
                            if (!Convert.IsDBNull(dr[2])) obj.code1 = dr[2].ToString().Trim();
                            if (!Convert.IsDBNull(dr[3])) obj.description = dr[3].ToString().Trim();
                            if (!Convert.IsDBNull(dr[5])) obj.dflt_scale_code = dr[5].ToString().Trim();
                            if (!Convert.IsDBNull(dr[8])) obj.dflt_py_acct_type = dr[8].ToString().Trim();

                            if (!Convert.IsDBNull(dr[9])) obj.mincode = dr[9].ToString().Trim();
                            if (!Convert.IsDBNull(dr[10])) obj.maxcode = dr[10].ToString().Trim();
                            if (!Convert.IsDBNull(dr[11])) obj.minperannum = Convert.ToDecimal(dr[11].ToString().Trim());
                            if (!Convert.IsDBNull(dr[12])) obj.maxperannum = Convert.ToDecimal(dr[12].ToString().Trim());

                            if (!Convert.IsDBNull(dr[13])) obj.Rowid = Convert.ToInt32(dr[13].ToString().Trim());

                            lstUpdSalPos.Add(obj);
                        }
                       
                         }
                         catch { }
                     }
             return lstUpdSalPos;
         }
     }


     public static int DeleteInfo(ref object objTransaction, ref DVOUpdateSalaryPositions objUpdDSalPos)
     {
         
             DVOUpdateSalaryPositions objDVOUpdateSalaryPositionsdel = new DVOUpdateSalaryPositions();
             DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
             bool statusObjTransaction = true;
             if (objTransaction == null)
             {
                 objTransaction = objDALBaseClassHelper.GetTransactionObject();
                 statusObjTransaction = false;
             }
             DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();    

             object[] parameter = new object[1];
             parameter[0] = objUpdDSalPos.Rowid;
             try
             {
                 object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVOUpdateSalaryPositionsdel.DELETE_SPNAME);
             if (o == null)
                 throw new Exception();
             else if (Convert.ToInt32(o) < 1)
                 throw new Exception();

             parameter = null;
             objDALBaseClass = null;
             if (!statusObjTransaction)
             objDALBaseClassHelper.CommitTransaction(ref objTransaction);
             return Convert.ToInt16(o);
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

     public static int UpdateSalPosInformation(ref object objTransaction, ref DVOUpdateSalaryPositions objDvoUpdateSalPosIns)
     {
         object success =null;
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
             
             object[] UpdParameters = new object[6];
             UpdParameters[0] = objDvoUpdateSalPosIns.code;
             UpdParameters[1] = objDvoUpdateSalPosIns.desc;
             UpdParameters[2] = objDvoUpdateSalPosIns.dflt_cat_code;
             UpdParameters[3] = objDvoUpdateSalPosIns.dflt_scale_code;
             UpdParameters[4] = objDvoUpdateSalPosIns.dflt_py_acct_type;
             UpdParameters[5] = objDvoUpdateSalPosIns.Rowid;
           
             success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UpdParameters, typeof(DVOUpdateSalaryPositions),true);
             if (success == null)
                 throw new Exception();
             else if (Convert.ToInt32(success) < 1)
                 throw new Exception();

             UpdParameters = null;
             if (!statusObjTransaction)
                 objDALBaseClassHelper.CommitTransaction(ref objTransaction);
             return Convert.ToInt16(success);
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
 }
}
