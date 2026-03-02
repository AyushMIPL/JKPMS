using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by : sanjay chawla
    /// Date : 26 May 2009
    /// Description :common class for Treasury Bill tenders
    /// Modified Date : 
    /// Description : 
    public class DVOTreasuryBillTenders : DVOBase
    {
        private int _Rowid;
        private int _tend_code;
        private string _tend_name;
        private string _tend_class;
        private string _address1;
        private string _address2;
        private string _city;
        private string _contact;
        private string _phone;
        private string _fax;
        private string _email;
        private int _Insertby;
        private DateTime _InsertDate;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private DateTime _Updatedate;
        private string _UpdateMachineInfo;

        private int _option;

        private int _tbschid;
        private int _issue_num;


        #region Constructor
        public DVOTreasuryBillTenders()
        {
            _Rowid = 0;
            _tend_code = 0;
            _tend_name = string.Empty;
            _tend_class = string.Empty;
            _address1 = string.Empty;
            _address2 = string.Empty;
            _city = string.Empty;
            _contact = string.Empty;
            _phone = string.Empty;
            _fax = string.Empty;
            _email = string.Empty;
            _Insertby = 0;
            _InsertDate = Convert.ToDateTime("01/01/1900");
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _Updatedate = Convert.ToDateTime("01/01/1900");
            _UpdateMachineInfo = string.Empty;

            _option = 0;
            _tbschid = 0;
            _issue_num = 0;
        }
        #endregion Constructor

        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }

        public int tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public string tend_name
        {
            get { return _tend_name; }
            set { _tend_name = value; }
        }
        public string tend_class
        {
            get { return _tend_class; }
            set { _tend_class = value; }
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
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        public int Insertby
        {
            get { return _Insertby; }
            set { _Insertby = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public DateTime Updatedate
        {
            get { return _Updatedate; }
            set { _Updatedate = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public int tbschid
        {
            get { return _tbschid; }
            set { _tbschid = value; }
        }

        public int issue_num
        {
            get { return _issue_num; }
            set { _issue_num = value; }
        }
        
         

        #endregion public properties
        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usp_ins_trea_tend"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_trea_tend"; }
        }
        //Added by Sunil Pahwa [21/10/09] to delete the treasury info after confirming from the tbissued table
        public override string DELETE_SPNAME
        {
            get { return "usptraizinfodel"; }//usp_del_trea_tend
        }
        //Added by sunil Pahwa  [21/10/09] for check the records in tbissued before delete
        public override string FIND_SPNAME
        {
            get { return "usptraninfoget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        //public string GET_Tender_List
        //{
        //    get { return "usp_get_tend_list"; }
        //}
        public override string TABLE_NAME
        {
            get { return "tbclients"; }
        }
        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return _tend_code.ToString(); }
            set
            {
                if (value.Trim().Length > 0)
                    _tend_code = Convert.ToInt32(value);
            }
        }

        public int Option
        {
            get { return _option; }
            set { _option = value; }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid,tend_code,tend_class,tend_name,address1,address2,contact,phone,fax,email FROM tbclients where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != "0")//tend_code
                    if (Convert.ToInt32(parameters[0]) > 0)
                        sql.Append(" AND tend_code =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)//tend_class
                    sql.Append(" AND tend_class LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)//tend_name
                    sql.Append(" AND tend_name LIKE '" + parameters[2].ToString().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)//address1
                    sql.Append(" AND address1 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)//address2
                    sql.Append(" AND address2 LIKE '" + parameters[4].ToString().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)//contact
                    sql.Append(" AND contact like '" + parameters[5].ToString().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim().Length > 0)//phone
                    sql.Append(" AND phone ='" + parameters[6].ToString() + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString().Trim().Length > 0)//fax
                    sql.Append(" AND fax like '" + parameters[7].ToString().Replace("'", "''") + "%'");
            if (parameters[8] != null)
                if (parameters[8].ToString().Trim().Length > 0)//email
                    sql.Append(" AND email ='" + parameters[8].ToString() + "'");
            if (Convert.ToInt32(parameters[9]) > 0)
                sql.Append(" AND  rowid=" + parameters[9].ToString());
            sql.Append(" order by tend_code");
            return sql.ToString();
        }
        //Modified by Sunil Pahwa for option on tend name 
        public string GET_Tender_List(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();


            sql.Append("select tbclients.tend_code,tbclients.tend_class,tbclients.tend_name,tbclients.address1,tbclients.address2,tbclients.city,");
            sql.Append("tbclients.contact,tbclients.phone,tbclients.email  from tbclients where 1=1 ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)//tend_code
                    sql.Append(" AND tbclients.tend_code ='" + parameters[0].ToString() + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)//tend_class
                    sql.Append(" AND tbclients.tend_class LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
            if (parameters[4] != null)
            {
                if (Convert.ToInt16(parameters[4]) == 1)
                {
                    if (parameters[2] != null)//tend_name
                        if (parameters[2].ToString().Trim().Length > 0)
                            sql.Append(" AND tbclients.tend_name MATCHES '" + parameters[2].ToString() + "*'");
                }
                if (Convert.ToInt16(parameters[4]) == 2)
                {
                    if (parameters[2] != null)
                        if (parameters[2].ToString().Trim().Length > 0)//tend_name
                            sql.Append(" AND tbclients.tend_name MATCHES  '*" + parameters[2].ToString() + "*'");

                }
                if (Convert.ToInt16(parameters[4]) == 3)
                {
                    if (parameters[2] != null)
                        if (parameters[2].ToString().Trim().Length > 0)//tend_name
                            sql.Append(" AND tbclients.tend_name MATCHES '*" + parameters[2].ToString() + "'");
                }
                if (Convert.ToInt16(parameters[4]) == 0)
                {
                    if (parameters[2] != null)
                        if (parameters[2].ToString() != string.Empty)//tend_name
                            sql.Append(" AND tbclients.tend_name  ='" + parameters[2].ToString() + "'");
                }
            }
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)//address1
                    sql.Append(" AND tbclients.address1 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");
            // if(parameters[3]!= null)
            //if (parameters[3].ToString() != string.Empty )//address2
            //    sql.Append(" AND tbclients.address2 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");

            //sql.Append(" AND tbissued.issue_num=(select max(tbissued.issue_num) from tbissued where tend_code=)");
            sql.Append("order by tend_code asc");


            return sql.ToString();


        }

        public string GET_TENDER_ISSUE_AMT(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tend_code ,amt_issued, amt_per_100 FROM  tbissued");
            sql.Append(" where 1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)//tend_code
                    sql.Append(" AND tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)//tend_class
                    sql.Append(" AND issue_num =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)//address1
                    sql.Append(" AND tend_code='" + parameters[2].ToString() + "'");
            // if(parameters[3]!= null)
            //if (parameters[3].ToString() != string.Empty )//address2
            //    sql.Append(" AND tbclients.address2 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");
            if (parameters.Length >= 4)
            {
                if (parameters[3] != null)
                {
                    if (Convert.ToInt32(parameters[3]) == 1)
                    {

                        sql.Append(" AND bill_status NOT IN ('C','P')");
                    }
                    else if (Convert.ToInt32(parameters[3]) == 0)
                    {
                        sql.Append(" AND bill_status NOT IN ('C')");
                    }
                }
            }
            //sql.Append(" AND tbissued.issue_num=(select max(tbissued.issue_num) from tbissued where tend_code=)");
            // sql.Append("order by tend_code asc");


            return sql.ToString();


        }
        public string GET_lOWER_ISSUE_NUM(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select Max(issue_num) FROM  tbissuer");
            sql.Append(" where 1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)//tend_code
                    sql.Append(" AND tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)//tend_class
                    sql.Append(" AND issue_num <" + parameters[1].ToString());
            // if(parameters[3]!= null)
            //if (parameters[3].ToString() != string.Empty )//address2
            //    sql.Append(" AND tbclients.address2 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");

            //sql.Append(" AND tbissued.issue_num=(select max(tbissued.issue_num) from tbissued where tend_code=)");
            // sql.Append("order by tend_code asc");


            return sql.ToString();


        }
        //public string GET_Tender_List(ref Object[] parameters)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("select tend_code,tend_name,address1,address2 from tbclients where 1=1");
        //    if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//tend_code
        //        sql.Append(" AND tbclients.tend_code =" + parameters[0].ToString());
        //    if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//tend_class
        //        sql.Append(" AND tbclients.tend_class LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
        //    if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//tend_name
        //        sql.Append(" AND tbclients.tend_name LIKE '" + parameters[2].ToString().Replace("'", "''") + "%'");
        //    if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//address1
        //        sql.Append(" AND tbclients.address1 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");
        //    if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//address2
        //        sql.Append(" AND tbclients.address2 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");
        //    return sql.ToString();
        //}
        #endregion store-procedures

        public string  GET_Tender_ListByIssueNum(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbclients.tend_code,tbclients.tend_class,tbclients.tend_name,");
            sql.Append(" tbclients.address1,tbclients.address2,tbclients.city,");
            sql.Append(" tbclients.contact,tbclients.phone,tbclients.email,tbissued.tbschid,");
            sql.Append(" tbissued.issue_num,tbschemes.tbschname  from tbclients,tbissued,tbschemes ");
            sql.Append("  where tbissued.tend_code=tbclients.tend_code and tbschemes.tbschid=tbissued.tbschid ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)//tend_code
                    sql.Append(" AND tbclients.tend_code ='" + parameters[0].ToString() + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)//tend_class
                    sql.Append(" AND tbclients.tend_class LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
            if (parameters[4] != null)
            {
                if (Convert.ToInt16(parameters[4]) == 1)
                {
                    if (parameters[2] != null)//tend_name
                        if (parameters[2].ToString().Trim().Length > 0)
                            sql.Append(" AND tbclients.tend_name MATCHES '" + parameters[2].ToString() + "*'");
                }
                if (Convert.ToInt16(parameters[4]) == 2)
                {
                    if (parameters[2] != null)
                        if (parameters[2].ToString().Trim().Length > 0)//tend_name
                            sql.Append(" AND tbclients.tend_name MATCHES  '*" + parameters[2].ToString() + "*'");

                }
                if (Convert.ToInt16(parameters[4]) == 3)
                {
                    if (parameters[2] != null)
                        if (parameters[2].ToString().Trim().Length > 0)//tend_name
                            sql.Append(" AND tbclients.tend_name MATCHES '*" + parameters[2].ToString() + "'");
                }
                if (Convert.ToInt16(parameters[4]) == 0)
                {
                    if (parameters[2] != null)
                        if (parameters[2].ToString() != string.Empty)//tend_name
                            sql.Append(" AND tbclients.tend_name  ='" + parameters[2].ToString() + "'");
                }
            }
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)//address1
                    sql.Append(" AND tbclients.address1 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");
            // if(parameters[3]!= null)
            //if (parameters[3].ToString() != string.Empty )//address2
            //    sql.Append(" AND tbclients.address2 LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");

            //sql.Append(" AND tbissued.issue_num=(select max(tbissued.issue_num) from tbissued where tend_code=)");
            if (parameters[5] != null)
                if (Convert.ToInt32(parameters[5]) > 0)
                    sql.Append(" AND tbissued.tbschid =" + parameters[5].ToString());
            if (parameters[6] != null)
                if (Convert.ToInt32(parameters[6]) > 0)
                    sql.Append(" AND tbissued.issue_num =" + parameters[6].ToString());

            sql.Append(" order by tend_code asc");


            return sql.ToString();

            
        }
    }
}
