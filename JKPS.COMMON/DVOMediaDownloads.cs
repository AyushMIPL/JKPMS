using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace JKPS.COMMON
///<Development and modification Details>
//Update By Bandra
{
  public class DVOMediaDownloads : DVOBase
  {
    private int _Id;
    private string _FileName;
    private bool _HasDownoaded;
    private DateTime _CreatedOn;
    private bool _IsProcessed;
    private string _MediaType;
    private int _CreatedBy;
    private string _CreatedMachineInfo;
    private string _ModifiedMachineInfo;
    private int _ModifiedBy;
    private DateTime _ModifiedOn;
    private bool _IsActive;
    private DateTime _dateFrom = Convert.ToDateTime("01/01/1900");
    private DateTime _dateto = Convert.ToDateTime("01/01/1900");

    #region Constructor

    public DVOMediaDownloads()
    {
      _Id = 0;
      _FileName = string.Empty;
      _MediaType = string.Empty;
      _HasDownoaded = false;
      _IsProcessed = false;
      _CreatedBy = 0;
      _CreatedOn = Convert.ToDateTime("01/01/1900");
      _CreatedMachineInfo = string.Empty;
      _ModifiedBy = 0;
      _ModifiedOn = Convert.ToDateTime("01/01/1900");
      _ModifiedMachineInfo = string.Empty;
      _IsActive = false;
    }

    #endregion

    #region Public Properties

    public int Id
    {
      get { return _Id; }
      set { _Id = value; }
    }
    public string FileName
    {
      get { return _FileName; }
      set { _FileName = value; }
    }

    public string MediaType
    {
      get { return _MediaType; }
      set { _MediaType = value; }
    }

    public bool HasDownoaded
    {
      get { return _HasDownoaded; }
      set { _HasDownoaded = value; }
    }
    public bool IsProcessed
    {
      get { return _IsProcessed; }
      set { _IsProcessed = value; }
    }

    public DateTime CreatedOn
    {
      get { return _CreatedOn; }
      set { _CreatedOn = value; }

    }
    public string CreatedMachineInfo
    {
      get { return _CreatedMachineInfo; }
      set { _CreatedMachineInfo = value; }
    }

    public int CreatedBy
    {
      get { return _CreatedBy; }
      set { _CreatedBy = value; }
    }

    public int ModifiedBy
    {
      get { return _ModifiedBy; }
      set { _ModifiedBy = value; }
    }
    public DateTime ModifiedOn
    {
      get { return _ModifiedOn; }
      set { _ModifiedOn = value; }
    }
    public string ModifiedMachineInfo
    {
      get { return _ModifiedMachineInfo; }
      set { _ModifiedMachineInfo = value; }
    }
    public bool IsActive
    {
      get { return _IsActive; }
      set { _IsActive = value; }
    }
    public DateTime dateto
    {
      get { return _dateto; }
      set { _dateto = value; }
    }
    public DateTime dateFrom
    {
      get { return _dateFrom; }
      set { _dateFrom = value; }
    }
    #endregion Properties

    #region Stored-Procedures

    public string AUTHENTICATION_SPNAME
    {
      get { return "uspsecauthenticate"; }
    }

    public override string INSERT_SPNAME
    {
      get { return "USP_MediaDownloadsIns"; }
    }

    public override string UPDATE_SPNAME
    {
      get { return ""; }
    }

    public override string DELETE_SPNAME
    {
      get { return "USP_MediaDownloadsdel"; }
    }

    public override string FIND_SPNAME
    {
      get { return "USP_MediaDownloadsGet"; }
    }

    public override string ALL_SPNAME
    {
      get { return ""; }
    }
    public override string TABLE_NAME
    {
      get { return "MediaDownloads"; }
    }

    public override int UNIQUE_ID
    {
      get { return Id; }
    }

    public override string NOTES_TABLE_RECORD_ID//
    {
      get { return string.Empty; }
      set { throw new Exception("The method or operation is not implemented."); }
    }

    public override string FIND_QUERY(ref Object[] parameters)
    {
      System.Text.StringBuilder sql = new StringBuilder();
      sql.Append("select Id,FileName,HasDownoaded,IsProcessed,CreatedMachineInfo,CreatedBy,");
      sql.Append("CreatedOn,ModifiedMachineInfo,ModifiedBy,ModifiedOn,IsActive,MediaType");
      sql.Append(" from MediaDownloads ");
      sql.Append(" where 1=1 ");

      if (parameters[0] != null)
        if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null && Convert.ToDateTime(parameters[0]) != Convert.ToDateTime("01/01/1900"))//dateFrom
          sql.Append(" AND CreatedOn >= '" + Convert.ToDateTime(parameters[0]).ToShortDateString() + "'");
      if (parameters[1] != null)
        if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null && Convert.ToDateTime(parameters[1]) != Convert.ToDateTime("01/01/1900"))//dateTo
          sql.Append(" AND CreatedOn <= '" + Convert.ToDateTime(parameters[1]).ToShortDateString() + "'");
      sql.Append(" ORDER BY  Id,CreatedOn ");
      return sql.ToString();
    }

    #endregion Stored-Procedures
  }

}
