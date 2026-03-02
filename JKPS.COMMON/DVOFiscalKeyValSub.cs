using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)******************** DevelopmentDate(Modified Date)
    ///1.)     DVO For Fiscal KeyValue Subtract                Rajeev(D)                                 08/06/2009(DD)
    ///2.) 
    ///<summery>
    public class DVOFiscalKeyValSub:DVOBase
    {
        private int _linkid;
        private string _acct_type;
        private string _keyvalue;

        private int _RowID;
        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;

         //Constructor to Assign the initial value to the decleared variable
        public DVOFiscalKeyValSub()
        {

            _linkid = 0;
            _acct_type = string.Empty;
            _keyvalue = string.Empty;

            _RowID = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = string.Empty;
            _InsertBy = 0;

            _UpdateMachineInfo = string.Empty;
            _UpdateDate = string.Empty;
            _UpdateBy = 0;
        }

        #region StartProperties

        public int linkid
        {
            get { return _linkid; }
            set { _linkid = value; }
        }

        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }

        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
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

        #endregion StartProperties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return ""; }
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
            get { return "fiskeyvalsub"; }
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
            //System.Text.StringBuilder sql = new StringBuilder();
            //sql.Append("Select Rowid,catid,mcatid,catdesc,catprintodr,InsertMachineInfo,InsertDate,InsertBy,UpdateMachineInfo,");
            //sql.Append(" UpdateDate,UpdateBy from fiscat");
            //sql.Append(" WHERE 1=1");
            //if (parameters[0] != null)
            //    if (Convert.ToInt32(parameters[0]) != 0)
            //        sql.Append("AND mcatid=" + Convert.ToInt32(parameters[0]));
            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(catdesc) LIKE '%" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[2] != null)
            //    if (parameters[2].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(catprintodr)  LIKE '%" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[3] != null)
            //    if (Convert.ToInt32(parameters[3]) != 0)
            //        sql.Append("AND Rowid=" + Convert.ToInt32(parameters[3]));

            //return sql.ToString();

            return "";
        }

        #endregion Stored-Procedures

    }
}
