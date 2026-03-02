using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOOrderUpdateShipInfoStrshipr : DVOBase
    {
        private string _cust_code;
        private string _ship_to_code;
        private string _bus_name;
        private string _taxable;
        private string _contact;
        private string _phone;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _zip;
        private string _country;
        private string _sls_psn_code;
        private string _trd_ds_code;
        private string _st_tx_code;
        private string _co_tx_code;
        private string _ci_tx_code;
        private string _comm_code;
        private string _mtax_freight;
        private string _mtax_misc;
        private string _ship_via_cd;

        private int _rowid;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        public DVOOrderUpdateShipInfoStrshipr()
        {
            _cust_code = string.Empty;
            _ship_to_code = string.Empty;
            _bus_name = string.Empty;
            _taxable = string.Empty;
            _contact = string.Empty;
            _phone = string.Empty;
            _address1 = string.Empty;
            _address2 = string.Empty;
            _city = string.Empty;
            _state = string.Empty;
            _zip = string.Empty;
            _country = string.Empty;
            _sls_psn_code = string.Empty;
            _trd_ds_code = string.Empty;
            _st_tx_code = string.Empty;
            _co_tx_code = string.Empty;
            _ci_tx_code = string.Empty;
            _comm_code = string.Empty;
            _mtax_freight = string.Empty;
            _mtax_misc = string.Empty;
            _ship_via_cd = string.Empty;
            _rowid = 0;
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

        }

        #region Public Properties
        public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }            
        }
        public string ship_to_code
        {
            get { return _ship_to_code; }
            set { _ship_to_code = value; } 
        }
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; } 
        }
        public string taxable
        {
            get { return _taxable; }
            set { _taxable = value; } 
        }
        public string contact
        {
            get { return _contact; }
            set { _contact = value; } 
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; } 
        }
        public string address1
        {
            get { return _address1; }
            set { _address1 = value; } 
        }
        public string address2
        {
            get { return _address2; }
            set { _address2 = value; } 
        }
        public string city
        {
            get { return _city; }
            set { _city = value; } 
        }
        public string state
        {
            get { return _state; }
            set { _state = value; } 
        }
        public string zip
        {
            get { return _zip; }
            set { _zip = value; } 
        }
        public string country
        {
            get { return _country; }
            set { _country = value; } 
        }
        public string sls_psn_code
        {
            get { return _sls_psn_code; }
            set { _sls_psn_code = value; } 
        }
        public string trd_ds_code
        {
            get { return _trd_ds_code; }
            set { _trd_ds_code = value; } 
        }
        public string st_tx_code
        {
            get { return _st_tx_code; }
            set { _st_tx_code = value; } 
        }
        public string co_tx_code
        {
            get { return _co_tx_code; }
            set { _co_tx_code = value; } 
        }
        public string ci_tx_code
        {
            get { return _ci_tx_code; }
            set { _ci_tx_code = value; } 
        }
        public string comm_code
        {
            get { return _comm_code; }
            set { _comm_code = value; } 
        }
        public string mtax_freight
        {
            get { return _mtax_freight; }
            set { _mtax_freight = value; } 
        }
        public string mtax_misc
        {
            get { return _mtax_misc; }
            set { _mtax_misc = value; } 
        }
        public string ship_via_cd
        {
            get { return _ship_via_cd; }
            set { _ship_via_cd = value; } 
        }

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

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
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        #endregion Public Properties


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspshipinfoins"; }
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
       

        public override string TABLE_NAME
        {
            get { return "strshipr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

       public  string GET_BUS_NAME
        {
            get { return "uspordentryeditget"; }
        }
        //************************************************************
        public override string FIND_QUERY(ref Object[] parameters)
        {
            //StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT  rowid,src_key, src_desc, src_num_desc, src_char_desc");
            //sql.Append(" FROM  stxinfor where 1=1");

            //if (parameters[1] != null)//src_type
            //    if (parameters[1].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(src_type) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[2] != null)//src_key
            //    if (parameters[2].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(src_key) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[3] != null)//src_desc
            //    if (parameters[3].ToString().Trim() != string.Empty)
            //        sql.Append(" AND Rtrim(src_desc) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[4] != null)//src_num_desc
            //    if (Convert.ToDecimal(parameters[4]) != 0)
            //        sql.Append(" and src_num_desc = " + parameters[4]);

            ////Added By Sunil Pahwa
            //if (Convert.ToInt32(parameters[0]) != 0)
            //    sql.Append(" AND rowid = " + parameters[0].ToString());
            ////********************
            //sql.Append(" order by src_key");

            //return sql.ToString();
            return "";
        }

      

        #endregion Stored-Procedures
    }
}
