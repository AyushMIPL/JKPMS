using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVOUpdatePayableDefaults:DVOBase 
    {
        //***********Implemented By: Sunil Pahwa*****************

            //table stptermr
            private string     _terms_code;
            private string     _terms_desc;
            private int        _due_days;
            private int        _disc_days;
            private decimal      _disc_pct;

          
            #region Constructor

            

            #endregion Constructor

            #region Public Properties
            public string terms_code
            {
                get { return _terms_code; }
                set {_terms_code=value;}
            }

            public string terms_desc
            {
                get { return _terms_desc; }
                set { _terms_desc = value; }
            }
            public int due_days
            {
                get { return _due_days; }
                set { _due_days = value; }
            }

            public int disc_days
            {
                get { return _disc_days ; }
                set { _disc_days  = value; }
            }

            public decimal  disc_pct
            {
                get { return _disc_pct ; }
                set { _disc_pct  = value; }
            }



     


            #endregion Public Properties

            #region Stored-Procedures
            public override string INSERT_SPNAME
            {
                get { return "uspinspaydefupd"; }
            }

            public override string UPDATE_SPNAME
            {
                get { return "uspUpdPayDefUpd "; }
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
                get { return "uspUpdPayDefGetAll"; }
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
             
                return sql.ToString();
            } 

            #endregion Stored-Procedures
        }
    }

