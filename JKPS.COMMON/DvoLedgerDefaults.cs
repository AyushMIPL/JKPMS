using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //*************Implemented By: Sunil Pahwa*******************
    public class DvoLedgerDefaults : DVOBase
    {

        #region Stored-Procedures

        //public string uspAccGrpsDtlIns
        //{
        //    get { return "uspAccGrpsDtlIns"; }
        //}

        //public string uspAccGrpsDtlupd
        //{
        //    get { return "uspAccGrpsDtlupd"; }
        //}

        //public string AUTHENTICATION_SPNAME
        //{
        //    get { return "uspsecauthenticate"; }
        //}

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
            get { return ""; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT gr.grp_key p_grp_key ,gr.grp_desc p_grp_desc,");
            //sql.Append("tr.acct_type v_acct_type, tr.keyvalue v_keyvalue,tr.acct_desc v_acct_desc,");
            //sql.Append("kh.accounttype l_accounttype, kh.desc l_desc ");
            //sql.Append("from stxactgr gr,stxactgd gd,PayrollGLAccounts tr,Flex_struct_Header kh ");
            //sql.Append("where gr.grp_key=gd.grp_key and gd.acct_no=tr.acct_no ");
            //sql.Append("and tr.acct_type=kh.accounttype");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(gr.grp_key) LIKE '" + parameters[0].ToString().Trim() + "%'");
            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(gr.grp_desc) LIKE '" + parameters[1].ToString().Trim() + "%'");



            return sql.ToString();
        }
        #endregion store-procedures

    }

}
