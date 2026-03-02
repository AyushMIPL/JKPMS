using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class PayrollSearchControl
  {
    #region Properties

    private int _AccountNumber = 0;
    /// <summary>
    /// Get Employee Code enterd by user
    /// </summary>
    public string EmployeeCode
    {
      get;
      set;
    }

    /// <summary>
    /// property to get or set First Name Entered by user
    /// </summary>
    public string FirstName
    {
      get;
      set;
    }

    //private string _AccountDescription = string.Empty;
    /// <summary>
    /// property to get Last Name  Entered by user
    /// </summary>
    public string LastName
    {
      get;
      set;
    }
    /// <summary>
    /// property to get Employee Type  Entered by user
    /// </summary>
    public string EmpType
    {
      get;
      set;
    }
    public string TxtEmpl_type
    {
      get;
      set;

    }
    /// <summary>
    /// property to get Job Code Type  Entered by user
    /// </summary>
    public string JobCode
    {
      get;
      set;
    }
    /// <summary>
    /// property to get Pay Period Entered by user
    /// </summary>
    public string PayPeriod
    {
      get;
      set;
    }
    /// <summary>
    /// property to get Title Entered by user
    /// </summary>
    public string Title
    {
      get;
      set;
    }
    /// <summary>
    /// property to get Last Pay Date Entered by user
    /// </summary>
    public DateTime LPayDate
    {
      get;
      set;
    }
    /// <summary>
    /// property to get Title Entered by user
    /// </summary>
    public string FullTime
    {
      get;
      set;
    }
    public bool LPayDateChecked
    {
      get;
      set;
    }

    public DateTime EOPDate
    {
      get;
      set;
    }
    public bool EOPDateChecked
    {
      get;
      set;
    }

    public DateTime PayrollDate
    {
      get;
      set;
    }
    public bool PayrollDateChecked
    {
      get;
      set;
    }

    #endregion Properties
  }
}
