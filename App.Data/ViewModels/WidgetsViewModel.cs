using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.ViewModels
{
  public class WidgetsViewModel
  {
    public bool Total_Registered ;
    public bool Total_Approved ;
    public bool Total_Unapproved;
    public bool Beneficiary_Paid;
    public bool RegisteredBeneficiary_DistrictWise;
    public bool Beneficiaries_PaymentSummary;
  }
   
}
