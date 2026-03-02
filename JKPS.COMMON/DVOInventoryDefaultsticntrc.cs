using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 12-06-2009 using for Update/Get Inventory Default
    public class DVOInventoryDefaultsticntrc :DVOBase
    {
        public int _RowId;
        public string _item_type;
        public string _item_class;
        public int _inv_acct_no;
        public int _sales_acct_no;
        public int _cog_acct_no;
        public int _adj_acct_no;
        public int _count_acct_no;
        public string _count_cycle;
        public string _comm_code;
        public string _allow_bo;
        public string _taxable;
        public string _terms_disc;
        public string _trade_disc;
        public string _ic_setup_done;
        public int _doc_no;
        public int _post_no;
        public string _use_department;
        public string _cost_method;
        public int _ilocar_ina_days;
        public int _ilocar_ret_days;
        public decimal _class1;
        public decimal _class2;
        public decimal _class3;
        public decimal _class4;
        public decimal _class5;
        public decimal _class6;
        public decimal _class7;
        public decimal _class8;
        public decimal _class9;
        public decimal _class10;
        public decimal _class11;
        public decimal _class12;
        public decimal _class13;

        //Keyvalue 
        private string _inv_acct_keyvalue;
        private string _salse_acct_keyvalue;
        private string _cog_acct_keyvalue;
        private string _adj_acct_keyvalue;
        private string _count_acct_keyvalue;

        //Account Type 
        private string _inv_acct_type;
        private string _salse_acct_type;
        private string _cog_acct_type;
        private string _adj_acct_type;
        private string _count_acct_type;

        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;


        #region Constructor
        public DVOInventoryDefaultsticntrc()
        {
            _RowId = 0;
            _item_type = "";
            _item_class = "";
            _inv_acct_no = 0;
            _sales_acct_no = 0;
            _cog_acct_no = 0;
            _adj_acct_no = 0;
            _count_acct_no = 0;
            _count_cycle = "";
            _comm_code = "";
            _allow_bo = "";
            _taxable = "";
            _terms_disc = "";
            _trade_disc = "";
            _ic_setup_done = "";
            _doc_no = 0;
            _post_no = 0;
            _use_department = "";
            _cost_method = "";
            _ilocar_ina_days = 0;
            _ilocar_ret_days = 0;
            _class1 = 0.0M;
            _class2 = 0.0M;
            _class3 = 0.0M;
            _class4 = 0.0M;
            _class5 = 0.0M;
            _class6 = 0.0M;
            _class7 = 0.0M;
            _class8 = 0.0M;
            _class9 = 0.0M;
            _class10 = 0.0M;
            _class11 = 0.0M;
            _class12 = 0.0M;
            _class13 = 0.0M;

            //Keyvalue 
            _inv_acct_keyvalue = "";
            _salse_acct_keyvalue = "";
            _cog_acct_keyvalue = "";
            _adj_acct_keyvalue = "";
            _count_acct_keyvalue = "";

            //Account Type 
            _inv_acct_type = "";
            _salse_acct_type = "";
            _cog_acct_type = "";
            _adj_acct_type = "";
            _count_acct_type = "";

            /// <summary>
            /// Private variable applicable only for SQL-Server
            /// </summary
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
        }
        #endregion

        #region Public Properties
        public int RowID
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public string item_type
        {
            get { return _item_type; }
            set { _item_type = value; }
        }
        public string item_class
        {
            get { return _item_class; }
            set { _item_class = value; }
        } 
        public int inv_acct_no
        {
            get { return _inv_acct_no; }
            set { _inv_acct_no = value; }
        }
        public int sales_acct_no
        {
            get { return _sales_acct_no; }
            set { _sales_acct_no = value; }
        }
        public int cog_acct_no
        {
            get { return _cog_acct_no; }
            set { _cog_acct_no = value; }
        }
        public int adj_acct_no
        {
            get { return _adj_acct_no; }
            set { _adj_acct_no = value; }
        }
        public int count_acct_no
        {
            get { return _count_acct_no; }
            set { _count_acct_no = value; }
        }
        public string count_cycle
        {
            get { return _count_cycle; }
            set { _count_cycle = value; }
        }
        public string comm_code
        {
            get { return _comm_code; }
            set { _comm_code = value; }
        }
        public string allow_bo
        {
            get { return _allow_bo; }
            set { _allow_bo = value; }
        }
        public string taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
        }
        public string terms_disc
        {
            get { return _terms_disc; }
            set { _terms_disc = value; }
        }
        public string trade_disc
        {
            get { return _trade_disc; }
            set { _trade_disc = value; }
        }
        public string ic_setup_done
        {
            get { return _ic_setup_done; }
            set { _ic_setup_done = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public int post_no
        {
            get { return _post_no; }
            set { _post_no = value; }
        }
        public string use_department
        {
            get { return _use_department; }
            set { _use_department = value; }
        }
        public string cost_method
        {
            get { return _cost_method; }
            set { _cost_method = value; }
        }
        public int ilocar_ina_days
        {
            get { return _ilocar_ina_days; }
            set { _ilocar_ina_days = value; }
        }
        public int ilocar_ret_days
        {
            get { return _ilocar_ret_days; }
            set { _ilocar_ret_days = value; }
        }
        public decimal class1
        {
            get { return _class1; }
            set { _class1 = value; }
        }
        public decimal class2
        {
            get { return _class2; }
            set { _class2 = value; }
        }
        public decimal class3
        {
            get { return _class3; }
            set { _class3 = value; }
        }
        public decimal class4
        {
            get { return _class4; }
            set { _class4 = value; }
        }
        public decimal class5
        {
            get { return _class5; }
            set { _class5 = value; }
        }
        public decimal class6
        {
            get { return _class6; }
            set { _class6 = value; }
        }
        public decimal class7
        {
            get { return _class7; }
            set { _class7 = value; }
        }
        public decimal class8
        {
            get { return _class8; }
            set { _class8 = value; }
        }
        public decimal class9
        {
            get { return _class9; }
            set { _class9 = value; }
        }
        public decimal class10
        {
            get { return _class10; }
            set { _class10 = value; }
        }
        public decimal class11
        {
            get { return _class11; }
            set { _class11 = value; }
        }
        public decimal class12
        {
            get { return _class12; }
            set { _class12 = value; }
        }
        public decimal class13
        {
            get { return _class13; }
            set { _class13 = value; }
        }

         //Keyvalue 
        public string inv_acct_keyvalue
        {
            get { return _inv_acct_keyvalue; }
            set { _inv_acct_keyvalue = value; }
        }
        public string salse_acct_keyvalue
        {
            get { return _salse_acct_keyvalue; }
            set { _salse_acct_keyvalue = value; }
        }
        public string cog_acct_keyvalue
        {
            get { return _cog_acct_keyvalue; }
            set { _cog_acct_keyvalue = value; }
        }
        public string adj_acct_keyvalue
        {
            get { return _adj_acct_keyvalue; }
            set { _adj_acct_keyvalue = value; }
        }
        public string count_acct_keyvalue
        {
            get { return _count_acct_keyvalue; }
            set { _count_acct_keyvalue = value; }
        }
                 

            //Account Type
         public string inv_acct_type
        {
            get { return _inv_acct_type; }
            set { _inv_acct_type = value; }
        }
        public string salse_acct_type
        {
            get { return _salse_acct_type; }
            set { _salse_acct_type = value; }
        }
        public string cog_acct_type
        {
            get { return _cog_acct_type; }
            set { _cog_acct_type = value; }
        }
        public string adj_acct_type
        {
            get { return _adj_acct_type; }
            set { _adj_acct_type = value; }
        }
        public string count_acct_type
        {
            get { return _count_acct_type; }
            set { _count_acct_type = value; }
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
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvtrydefupd"; }
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
            get { return "uspinventorydefget"; }
        }

        public override string TABLE_NAME
        {
            get { return "sticntrc"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string GET_XINFOR
        {
            get { return "uspxinforget"; }
        }

        

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();


            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
