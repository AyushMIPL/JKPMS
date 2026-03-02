using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)******************** DevelopmentDate(Modified Date)
    ///1.)     DVO For Fiscal BTL  Linker                Rajeev(D)                                 31/12/2009(DD)
    ///2.) 
    ///<summery>
    public class DVOBTLFiscalLinkerFisbtllinker : DVOBase
    {
        private int _btlid;
        private string _period_month;
        private string _period_year;
        private int _mcatid;
        private int _catid;
        private int _subcatid;
        private string _description;
        private Int32 _order;

        private int _RowID;
        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;


        //Added by Rajeev For BTL Setup
        //Added Date :17/01/10 

        private int _accountcheck;
        private string _accountKeyval; 
         

        public DVOBTLFiscalLinkerFisbtllinker()
        {
            _btlid=0;
            _period_month=string.Empty;
            _period_year=string.Empty;
            _mcatid=0;
            _catid = 0;
            _subcatid = 0;
            _description=string.Empty;
            _order = 0;

            _RowID = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";
            _InsertBy = 0;

            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";
            _UpdateBy = 0;

            _accountcheck = 0;
            _accountKeyval = string.Empty;
        }

        #region StartProperties

        public int btlid
        {
            get { return _btlid; }
            set { _btlid = value; }
        }
        public string period_month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public int mcatid
        {
            get { return _mcatid; }
            set { _mcatid = value; }
        }
        public int catid
        {
            get { return _catid; }
            set { _catid = value; }
        }
        public int subcatid
        {
            get { return _subcatid; }
            set { _subcatid = value; }
        }

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Int32 order
        {
            get { return _order; }
            set { _order = value; }
        }

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public string InsertDate
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
        public string UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        //added by rajeev to setup BELLIN account
        public int accountcheck
        {
            get { return _accountcheck; }
            set { _accountcheck = value; }
        }
        public string accountKeyval
        {
            get { return _accountKeyval; }
            set { _accountKeyval = value; }
        }

        #endregion StartProperties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspfisnbtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspfisnbtlhupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspbtlnrecdel"; } 
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
            get { return "fisbtlhdr"; }
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
        public override string FIND_QUERY(ref Object[] parameters)
        {            
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,linkid,period_month,period_year,mcatid,catid,subcatid,accountcheck,accountKeyval,InsertMachineInfo,InsertDate,InsertBy,UpdateMachineInfo,");
            sql.Append(" UpdateDate,UpdateBy from fisbtlhdr");
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append(" AND catid=" + Convert.ToInt32(parameters[0]));
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) != 0)
                    sql.Append(" AND mcatid=" + Convert.ToInt32(parameters[1]));
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) != 0)
                    sql.Append(" AND subcatid=" + Convert.ToInt32(parameters[2]));
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND period_month=" + parameters[3].ToString().Trim().Replace("'", "''"));
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND period_year=" + parameters[4].ToString().Trim().Replace("'", "''"));
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND accountcheck=" + Convert.ToInt32(parameters[5]));
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    if(parameters[6].ToString().Trim()!="0")
                    sql.Append(" AND accountKeyval=" + parameters[6].ToString().Trim().Replace("'", "''"));
            if (parameters[7] != null)
                if (Convert.ToInt32(parameters[7]) != 0)
                    sql.Append(" AND Rowid=" + Convert.ToInt32(parameters[7]));

            return sql.ToString();
        }

        
        public string Find_next_order
        {
            get { return "uspbtlnxtodrget"; }
        }

        public string GET_DESCRIPTION_BY_ID
        {
            get { return "uspbtldescget"; }
        }


       
        //Updated These procedure name by new Bellin setup 
        //public override string INSERT_SPNAME
        //{
        //    get { return "uspfisbtlins"; }
        //}

        //public override string UPDATE_SPNAME
        //{
        //    get { return "uspfisbtlrecupd"; }
        //}

        //public override string DELETE_SPNAME
        //{
        //    get { return "uspbtlrecdel"; }
        //}

        //public override string TABLE_NAME
        //{
        //    get { return "fisbtllinker"; }
        //}

        
        #endregion Stored-Procedures
    }
}
