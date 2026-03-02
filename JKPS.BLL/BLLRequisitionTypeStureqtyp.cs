using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
    public class BLLRequisitionTypeStureqtyp
    {

        public static List<DVORequisitionTypeStureqtyp> GetRequisitionInfo(ref DVORequisitionTypeStureqtyp SearchDVORequisitionTypeStureqtyp)
        {
            object[] Parameter = new object[5];
            Parameter[0] = SearchDVORequisitionTypeStureqtyp.req_typ_code;
            Parameter[1] = SearchDVORequisitionTypeStureqtyp.req_typ_desc;
            Parameter[2] = SearchDVORequisitionTypeStureqtyp.stk_itms;
            Parameter[3] = SearchDVORequisitionTypeStureqtyp.non_stk_itms;
            Parameter[4] = SearchDVORequisitionTypeStureqtyp.rowid;
            List<DVORequisitionTypeStureqtyp> lstDVORequisitionTypeStureqtyp = new List<DVORequisitionTypeStureqtyp>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVORequisitionTypeStureqtyp)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVORequisitionTypeStureqtyp obj = new DVORequisitionTypeStureqtyp();

                    obj.req_typ_code = dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty;
                    obj.req_typ_desc = dr[2] != DBNull.Value ? dr[2].ToString().Trim() : string.Empty;
                    obj.need_approv = dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty;
                    obj.stk_itms = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
                    obj.non_stk_itms = dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty;
                    obj.dflt_vend_code = dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty;
                    obj.can_vend_change = dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty;
                    obj.dflt_warehouse = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
                    obj.can_whse_change = dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty;
                    obj.dflt_line_type = dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty;
                    obj.can_lintyp_change = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
                    obj.rowid = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;//"p_rowid"
                    lstDVORequisitionTypeStureqtyp.Add(obj);
                }
                return lstDVORequisitionTypeStureqtyp;
            }
        }
        public static object InsertRequisitionInfo(ref DVORequisitionTypeStureqtyp objDVORequisitionTypeStureqtypins)
        {  try
            {
                object success = null;
                object[] InParameters = new object[13];
                InParameters[0] = objDVORequisitionTypeStureqtypins.req_typ_code;
                InParameters[1] = objDVORequisitionTypeStureqtypins.req_typ_desc;
                InParameters[2] = objDVORequisitionTypeStureqtypins.need_approv;
                InParameters[3] = objDVORequisitionTypeStureqtypins.stk_itms;
                InParameters[4] = objDVORequisitionTypeStureqtypins.non_stk_itms;
                InParameters[5] = objDVORequisitionTypeStureqtypins.dflt_vend_code;
                InParameters[6] = objDVORequisitionTypeStureqtypins.can_vend_change;
                InParameters[7] = objDVORequisitionTypeStureqtypins.dflt_warehouse;
                InParameters[8] = objDVORequisitionTypeStureqtypins.can_whse_change;
                InParameters[9] = objDVORequisitionTypeStureqtypins.dflt_line_type;
                InParameters[10] = objDVORequisitionTypeStureqtypins.can_lintyp_change;
                InParameters[11] = objDVORequisitionTypeStureqtypins.insertby;
                InParameters[12] = objDVORequisitionTypeStureqtypins.insertmachineinfo;

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

                success = objDalBaseClass.InsertData(ref InParameters, typeof(DVORequisitionTypeStureqtyp), true);

                if (success == null)
                    throw new Exception();
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

        public static object UpdateRequisitionInfo(ref object objTransaction, ref DVORequisitionTypeStureqtyp objDVORequisitionTypeStureqtypupd)
        {
            object success = null;
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

                object[] UpdParameters = new object[8];
                UpdParameters[0] = objDVORequisitionTypeStureqtypupd.rowid;
                UpdParameters[1] = objDVORequisitionTypeStureqtypupd.req_typ_code;
                UpdParameters[2] = objDVORequisitionTypeStureqtypupd.req_typ_desc;
                UpdParameters[3] = objDVORequisitionTypeStureqtypupd.need_approv;
                UpdParameters[4] = objDVORequisitionTypeStureqtypupd.stk_itms;
                UpdParameters[5] = objDVORequisitionTypeStureqtypupd.non_stk_itms;
                UpdParameters[6] = objDVORequisitionTypeStureqtypupd.dflt_vend_code;
                UpdParameters[7] = objDVORequisitionTypeStureqtypupd.can_vend_change;
                UpdParameters[8] = objDVORequisitionTypeStureqtypupd.dflt_warehouse;
                UpdParameters[9] = objDVORequisitionTypeStureqtypupd.can_whse_change;
                UpdParameters[10] = objDVORequisitionTypeStureqtypupd.dflt_line_type;
                UpdParameters[11] = objDVORequisitionTypeStureqtypupd.can_lintyp_change;

                UpdParameters[12] = objDVORequisitionTypeStureqtypupd.updateby;
                UpdParameters[13] = objDVORequisitionTypeStureqtypupd.updatemachineinfo;

                success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UpdParameters, typeof(DVORequisitionTypeStureqtyp), true);
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

        public static int DeleteRequisitiontypeInfo(ref object objTransaction, ref DVORequisitionTypeStureqtyp DVORequisitionTypeStureqtypdel)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            object[] parameter = new object[1];
            parameter[0] = DVORequisitionTypeStureqtypdel.rowid;
            try
            {
                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, DVORequisitionTypeStureqtypdel.DELETE_SPNAME);
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
    }
}
