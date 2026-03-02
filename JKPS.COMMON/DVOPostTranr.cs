using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVOPostTranr:DVOBase
    {
       private int _acct_no;
       private string _acct_cat;
       private string _acct_status;
       private int _doc_no;
      public int acct_no
      {

       get {return _acct_no;}
          set {_acct_no=value;}
      }
       public string acct_cat
       {
           get{return _acct_cat ;}
           set{_acct_cat=value;}
       }
       public string acct_status
       {
           get{return _acct_status; }
           set {_acct_status=value;}

       }
       public int doc_no
       {

           get { return _doc_no ; }
           set { _doc_no  = value; }
       }

         #region Stored-Procedures


        public override string INSERT_SPNAME
        {
            get { return ""; }//uspaspurclsins
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspsblistingupd"; }
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

       //public string GET_SAVING_BANK_EDIT_LISTING
       //{
       //   get { return "uspsblistinggetall"; }
       //}

       public string GET_SAVING_BANK_POST_LISTING
       {
           get { return "uspsbpostlist"; }
       }
       public string UPDATE_CLIENT_STATUS_IN_CLOSE_ACCOUNT
       {
           get { return "uspclosesbdtlupd"; }
       }
       public string GET_SB_GL_SUM
       {
           get { return "uspsbglsum"; }
       }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
             sql.Append(" select acct_id,acct_cat,acct_no,acct_status,");
             sql.Append(" doc_date,withdrawn_amt,deposit_amt, ");
             sql.Append(" acct_balance,operator,tran_type,tarn_no,doc_no ");
             sql.Append(" from sbposttranr where 1=1 ");
             if (parameters[0] != null)
                 if (parameters[0].ToString() != string.Empty)
                     sql.Append(" AND  acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
             if (parameters[1] != null)
                 if (Convert.ToInt32(parameters[1]) > 0)
                     sql.Append(" AND acct_no =" + parameters[1].ToString());

             //Commented by Rahul Jain no need Status parameter 27/08/2009
             //if (parameters[2] != null)
             //    if (parameters[2].ToString() != string.Empty)
             //        sql.Append(" AND acct_status=" + "'" + parameters[2].ToString().Replace("'", "''")+"'");
             
            sql.Append(" order by doc_no");
            return sql.ToString();
        }

        #endregion Stored-Procedures
    
}
    }

