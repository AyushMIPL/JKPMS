using System;
using System.Collections.Generic;
using System.Text;

///<Development and modification Details>
 
///<summery>

namespace JKPS.COMMON
{
   public class DVOFlexKeySegmentDefinition : DVOBase
    {
       private int _RowID;
        private int _segmentid;
        private int _id;
        private string _keyvalue;
        private string _accounttype;       
        private string _desc;
        private decimal _balance;   
        private int _printsafter;   
        private int _issubto;
        private string _abbreviation;
     
       private string _period;
       private string _year;
       private int _chk;
        
        /// <summary>
        /// Private variable applicable only for SQL-Server
        /// </summary>
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private string  _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        /// <summary>
        /// Default Constructor used here to initialize the private member.
        /// </summary>ghghgh
        public DVOFlexKeySegmentDefinition()
        {
            _RowID = 0;
            _segmentid = 0;
            _id = 0;
            _keyvalue = string.Empty;
            _accounttype = string.Empty;
            _desc = string.Empty;
            _balance = 0;
            _printsafter = -1;
            _issubto = 0;
            _abbreviation = string.Empty;
            _period = string.Empty;
            _year = string.Empty;
            _chk = 0;

            _InsertMachineInfo = string.Empty;
            _InsertDate = DateTime.Now;
            _InsertBy = string.Empty;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
        }

        #region Properties item

       public int RowID
       {
           get { return _RowID; }
           set { _RowID = value; }
       }

        public int segmentid
        {
            get {return _segmentid;}
            set {_segmentid = value;}
        }

        public int id
        {
            get {return _id;}
            set {_id = value;}
        }
        public string keyvalue
        {
            get { return _keyvalue;}
            set { _keyvalue = value; }
        }
       public string accounttype
        {
            get { return _accounttype; }
            set { _accounttype = value; }
        }
        public string desc 
        {
            get { return _desc; }
            set { _desc = value; }
        }
       public decimal balance 
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public int printsafter
        {
            get { return _printsafter; }
            set { _printsafter = value; }
        }
        public int issubto
        {
            get { return _issubto; }
            set { _issubto = value; }
        }
        public string abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        }
       public string period
       {
           get { return _period; }
           set { _period = value; }
       }
       public string year 
       {
           get { return _year; }
           set { _year = value; }
       }
       public int chk
       {
           get { return _chk; }
           set { _chk = value;}
       }

         public string InsertMachineInfo 
         {
             get{return _InsertMachineInfo;}
             set{_InsertMachineInfo=value;}
         }
       public DateTime InsertDate
       {
           get { return _InsertDate; }
           set { _InsertDate = value; }
       }
        public string InsertBy
        {
            get{return _InsertBy;}
            set{_InsertBy=value;}
        }
        public string UpdateMachineInfo
        {
            get{return _UpdateMachineInfo;}
            set{_UpdateMachineInfo=value;}
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
            get{return _UpdateBy;}
            set{_UpdateBy=value;}
        }
           

        #endregion Properties item

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_FlxSegValIns"; } //uspflxsegvalins
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_Master_SegmentDel"; } //old //uspflxsegvaldel
        }

        public override string FIND_SPNAME
        {
            get { return "USP_FlxSegValueGet"; } //uspflxsegvalueget
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

       public string FIND_POPUPSPNAME
       {
           get { return "USP_FlxSegValpget"; } //uspflxsegvalpget

       }

       public string FIND_OBJECTDESCRIPTION
       {
           get { return "USP_ObjDescGet"; } //uspobjdescget

       }

       public string FIND_OBJECT_DETAIL_DESCRIPTION
       {
           get { return "USP_ObjDtlDesGet"; } //uspobjdtldesget

       }


      
       public string SigbudUtilityExpStep1
       {
           get { return "uspSigUtiExpStep1"; } //uspsigutiexpstep1
       }
       public string SigbudUtilityExpStep2
       {
           get { return "uspSigUtiExpStep2"; } //uspsigutiexpstep2
       }
       public string SigbudUtilityExpStep3
       {
           get { return "uspSigUtiExpStep3"; }  //uspsigutiexpstep3
       }
       public string GetDataSegUtiExp
       {
           get { return "UspGetSegUtiExp"; } //uspgetsegutiexp
       }
       public string InsertDataSegUtiExp
       {
           get { return "UspSegUtiExpIns"; } //uspsegutiexpins
       }
       public string GetAccountNumber
       {
           get { return "UspSegUtiImpAcct"; } //uspsegutiimpacct
       }
       public string InsertNewAcctInfo
       {
           get { return "UspNewAcctInfoIns"; } //uspnewacctinfoins
       }   
       public string InsertSigbudImportData
       {
           get { return "UspSigbudImpData"; } //uspsigbudimpdata
       }   
       public string Getbudyear
       {
           get { return "Uspbudyearget"; } //uspbudyearget
       }  
       public string SigbudImportDataCheck
       {
           get { return "UspImpDataCheck"; } //uspimpdatacheck
       }  
       public override string TABLE_NAME
       {
           get { return "Flex_Segment_Value_Details"; }
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
       public override string FIND_QUERY(ref Object[] parameters)
       {
           return "";
       }

       public string FIND_SEGMENTDETAIL(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();
           sql.Append("SELECT segmentid,id ,keyvalue,[desc] description ,printsafter ,issubto ,abbreviation ");
           sql.Append(" from Flex_Segment_Value_Details  ");
           sql.Append(" Where 1=1  ");
           if (Convert.ToInt32(parameters[0]) > 0)
               sql.Append(" AND segmentid=" + parameters[0].ToString());
           sql.Append("Order By id ");
           return sql.ToString();
       }

        #endregion Stored-Procedures

    }
}
