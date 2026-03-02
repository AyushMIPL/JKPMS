using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)******************** DevelopmentDate(Modified Date)
    ///1.)     DVO For Fiscal Main Category            Rajeev(D)                                 26/05/2009(DD)
    ///2.) 
    ///<summery>
    public class DVOFiscalMainCat : DVOBase
    {
        private int _mcatid;
        private string _mcatdesc;
        private string _mcatprintodr;

        private int _RowID;
        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;

        //Constructor to Assign the initial value to the decleared variable
        public DVOFiscalMainCat()
        {
            _mcatid = 0;
            _mcatdesc = string.Empty;
            _mcatprintodr = string.Empty;

            _RowID = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = string.Empty;
            _InsertBy = 0;

            _UpdateMachineInfo = string.Empty;
            _UpdateDate = string.Empty;
            _UpdateBy = 0;
        }

        #region StartProperties
        public int mcatid
        {
            get { return _mcatid; }
            set { _mcatid = value; }
        }

        public string mcatdesc
        {
            get { return _mcatdesc; }
            set { _mcatdesc = value; }
        }

        public string mcatprintodr
        {
            get { return _mcatprintodr; }
            set { _mcatprintodr = value; }
        }

        public int RowID
        {
            get { return _RowID;}
            set {_RowID=value;}
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
            get { return "uspfismdatains"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspfismdataupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspfismdatadel"; }
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
            get { return "fismcat"; }
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
            sql.Append("Select Rowid,mcatid,mcatdesc,mcatprintodr,InsertMachineInfo,InsertDate,InsertBy,UpdateMachineInfo,");
            sql.Append(" UpdateDate,UpdateBy from fismcat");            
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mcatdesc) LIKE '%" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mcatprintodr)  LIKE '%" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) != 0)
                    sql.Append("AND Rowid=" + Convert.ToInt32(parameters[2]));
            sql.Append(" ORDER by mcatprintodr asc ");

            return sql.ToString();            
        }

        public string GET_ALL_FISCAL_DATA
        {
            get { return "uspfismcatget"; }
        }

        public string Find_next_order
        {
            get { return "uspfismcatodr"; }
        }

        #endregion Stored-Procedures

    }
}
