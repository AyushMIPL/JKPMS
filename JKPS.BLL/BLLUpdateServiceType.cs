using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLUpdateServiceType
    {
        public static List<DVOstuservtyp> Get_Service_Type_Info(ref DVOstuservtyp SearchServTypeInfo)
        {
            object[] parameters = new object[5];
            parameters[0] = SearchServTypeInfo.rowid;
            parameters[1] = SearchServTypeInfo.serv_typ_id;
            parameters[2] = SearchServTypeInfo.serv_typ_name;
            parameters[3] = SearchServTypeInfo.serv_typ_desc;
            parameters[4] = SearchServTypeInfo.need_prev_ordr_no;

            List<DVOstuservtyp> lstDVOstuservtyp = new List<DVOstuservtyp>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOstuservtyp)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOstuservtyp objDVOstuservtyp = new DVOstuservtyp();
                    if (!Convert.IsDBNull(dr[0])) objDVOstuservtyp.rowid = Convert.ToInt32(dr[0]);
                    if (!Convert.IsDBNull(dr[1])) objDVOstuservtyp.serv_typ_id = Convert.ToInt32(dr[1]);
                    if (!Convert.IsDBNull(dr[2])) objDVOstuservtyp.serv_typ_name = Convert.ToString(dr[2]);
                    if (!Convert.IsDBNull(dr[3])) objDVOstuservtyp.serv_typ_desc = dr[3].ToString().Trim();
                    if (!Convert.IsDBNull(dr[4])) objDVOstuservtyp.need_prev_ordr_no = Convert.ToString(dr[4]);

                    lstDVOstuservtyp.Add(objDVOstuservtyp);
                }
            }
            return lstDVOstuservtyp;
        }
        public static int Insert_Service_Type_Info(DVOstuservtyp objDVOstuservtypIns)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            object objTransaction = null;
            objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {

                object obj = null;
                object[] parameter = new object[5];
                parameter[0] = objDVOstuservtypIns.serv_typ_name;
                parameter[1] = objDVOstuservtypIns.serv_typ_desc;
                parameter[2] = objDVOstuservtypIns.need_prev_ordr_no;
                parameter[3] = objDVOstuservtypIns.insertby;
                parameter[4] = objDVOstuservtypIns.insertmachineInfo;

                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                obj = objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameter, typeof(DVOstuservtyp), true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt16(obj) < 1)
                    throw new Exception();
                else
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }
        public static int Update_Service_Type_Info(ref object objTransaction, DVOstuservtyp objDVOstuservtypUpd)
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
                object success = null;
                object[] UpdParameter = new object[6];

                UpdParameter[0] = objDVOstuservtypUpd.rowid;
                UpdParameter[1] = objDVOstuservtypUpd.serv_typ_name;
                UpdParameter[2] = objDVOstuservtypUpd.serv_typ_desc;
                UpdParameter[3] = objDVOstuservtypUpd.need_prev_ordr_no;
                UpdParameter[4] = objDVOstuservtypUpd.updateby;
                UpdParameter[5] = objDVOstuservtypUpd.updatemachineInfo;

                success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref UpdParameter, typeof(DVOstuservtyp), true);
                if (success == null)
                    throw new Exception();
                else if (Convert.ToInt16(success) < 1)
                    throw new Exception();
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                UpdParameter = null;
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
        public static int Delete_Service_Type_Info(ref object objTransaction, DVOstuservtyp objDVOstuservtypDel)
        {
            object Delobj = null;
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
                object[] Parameter = new object[1];
                Parameter[0] = objDVOstuservtypDel.rowid;
                Delobj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOstuservtyp), true);
                if (Delobj == null)
                    throw new Exception();
                else if (Convert.ToInt16(Delobj) < 1)
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
        public static List<DVOstuservtyp> Get_Service_Type_Duplicate(ref DVOstuservtyp SearchServTypeInfo)
        {
            object[] parameters = new object[1];
            parameters[0] = SearchServTypeInfo.serv_typ_name;

            List<DVOstuservtyp> lstDVOstuservtyp = new List<DVOstuservtyp>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(SearchServTypeInfo.FIND_DUPLICATE(ref parameters)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOstuservtyp objDVOstuservtyp = new DVOstuservtyp();
                    if (!Convert.IsDBNull(dr[0])) objDVOstuservtyp.rowid = Convert.ToInt32(dr[0]);
                    if (!Convert.IsDBNull(dr[1])) objDVOstuservtyp.serv_typ_id = Convert.ToInt32(dr[1]);
                    if (!Convert.IsDBNull(dr[2])) objDVOstuservtyp.serv_typ_name = Convert.ToString(dr[2]);
                    if (!Convert.IsDBNull(dr[3])) objDVOstuservtyp.serv_typ_desc = dr[3].ToString().Trim();
                    if (!Convert.IsDBNull(dr[4])) objDVOstuservtyp.need_prev_ordr_no = Convert.ToString(dr[4]);

                    lstDVOstuservtyp.Add(objDVOstuservtyp);
                }
            }
            return lstDVOstuservtyp;
        }
    }
}
