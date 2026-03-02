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
  public class BLLEmpPayDates
    {


        public static List<DVOEmpPayDates> GetEmpPayDatesInfo(ref DVOEmpPayDates objDvoEmpPayDateGet)
        {
            object[] EmpPayDatesParam = new object[2];
            EmpPayDatesParam[0] = objDvoEmpPayDateGet.salary_type;
            EmpPayDatesParam[1] = objDvoEmpPayDateGet.year;
            //EmpPayDatesParam[2] = objDvoEmpPayDateGet.rowid;

            List<DVOEmpPayDates> objDVOEmpPayDateslst = new List<DVOEmpPayDates>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref EmpPayDatesParam, typeof(DVOEmpPayDates), objDvoEmpPayDateGet.GET_EMP_PAY_DATE_INFO))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOEmpPayDates obDVOEmpPayDates = new DVOEmpPayDates();
                    if (!Convert.IsDBNull(dr[0])) obDVOEmpPayDates.date1 = Convert.ToDateTime(dr[0]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[1])) obDVOEmpPayDates.date2 = Convert.ToDateTime(dr[1]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[2])) obDVOEmpPayDates.date3 = Convert.ToDateTime(dr[2]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[3])) obDVOEmpPayDates.date4 = Convert.ToDateTime(dr[3]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[4])) obDVOEmpPayDates.date5 = Convert.ToDateTime(dr[4]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[5])) obDVOEmpPayDates.date6 = Convert.ToDateTime(dr[5]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[6])) obDVOEmpPayDates.date7 = Convert.ToDateTime(dr[6]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[7])) obDVOEmpPayDates.date8 = Convert.ToDateTime(dr[7]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[8])) obDVOEmpPayDates.date9 = Convert.ToDateTime(dr[8]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[9])) obDVOEmpPayDates.date10 = Convert.ToDateTime(dr[9]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[10])) obDVOEmpPayDates.date11 = Convert.ToDateTime(dr[10]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    if (!Convert.IsDBNull(dr[11])) obDVOEmpPayDates.date12 = Convert.ToDateTime(dr[11]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                   // if (!Convert.IsDBNull(dr[12])) obDVOEmpPayDates.rowid = Convert.ToInt32(dr[12].ToString().Trim());
                    objDVOEmpPayDateslst.Add(obDVOEmpPayDates);
                }
            }

            return objDVOEmpPayDateslst;




        }



      public static object InsertEmpPayDatesInfo(ref object objTransaction, ref DVOEmpPayDates objDvoEmpPayDatesIns)
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
               object[] Parameter = new object[17];
               Parameter[0] = objDvoEmpPayDatesIns.salary_type;
               Parameter[1] = objDvoEmpPayDatesIns.date1;
               Parameter[2] = objDvoEmpPayDatesIns.date2;
               Parameter[3] = objDvoEmpPayDatesIns.date3;
               Parameter[4] = objDvoEmpPayDatesIns.date4;
               Parameter[5] = objDvoEmpPayDatesIns.date5;
               Parameter[6] = objDvoEmpPayDatesIns.date6;
               Parameter[7] = objDvoEmpPayDatesIns.date7;
               Parameter[8] = objDvoEmpPayDatesIns.date8;
               Parameter[9] = objDvoEmpPayDatesIns.date9;
               Parameter[10] = objDvoEmpPayDatesIns.date10;
               Parameter[11] = objDvoEmpPayDatesIns.date11;
               Parameter[12] = objDvoEmpPayDatesIns.date12;
               Parameter[13] = objDvoEmpPayDatesIns.year;

               Parameter[14] = objDvoEmpPayDatesIns.insertby;
               Parameter[15] = objDvoEmpPayDatesIns.insertdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
               Parameter[16] = objDvoEmpPayDatesIns.insertmachineinfo;
               object obj=objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOEmpPayDates),true);

               if (!statusObjTransaction)
                   objDALBaseClassHelper.CommitTransaction(ref objTransaction);
          
           
           }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return 0;
            }
            return 1;


        }
     

      public static int  UPdateEmpPayDatesInfo(ref object objTransaction, ref DVOEmpPayDates objDvoEmpPayDatesUpd)
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
                object[] Parameter = new object[17];
                Parameter[0] = objDvoEmpPayDatesUpd.date1;
                Parameter[1] = objDvoEmpPayDatesUpd.date2;
                Parameter[2] = objDvoEmpPayDatesUpd.date3;
                Parameter[3] = objDvoEmpPayDatesUpd.date4;
                Parameter[4] = objDvoEmpPayDatesUpd.date5;
                Parameter[5] = objDvoEmpPayDatesUpd.date6;
                Parameter[6] = objDvoEmpPayDatesUpd.date7;
                Parameter[7] = objDvoEmpPayDatesUpd.date8;
                Parameter[8] = objDvoEmpPayDatesUpd.date9;
                Parameter[9] = objDvoEmpPayDatesUpd.date10;
                Parameter[10] = objDvoEmpPayDatesUpd.date11;
                Parameter[11] = objDvoEmpPayDatesUpd.date12;
                Parameter[12] = objDvoEmpPayDatesUpd.year;
                Parameter[13] = objDvoEmpPayDatesUpd.salary_type;

                Parameter[14] = objDvoEmpPayDatesUpd.updateby;
                Parameter[15] = objDvoEmpPayDatesUpd.updatedate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                Parameter[16] = objDvoEmpPayDatesUpd.updatemachineinfo;
                object obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOEmpPayDates), true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt16(obj) < 1)
                    throw new Exception();

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return 0;
            }
            return 0;

        }

      public static List<DVOEmpPayDates> GetEmpPayDatesAllInfo(ref DVOEmpPayDates obDvoEmpPayDateGet)
      {
          object[] GetAllParam = new object[3];
          GetAllParam[0] = obDvoEmpPayDateGet.salary_type;
          GetAllParam[1] = obDvoEmpPayDateGet.year;
          GetAllParam[2] = obDvoEmpPayDateGet.rowid;

          List<DVOEmpPayDates> objDVOEmpPayDteslst = new List<DVOEmpPayDates>();
          DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
          DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

          using (DataSet ds = objDalBaseClass.GetData(ref GetAllParam, typeof(DVOEmpPayDates)))
          {
              foreach (DataRow dr in ds.Tables[0].Rows)
              {
                  DVOEmpPayDates obDVOEmpPyDtes = new DVOEmpPayDates();
                  if (!Convert.IsDBNull(dr[0])) obDVOEmpPyDtes.date1 = Convert.ToDateTime(dr[0]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[1])) obDVOEmpPyDtes.date2 = Convert.ToDateTime(dr[1]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[2])) obDVOEmpPyDtes.date3 = Convert.ToDateTime(dr[2]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[3])) obDVOEmpPyDtes.date4 = Convert.ToDateTime(dr[3]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[4])) obDVOEmpPyDtes.date5 = Convert.ToDateTime(dr[4]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[5])) obDVOEmpPyDtes.date6 = Convert.ToDateTime(dr[5]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[6])) obDVOEmpPyDtes.date7 = Convert.ToDateTime(dr[6]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[7])) obDVOEmpPyDtes.date8 = Convert.ToDateTime(dr[7]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[8])) obDVOEmpPyDtes.date9 = Convert.ToDateTime(dr[8]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[9])) obDVOEmpPyDtes.date10 = Convert.ToDateTime(dr[9]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[10])) obDVOEmpPyDtes.date11 = Convert.ToDateTime(dr[10]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                  if (!Convert.IsDBNull(dr[11])) obDVOEmpPyDtes.date12 = Convert.ToDateTime(dr[11]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                  if (!Convert.IsDBNull(dr[12])) obDVOEmpPyDtes.salary_type = Convert.ToString(dr[12]);
                  if (!Convert.IsDBNull(dr[13])) obDVOEmpPyDtes.year = dr[13].ToString().Trim();
                  if (!Convert.IsDBNull(dr[14])) obDVOEmpPyDtes.active = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
                  if (!Convert.IsDBNull(dr[15])) obDVOEmpPyDtes.rowid = Convert.ToInt32(dr[15].ToString().Trim());
                  objDVOEmpPayDteslst.Add(obDVOEmpPyDtes);
              }
          }
          return objDVOEmpPayDteslst;


      }

      public static int DeleteEmpPayDates(ref object objTransaction, DVOEmpPayDates objDvoEmpPayDateDel)
      {
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
              object[] Parameter = new object[5];
              Parameter[0] = objDvoEmpPayDateDel.salary_type;
              Parameter[1] = objDvoEmpPayDateDel.year;
              Parameter[2] = objDvoEmpPayDateDel.updateby;
              Parameter[3] = objDvoEmpPayDateDel.updatedate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
              Parameter[4] = objDvoEmpPayDateDel.updatemachineinfo;


              object c = objDalBaseClass.DeleteData_ByTransaction(ref objTransaction, ref Parameter, typeof(DVOEmpPayDates), true);
              if (c == null)
                  throw new Exception();
              else if (Convert.ToInt16(c) < 1)
                  throw new Exception();
              if (!statusObjTransaction)
              {
                  objDALBaseClassHelper.CommitTransaction(ref objTransaction);
              }
              return 1;
          }
          catch (Exception ex)
          {
              ExceptionManager.Publish(ex);
              System.Windows.Forms.MessageBox.Show(ex.Message);
              return 0;
          }
          return 0;
      
         
      }
  }
}
