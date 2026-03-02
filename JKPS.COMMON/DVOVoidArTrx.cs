using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOVoidArTrx : DVOBase
    {

        private int _rowId;           
        private int _ar_doc_no;
        private int _doc_no;
        private string _void_date;//date
        private string _ok_to_post;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;

        #region Constructor
        public DVOVoidArTrx()
        {
            _rowId = 0;
            _ar_doc_no=0;
            _doc_no=0;
            _void_date = "01/01/1900";//date
            _ok_to_post=string.Empty;
            _InsertMachineInfo = DVOApplicationUserInfo.MachineInfo;
            _InsertDate = "01/01/1900";
            _InsertBy = DVOApplicationUserInfo.UserId;
            _UpdateMachineInfo = DVOApplicationUserInfo.MachineInfo;
            _UpdateDate = "01/01/1900";
            _UpdateBy = DVOApplicationUserInfo.UserId;
        }
        #endregion Constructor

        #region Properties

        public int RowId
        {
            get { return _rowId; }
            set { _rowId = value; }
        }

        public int ar_doc_no
        {
            get { return _ar_doc_no; }
            set { _ar_doc_no = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string void_date
        {
            get { return _void_date; }
            set { _void_date = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
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
        #endregion Properties

        #region Store Procedures
        public override string INSERT_SPNAME
        {
            get { return "uspstrvoidrins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstrvoidrupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspstrvoidrdel"; }
        }

        public override string FIND_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string TABLE_NAME
        {
            get { return "strvoidr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string CHK_XTRANR
        {
            get { return "uspxtranrarvd"; }
        }
        public string GET_VOIDR
        {
            get { return "uspstrvoidrget"; }
        }
        public string UPD_STS
        {
            get { return "uspstrvoidrupdsts"; }
        }
        public string GET_XTRANR
        {
            get { return "usparxtranrget"; }
        }
        public string GET_GLACT
        {
            get { return "usparglactget"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();


            sql.Append("select rowid,ar_doc_no,doc_no,void_date,ok_to_post from strvoidr where ok_to_post <> 'C'");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND ar_doc_no= " + parameters[0].ToString());

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(ok_to_post) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0)
                    sql.Append(" AND doc_no= " + parameters[2].ToString());

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && parameters[3].ToString().Trim()!="01/01/1900")
                sql.Append(" AND void_date = '" + parameters[3].ToString() + "'");

            if (parameters[4] != null)
               if (Convert.ToInt32(parameters[4]) > 0)
                   sql.Append(" AND rowid= " + parameters[4].ToString());

           sql.Append(" order by ar_doc_no");

            return sql.ToString();
        }
        #endregion Store Procedures
    }
}
