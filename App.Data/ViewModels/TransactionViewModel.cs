using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace App.Data.ViewModels
{
    /// <summary>
    /// Flat DTO for displaying joined txnHeader + txnDetail data in the DataTable
    /// </summary>
    public class TransactionDetailViewModel
    {
        public int DetailId { get; set; }
        public int HeaderId { get; set; }

        // Header fields
        public string SourceTable { get; set; }
        public DateTime? TxnDate { get; set; }
        public DateTime? ImportedOn { get; set; }

        // Detail fields
        public string ApplicationReferenceNo { get; set; }
        public string ApplicationReferenceNo1 { get; set; }
        public string Department { get; set; }
        public string DepartmentAccountNo { get; set; }
        public double? Amount { get; set; }
        public string DateText { get; set; }
        public string DepartmentBankName { get; set; }
        public string DepartmentBankIFSC { get; set; }
        public string Name { get; set; }
        public string IFSC { get; set; }
        public string AccountNo { get; set; }
        public string Scheme { get; set; }
        public string Status { get; set; }
        public string TransactionReference { get; set; }
        public string TransactionDate { get; set; }
        public string Remarks { get; set; }
    }

    /// <summary>
    /// ViewModel for the Transaction Index page (filter dropdowns)
    /// </summary>
    public class TransactionIndexViewModel
    {
        public string TxnDateFrom { get; set; }
        public string TxnDateTo { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string Scheme { get; set; }
        public string SearchAppRef { get; set; }
        public string SearchAccNo { get; set; }
        public string SearchTxnRef { get; set; }

        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        public List<SelectListItem> Schemes { get; set; }
    }
}
