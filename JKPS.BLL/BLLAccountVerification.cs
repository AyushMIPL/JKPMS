using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLAccountVerification
    {
        public static DataTable AccountVerification()
        {
            DataTable objDataTable = new DataTable();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = null;
            try
            {
                DataSet ds = GetDataForVerification();
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    return ds.Tables[0];
                }
                objDataTable = ds.Tables[0].Clone();
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                string acct_type = string.Empty;
                int acct_no = 0;
                string keyvalue = string.Empty;
                DataSet ds_ingflxkh = null;
                DataRow dr_flxkh = null;
                DataSet ds_Flex_struct_Details = null;
                DataSet ds_flxdr = null;
                DataSet ds_gfkvad = null;
                bool errmess = true;
                int numSeg = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr=null;
                    dr = ds.Tables[0].Rows[i];

                    acct_type = dr["acct_type"] != DBNull.Value ? dr["acct_type"].ToString().Trim() : string.Empty;
                    acct_no = dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"]) : 0;
                    keyvalue = dr["keyvalue"] != DBNull.Value ? dr["keyvalue"].ToString().Trim() : string.Empty;

                    #region processing on before acct_type group...............
                    bool _postingStatus0 = false;
                    if (i != 0)
                    {
                        if (dr["acct_type"].ToString().Trim() != ds.Tables[0].Rows[i - 1]["acct_type"].ToString().Trim())
                        {
                            _postingStatus0 = true;
                        }
                    }
                    else if (i == 0)
                        _postingStatus0 = true;
                    if (_postingStatus0)
                    {
                        // OPEN gflxkhCurs USING curs.acct_type    
                        ds_ingflxkh = GET_ingflxkh_curs(acct_type);
                        if (ds_ingflxkh.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds_ingflxkh.Tables[0].Rows.Count; j++)
                            {
                                numSeg = 0;
                                dr_flxkh = ds_ingflxkh.Tables[0].Rows[j];
                                ds_Flex_struct_Details = GET_Flex_struct_Details_curs(Convert.ToInt32(dr_flxkh["id"]));
                                foreach (DataRow dr_fxacd in ds_Flex_struct_Details.Tables[0].Rows)
                                {
                                    //ds_flxdr = GET_inxflxdr_curs(Convert.ToInt32(dr_fxacd["flexsegid"]));
                                    DataSet dsdr = GET_inxflxdr_curs(Convert.ToInt32(dr_fxacd["flexsegid"]));
                                    if (dsdr.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds_flxdr == null)
                                            ds_flxdr = dsdr.Clone();
                                        ds_flxdr.Merge(dsdr);
                                    }
                                    else
                                    {
                                        dr["Problem2"] = "02 - Missing segment definition entry (inxflxdr) for segment id " + dr_fxacd["flexsegid"].ToString().Trim() + " For account type " + dr_flxkh["accounttype"];
                                        errmess = false;
                                        break; //return
                                    }
                                    numSeg++;
                                }
                                //numSeg = numSeg - 1;
                                if (numSeg < 1)
                                {
                                    dr["Problem3"] = "03 - Missing Detail Key Definition Entry (Flex_struct_Details) For account type " + dr_flxkh["accounttype"];
                                    errmess = false;
                                    break; //return
                                }
                            }
                        }
                        else
                        {
                            dr["Problem1"] = "01 - Missing Account Type Entry (ingflxkh)";
                            errmess = false;
                            break;//return
                        }
                    }
                    #endregion process on before acct_type group...............

                    #region processing on before acct_no group...............
                    bool _postingStatus1 = false;
                    if (i != 0)
                    {
                        if (Convert.ToInt32(dr["acct_no"]) != Convert.ToInt32(ds.Tables[0].Rows[i - 1]["acct_no"]))
                        {
                            _postingStatus1 = true;
                        }
                    }
                    else if (i == 0)
                        _postingStatus1 = true;
                    if (_postingStatus1)
                    {
                        //process on acct_no
                    }
                    #endregion process on before acct_no group...............

                    #region Process on on_every_row
                    // Define local variables  
                    int k;
                    int m = 0;
                    int locPos1 = 0;
                    int locPos2 = 0;
                    int locLen = 0;
                    int isSubTo = 0;
                    bool someNotProcessed = true;
                    string newKeyValue;


                    // FOR i = 1 TO 100
                    // INITIALIZE m_xsegvd[i].* TO NULL
                    // END FOR
                    List<DVOFlexKeySegmentDefinition> listm_Master_Segment = new List<DVOFlexKeySegmentDefinition>();
                    dr["segid"] = DBNull.Value;
                    dr["subdivides"] = DBNull.Value;
                    dr["subdividedby"] = DBNull.Value;
                    dr["part_keyvalue"] = DBNull.Value;
                    dr["segvdid"] = DBNull.Value;
                    dr["issubto"] = DBNull.Value;
                    dr["printsafter"] = DBNull.Value;

                    newKeyValue = dr["keyvalue"].ToString().Trim();
                    //# First, test the key value to see if it is of the correct structure
                    if (!ValidateKeyValue(acct_type,keyvalue))//IF NOT testFlexAccountKey( curs.acct_type, curs.keyvalue, "N", 0 )
                    {
                        int count = GET_inxchrtd_curs(acct_no);
                        if (count > 0)
                        {
                            dr["Problem4"] = "04 - Invalid account number with period entries";
                            dr["Problem0"] = "   - Account not deleted!";
                            errmess = false;
                        }
                        else
                        {
                            dr["Problem4"] = "04 - Invalid account number with no period entries";
                            errmess = false;
                            //DELETE FROM PayrollGLAccounts WHERE acct_no = curs.acct_no
                            int status = DeletePayrollGLAccountsAcct(ref objTransaction, acct_no);
                            //DELETE FROM ingfkvad WHERE accountnumber = curs.acct_no
                            int status2 = DeleteingfkvadAcct(ref objTransaction, acct_no);
                            if (status > 0 && status2 > 0)
                            {
                                dr["Problem0"] = "   - Account has been deleted!";
                            }
                        }
                        //dr["Problem0"] = null;
                        objDataTable.Rows.Add(dr.ItemArray);
                        break;
                    }

                    List<DVOFlexKeySegmentDefinition> p_xsegvd = new List<DVOFlexKeySegmentDefinition>();
                    List<DVOFlexKeySegmentDefinition> listMaster_Segment = new List<DVOFlexKeySegmentDefinition>();
                    while (true)
                    {
                        someNotProcessed = false;
                        for (k = 0; k < numSeg; k++)
                        {
                            if (listm_Master_Segment.Count == 0)
                            {
                                someNotProcessed = true;
                                if (Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["subdivides"]) == 0)
                                {
                                    isSubTo = 0;
                                    break;
                                }
                                else
                                {
                                    //# Find its parent
                                    isSubTo = 0;
                                    for (m = 0; m < numSeg; m++)
                                    {
                                        if (Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["subdivides"]) == Convert.ToInt32(ds_flxdr.Tables[0].Rows[m]["id"]))
                                        {
                                            //# found its parent, does it have a value?
                                            //IF m_xsegvd[j].id IS NOT NULL
                                            if (listm_Master_Segment.Count != 0)
                                            {
                                                //# Yes, so we can do this one now
                                                isSubTo = listm_Master_Segment[m].id;
                                            }
                                            //# Found parent (event if not yet defined so must exit)
                                            break;
                                        }
                                    }
                                    if (isSubTo != 0)
                                    {
                                        break;
                                    }
                                }
                            }//if end
                        } //for end

                        if (k > numSeg - 1)
                        {
                            if (someNotProcessed)
                            {
                                //CALL fg_err( 1 ) # Could not complete processing but passed by all
                                //EXIT PROGRAM( 1 )
                                return objDataTable;
                            }
                            else
                            {
                                //# All segments are done so exit loop
                                break;//EXIT WHILE
                            }
                        }
                        locPos1 = Convert.ToInt32(ds_Flex_struct_Details.Tables[0].Rows[k]["position"]) - 1;// m_gfxacd[i].position
                        locPos2 = Convert.ToInt32(ds_Flex_struct_Details.Tables[0].Rows[k]["position"]) + (Convert.ToInt32(ds_Flex_struct_Details.Tables[0].Rows[k]["length"]) - 1) - 1;//m_gfxacd[i].position + m_gfxacd[i].length - 1
                        locLen = Convert.ToInt32(ds_Flex_struct_Details.Tables[0].Rows[k]["length"]);//m_gfxacd[i].length
                        dr["segid"] = Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["id"]);//m_xflxdr[i].id
                        dr["subdivides"] = Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["subdivides"]);//m_xflxdr[i].subdivides
                        dr["subdividedby"] = Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["subdividedby"]);//m_xflxdr[i].subdividedby
                        dr["part_keyvalue"] = dr["keyvalue"].ToString().Substring(locPos1, locLen).Trim();//curs.keyvalue[locPos1, locPos2]

                        listm_Master_Segment = GET_Master_Segment1_curs(isSubTo, Convert.ToInt32(ds_Flex_struct_Details.Tables[0].Rows[k]["flexsegid"]), dr["part_keyvalue"].ToString().Trim());
                        if (listm_Master_Segment.Count == 0)
                        {
                            dr["segvdid"] = DBNull.Value;
                            dr["issubto"] = DBNull.Value;
                            dr["printsafter"] = DBNull.Value;
                            //listm_Master_Segment[k].id = -1;
                            dr["Problem5"] = "05 - Missing segment value for segmentId " + ds_Flex_struct_Details.Tables[0].Rows[k]["flexsegid"].ToString().Trim();
                            errmess = false;
                            //CALL ml_manual_output()
                        }
                        else
                        {
                            dr["segvdid"] = listm_Master_Segment[k].id;
                            dr["issubto"] = listm_Master_Segment[k].issubto;
                            dr["printsafter"] = listm_Master_Segment[k].printsafter;
                            //IF printAll
                            //THEN
                            //    CALL ml_manual_output()
                            //END IF
                        }
                        //# Test the printsafter to make sure it is of the same type
                        p_xsegvd = new List<DVOFlexKeySegmentDefinition>();

                        if(listm_Master_Segment.Count!=0)
                            if (listm_Master_Segment[k].printsafter != null && listm_Master_Segment[k].printsafter != 0)
                            {
                                p_xsegvd = GET_Master_Segment2_stmt(listm_Master_Segment[k].printsafter);
                                if (p_xsegvd.Count == 0)
                                {
                                    dr["Problem6"] = "06 - This segment value not connected via the printsafter, value not defined " + listm_Master_Segment[k].printsafter.ToString().Trim();
                                    errmess = false;
                                    //CALL ml_manual_output()
                                }
                                if (p_xsegvd.Count != 0)
                                    if (p_xsegvd[0].segmentid != listm_Master_Segment[k].segmentid)
                                    {
                                        dr["Problem7"] = "07 - Not same segment type as that of previous value defined by printsafter entry " + listm_Master_Segment[k].printsafter.ToString().Trim();
                                        errmess = false;
                                        //CALL ml_manual_output()
                                    }
                            }

                        //# Test the hierarchy
                        if (listm_Master_Segment.Count != 0)
                            if (listm_Master_Segment[k].issubto != null && listm_Master_Segment[k].issubto != 0)
                            {
                                //# First, find the segvd entry for the next higher level
                                p_xsegvd = GET_Master_Segment2_stmt(listm_Master_Segment[k].issubto);
                                if (p_xsegvd.Count == 0)
                                {
                                    dr["Problem8"] = "08 - This segment value not connected via the issubto, value not defined " + listm_Master_Segment[k].issubto.ToString().Trim();
                                    errmess = false;
                                    //CALL ml_manual_output()
                                }
                                //# Next, find its containing segment
                                if (listm_Master_Segment[k].segmentid != null)
                                {
                                    for (k = 0; k < numSeg; k++)
                                    {
                                        if (p_xsegvd[0].segmentid == Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["id"]))
                                        {
                                            //# Next higher segment level definition found Is this the higher level for our segment?
                                            if (Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["subdivides"]) != Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["id"]))
                                            {
                                                dr["Problem9"] = "09 - This segment value's segment hierarchy is not properly connected " + listm_Master_Segment[k].id;
                                                errmess = false;
                                                //CALL ml_manual_output()
                                            }
                                            //# Is out value in the next lower segment of the one found?
                                            if (Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["id"]) != Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["subdividedby"]))
                                            {
                                                dr["Problem10"] = "10 - This segment value's segment hierarchy is not properly connected " + listm_Master_Segment[k].id;
                                                errmess = false;
                                                //CALL ml_manual_output()
                                            }
                                            break;
                                        }
                                    }
                                    if (m > numSeg-1)
                                    {
                                        //# Next level up segment not found
                                        dr["Problem11"] = "11 - Next higher segment definition not found for segment value " + listm_Master_Segment[k].id;
                                        errmess = false;
                                        //CALL ml_manual_output()
                                    }
                                }
                            }
                        // # Now test to see if the entry in the segment values/acct no usage is there
                        if (listm_Master_Segment.Count != 0)
                            if (listm_Master_Segment[k].id != null && listm_Master_Segment[k].id != -1 && ds_ingflxkh.Tables[0].Rows[k]["id"] != null)
                            {
                                ds_gfkvad = GET_ingfkvad1_curs(acct_no, Convert.ToInt32(ds_ingflxkh.Tables[0].Rows[k]["id"]), listm_Master_Segment[k].id);
                                if (ds_gfkvad != null)
                                    if (ds_gfkvad.Tables[0].Rows.Count <= 0)
                                    {
                                        dr["Problem12"] = "12 - No usage entry for this acct/segment value";
                                        errmess = false;
                                        listMaster_Segment = new List<DVOFlexKeySegmentDefinition>();
                                        listMaster_Segment = GET_ingfkvad2_stmt(acct_no, Convert.ToInt32(ds_ingflxkh.Tables[0].Rows[k]["id"]), Convert.ToInt32(ds_flxdr.Tables[0].Rows[k]["id"]));
                                        if (listMaster_Segment == null || listMaster_Segment.Count == 0)
                                        {
                                            dr["Problem13"] = "13 - No entry for this acct/segment by segment";
                                            errmess = false;
                                            //IF corrErr
                                            //{
                                            //INSERT INTO ingfkvad VALUES (curs.acct_no,m_gflxkh.id,m_xsegvd[i].id
                                            if (!Insertingfkvad(ref objTransaction, acct_no, Convert.ToInt32(ds_ingflxkh.Tables[0].Rows[k]["id"]), listm_Master_Segment[k].id))
                                            {
                                                throw new Exception("Error has occurred while making inserting ingfkvad.");
                                            }
                                            dr["Problem0"] = "   - Value inserted!";
                                            //    CALL ml_manual_output()
                                            //END IF
                                        }
                                        else
                                        {
                                            if (listMaster_Segment[0].keyvalue != listm_Master_Segment[k].keyvalue)
                                            {
                                                dr["Problem14"] = "14 - key values do not match,got '" + listMaster_Segment[0].keyvalue.Trim() + "' from gfkvad ";
                                                errmess = false;
                                                //CALL ml_manual_output()
                                                newKeyValue = listMaster_Segment[0].keyvalue.Substring(0, locLen).Trim();
                                            }
                                        }
                                    }
                            }
                    }//end while
                    //# Reset to null for the report section to print right
                    //dr["segid"] = DBNull.Value;
                    //dr["subdivides"] = DBNull.Value;
                    //dr["subdividedby"] = DBNull.Value;
                    //dr["part_keyvalue"] = DBNull.Value;
                    //dr["segvdid"] = DBNull.Value;
                    //dr["issubto"] = DBNull.Value;
                    //dr["printsafter"] = DBNull.Value;
                    if (!errmess)
                    {
                        int count1 = GET_inxchrtd_curs(acct_no);
                        if (count1 > 0)
                            dr["Problem15"] = "15 - Account has entries in period detail";
                        else
                            dr["Problem16"] = "16 - Account is not used in any posting period";
                        //CALL ml_manual_output()
                        errmess = true;
                    }
                    //IF corrErr
                    //THEN
                    if (keyvalue != newKeyValue)
                    {
                        //UPDATE PayrollGLAccounts SET PayrollGLAccounts.keyvalue = newKeyValue WHERE PayrollGLAccounts.acct_no = curs.acct_no
                        
                        if (!UpdatePayrollGLAccounts(ref objTransaction, acct_no, newKeyValue))
                        {
                            throw new Exception("Error has occurred while making updating PayrollGLAccounts keyvalue.");
                        }
                        
                        dr["Problem17"] = "17 - KeyValue in chart changed";
                        //CALL ml_manual_output()
                    }

                    #endregion
                    objDataTable.Rows.Add(dr.ItemArray);
                }//end for
                    //COMMIT WORK
                    if (objTransaction != null)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //END IF
            }//end try
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return objDataTable;
        }

        #region GetDataForVerification--------------------------------
        public static DataSet GetDataForVerification()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            DVOGLPayrollGLAccounts objDVOGLPayrollGLAccounts = new DVOGLPayrollGLAccounts();
            DataSet ds = null;
            try
            {
                object[] parameters = new object[0];
                ds = ObjDALBaseClass.GetData(objDVOGLPayrollGLAccounts.GET_GL_ACCOUNTS(ref parameters));
                if (ds == null || ds.Tables.Count <= 0)
                    throw new Exception();
                ds.Tables[0].Columns[0].ColumnName = "acct_no";
                ds.Tables[0].Columns[1].ColumnName = "acct_type";
                ds.Tables[0].Columns[2].ColumnName = "keyvalue";
                ds.Tables[0].Columns[3].ColumnName = "acct_desc";
                ds.Tables[0].Columns.Add("segid", typeof(int));
                ds.Tables[0].Columns.Add("segvdid", typeof(int));
                ds.Tables[0].Columns.Add("issubto", typeof(int));
                ds.Tables[0].Columns.Add("printsafter", typeof(int));
                ds.Tables[0].Columns.Add("subdivides", typeof(int));
                ds.Tables[0].Columns.Add("subdividedby", typeof(int));
                ds.Tables[0].Columns.Add("part_keyvalue");
                ds.Tables[0].Columns.Add("Problem0");
                ds.Tables[0].Columns.Add("Problem1");
                ds.Tables[0].Columns.Add("Problem2");
                ds.Tables[0].Columns.Add("Problem3");
                ds.Tables[0].Columns.Add("Problem4");
                ds.Tables[0].Columns.Add("Problem5");
                ds.Tables[0].Columns.Add("Problem6");
                ds.Tables[0].Columns.Add("Problem7");
                ds.Tables[0].Columns.Add("Problem8");
                ds.Tables[0].Columns.Add("Problem9");
                ds.Tables[0].Columns.Add("Problem10");
                ds.Tables[0].Columns.Add("Problem11");
                ds.Tables[0].Columns.Add("Problem12");
                ds.Tables[0].Columns.Add("Problem13");
                ds.Tables[0].Columns.Add("Problem14");
                ds.Tables[0].Columns.Add("Problem15");
                ds.Tables[0].Columns.Add("Problem16");
                ds.Tables[0].Columns.Add("Problem17");
                parameters = null;
                ObjDALBaseClass = null;
                objDVOGLPayrollGLAccounts = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return ds;
        }
        #endregion GetDataForVerification--------------------------------------------

        #region GetData from ingflxkh ---------------------------------
        public static DataSet GET_ingflxkh_curs(string acct_type)
        {
            object[] parameters = new object[1];
            parameters[0] = acct_type;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLPayrollGLAccounts), (new DVOGLPayrollGLAccounts()).GET_Flex_struct_Header_curs);
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "DSingflxkh";
                    ds.Tables[0].Columns[0].ColumnName = "id";
                    ds.Tables[0].Columns[1].ColumnName = "accounttype";
                    ds.Tables[0].Columns[2].ColumnName = "desc";
                    ds.Tables[0].Columns[3].ColumnName = "keylength";
                    ds.Tables[0].Columns[4].ColumnName = "segmentcnt";
                    ds.Tables[0].Columns[5].ColumnName = "printsafter";
                    ds.Tables[0].Columns[6].ColumnName = "dfltacctcat";
                    ds.Tables[0].Columns[7].ColumnName = "dfltincrwcrdt";
                }
            return ds;
        }
        #endregion GetData from ingflxkh ------------------------------

        #region GetData from Flex_struct_Details ---------------------------------
        public static DataSet GET_Flex_struct_Details_curs(int id)
        {
            object[] parameters = new object[1];
            parameters[0] = id;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLPayrollGLAccounts), (new DVOGLPayrollGLAccounts()).GET_Flex_struct_Details_curs);
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "DSingflxkh";
                    ds.Tables[0].Columns[0].ColumnName = "strucid";
                    ds.Tables[0].Columns[1].ColumnName = "flexsegid";
                    ds.Tables[0].Columns[2].ColumnName = "position";
                    ds.Tables[0].Columns[3].ColumnName = "length";
                    ds.Tables[0].Columns[4].ColumnName = "required";
                    ds.Tables[0].Columns[5].ColumnName = "subtotal";
                }
            return ds;
        }
        #endregion GetData from Flex_struct_Details ------------------------------

        #region GetData from inxflxdr ---------------------------------
        public static DataSet GET_inxflxdr_curs(int flexsegid)
        {
            object[] parameters = new object[1];
            parameters[0] = flexsegid;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLPayrollGLAccounts), (new DVOGLPayrollGLAccounts()).GET_Master_Segment_curs);
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "DSingflxkh";
                    ds.Tables[0].Columns[0].ColumnName = "id";
                    ds.Tables[0].Columns[1].ColumnName = "desc";
                    ds.Tables[0].Columns[2].ColumnName = "keylength";
                    ds.Tables[0].Columns[3].ColumnName = "subdividedby";
                    ds.Tables[0].Columns[4].ColumnName = "subdivides";
                    ds.Tables[0].Columns[5].ColumnName = "segmenttype";
                }
            return ds;
        }
        #endregion GetData from inxflxdr ------------------------------

        #region GetData from inxchrtd count---------------------------------
        public static int GET_inxchrtd_curs(int acct_no)
        {
            int count = 0;
            object[] parameters = new object[1];
            parameters[0] = acct_no;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object Ob = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOGLPayrollGLAccounts()).GET_inxchrtd_curs);
            if (Ob == DBNull.Value || Ob == null || Ob.ToString().Trim().Length < 0)
                throw new Exception("Error occured while updating accountbalances on void SBTransaction.");
            else if (Convert.ToInt16(Ob) < 0)
                throw new Exception("Error occured while updating accountbalances on void SBTransaction.");
            if (Convert.ToInt16(Ob) >= 0)
                count = Convert.ToInt16(Ob);
            return count;
        }
        #endregion GetData from inxchrtd count------------------------------

        #region Delete data from PayrollGLAccounts -----------------------------
        public static int DeletePayrollGLAccountsAcct(ref object TransactionObject, int acct_no)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            int success = 0;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = acct_no;
                object obj = objDalBaseClass.ExecuteProcedure_ByTransaction(ref TransactionObject, ref parameters, (new DVOGLPayrollGLAccounts()).DELETE_PayrollGLAccounts_ACCT, true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region Delete data from ingfkvad -----------------------------
        public static int DeleteingfkvadAcct(ref object TransactionObject, int acct_no)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            int success = 0;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = acct_no;
                object obj = objDalBaseClass.ExecuteProcedure_ByTransaction(ref TransactionObject, ref parameters, (new DVOGLPayrollGLAccounts()).DELETE_ingfkvad_ACCT, true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region GetData from Master_Segment ---------------------------------
        public static List<DVOFlexKeySegmentDefinition> GET_Master_Segment1_curs(int issubto, int segmentid, string keyvalue)
        {
            object[] parameters = new object[3];
            parameters[0] = issubto;
            parameters[1] = segmentid;
            parameters[2] = keyvalue;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOFlexKeySegmentDefinition> list = new List<DVOFlexKeySegmentDefinition>();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLPayrollGLAccounts), (new DVOGLPayrollGLAccounts()).GET_Master_Segment1_curs);
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "DSMaster_Segment";
                    ds.Tables[0].Columns[0].ColumnName = "segmentid";
                    ds.Tables[0].Columns[1].ColumnName = "id";
                    ds.Tables[0].Columns[2].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[3].ColumnName = "desc";
                    ds.Tables[0].Columns[4].ColumnName = "printsafter";
                    ds.Tables[0].Columns[5].ColumnName = "issubto";
                    ds.Tables[0].Columns[6].ColumnName = "abbreviation";
                }
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    using (ds)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOFlexKeySegmentDefinition tempobj = new DVOFlexKeySegmentDefinition();
                            tempobj.segmentid = (dr["segmentid"] != DBNull.Value ? Convert.ToInt32(dr["segmentid"]) : 0);
                            tempobj.id = (dr["id"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0);
                            tempobj.keyvalue = (dr["keyvalue"] != DBNull.Value ? dr["keyvalue"].ToString().Trim() : string.Empty);
                            tempobj.desc = (dr["desc"] != DBNull.Value ? dr["desc"].ToString().Trim() : string.Empty);
                            tempobj.printsafter = (dr["printsafter"] != DBNull.Value ? Convert.ToInt32(dr["printsafter"]) : 0);
                            tempobj.issubto = (dr["issubto"] != DBNull.Value ? Convert.ToInt32(dr["issubto"]) : 0);
                            tempobj.abbreviation = (dr["abbreviation"] != DBNull.Value ? dr["abbreviation"].ToString().Trim() : string.Empty);
                            list.Add(tempobj);
                        }
                    }
                }
            return list;
        }
        #endregion GetData from Master_Segment ------------------------------

        #region GetData from Master_Segment by id ---------------------------------
        public static List<DVOFlexKeySegmentDefinition> GET_Master_Segment2_stmt(int id)
        {
            object[] parameters = new object[1];
            parameters[0] = id;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOFlexKeySegmentDefinition> list = new List<DVOFlexKeySegmentDefinition>();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLPayrollGLAccounts), (new DVOGLPayrollGLAccounts()).GET_Master_Segment2_stmt);
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "DSMaster_Segment";
                    ds.Tables[0].Columns[0].ColumnName = "segmentid";
                    ds.Tables[0].Columns[1].ColumnName = "id";
                    ds.Tables[0].Columns[2].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[3].ColumnName = "desc";
                    ds.Tables[0].Columns[4].ColumnName = "printsafter";
                    ds.Tables[0].Columns[5].ColumnName = "issubto";
                    ds.Tables[0].Columns[6].ColumnName = "abbreviation";
                }
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    using (ds)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOFlexKeySegmentDefinition tempobj = new DVOFlexKeySegmentDefinition();
                            tempobj.segmentid = (dr["segmentid"] != DBNull.Value ? Convert.ToInt32(dr["segmentid"]) : 0);
                            tempobj.id = (dr["id"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0);
                            tempobj.keyvalue = (dr["keyvalue"] != DBNull.Value ? dr["keyvalue"].ToString().Trim() : string.Empty);
                            tempobj.desc = (dr["desc"] != DBNull.Value ? dr["desc"].ToString().Trim() : string.Empty);
                            tempobj.printsafter = (dr["printsafter"] != DBNull.Value ? Convert.ToInt32(dr["printsafter"]) : 0);
                            tempobj.issubto = (dr["issubto"] != DBNull.Value ? Convert.ToInt32(dr["issubto"]) : 0);
                            tempobj.abbreviation = (dr["abbreviation"] != DBNull.Value ? dr["abbreviation"].ToString().Trim() : string.Empty);
                            list.Add(tempobj);
                        }
                    }
                }
            return list;
        }
        #endregion GetData from Master_Segment by id ------------------------------

        #region GetData from ingfkvad ---------------------------------
        public static DataSet GET_ingfkvad1_curs(int acct_no, int strucid, int segvd_id)
        {
            int count = 0;
            object[] parameters = new object[3];
            parameters[0] = acct_no;
            parameters[1] = strucid;
            parameters[2] = segvd_id;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLPayrollGLAccounts), (new DVOGLPayrollGLAccounts()).GET_ingfkvad1_curs);
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "DSingflxkh";
                    ds.Tables[0].Columns[0].ColumnName = "accountnumber";
                    ds.Tables[0].Columns[1].ColumnName = "strucid";
                    ds.Tables[0].Columns[2].ColumnName = "segvd_id";
                }
            return ds;
        }
        #endregion GetData from ingfkvad ------------------------------

        #region GetData from ingfkvad by id ---------------------------------
        public static List<DVOFlexKeySegmentDefinition> GET_ingfkvad2_stmt(int accountnumber, int strucid, int segmentid)
        {
            object[] parameters = new object[3];
            parameters[0] = accountnumber;
            parameters[1] = strucid;
            parameters[2] = segmentid;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOFlexKeySegmentDefinition> listMaster_Segment = new List<DVOFlexKeySegmentDefinition>();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGLPayrollGLAccounts), (new DVOGLPayrollGLAccounts()).GET_ingfkvad2_stmt);
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "DSMaster_Segment";
                    ds.Tables[0].Columns[0].ColumnName = "segmentid";
                    ds.Tables[0].Columns[1].ColumnName = "id";
                    ds.Tables[0].Columns[2].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[3].ColumnName = "desc";
                    ds.Tables[0].Columns[4].ColumnName = "printsafter";
                    ds.Tables[0].Columns[5].ColumnName = "issubto";
                    ds.Tables[0].Columns[6].ColumnName = "abbreviation";
                }
            if (ds != null)
                if (ds.Tables.Count > 0)
                {
                    using (ds)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOFlexKeySegmentDefinition tempobj = new DVOFlexKeySegmentDefinition();
                            tempobj.segmentid = (dr["segmentid"] != DBNull.Value ? Convert.ToInt32(dr["segmentid"]) : 0);
                            tempobj.id = (dr["id"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0);
                            tempobj.keyvalue = (dr["keyvalue"] != DBNull.Value ? dr["keyvalue"].ToString().Trim() : string.Empty);
                            tempobj.desc = (dr["desc"] != DBNull.Value ? dr["desc"].ToString().Trim() : string.Empty);
                            tempobj.printsafter = (dr["printsafter"] != DBNull.Value ? Convert.ToInt32(dr["printsafter"]) : 0);
                            tempobj.issubto = (dr["issubto"] != DBNull.Value ? Convert.ToInt32(dr["issubto"]) : 0);
                            tempobj.abbreviation = (dr["abbreviation"] != DBNull.Value ? dr["abbreviation"].ToString().Trim() : string.Empty);
                            listMaster_Segment.Add(tempobj);
                        }
                    }
                }
            return listMaster_Segment;
        }
        #endregion GetData from Master_Segment by id ------------------------------

        #region Update PayrollGLAccounts keyvalue ------------------------------------
        public static bool UpdatePayrollGLAccounts(ref object objTransaction, int acct_no, string newkeyvalue)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            DVOGLPayrollGLAccounts objDVOGLPayrollGLAccounts = new DVOGLPayrollGLAccounts();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = acct_no;
                parameters[1] = newkeyvalue;
                Object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOGLPayrollGLAccounts.UPDATE_PayrollGLAccounts);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) != 1)
                    throw new Exception();
                parameters = null;
                objDALBaseClass = null;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
            return false;
        }
        #endregion Update PayrollGLAccounts keyvalue ------------------------------------

        #region insert data ingfkvad ------------------------------------
        public static bool Insertingfkvad(ref object objTransaction,int acct_no,int flxkhid,int Master_Segmentid)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DVOGLPayrollGLAccounts objDVOGLPayrollGLAccounts = new DVOGLPayrollGLAccounts();
            try
            {
                object[] InsParameter = new object[3];
                InsParameter[0] = acct_no;
                InsParameter[1] = flxkhid;
                InsParameter[2] = Master_Segmentid;
                object obj = objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref InsParameter, typeof(DVOGLPayrollGLAccounts), objDVOGLPayrollGLAccounts.INSERT_Flex_struct_Header);
                if (obj == null)
                    throw new Exception("Error occured to insert data in sbvoidclsacct.");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured to insert data in sbvoidclsacct.");

                InsParameter = null;
                objDalBaseClass = null;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
            return false;
        }
        #endregion insert data ingfkvad ------------------------------------


        #region Validate keyvalue structrue ------------------------------
        public static bool ValidateKeyValue(string acct_type,string keyvalue)
        {
            bool status = false;
            DVOGLAccountTypeMaintenance objDVOGLAccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
            objDVOGLAccountTypeMaintenance.accounttype = acct_type.Trim();
            List<DVOGLAccountTypeMaintenance> listDVOGLAccountTypeMaintenance = BLLGLAccountTypeMaintenance.GetAccountTypeMaintenance(ref objDVOGLAccountTypeMaintenance);
            if (listDVOGLAccountTypeMaintenance.Count > 0)
            {
                if (listDVOGLAccountTypeMaintenance[0].keylength != keyvalue.Trim().Length)
                     status = false;
                else
                     status = true;
            }
            else
                 status = false;
            listDVOGLAccountTypeMaintenance = null;
            objDVOGLAccountTypeMaintenance = null;
            return status;
        }
        #endregion Validate keyvalue structrue ------------------------------
    }
}
