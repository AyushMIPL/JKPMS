using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public class DVOQuarterlyReport : DVOBase
  {
    #region Private Variables
    #endregion

    #region Constructure
    public DVOQuarterlyReport()
    {

    }
    #endregion

    #region Properties
    #endregion

    #region Store Procedures
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

    public override string FIND_QUERY(ref object[] parameters)
    {
      return "";
    }
    public string GET_QTR941RPT
    {
      get { return "usppyqtr491rptget"; }
    }
    public string GET_EMP
    {
      get { return "usppyempget"; }
    }
    #endregion
  }
  public class ArearModel
  {
    public string ApplicationReferenceNo { get; set; }
    public string ApplicantName { get; set; }
    public string Jan { get; set; }
    public string Feb { get; set; }
    public string Mar { get; set; }
    public string Apr { get; set; }
    public string May { get; set; }
    public string Jun { get; set; }
    public string Jul { get; set; }
    public string Aug { get; set; }
    public string Sep { get; set; }
    public string Oct { get; set; }
    public string Nov { get; set; }
    public string Dec { get; set; }
    public string GrossAmount { get; set; }
  }
}
