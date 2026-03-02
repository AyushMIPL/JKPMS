using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOUserAccountPermissionInaccdef:DVOBase
    {
        private int _user_id;
        private string _acc_mask;
        private int _acd_id;
        private int _acct_type_id;
        private string _accounttype;
        private int _approval_level;
        private int _RowID;

        private string _Login_Id;
        private string _Module;
        private string _Dept;
        private decimal _TotalAmount;

        //*******  Particularly to approve documents, not for inaccdef table
        private int _doc_no;
        private int _pre_current_approval;
        private int _post_current_approval;
        //*****************************************************************

        #region Constructor

        public DVOUserAccountPermissionInaccdef()
        {
            _user_id = 0;
            _acc_mask = string.Empty;
            _acd_id = 0;
            _acct_type_id = 0;
            _accounttype = string.Empty;
            _approval_level = 0;
            _RowID = 0;

            _Login_Id = string.Empty;
            _Module = string.Empty;
            _Dept = string.Empty;
            _TotalAmount = 0;

            _doc_no = 0;
            _pre_current_approval = 0;
            _post_current_approval = 0;
        }

        #endregion Constructor

        #region public properties

        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public string acc_mask
        {
            get{return _acc_mask;}
            set{_acc_mask = value;}
        }
        public int acd_id
        {
            get{return _acd_id;}
            set{_acd_id = value;}
        }

        public int acct_type_id
        {
            get{return _acct_type_id;}
            set{_acct_type_id = value;}
        }
        public string accounttype
        {
            get{return _accounttype;}
            set{ _accounttype = value;}
        }
        public int approval_level
        {
            get{return _approval_level;}
            set{_approval_level = value;}
        }
        public int RowID
        {
            get{return _RowID;}
            set {_RowID = value;}
        }



        public string Login_Id
        {
            get { return _Login_Id; }
            set { _Login_Id = value; }
        }
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }
        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }



        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public int pre_current_approval
        {
            get { return _pre_current_approval; }
            set { _pre_current_approval = value; }
        }
        public int post_current_approval
        {
            get { return _post_current_approval; }
            set { _post_current_approval = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public string SET_AP_DOCUMENT_APPROVE
        {
            get { return "uspaprovapdoc"; }
        }
        public string SET_AP_DOCUMENT_NOT_APPROVE
        {
            get { return "uspnotaprovapdoc"; }
        }
        public string SET_INV_DOCUMENT_APPROVE
        {
            get { return "uspaprovinvdoc"; }
        }
        public string SET_INV_DOCUMENT_NOT_APPROVE
        {
            get { return "uspnotaprovinvdoc"; }
        }

       
        public string SET_PU_DOCUMENT_APPROVE
        {
            get { return "usppurovpudoc"; }
        }
        public string SET_PU_DOCUMENT_NOT_APPROVE
        {
            get { return "uspnotpurovpudoc"; }
        }
        //****************
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
            get { return "inaccdef"; }
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

        public override string FIND_QUERY(ref object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            if (parameters[0].ToString().Trim() == "H")
            {
                sql.Append("SELECT inaccdef.user_id v_user_id,inaccdef.acc_mask v_acc_mask,inaccdef.acd_id v_acd_id,");
                sql.Append(" inaccdef.acct_type_id v_acct_type_id,Flex_struct_Header.accounttype v_accounttype,");
                sql.Append(" inaccdef.approval_level v_approval_level,inaccdef.RowId v_RowId,inxuserr.login_id v_login_id,");
                sql.Append(" inappcls.module v_module,inappcls.dept v_dept,inappcls.total_amount v_total_amount");
                sql.Append(" FROM inaccdef,inappcls,inxuserr,Flex_struct_Header");
                sql.Append(" WHERE inaccdef.user_id=inxuserr.user_id and inaccdef.acd_id=inappcls.acd_id and inaccdef.acct_type_id=Flex_struct_Header.id");

                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND inaccdef.user_id = " + parameters[1].ToString());
                if (parameters[2] != null)
                    if (parameters[2].ToString() != string.Empty)
                        sql.Append(" AND Rtrim(inaccdef.acc_mask) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[3]) > 0)
                    sql.Append(" AND inaccdef.acd_id = " + parameters[3].ToString());
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" AND inaccdef.acct_type_id = " + parameters[4].ToString());
                if (Convert.ToInt32(parameters[5]) > 0)
                    sql.Append(" AND inaccdef.approval_level = " + parameters[5].ToString());
                if (parameters[6] != null)
                    if (parameters[6].ToString() != string.Empty)
                        sql.Append(" AND inappcls.module = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[7]) > 0)
                    sql.Append(" AND inaccdef.RowId = " + parameters[7].ToString());
            }
            //this is specifically to approve the AP documents
            else if (parameters[0].ToString().Trim() == "DAP")
            {
                sql.Append("SELECT Distinct stpcashe.doc_no p_doc_no,'APC' v_type,bus_name v_bus_name,chk_date p_chk_date,");
                sql.Append(" required_approval v_required_aproval,current_approval v_current_approval,PayrollGLAccounts.keyvalue v_keyvalue,");
                sql.Append(" acd_id v_acd_id,cash_amt v_cash_amt,batch_id p_batch_id,PayrollGLAccounts.acct_type,stpcashe.RowId v_RowId,vend_code p_vend_code");
                sql.Append(" FROM stpcashe,PayrollGLAccounts,stpcashd WHERE stpcashe.doc_no=stpcashd.doc_no AND stpcashd.dist_acct=PayrollGLAccounts.acct_no");
                sql.Append(" AND required_approval >= 0");// 
                sql.Append(" AND current_approval > -1");
                sql.Append(" AND ap_type = 'N'");
                sql.Append(" AND Rtrim(ok_to_post) NOT IN ('P','C')");
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND Rtrim(PayrollGLAccounts.acct_type) = '" + parameters[1].ToString().Trim() + "'");
                if (Convert.ToInt32(parameters[2]) > 0)
                    sql.Append(" AND (required_approval <= " + parameters[2].ToString() + " OR  required_approval is null)");
                
                //sql.Append("SELECT chk_date p_chk_date,stpcashe.doc_no p_doc_no,vend_code p_vend_code,pay_to_code p_pay_to_code,");
                //sql.Append(" gross_entry v_gross_entry,def_mtaxcd v_def_mtaxcd,check_no p_check_no,doc_desc p_doc_desc,");
                //sql.Append(" cash_amt v_cash_amt,cash_acct v_cash_acct,cash_department v_cash_department,");
                //sql.Append(" cash_deb_cred v_cash_deb_cred,oa_amt v_oa_amt,oa_acct v_oa_acct,oa_department v_oa_department,");
                //sql.Append(" oa_deb_cred v_oa_deb_cred,print_chk p_print_chk,ok_to_post p_ok_to_post,chk_printed v_chk_printed,");
                //sql.Append(" ap_type v_ap_type,batch_id p_batch_id,min_voucher_no v_min_voucher_no,tre_voucher_no v_tre_voucher_no,");
                //sql.Append(" bus_name v_bus_name,required_approval v_required_aproval,current_approval v_current_approval,");
                //sql.Append(" acd_id v_acd_id,stpcashe.RowId v_RowId,PayrollGLAccounts.keyvalue v_keyvalue,'APC' v_type");
                //sql.Append(" AND current_approval = " + parameters[1].ToString());
                //if (parameters[2] != null)
                //    if (parameters[2].ToString() != string.Empty)
                //        sql.Append(" AND Rtrim(ok_to_post) = '" + parameters[2].ToString().Trim() + "'");
                //    else
                //        sql.Append(" AND Rtrim(ok_to_post) NOT IN ('P','C')");
                //if (Convert.ToInt32(parameters[3]) > 0)
                //    sql.Append(" AND required_approval  = " + parameters[3].ToString());
            }
            //this is specifically to approve the GL Invoices
            else if (parameters[0].ToString().Trim() == "DINV")
            {
                sql.Append("SELECT Distinct stpinvce.doc_no v_doc_no,'INV' v_type,inv_no v_inv_no, stpvendr.bus_name v_bus_name,");
                sql.Append(" inv_date v_inv_date,required_approval v_required_aproval,current_approval v_current_approval,");
                sql.Append(" keyvalue v_keyvalue,acd_id v_acd_id,ap_amount v_ap_amount,batch_id v_batch_id,acct_type v_acct_type,");
                sql.Append(" stpvendr.vend_code v_vend_code");
                sql.Append(" FROM stpinvce, PayrollGLAccounts, stpvendr WHERE stpinvce.ap_acct_no = PayrollGLAccounts.acct_no");//stpinvcd, stpinvce.doc_no = stpinvcd.doc_no and 
                sql.Append(" and stpvendr.vend_code = stpinvce.vend_code");
                sql.Append(" and (required_approval >= 0 OR  required_approval is null) and (current_approval > -1 or current_approval is null)");
                sql.Append(" and stpinvce.ok_to_post NOT IN ('P','C')");
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND Rtrim(PayrollGLAccounts.acct_type) = '" + parameters[1].ToString().Trim() + "'");
                if (Convert.ToInt32(parameters[2]) > 0)
                    sql.Append(" AND (required_approval <= " + parameters[2].ToString() + " OR  required_approval is null)");
                
            }

            else if (parameters[0].ToString().Trim() == "PUH")
            {
                sql.Append("SELECT inaccdef.user_id v_user_id,inaccdef.acc_mask v_acc_mask,inaccdef.acd_id v_acd_id,");
                sql.Append(" inaccdef.acct_type_id v_acct_type_id,Flex_struct_Header.accounttype v_accounttype,");
                sql.Append(" inaccdef.approval_level v_approval_level,inaccdef.RowId v_RowId,inxuserr.login_id v_login_id,");
                sql.Append(" inappcls.module v_module,inappcls.dept v_dept,inappcls.total_amount v_total_amount");
                sql.Append(" FROM inaccdef,inappcls,inxuserr,Flex_struct_Header");
                sql.Append(" WHERE inaccdef.user_id=inxuserr.user_id and inaccdef.acd_id=inappcls.acd_id and inaccdef.acct_type_id=Flex_struct_Header.id");

                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND inaccdef.user_id = " + parameters[1].ToString());
                if (parameters[2] != null)
                    if (parameters[2].ToString() != string.Empty)
                        sql.Append(" AND Rtrim(inaccdef.acc_mask) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[3]) > 0)
                    sql.Append(" AND inaccdef.acd_id = " + parameters[3].ToString());
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" AND inaccdef.acct_type_id = " + parameters[4].ToString());
                if (Convert.ToInt32(parameters[5]) > 0)
                    sql.Append(" AND inaccdef.approval_level = " + parameters[5].ToString());
                if (parameters[6] != null)
                    if (parameters[6].ToString() != string.Empty)
                        sql.Append(" AND inappcls.module = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[7]) > 0)
                    sql.Append(" AND inaccdef.RowId = " + parameters[7].ToString());
            }

              //this is specifically to approve the Purchasing documents
            else if (parameters[0].ToString().Trim() == "DPU")
            {
                sql.Append("SELECT Distinct stuordre.doc_no p_doc_no,'PU' v_type,bus_name v_bus_name,po_date p_po_date,");
                sql.Append(" required_approval v_required_aproval,current_approval v_current_approval,PayrollGLAccounts.keyvalue v_keyvalue,");
                sql.Append(" acd_id v_acd_id,total_amount v_total_amount,");
                //batch_id p_batch_id,
                sql.Append("PayrollGLAccounts.acct_type,stuordre.RowId v_RowId,vend_code p_vend_code");
                sql.Append(" FROM stuordre,PayrollGLAccounts WHERE stuordre.po_acct_no=PayrollGLAccounts.acct_no");
                sql.Append(" AND required_approval >= 0");//stpcashd,stpcashe.doc_no=stpcashd.doc_no AND 
                sql.Append(" AND current_approval > -1");
                //sql.Append(" AND po_type = 'N'");
                sql.Append(" AND Rtrim(po_status) <> 'CAN' AND Rtrim(po_stage) <> 'CAN'");//,'COM'
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND Rtrim(PayrollGLAccounts.acct_type) = '" + parameters[1].ToString().Trim() + "'");
                if (Convert.ToInt32(parameters[2]) > 0)
                    sql.Append(" AND (required_approval <= " + parameters[2].ToString() + " OR  required_approval is null)");

                if (parameters[3] != null)
                {
                    if (Convert.ToBoolean(parameters[3]))
                        sql.Append(" AND stuordre.po_type='CPU' ");
                    else
                        sql.Append(" AND stuordre.po_type <> 'CPU' ");
                }
                


            }

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
