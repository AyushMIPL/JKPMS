using System.ComponentModel.DataAnnotations.Schema;

namespace App.Web.Entities
{
  public class PaymentModel
  {
    public string AccountNo { get; set; }
    public string BankName { get; set; }
    public string IFSCCode { get; set; }
    public string PaidOn { get; set; }
    public string amount { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public string EmplCode { get; set; }

    [NotMapped]
    public string ApplicationReferenceNo { get; set; }
    [NotMapped]
    public string PresentAddress { get; set; }
    [NotMapped]
    public string BankCode { get; set; }
    [NotMapped]
    public string BranchName { get; set; }
    [NotMapped]
    public string Name { get; set; }

    [NotMapped]
    public string PayDocNo { get; set; }

  }
}