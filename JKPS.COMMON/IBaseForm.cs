using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public interface IBaseForm
    {
        string PermissionsString
        {
            get;
            set;
        }
    }
    public interface IBatchImplement
    {
        int BatchID
        {
            set;
            get;
        }
        //****************************Added by Bharat Dhall **************************
        string BatchType
        {
            get;
        }
        string CurrentUser
        {
            set;
            get;
        }
        bool AllBatches
        {
            get;
            set;
        }
        int ActiveBatchID
        {
            set;
            get;
        }
        //****************************************************************************
    }

    //****************************Added by Bharat Dhall **************************
    public class BatchTypes
    {
        public const string AccountPayable = "AP";
        public const string AccountReceivable = "AR";
        public const string C2 = "C2";
        public const string APCashDeposit = "CD";
        public const string ARCashReceive = "CR";
        public const string GeneralJournal = "GJ";
        public const string GeneralLedger = "GL";
        public const string PQ = "PQ";
        public const string PurchaseOrder = "PR";
        public const string PU = "PU";
        public const string OrderEntryManagement = "OE";
        public const string NSSCashTransaction = "NC";
        public const string NSSTransferVoucher = "NT";
        public const string SBCashTransaction = "SC";
        public const string SBCashLessTransaction = "SL";
        public const string TreasuryBillReceipt = "TR";
    }
    //*****************************************************************************


    //****************************Added by Bharat Dhall [08/20/2009] **************************
    public class JKPSModules
    {
        public const string AccountPayable = "AP";
        public const string AccountReceivable = "AR";
        public const string APCheckPrinting = "CP";
        public const string Payroll = "PP";
        //public const string C2 = "C2";
        //public const string APCashDeposit = "CD";
        //public const string ARCashReceive = "CR";
        //public const string GeneralJournal = "GJ";
        public const string GeneralLedger = "GL";
        //public const string PQ = "PQ";
        //public const string PurchaseOrder = "PR";
        //public const string PU = "PU";
        //public const string OrderEntryManagement = "OE";
        //public const string NSSCashTransaction = "NC";
        //public const string NSSTransferVoucher = "NT";
        //public const string SBCashTransaction = "SC";
        //public const string SBCashLessTransaction = "SL";
        public const string TreasuryBill = "TB";
        public const string TreasuryBillReceives = "TR";
        public const string TreasuryBillPayments = "TP";
    }
    //*****************************************************************************

    //****************************Added by Bharat Dhall [02/02/2010] **************************
    /// <summary>
    /// Interface to implement Notes functionality in JKPS Screens through JKPSBasicForm
    /// </summary>
    public interface INotesImplement
    {
        /// <summary>
        /// Name of DVO for which you want to make notes and that should be implements iDVO interface,
        /// to implement proper 'NOTES' functionality TABLE_NAME & NOTES_TABLE_RECORD_ID must be set for this DVO.
        /// create an object of DVO for which you want to make notes and set its value.
        /// </summary>
        iDVO DVOName
        {
            set;
            get;
        }


        ///// <summary>
        ///// TableName, for which record you want to make notes.
        ///// e.g. if you want to make notes for employees set it to 'MasterEmployee'.
        ///// create a string object Notes_TableName and set its value.
        ///// </summary>
        //string TableName
        //{
        //    set;
        //    get;
        //}
        ///// <summary>
        ///// Unique key in table for which you want to make notes.
        ///// e.g. if you want to make notes for employees set it to 'EMP0001'(employee code).
        ///// create a string object Notes_TableRecordId to hold its value.
        ///// </summary>
        //string TableRecordId
        //{
        //    set;
        //    get;
        //}
        /// <summary>
        /// List of notes in list of DVOstxnoted objects.
        /// create a List<DVOstxnoted> type object to hold its value.
        /// </summary>
        List<DVOstxnoted> Notes
        {
            set;
            get;
        }
    }
    //****************************************************************************
}
