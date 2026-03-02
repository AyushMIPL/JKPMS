using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMonthlyBudgetAlloca:DVOBase
    {
        private string _acct_type;
        private string _year;
        private string _keyvalue;

        #region Properties
        public string AccountType
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string Keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        #endregion Properties

        #region StoreProcedure
        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string DELETE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_SPNAME
        {
            get { return "uspMonthlyBudgetAlloca"; }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
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
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,");

            sql.Append("sum(inbestid.revised) TotalBudget ,");
            sql.Append("inballod.allocamount,inballod.startingperiod,PayrollGLAccounts.acct_type ,Flex_struct_Header.desc");
            sql.Append(" from PayrollGLAccounts,inbestid,inballod,Flex_struct_Header ");
            sql.Append(" where 1=1");

            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//Acct_type
                sql.Append(" and  PayrollGLAccounts.acct_type='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//Year
                sql.Append(" and inbestid.year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//
                sql.Append(" AND Rtrim(PayrollGLAccounts.Keyvalue) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");//AcctNo
            //sql.Append(" and inballod.startingperiod  is not null and inballod.allocamount is not null ");
            sql.Append(" and inbestid.account=PayrollGLAccounts.acct_no");
            sql.Append(" and inballod.estid=inbestid.id"); 
            sql.Append(" and PayrollGLAccounts.acct_cat='U'");
            sql.Append(" and Flex_struct_Header.accounttype=PayrollGLAccounts.acct_type");
            sql.Append(" group by PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,");
            sql.Append("inballod.allocamount,inballod.startingperiod ,PayrollGLAccounts.acct_type ,Flex_struct_Header.desc");
           
            return sql.ToString(); 
        }
        #endregion StoreProcedure
    }

    }
