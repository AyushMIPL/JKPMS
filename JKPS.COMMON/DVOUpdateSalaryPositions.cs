using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Implemented By Sunil Pahwa
   public  class DVOUpdateSalaryPositions:DVOBase 
    {
       private string _code;
       private string _desc;
       private string _dflt_cat_code;
       private string _dflt_scale_code;
       private string _authorizer;
       private decimal? _max_amount;
       private string _dflt_py_acct_type;

       private string _description;

       private string  _acct_desc;
       private string _acct_type;
       private string _keyvalue;

       private string _mincode;
       private string _maxcode;
       private decimal? _minperannum;
       private decimal? _maxperannum;
       private string _scalecode;

       private string _code1;
       private decimal? _per_annum;

       private string _code2;

       private int _Rowid;

       public DVOUpdateSalaryPositions()
       {
         _code=string.Empty;
         _desc=string.Empty ;
         _dflt_cat_code=string.Empty ;
         _dflt_scale_code=string.Empty ;
         _authorizer=string.Empty ;
         _max_amount = null;
         _dflt_py_acct_type=string.Empty;

         _description = string.Empty;
         _acct_desc = string.Empty ;
         _acct_type = string.Empty;
         _keyvalue = string.Empty;

         _code1 = string.Empty;
         _per_annum = null;

         _mincode=string.Empty;
         _maxcode=string.Empty ;
         _minperannum = null;
         _maxperannum = null;
         _scalecode=string.Empty;

         _Rowid = 0;
 

       }
   public string code
   {
       get{return _code;}
       set { _code = value; }
   }
       public string desc
       {
           get { return _desc; }
           set { _desc = value; }
       }

       public string dflt_cat_code
       {
           get { return _dflt_cat_code; }
           set { _dflt_cat_code = value; }
       }
       public string dflt_scale_code
       {
           get { return _dflt_scale_code; }
           set { _dflt_scale_code = value; }
       }
       public string authorizer
       {
           get { return _authorizer; }
           set { _authorizer = value; }
       }

       public decimal? max_amount
       {
           get { return _max_amount; }
           set { _max_amount = value; }
       }


       public string dflt_py_acct_type
       {
           get { return _dflt_py_acct_type; }
           set { _dflt_py_acct_type = value; }
       }

       public string description
       {
           get { return _description; }
           set { _description = value; }
       }
       
       
       public string  acct_desc
       {
           get { return _acct_desc ; }
           set { _acct_desc = value; }
       }

       public string  acct_type
       {
           get { return _acct_type ; }
           set { _acct_type  = value; }
       }



       public string keyvalue
       {
           get { return _keyvalue ; }
           set { _keyvalue  = value; }
       }


       public string code1
       {
           get { return _code1 ; }
           set { _code1  = value; }
       }

       public string code2
       {
           get { return _code2; }
           set { _code2 = value; }
       }

       public decimal?  per_annum
       {
           get { return _per_annum ; }
           set { _per_annum  = value; }
       }
       public  string mincode
       {
           get {return _mincode ;}
           set {_mincode=value;}

       }
       public string maxcode
       {
           get { return _maxcode; }
           set { _maxcode = value; }

       }
       public  decimal? minperannum
       {
           get { return _minperannum; }
           set { _minperannum = value; }

       }

       public  decimal? maxperannum
       {
           get { return _maxperannum; }
           set { _maxperannum = value; }

       }
       public string scalecode
       {
           get { return _scalecode; }
           set { _scalecode = value; }

       }


       public  int Rowid
       {
           get { return _Rowid; }
           set { _Rowid = value; }
       }
       //***************************  Added by Bharat [19 December, 2008] ************
       public string MinMaxScaleCodes
       {
           get { return ((_mincode == null) ? "" : (_mincode + "-")) + ((_maxcode == null) ? "" : _maxcode); }
       }
       //*******************************************************************************
    
       //binding with mcgdfltsalscale 
       public string uspDfltSalScgetall
       {
           get { return "uspDfltSalScgetall"; }
       }
       public string usppaysalposget
       {
           get { return "usppaysalposget"; }
       }

       public string usppaysalDetailIns
       {
           get { return "usppaysalDetailIns"; }
       }

       public string uspUpdSalgetall
       {
           get { return "uspUpdSalgetall"; }
       }

      
       /// <summary>
       /// stored procedures
       /// </summary>

       public override string INSERT_SPNAME
       {
           get { return "uspupdsalposins"; }
       }

       public override string UPDATE_SPNAME
       {
           get { return "uspupdsalposupd"; }
       }

       public override string DELETE_SPNAME
       {
           get { return "uspupdsalposdel"; }
       }

       public override string FIND_SPNAME
       {
         get { return ""; }
       }

       public override string ALL_SPNAME
       {
           get { return "uspsalaryposgetall"; }
       }
       public override string TABLE_NAME
       {
           get { return "inyposie"; }
       }

       public override int UNIQUE_ID
       {
           get { return _Rowid ; }
       }

       public override string NOTES_TABLE_RECORD_ID
       {
           get { return string.Empty; }
           set { throw new Exception("The method or operation is not implemented."); }
       }


       public override string FIND_QUERY(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();

          // sql.Append("select E.code,E.desc ,E.dflt_cat_code ,E.dflt_scale_code,");
          // sql.Append(" E.authorizer,E.max_amount,E.dflt_py_acct_type ,");
          // sql.Append(" H.keyvalue from inyposie E ");
          //// sql.Append(" where H.acct_type=E.dflt_py_acct_type and");
          // //sql.Append(" H.acct_type=K.Accounttype");
           
          // if (parameters[0] != null)
          //     if (parameters[0].ToString() != string.Empty)
          //         sql.Append(" AND Rtrim(code) LIKE '" + parameters[0].ToString().Trim() + "%'");

          // if (parameters[1] != null)
          //     if (parameters[1].ToString() != string.Empty)
          //         sql.Append(" AND Rtrim(desc) LIKE '" + parameters[1].ToString().Trim() + "%'");


          // if (parameters[2] != null)
          //     if (parameters[2].ToString() != string.Empty)
          //         sql.Append(" AND Rtrim(dflt_cat_code) LIKE '" + parameters[2].ToString().Trim() + "%'");

          // if (parameters[3] != null)
          //     if (parameters[3].ToString() != string.Empty)
          //         sql.Append(" AND Rtrim(dflt_scale_code) LIKE '" + parameters[3].ToString().Trim() + "%'");

      
         
           return sql.ToString();
       }

   }

   
}
