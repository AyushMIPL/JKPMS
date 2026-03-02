using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOCommonEntities: DVOBase
    {
      //*****************Added By Rahul jain on 27/11/2008 for getting server date ***********************
        private string _ServerDate;
      //***************************************************************************************************
        #region Constructor
       //*****************Added By Rahul jain on 27/11/2008 for getting server date ***********************
           public DVOCommonEntities()
            {
                _ServerDate = string.Empty;
            }
       //**************************************************************************************************
        #endregion Constructor

        #region Public Properties
        //*****************Added By Rahul jain on 27/11/2008 for getting server date ***********************
           public string ServerDate
           {
               get { return _ServerDate; }
               set { _ServerDate = value; }
           }
       //***************************************************************************************************
   


        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspstgtranins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
           
            return sql.ToString();
        }
       public string AutonextgetQuery(ref Object[] parameters)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append("Select rowid, " + parameters[1] + " from " + parameters[0]);
           return sql.ToString();
       }
       //Added By rahul jain using in budget posting get data by selected year
       public string AutonextgetQueryBudget(ref Object[] parameters)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append("Select rowid, " + parameters[1] + " from " + parameters[0] + " Where rowid = " + parameters[2]);
           return sql.ToString();
       }
       public string AutoNextUpdateQuery(ref Object[] parameters)
       {  
           StringBuilder sql = new StringBuilder();
           sql.Append("Update  " + parameters[0] + " Set " + parameters[1] + " = " + parameters[3] + " where rowid=" + parameters[2]);
           return sql.ToString();
       }
       public string GETACCTPERIOD
       {
           get { return "uspaccperiod"; }
       }
       public string GETACCOUNTDETAILS
       {
           get { return "uspaccchrtn"; }//
       }

       //*************************** Added By Bharat *****************************
       public string CHECK_USER_ACCOUNT
       {
           get { return "uspChkUsrAcct"; }
       }
       public string USER_ACCOUNT_PERMISSION
       {
           get { return "uspUsrAcctPerm"; }
       }
       public string ACCOUNT_PERMISSION
       {
           get { return "uspacctperm"; }
       }
       public string RECORD_LOCK
       {
           get { return "uspRecordLock"; }
       }
       public string RECORD_RELEASE
       {
           get { return "uspRecordRelease"; }
       }
       public string MONITOR_LOCKS
       {
           get { return "uspLockMonitor"; }
       }
       public string TREASURY_BILL_ACCNT_NO
       {
           get { return "uspTreBilActNoGet"; }
       }
       public string FIND_BUDGET_KEY
       {
           get { return "uspFndBdgtKey"; }
       }
       public string CHECK_EXPENSE
       {
           get { return "uspChkExpens"; }
       }
       public string GET_BUDGET_CHECKING_LEVEL
       {
           get { return "uspBdgtChkLvl"; }
       }
       public string GET_ACTUAL_KEYVALUE
       {
           get { return "uspGetActualKeyval"; }
       }
       public string PAYROLL_CHECKTAXTABLE
       {
           get { return "usptaxtablechk"; }
       }
       public string GET_SEGMENTS
       {
           get { return "uspaccttypestrc"; }
       }

       #region Auto_Next Stored Procedures

       public string NEW_APCHECK_GET
       {
           get { return "uspnewapchk"; }
       }
       public string NEW_APDOC_GET
       {
           get { return "USP_NewApDoc"; }
       }
       public string NEW_APPOSTNO_GET
       {
           get { return "uspnewappostno"; }
       }
       public string NEW_AP_CDPOSTNO_GET
       {
           get { return "uspnewapcdpostno"; }
       }
       public string NEW_ARDOC_GET
       {
           get { return "uspnewardoc"; }
       }
       public string NEW_AR_CRPOSTNO_GET
       {
           get { return "uspnewarcrpostno"; }
       }
       public string NEW_SBPOSTNO_GET
       {
           get { return "uspnewsbpostno"; }
       }
       public string NEW_NSSPOSTNO_GET
       {
           get { return "uspnewnsspostno"; }
       }
       public string NEW_TBPOSTNO_GET
       {
           get { return "uspnewtbpostno"; }
       }
       public string NEW_TB_RCPTNO_GET
       {
           get { return "uspnewtbrcptno"; }
       }
       public string NEW_TB_TENDCODE_GET
       {
           get { return "uspnewtbtendcode"; }
       }
       public string NEW_PYPOSTNO_GET
       {
           get { return "uspnewpypostno"; }
       }
       public string NEW_PYDOCNO_GET
       {
           get { return "uspnewpydocno"; }
       }
       public string NEW_PU_RECPOSTNO_GET
       {
           get { return "uspnewpurecpostno"; }
       }
       public string NEW_PU_INVPOSTNO_GET
       {
           get { return "uspnewpuinvpostno"; }
       }
       public string NEW_PU_REQDOCNO_GET
       {
           get { return "uspnewpureqdocno"; }
       }
       public string NEW_ICPOSTNO_GET
       {
           get { return "uspnewicpostno"; }
       }
       public string NEW_ICDOCNO_GET
       {
           get { return "uspnewicdocno"; }
       }
       public string NEW_OEPOSTNO_GET
       {
           get { return "uspnewoepostno"; }
       }
       public string NEW_GJPOSTNO_GET
       {
           get { return "uspnewgjpostno"; }
       }
       public string NEW_GJDOCNO_GET
       {
           get { return "uspnewgjdocno"; }
       }
       public string NEW_GLPOSTNO_GET
       {
           get { return "uspnewglpostno"; }
       }
       public string NEW_BDG_POSTNO_GET
       {
           get { return "uspnewbdgpostno"; }
       }

       #endregion Auto_Next Stored Procedures


       public string GET_DATA_FOR_SMARTTEXTBOX(string COLUMNNAMES, string TABLENAME)
       {
           return "SELECT " + COLUMNNAMES + " FROM " + TABLENAME;
       }
       public string GET_DATA_FOR_SMARTTEXTBOX(string COLUMNNAMES, string TABLENAME, string WHERECONDITION)
       {
           return "SELECT " + COLUMNNAMES + " FROM " + TABLENAME + " WHERE " + WHERECONDITION;
       }    
      
       //*************************************************************************
       public override string TABLE_NAME
       {
           get { return ""; }
       }

       public override int UNIQUE_ID
       {
           get { return 0; }
       }

       public override string NOTES_TABLE_RECORD_ID
       {
           get { return string.Empty; }
           set { throw new Exception("The method or operation is not implemented."); }
       }

       //*****************Added By Rahul jain on 27/11/2008 for getting server date ***********************
       public string GETSERVERDATE
       {
           get { return "USP_GetSeverDate"; }
       }
       //**************************************************************************************************
       public string GETCURPRD
       {
           get { return "uspcurperiod"; }
       }
        #endregion Stored-Procedures
    }
}
