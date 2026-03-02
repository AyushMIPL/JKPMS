using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOItemCategoryStuitmcat:DVOBase 
    {
        private int _itemcat_id;// serial
        private string _itemcat_name;//char(50)
        private string _itemcat_desc;//char(200)
        private int _req_cpu_approval;//integer
        private int _insertby;// integer 
        private string _insertdate;//datetime year to fraction(3)
        private string _insertmachineinfo;// char(50)
        private int _updateby;//integer 
        private string _updatedate;// datetime year to fraction(3)
        private string _updatemachineinfo;//char(50)
        private int _rowid;

        public DVOItemCategoryStuitmcat()
        {
            _itemcat_id = 0;
            _itemcat_name = string.Empty;
            _itemcat_desc = string.Empty;
            _req_cpu_approval = 0;
            _insertby = 0;
            _insertdate = "01/01/1900";
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";
            _updatemachineinfo = string.Empty;
            _rowid = 0;
        }

        public int itemcat_id
        {
            get { return _itemcat_id; }
            set { _itemcat_id = value; }
        }
        public string itemcat_name
        {
            get { return _itemcat_name; }
            set { _itemcat_name = value; }
        }
        public string itemcat_desc
        {
            get { return _itemcat_desc; }
            set { _itemcat_desc = value; }
        }
        public int req_cpu_approval
        {
            get { return _req_cpu_approval; }
            set { _req_cpu_approval = value; }
        }

        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        #region storedProcedures


        public override string INSERT_SPNAME
        {
            get { return "uspstuitmcatins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstuitmcatupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspstuitmcatdel"; }
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
            get { return "stuitmcat"; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select itemcat_id,itemcat_name,itemcat_desc,req_cpu_approval,rowid ");
            sql.Append(" from stuitmcat where 1=1");


            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" and itemcat_name ='" + parameters[0].ToString().Trim() + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(itemcat_desc) LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");

            if (parameters[2] != null && parameters[2].ToString().Trim().Length > 0 && Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" and stuitmcat.req_cpu_approval=" + parameters[2].ToString());
            
            if (parameters[3] != null && parameters[3].ToString().Trim().Length > 0 && Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" and stuitmcat.rowid=" + parameters[3].ToString());
            sql.Append(" order by itemcat_id ");

            return sql.ToString();
        }
        #endregion StoredProcedures




    }
}
