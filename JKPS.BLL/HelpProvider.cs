using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;

namespace JKPS.BLL
{
  public class JKPSHelpProvider
    {
      public HelpProvider HelpP(Control ctr, string str)
      {
          System.Windows.Forms.HelpProvider hlp = new System.Windows.Forms.HelpProvider();
          try
          {
                             
            //  hlp.HelpNamespace = Application.StartupPath + @"\JKPS.chm";
              hlp.HelpNamespace = Application.StartupPath + System.Configuration.ConfigurationSettings.AppSettings["JKPSCHMFile"].Trim();
              hlp.SetHelpKeyword(ctr, str); 
              
             hlp.SetHelpNavigator(ctr, HelpNavigator.Index);

             hlp.SetHelpKeyword(ctr, str);

             hlp.SetShowHelp(ctr, true);
              
              return hlp;
          }
          
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message);
          }
          
          finally
          {
              hlp.Dispose();
          }
        return hlp; 
      }

      //Use:
       //try
       //     {
       //         /*Added by Shrishanshu Mishra
       //         * To Provide Help for every form
       //         * Description:
       //         *          Use this line in Form_Load() , to use help for specific form.
       //                    Provide the Form Name as string in the line;
       //                    ctr=hlp.HelpP(this,"STRING");  */
       //         //-------------------------Help Start Here--------------------------------------//
       //         HelpProvider ctl = new HelpProvider();
       //         BLL.JKPSHelpProvider hlp = new JKPSHelpProvider();
       //         ctl = hlp.HelpP(this, "Update General Journal");
       //         //-------------------------Help End Here--------------------------------------//
       //     }
       //     catch (Exception ex)
       //     { 
       //         MessageBox.Show(ex.Message, "Error"); 
       //     }    
       
       
    }


    public class DRtest
    {
        public static IDataReader ExecuteFunction()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //IfxHelper helperclass = new IfxHelper();
            object[] parameters = new object[3];
            parameters[0] = "MasterIncCodes";
            parameters[1] = "BASIC";
            parameters[2] = "RECEXP";
            DVOFlexSegCommon obj = new DVOFlexSegCommon();
            IDataReader dr = objDALBaseClass.GetDataByReader(ref parameters, "uspsegvalcomgetest");
            return dr;

            //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            //DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //object[] parameters = new object[3];
            //parameters[0] = "MasterIncCodes";
            //parameters[1] = "BASIC";
            //parameters[2] = "RECEXP";
            //DVOFlexSegCommon obj = new DVOFlexSegCommon();
            //IDataReader dr = objDALBaseClass.GetDataByReader(ref parameters, "uspsegvalcomgetest");
            //return dr;
        }

        public static IDataReader GetNSSDetailByDR(ref DVONSSDetail objNSSDetail)
        {
            Object[] parameters = new object[4];

            parameters[0] = objNSSDetail.account_no;
            parameters[1] = objNSSDetail.nss_status;
            parameters[2] = objNSSDetail.first_name;
            parameters[3] = objNSSDetail.last_name;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            IDataReader drNSSDetail = objDalBaseClass.GetDataByReader(ref parameters, typeof(DVONSSDetail));
            return drNSSDetail;
        }
        public static IDataReader ExecuteFunction2()
        {

            IDataReader dr = null;
            return dr;
        }


    }
}
