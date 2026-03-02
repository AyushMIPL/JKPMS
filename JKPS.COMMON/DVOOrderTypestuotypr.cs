using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOOrderTypestuotypr : DVOBase
    {
        private int _rowid;
        private string _po_type;
        private string _description;
        private string _vend_check;
        private string _master_ord;
        private string _part_relse;
        private string _accumulate;
        private string _ord_post;
        private string _process_hold;
        private string _reqdate;
        private string _lead_calc;
        private string _print_rec;
        private string _print_ack;
        private string _print_po;
        private string _no_cost_print;
        private string _post_to_ledgers;

        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        #region Constructor
        public DVOOrderTypestuotypr()
        {
            _rowid = 0;
            _po_type = "";
            _description = "";
            _vend_check = "";
            _master_ord = "";
            _part_relse = "";
            _accumulate = "";
            _ord_post = "";
            _process_hold = "";
            _reqdate = "";
            _lead_calc = "";
            _print_rec = "";
            _print_ack = "";
            _print_po = "";
            _no_cost_print = "";
            _post_to_ledgers = "";

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
        }
#endregion Constructor

        #region Properties
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string po_type
        {
            get { return _po_type; }
            set { _po_type = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string vend_check
        {
            get { return _vend_check; }
            set { _vend_check = value; }
        }
        public string master_ord
        {
            get { return _master_ord; }
            set { _master_ord = value; }
        }
         public string part_relse
        {
            get { return _part_relse; }
            set { _part_relse = value; }
        }
         public string accumulate
        {
            get { return _accumulate; }
            set { _accumulate = value; }
        }
         public string ord_post
        {
            get { return _ord_post; }
            set { _ord_post = value; }
        }
         public string process_hold
        {
            get { return _process_hold; }
            set { _process_hold = value; }
        }
        public string reqdate
        {
            get { return _reqdate; }
            set { _reqdate = value; }
        }
        public string lead_calc
        {
            get { return _lead_calc; }
            set { _lead_calc = value; }
        }
         public string print_rec
        {
            get { return _print_rec; }
            set { _print_rec = value; }
        }
         public string print_ack
        {
            get { return _print_ack; }
            set { _print_ack = value; }
        }
        public string print_po
        {
            get { return _print_po; }
            set { _print_po = value; }
        }
        public string no_cost_print
        {
            get { return _no_cost_print; }
            set { _no_cost_print = value; }
        }
        public string post_to_ledgers
        {
            get { return _post_to_ledgers; }
            set { _post_to_ledgers = value; }
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
        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspordertypeins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspordertypeupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspordertypedel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspordertypeget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspstuotyprgetall"; }
        }

        public override string TABLE_NAME
        {
            get { return "stuotypr"; }
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
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  rowid, po_type, description,vend_check,master_ord,part_relse,accumulate,");
            sql.Append(" ord_post,process_hold,reqdate,lead_calc,print_rec,print_ack,print_po ,no_cost_print,post_to_ledgers  ");
            sql.Append(" FROM  stuotypr where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND po_type  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND description LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND post_to_ledgers = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND print_po = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND no_cost_print = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if(Convert.ToInt32(parameters[5]) > 0)
                 sql.Append(" AND rowid =" + parameters[5]);
            sql.Append(" order by po_type");

            return sql.ToString();
        }
        public string FIND_DUPLICATE_POTYPE(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  rowid, po_type, description,vend_check,master_ord,part_relse,accumulate,");
            sql.Append(" ord_post,process_hold,reqdate,lead_calc,print_rec,print_ack,print_po ,no_cost_print,post_to_ledgers  ");
            sql.Append(" FROM  stuotypr where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND po_type  ='" + parameters[0].ToString().Replace("'", "''") + "'");
            sql.Append(" order by po_type");
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
