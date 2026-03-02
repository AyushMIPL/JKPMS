using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.)   DVO To get the Control Data                    Rajeev(Rahul Jain)                                    12/02/2009(DD)
    ///2.) 
    ///<summery>
   public class DVONssControls : DVOBase
    {
       
       private int _contract_duration;
       private decimal _minimum_contrib;
       private decimal _maximum_contrib;
       private decimal _bonus_pc;
       private int _bonus_months;
       private decimal _bonus_anniv1_pc;
       private int _bonus_anniv1_mnths;
       private decimal _bonus_anniv2_pc;
       private int _bonus_anniv2_mnths;
       private decimal _stopped_pc1;
       private decimal _stopped_pc2;
       private decimal _other_pc1;
       private decimal _other_pc2;
       private decimal _other_pc3;
       private int _defaults_allowed;
       private string _principal_acctno;
       private string _interest_acctno;
       private string _bank_acctno;
       private string _nss_acctno;
       private string _principal_accttype;
       private string _interest_accttype;
       private string _bank_accttype;
       private string _principal_keyvalue;
       private string _interest_keyvalue;
       private string _bank_keyvalue;
       private string _nss_accttype;
       private string _nss_keyvalue;

       /// <summary>
       /// Private variable applicable only for SQL-Server
       /// </summary
       private int _RowID;
       private string _InsertMachineInfo;
       private DateTime _InsertDate;
       private int _InsertBy;

       private string _UpdateMachineInfo;
       private DateTime _UpdateDate;
       private int _UpdateBy;

       
     
       
       
       public DVONssControls()
       {
           _RowID = 0;
         _contract_duration=0;
         _minimum_contrib=0.0M;
         _maximum_contrib=0.0M;
         _bonus_pc=0.0M;
         _bonus_months=0;
         _bonus_anniv1_pc=0.0M;
         _bonus_anniv1_mnths=0;
         _bonus_anniv2_pc=0.0M;
         _bonus_anniv2_mnths=0;
         _stopped_pc1=0.0M;
         _stopped_pc2=0.0M;
         _other_pc1=0.0M;
         _other_pc2=0.0M;
         _other_pc3=0.0M;
         _defaults_allowed=0;
         _principal_acctno="";
         _interest_acctno="";
         _bank_acctno="";
         _nss_acctno = string.Empty;
         _principal_accttype="";
         _interest_accttype="";
         _bank_accttype="";
         _principal_keyvalue = "";
         _interest_keyvalue = "";
         _bank_keyvalue = "";
         _nss_accttype = "";
         _nss_keyvalue = "";

         _InsertMachineInfo = "App";
         _InsertDate = DateTime.Now;
         _InsertBy = -1;
         _UpdateMachineInfo = "App";
         _UpdateDate = DateTime.Now;
         _UpdateBy = -1;
       }

       #region Properties

       public int RowID
       {
           get { return _RowID; }
           set { _RowID = value; }
       }

       public int contract_duration
       {
           get { return _contract_duration; }
           set { _contract_duration = value; }
       }
       public decimal minimum_contrib
       {
           get { return _minimum_contrib; }
           set { _minimum_contrib = value; }
       }
       public decimal maximum_contrib
       {
           get { return _maximum_contrib; }
           set { _maximum_contrib = value; }
       }
       public decimal bonus_pc
       {
           get { return _bonus_pc; }
           set { _bonus_pc = value; }
       }
       public int bonus_months
       {
           get { return _bonus_months; }
           set { _bonus_months = value; }
       }
       public decimal bonus_anniv1_pc
       {
           get { return _bonus_anniv1_pc; }
           set { _bonus_anniv1_pc = value; }
       }
       public int bonus_anniv1_mnths
       {
           get { return _bonus_anniv1_mnths; }
           set { _bonus_anniv1_mnths = value; }
       }
       public decimal bonus_anniv2_pc
       {
           get { return _bonus_anniv2_pc; }
           set { _bonus_anniv2_pc = value; }
       }
       public int bonus_anniv2_mnths
       {
           get { return _bonus_anniv2_mnths; }
           set { _bonus_anniv2_mnths = value; }
       }
       public decimal stopped_pc1
       {
           get { return _stopped_pc1; }
           set { _stopped_pc1 = value; }
       }
       public decimal stopped_pc2
       {
           get { return _stopped_pc2; }
           set { _stopped_pc2 = value; }
       }
       public decimal other_pc1
       {
           get { return _other_pc1; }
           set { _other_pc1 = value; }
       }
       public decimal other_pc2
       {
           get { return _other_pc2; }
           set { _other_pc2 = value; }
       }
       public decimal other_pc3
       {
           get { return _other_pc3; }
           set { _other_pc3 = value; }
       }
       public int defaults_allowed
       {
           get { return _defaults_allowed; }
           set { _defaults_allowed = value; }
       }
       public string principal_acctno
       {
           get { return _principal_acctno; }
           set { _principal_acctno = value; }
       }
       public string interest_acctno
       {
           get { return _interest_acctno; }
           set { _interest_acctno = value; }
       }
       public string bank_acctno
       {
           get { return _bank_acctno; }
           set { _bank_acctno = value; }
       }
       public string nss_acctno
       {
           get { return _nss_acctno; }
           set { _nss_acctno = value; }
       }

       public string bank_accttype
       {
           get { return _bank_accttype; }
           set { _bank_accttype = value; }
       }
       public string principal_accttype
       {
           get { return _principal_accttype; }
           set { _principal_accttype = value; }
       }
       public string interest_accouttype
       {
           get { return _interest_accttype; }
           set { _interest_accttype = value; }
       }

       public string bank_keyvalue
       {
           get { return _bank_keyvalue; }
           set { _bank_keyvalue = value; }
       }
       public string principal_keyvalue
       {
           get { return _principal_keyvalue; }
           set { _principal_keyvalue = value; }
       }
       public string interest_keyvalue
       {
           get { return _interest_keyvalue; }
           set { _interest_keyvalue = value; }
       }
       public string nss_accouttype
       {
           get { return _nss_accttype; }
           set { _nss_accttype = value; }
       }

       public string nss_keyvalue
       {
           get { return _nss_keyvalue; }
           set { _nss_keyvalue = value; }
       }

       //Properties used for only SQL Server

       public string InsertMachineInfo
       {
           get { return _InsertMachineInfo; }
           set { _InsertMachineInfo = value; }
       }
       public DateTime InsertDate
       {
           get { return _InsertDate; }
           set { _InsertDate = value; }
       }
       public int InsertBy
       {
           get { return _InsertBy; }
           set { _InsertBy = value; }
       }
       public string UpdateMachineInfo
       {
           get { return _UpdateMachineInfo; }
           set { _UpdateMachineInfo = value; }
       }
       public DateTime UpdateDate
       {
           get
           {
               return _UpdateDate;
           }
           set { _UpdateDate = value; }
       }
       public int UpdateBy
       {
           get { return _UpdateBy; }
           set { _UpdateBy = value; }
       }
   
      #endregion Properties

       #region Stored-Procedures

       public override string INSERT_SPNAME
       {
           get { return ""; }
       }

       public override string UPDATE_SPNAME
       {
           get { return "uspNssControlUpd"; }
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
           get { return "uspNssControlGet"; }
       }

       public override string FIND_QUERY(ref Object[] parameters)
       {          

           return "";
       }

       public override string TABLE_NAME
       {
           get { return "nsscontrol"; }
       }

       public override int UNIQUE_ID
       {
           get { return _RowID; }
       }

       public override string NOTES_TABLE_RECORD_ID
       {
           get { return string.Empty; }
           set { throw new Exception("The method or operation is not implemented."); }
       }

      


       #endregion Stored-Procedures

   }

 
}
