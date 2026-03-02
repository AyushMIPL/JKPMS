using System;
using System.Collections.Generic;
using System.Text;
using JKPS.CommonUtilities;
using JKPS.DL;
using JKPS.COMMON;
using System.Data;
using ExceptionManagement;


namespace JKPS.BLL
{
  
 
    //
    public class BLLFlexSegment
    {
        //inserting the flxsegment information  in the database
        public static int InsertFlxseg(ref DVOFlexSegment objflxseg)
        {
            int success = 0;
            object[] parameter = new object[5];
            parameter[0] = objflxseg.desc;
            parameter[1] = objflxseg.keylength;
            parameter[2] = objflxseg.subdividedby;
            parameter[3] = objflxseg.subdivides;
            parameter[4] = objflxseg.segmenttype;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                success = objDalBaseClass.InsertData(ref parameter, typeof(DVOFlexSegment));
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return success;
        }

        //returning a list of all the table contents to the presentation layer
        //table name is Master_Segment
        public static List<DVOFlexSegment> GetALLSegment()
        {
            List<DVOFlexSegment> objFlxsegmentlst = new List<DVOFlexSegment>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOFlexSegment)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOFlexSegment objflexSeg = new DVOFlexSegment();
                  if(!Convert.IsDBNull(dr[0]))  objflexSeg.id = Convert.ToInt32(dr[0]);//"p_id"
                  if (!Convert.IsDBNull(dr[1])) objflexSeg.desc = dr[1].ToString().Trim();//"p_desc"
                  if (!Convert.IsDBNull(dr[2])) objflexSeg.keylength = Convert.ToInt32(dr[2]);//"p_keylength"
                    //objflexSeg.subdividedby=Convert.ToInt32(dr["p_subdividedby"]);
                  if (!Convert.IsDBNull(dr[3])) objflexSeg.subdivides = Convert.ToInt32(dr[3]);//"p_subdivides"
                  if (!Convert.IsDBNull(dr[4])) objflexSeg.segmenttype = (dr[4]).ToString().Trim();//"p_segmenttype"
                 if (!Convert.IsDBNull(dr[5])) objflexSeg.segmenttype = (dr[5]).ToString().Trim();//"p_segmenttype"
                    objFlxsegmentlst.Add(objflexSeg);
                }
                return objFlxsegmentlst;
            }
        }

      
        public static List<DVOFlexSegment> GetALLSegmentNotInSubdivideby(ref DVOFlexSegment objflxseg)
        {
            try
            {   
                List<DVOFlexSegment> objFlxsegmentlst = new List<DVOFlexSegment>();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object[] parameters = new object[1];
                parameters[0] = objflxseg.id;
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexSegment),(new DVOFlexSegment().FINDSUBDIVIDE_SPNAME)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOFlexSegment objflexSeg = new DVOFlexSegment();
                        objflexSeg.id = Convert.ToInt32(dr[0]);//"p_id"
                        objflexSeg.desc = dr[1].ToString().Trim();//"p_desc"
                        objflexSeg.keylength = Convert.ToInt32(dr[2]);//"p_keylength"
                        objflexSeg.subdividedby=Convert.ToInt32(dr[3]);//"p_subdividedby"
                        objflexSeg.subdivides = Convert.ToInt32(dr[4]);//"p_subdivides"
                        objflexSeg.segmenttype = (dr[5]).ToString().Trim();//"p_segmenttype"
                        objFlxsegmentlst.Add(objflexSeg);
                    }
                    return objFlxsegmentlst;
                }
            }
            catch (Exception ex)
            {

            }
            return null;

            
        }

        //returning a list of all the table contents to the presentation layer
        //table name is Master_Segment
        public static List<DVOFlexSegment> GetSegment(ref DVOFlexSegment objflxseg)
        {
            object[] parameters = new object[7];
            parameters[0] = objflxseg.id;
            parameters[1] = objflxseg.desc;
            parameters[2] = objflxseg.keylength;

            parameters[3] = objflxseg.subdividedby;
            parameters[4] = objflxseg.subdivides;
            parameters[5] = objflxseg.segmenttype;
            parameters[6] = objflxseg.Rowid;

            List<DVOFlexSegment> objflxseglst = new List<DVOFlexSegment>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexSegment)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOFlexSegment obflxseg = new DVOFlexSegment();
                    if (!Convert.IsDBNull(dr[0])) obflxseg.id = Convert.ToInt32(dr[0]);//"p_id"
                    if (!Convert.IsDBNull(dr[1])) obflxseg.desc = dr[1].ToString().Trim();//"p_desc"
                    if (!Convert.IsDBNull(dr[2])) obflxseg.keylength = Convert.ToInt32(dr[2]);//"p_keylength"
                    if (!Convert.IsDBNull(dr[3])) obflxseg.subdividedby = Convert.ToInt32(dr[3]);//"p_subdividedby"
                    if (!Convert.IsDBNull(dr[4])) obflxseg.subdividedbydesc = dr[4].ToString().Trim();//"p_subdividedbydesc"
                    if (!Convert.IsDBNull(dr[5])) obflxseg.subdivides = Convert.ToInt32(dr[5]);//"p_subdivides"
                    if (!Convert.IsDBNull(dr[6])) obflxseg.segmenttype = dr[6].ToString().Trim();//"p_segmenttype"
                    if (!Convert.IsDBNull(dr[7])) obflxseg.Rowid =Convert.ToInt32 (dr[7].ToString().Trim());//"p_rowid"
                    objflxseglst.Add(obflxseg);
                }
            }

            return objflxseglst;
        }

        //updating the flxseg table in id base
        //public static int UpdateFlxSeg(ref DVOFlexSegment objflxsegupd)
        //{
        //    object[] Parameter = new object[3];
        //    Parameter[0] = objflxsegupd.id;
        //    Parameter[1] = objflxsegupd.desc;
        //    Parameter[2] = objflxsegupd.keylength;

        //   // Parameter[3] = objflxsegupd.subdividedby;
        //  //  Parameter[4] = objflxsegupd.subdivides;
        //   // Parameter[5] = objflxsegupd.segmenttype;
        //    //connectionstring is taken in the object
        //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
            
        //    int c = objDalBaseClass.UpdateData(ref Parameter, typeof(DVOFlexSegment));
        //    return c;
        //}


        public static int DeleterFlxSeg(ref DVOFlexSegment objflxdel)
        {
            int success = 0;
            object[] parameter = new object[1];
            parameter[0] = objflxdel.id;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                success = objDalBaseClass.DeleteData(ref parameter, typeof(DVOFlexSegment));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return success;
        }

        public static DataSet GetALLSeg()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetAllData(typeof(DVOFlexSegment));
            return ds;
        }

       
        public static DataSet GetFlexSegmentItems(ref DVOFlexSegment objflxseg)
        {
            ////object[] parameters = new object[2];
            ////parameters[0] = objflxseg.id;
            ////parameters[1] = objflxseg.IssubtoInSegmentItems;
            ////parameters[2] = objflxseg.PositionInAccountKeyValue;
            ////parameters[3] = objflxseg.LenghtInAccountKeyValue;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexSegment), objflxseg.GET_SEGMENT_ITEMS);
            DataSet ds = objDalBaseClass.GetData(objflxseg.GET_SEGMENT_ITEMS);
            if (ds != null && ds.Tables.Count > 0)
            {
                ds.Tables[0].Columns[0].ColumnName = "v_segmentid";
                ds.Tables[0].Columns[1].ColumnName = "v_id";
                ds.Tables[0].Columns[2].ColumnName = "v_keyvalue";
                ds.Tables[0].Columns[3].ColumnName = "v_segitm_desc";
                ds.Tables[0].Columns[4].ColumnName = "v_issubto";
                ds.Tables[0].Columns[5].ColumnName = "v_seg_desc";
            } 
            /* Column 1 -   v_segmentid
             * Column 2 -   v_id
             * Column 3 -   v_keyvalue
             * Column 4 -   v_segitm_desc
             * Column 5 -   v_issubto
             * Column 6 -   v_seg_desc       */
            ////parameters = null;
            objDalBaseClass = null;
            return ds;
        }
        //********************************************************************************
        //Added by sanjay 
        //For Report Print Employee List By Ministry
        //returning a list of all the table contents to the presentation layer
        //table name is Master_Segment
        //
        public static List<DVOFlexSegment> GetSegmentVlaue(ref DVOFlexSegment objflxseg)
        {
            object[] parameters = new object[1];
            parameters[0] = objflxseg.FlexDesc;           

            List<DVOFlexSegment> objflxseglst = new List<DVOFlexSegment>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOFlexSegment), objflxseg.GET_SEGMENT_VALUE))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOFlexSegment obflxseg = new DVOFlexSegment();
                    if (!Convert.IsDBNull(dr[0])) obflxseg.FlexDesc = dr[0].ToString().Trim();//"v_segment"
                    if (!Convert.IsDBNull(dr[1])) obflxseg.keyvalue = dr[1].ToString().Trim();//"v_keyvalue"
                    if (!Convert.IsDBNull(dr[2])) obflxseg.desc = dr[2].ToString().Trim();//"v_desc"
                    if (!Convert.IsDBNull(dr[3])) obflxseg.subdividedbydesc = dr[3].ToString().Trim();//"v_subdivides"
                    
                    objflxseglst.Add(obflxseg);
                }
            }

            return objflxseglst;
        }

        public static int UpdateFlxSegment(ref object objTransaction, ref DVOFlexSegment objflxsegupd)
        {
            object UPDResult = null;
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
                object[] Parameter = new object[3];
                Parameter[0] = objflxsegupd.id;
                Parameter[1] = objflxsegupd.desc;
                Parameter[2] = objflxsegupd.keylength;

                 //Parameter[3] = objflxsegupd.subdividedby;
                 //Parameter[4] = objflxsegupd.subdivides;
                 //Parameter[5] = objflxsegupd.segmenttype;

                UPDResult = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref Parameter, typeof(DVOFlexSegment),true);
                if (UPDResult == null)
                    throw new Exception();
                else if (Convert.ToInt32(UPDResult) < 1)
                    throw new Exception();

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

     

        public static List<DVOFlexSegment> GetSegmentBySegmetId(ref DVOFlexSegment objflxseg)
        {
            try
            {
                List<DVOFlexSegment> objFlxsegmentlst = new List<DVOFlexSegment>();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object[] Parameter = new object[1];
                Parameter[0] = objflxseg.id;
                using (DataSet ds = objDalBaseClass.GetData(objflxseg.FIND_SEGMENT(ref Parameter)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOFlexSegment objflexSeg = new DVOFlexSegment();
                        objflexSeg.id = Convert.ToInt32(dr[0]);//"p_id"
                        objflexSeg.desc = dr[1].ToString().Trim();//"p_desc"
                        objflexSeg.keylength = Convert.ToInt32(dr[2]);//"p_keylength"
                        objflexSeg.subdividedby = Convert.ToInt32(dr[3]);//"p_subdividedby"
                        objflexSeg.subdivides = Convert.ToInt32(dr[4]);//"p_subdivides"
                        objflexSeg.segmenttype = (dr[5]).ToString().Trim();//"p_segmenttype"
                        objFlxsegmentlst.Add(objflexSeg);
                    }
                    return objFlxsegmentlst;
                }
            }
            catch (Exception ex)
            {

            }
            return null;


        }
    }
}

