using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
  public enum Gender
  {
    Male = 1,
    Female = 2,
    Other = 3
  }

  public enum BookingStatus
  {
    NotAssigned = 0,
    Assigned = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4,
  }

  public enum EmployerType
  {
    FullTime = 1,
    PartTime = 2,
    Unemployed = 3
  }

  public enum JobType
  {
    FullTime = 1,
    PartTime = 2,
    UnEmployed = 3
  }

  //public enum JobStatus
  //{
  //  Retirement = 1,
  //  Resignation = 2,
  //  Termination = 3
  //}

  public enum BenefitType
  {
    Pension = 1,
    GratuityReducedPension = 2,
    DiscountedGratuityReducedPension = 3
  }
  public enum PensionerType
  {
    Member = 1,
    Spouse = 2,
    Child = 3
  }

  //public enum dir_dept
  //{
  //  Yes = 'Y',
  //  No = 'N'
  //}

  public enum typeofacct
  {
    //C = 1,
    //S = 2
    Current = 'C',
    Saving = 'S'
  }
  //public enum type
  //{
  //  P = 1,
  //  A = 2
  //}
  public enum applicationType
  {
    Pension = 1,
    Refund = 2
  }

  public enum BankAmountType
  {
    Percentage = 'P',
    Amount = 'A',
    Remainder = 'R'
  }

  public enum PensionType
  {
    Police = 1,
    Public = 2

  }
  public enum jobStatus
  {
    ActiveContributor = 1,
    DeathInService = 2,
    Pensioner = 3,
    Resignee = 4,
    Supernumerary = 5,
    TreasuryUnknowns = 6,
    Unknown = 7
  }


  public enum DownloadNUploadFile
  {
    DownloadVerification = 1,
    UploadVerification = 2,
    DownloadbaknkMedia = 3,
    UploadBankMedia = 4,
  }

  public enum PaymentStatus
  {
    Resume = 'R',
    HoldPayment = 'H',
    StopPayment = 'S',
    OkPayment = 'Y',
  }



  //public enum dedfrequency
  //{
  //  Weekly = 'W',
  //  Monthly = 'M'
  //} 
}
