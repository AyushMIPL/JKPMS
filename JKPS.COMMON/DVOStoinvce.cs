using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa[25/07/2009] 
   public  class DVOStoinvce:DVOBase
    {

            private int _doc_no;
            private string  _order_no;
            private string  _bill_to_code;
            private string  _sell_to_code;
            private string  _ship_to_code;
            private int     _inv_doc_no;
            private string  _stage;
            private string  _inv_no;
            private string  _inv_date ;
            private string  _inv_printed;
            private string  _ok_to_post;
            private string  _terms_code;
            private string  _terms_approval;
            private string  _pay_method ;
            private string  _payment;
            private string  _card_no;
            private string  _exp_date;
            private string  _check_no;
            private string  _fob_point;
            private string  _ship_via;
            private decimal  _ship_weight;
            private string  _freight_doc;
            private string  _st_tx_code;
            private string  _co_tx_code;
            private string  _ci_tx_code;
            private decimal  _st_tx_rate;
            private decimal  _co_tx_rate;
            private decimal  _ci_tx_rate ;
            private decimal  _tax_rate;
            private decimal  _trd_ds_rate;
            private decimal  _item_amount;
            private decimal  _discountable;

            private decimal  _trd_ds_amount;
            private decimal  _taxable;
            private decimal  _st_tx_amount;
            private decimal  _co_tx_amount;
            private decimal  _ci_tx_amount;
            private decimal  _frght_amount;
            private decimal  _total_amount;
            private int  _td_ds_acct;
            private int  _st_tx_acct;
            private int  _co_tx_acct;
            private int  _ci_tx_acct;
            private int  _freight_acct;
            private int _asset_acct;
            private string _td_ds_dept;
            private string _t_tx_dept;
            private string _co_tx_dept;
            private string _ci_tx_dept;
            private string _freight_dept;
            private string _asset_dept;
            private string _mtaxg_code;
            private decimal _tax_amount;
            private string _currency_code;
            private string _curr_rate_type;
            private decimal _currency_rate;
            private int _batch_id ;
            private string _reprint;
            private string _cust_code;

     public DVOStoinvce()
     {
            _doc_no=0;
            _order_no=string.Empty;
            _bill_to_code=string.Empty;
            _sell_to_code=string.Empty;
            _ship_to_code=string.Empty;
            _inv_doc_no=0;
            _stage=string.Empty;
            _inv_no=string.Empty;
            _inv_date = "01/01/1900";
             _inv_printed=string.Empty;
             _ok_to_post=string.Empty;
             _terms_code=string.Empty;
             _terms_approval=string.Empty;
            _pay_method=string.Empty;
             _payment=string.Empty;
             _card_no=string.Empty;
             _exp_date=string.Empty;
            _check_no=string.Empty;
            _fob_point=string.Empty;
            _ship_via=string.Empty;
            _ship_weight=0;
            _freight_doc=string.Empty;
             _st_tx_code=string.Empty;
            _co_tx_code=string.Empty;
            _ci_tx_code=string.Empty;
            _st_tx_rate=0;
            _co_tx_rate=0;
             _ci_tx_rate=0 ;
             _tax_rate=0;
            _trd_ds_rate=0;
            _item_amount=0;
            _discountable=0;
            _trd_ds_amount=0;
            _taxable=0;
            _st_tx_amount=0;
            _co_tx_amount=0;
            _ci_tx_amount=0;
            _frght_amount=0;
            _total_amount=0;
            _td_ds_acct=0;
            _st_tx_acct=0;
            _co_tx_acct=0;
           _ci_tx_acct=0;
            _freight_acct=0;
            _asset_acct=0;
            _td_ds_dept=string.Empty;
           _t_tx_dept=string.Empty;
           _co_tx_dept=string.Empty;
           _ci_tx_dept=string.Empty;
           _freight_dept=string.Empty;
           _asset_dept=string.Empty;
            _mtaxg_code=string.Empty;
           _tax_amount=0;
            _currency_code=string.Empty;
           _curr_rate_type=string.Empty;
           _currency_rate=0;
            _batch_id =0;
            _reprint = string.Empty;
            _cust_code = string.Empty;

        }

        #region public properties

       
     

     public int doc_no
     {
         get {return _doc_no; }
         set { _doc_no = value; }
     }
     public string order_no
     {
         get { return _order_no; }
         set { _order_no = value; }
     }
     public string bill_to_code
     {
         get { return _bill_to_code; }
         set { _bill_to_code = value; }

     }
     public string sell_to_code
     {
         get { return _sell_to_code; }
         set { _sell_to_code = value; }
     }
     public string ship_to_code
     {
         get { return _ship_to_code; }
         set { _ship_to_code = value; }
     }

    

     public int inv_doc_no
     {
         get { return _inv_doc_no; }
         set { _inv_doc_no = value; }
     }
     public string stage
     {
          get { return _stage; }
         set { _stage = value; }
     }
      public string inv_no
     {
           get { return _inv_no; }
         set { _inv_no = value; }
     }
      public string inv_date
     {
          get { return _inv_date; }
         set { _inv_date = value; }

     }
      public string inv_printed
     {
           get { return _inv_printed; }
         set { _inv_printed = value; }
     }
      public string ok_to_post
     {
           get { return _ok_to_post; }
         set {  _ok_to_post= value; }
     }


      public string terms_code
     {
         get { return _terms_code; }
         set { _terms_code = value; }
     }
      public string terms_approval
     {
           get { return _terms_approval; }
         set { _terms_approval = value; }
     }
      public string pay_method
      {
           get { return _pay_method; }
         set { _pay_method = value; }
      }
     public string payment
      {
          get { return _payment; }
         set { _payment = value; }
      }




     public string card_no
      {
          get { return _card_no; }
         set { _card_no = value; }
      }
     public string exp_date
      {
          get { return _exp_date; }
         set { _exp_date = value; }
      }
     public string check_no
      {
          get { return _check_no; }
         set { _check_no = value; }
      }


   
     public string fob_point
      {
         get { return _fob_point; }
          set { _fob_point = value; }
         
      }
     public string ship_via
      {
          get { return _ship_via; }
          set { _ship_via = value; }
          
      }

     public decimal  ship_weight
     {
          get { return _ship_weight; }
          set { _ship_weight = value; }

     }
      //public string ship_via
      //{

      //}
      public string freight_doc
      {
           get { return _freight_doc; }
          set { _freight_doc = value; }
          
      }
      public string st_tx_code
      {
          get { return _st_tx_code; }
          set { _st_tx_code = value; }
      }
      public string co_tx_code
      {
          get { return _co_tx_code; }
          set { _co_tx_code = value; }
      }

      public string ci_tx_code
      {
           get { return _ci_tx_code; }
          set { _ci_tx_code = value; }
      }



     
       public decimal  st_tx_rate
     {
         get { return _st_tx_rate; }
         set { _st_tx_rate = value; }
     }
       public decimal  co_tx_rate
     {
         get { return _co_tx_rate; }
         set { _co_tx_rate = value; }
     }
       public decimal  ci_tx_rate
     {
         get { return _ci_tx_rate; }
         set { _ci_tx_rate = value; }
     }
       public decimal  tax_rate
     {
         get { return _tax_rate; }
         set { _tax_rate = value; }
     }





       public decimal  trd_ds_rate
     {
         get { return _trd_ds_rate; }
         set { _trd_ds_rate = value; }
     }
       public decimal  item_amount
     {
         get { return _item_amount; }
         set { _item_amount = value; }
     }
       public decimal  discountable
     {
         get { return _discountable; }
         set { _discountable = value; }
     }


     


       public decimal  trd_ds_amount
     {
         get { return _trd_ds_amount; }
         set { _trd_ds_amount = value; }
     }
     

       public decimal  taxable
     {
         get { return _taxable; }
         set { _taxable = value; }
     }
       public decimal  st_tx_amount
     {
         get { return _st_tx_amount; }
         set { _st_tx_amount = value; }
     }
       public decimal  co_tx_amount
     {
         get { return _co_tx_amount; }
         set { _co_tx_amount = value; }
     }
       public decimal  ci_tx_amount
     {
         get { return _ci_tx_amount; }
         set { _ci_tx_amount = value; }
     }

       public decimal  frght_amount
     {
         get { return _frght_amount; }
         set { _frght_amount = value; }
     }
       public decimal  total_amount
     {
         get { return _total_amount; }
         set { _total_amount = value; }
     }

       public int td_ds_acct
     {
         get { return _td_ds_acct; }
         set { _td_ds_acct = value; }
     }
       public int st_tx_acct
     {
         get { return _st_tx_acct; }
         set { _st_tx_acct = value; }
     }
     

       public int co_tx_acct
     {
         get { return _co_tx_acct; }
         set { _co_tx_acct = value; }
     }
       public int ci_tx_acct
     {
         get { return _ci_tx_acct; }
         set { _ci_tx_acct = value; }
     }
       public int freight_acct
     {
         get { return _freight_acct; }
         set { _freight_acct = value; }
     }
       public int asset_acct
     {
         get { return _asset_acct; }
         set { _asset_acct = value; }
     }

   
     public string td_ds_dept
      {
          get { return _td_ds_dept; }
          set { _td_ds_dept = value; }
      }
      public string t_tx_dept
      {
          get { return _t_tx_dept; }
          set { _t_tx_dept = value; }
      }
      public string co_tx_dept
      {
          get { return _co_tx_dept; }
          set { _co_tx_dept = value; }
      }
      public string ci_tx_dept
      {
          get { return _ci_tx_dept; }
          set { _ci_tx_dept = value; }
      }

     
     public string freight_dept
      {
          get { return _freight_dept; }
          set { _freight_dept = value; }
      }
      public string asset_dept
      {
          get { return _asset_dept; }
          set { _asset_dept = value; }
      }
      public string mtaxg_code
      {
          get { return _mtaxg_code; }
          set { _mtaxg_code = value; }
      } 
          
      public decimal  tax_amount
      {
          get { return _tax_amount; }
          set { _tax_amount = value; }
      } 

      public string currency_code
      {
          get { return _currency_code; }
          set { _currency_code = value; }
      }
      public string curr_rate_type
      {
          get { return _curr_rate_type; }
          set { _curr_rate_type = value; }
      } 
      public decimal  currency_rate
      {
          get { return _currency_rate; }
          set { _currency_rate = value; }
      } 
      public int  batch_id
      {
           get { return _batch_id; }
          set { _batch_id = value; }
      }


       public string reprint
       {
           get { return _reprint; }
           set { _reprint = value; }
       }


       public string cust_code
       {
           get { return _cust_code; }
           set { _cust_code = value; }
       }
         
         
         


   
           
     
         
            
          


        #endregion properties.............

        #region stored Procedure

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
          get { return "stoinvce"; }
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
       //Added by Sunil Pahwa for Order Entry Edit 
       public  string UPD_ORDER_ENTRY_EDIT_LIST_INFO
       {
           get { return "uspordentystvceupd"; }
       }

       public  string UPD_ORDER_ENTRY_EDIT_LIST_INF_INVCE
       {
           get { return "uspordentysnvceupd"; }
       }
       //*************************************************
      //Added by Sunil Pahwa on [25/07/2009] for getting invoices and memos info
      public override string FIND_QUERY(ref object[] parameters)
      {
          StringBuilder sql = new StringBuilder();
          if (Convert.ToString(parameters[3]) == string.Empty)
          {
             
              sql.Append(" Select stoinvce.bill_to_code, stoinvce.currency_code, stoinvce.doc_no, ");
              sql.Append(" stoinvce.freight_doc, stoinvce.frght_amount,stoinvce.inv_date, ");
              sql.Append(" stoinvce.inv_doc_no, stoinvce.inv_no, stoinvce.item_amount, stoinvce.order_no, ");
              sql.Append(" stoinvce.pay_method,stoinvce.payment, stoinvce.sell_to_code,stoinvce.ship_to_code, ");
              sql.Append(" stoinvce.ship_via, stoinvce.tax_amount, stoinvce.terms_code, stoinvce.total_amount, ");
              sql.Append(" stoinvce.trd_ds_amount, stoshipd.item_code,stoshipd.line_no, stoshipd.net_amount, ");
              sql.Append(" stoshipd.price,stoshipd.ship_qty, stoshipd.stage,stoordre.cust_code, stoinvce.rowid as stoinvce_rowid ");
              sql.Append(" from  stoinvce, stoshipd,stoordre where stoinvce.doc_no = stoshipd.doc_no and ");
              sql.Append(" stoinvce.bill_to_code = stoshipd.bill_to_code and ");
              sql.Append(" stoinvce.sell_to_code = stoshipd.sell_to_code and ");
              sql.Append(" stoinvce.ship_to_code = stoshipd.ship_to_code and stoinvce.order_no=stoordre.order_no and ");
              sql.Append(" (stoinvce.inv_doc_no = stoshipd.inv_doc_no or ");
              sql.Append("  stoshipd.inv_doc_no is null) ");
              sql.Append("  and stoshipd.stage != 'CAN'");
          }
          else
          {
             
              sql.Append(" Select stoinvce.bill_to_code, stoinvce.currency_code, stoinvce.doc_no, ");
              sql.Append(" stoinvce.freight_doc, stoinvce.frght_amount,stoinvce.inv_date, ");
              sql.Append(" stoinvce.inv_doc_no, stoinvce.inv_no, stoinvce.item_amount, stoinvce.order_no, ");
              sql.Append(" stoinvce.pay_method,stoinvce.payment, stoinvce.sell_to_code,stoinvce.ship_to_code, ");
              sql.Append(" stoinvce.ship_via, stoinvce.tax_amount, stoinvce.terms_code, stoinvce.total_amount, ");
              sql.Append(" stoinvce.trd_ds_amount, stoshipd.item_code,stoshipd.line_no, stoshipd.net_amount, ");
              sql.Append(" stoshipd.price,stoshipd.ship_qty, stoshipd.stage, stoinvce.rowid as stoinvce_rowid ");
              sql.Append(" from  stoinvce, stoshipd,stoordre where stoinvce.doc_no = stoshipd.doc_no and ");
              sql.Append(" stoinvce.bill_to_code = stoshipd.bill_to_code and ");
              sql.Append(" stoinvce.sell_to_code = stoshipd.sell_to_code and ");
              sql.Append(" stoinvce.ship_to_code = stoshipd.ship_to_code and stoinvce.order_no=stoordre.order_no and ");
              sql.Append(" (stoinvce.inv_doc_no = stoshipd.inv_doc_no or ");
              sql.Append("  stoshipd.inv_doc_no is null) ");
              sql.Append("  and stoshipd.stage != 'CAN' and stoshipd.stage != 'PST' ");
          }
          if (parameters[0] != null)
              if (parameters[0].ToString() != string.Empty)
                  sql.Append(" AND Rtrim(stoordre.cust_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

          if (!parameters[1].ToString().Contains("1900"))
              sql.Append(" AND  stoinvce.inv_date=" + "'" + parameters[1].ToString().Replace("'", "''") + "'");

          if (parameters[2] != null)
              if (parameters[2].ToString() != string.Empty)
                  sql.Append(" AND Rtrim(stoinvce.order_no) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

          if (parameters[3] != null)
              if (parameters[3].ToString() != string.Empty)
                  sql.Append(" AND Rtrim(stoinvce.inv_no) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

         
          //if (parameters[0].ToString() != string.Empty && Convert.ToDateTime(parameters[0].ToString()) != Convert.ToDateTime("01/01/1900"))
          //if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//
          //sql.Append(" and stoordre.order_date >='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");


        

          if (Convert.ToInt32(parameters[4]) > 0)
              sql.Append(" AND stoinvce.inv_doc_no = " + parameters[4].ToString());

          //if (parameters[3] != null)
          //    if (parameters[3].ToString() != string.Empty)
          sql.Append(" AND  stoinvce.inv_printed ='" + parameters[5].ToString().Trim()+"'");

          sql.Append(" order by stoinvce.doc_no, stoinvce.inv_doc_no, stoinvce.bill_to_code, stoinvce.sell_to_code, stoinvce.ship_to_code, ");
          sql.Append(" stoshipd.line_no, stoshipd.price ");

          return sql.ToString();




      }

      #endregion stored Procedures






     
    


  }
}