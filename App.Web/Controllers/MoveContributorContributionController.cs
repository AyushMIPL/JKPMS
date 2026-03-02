using App.Data;
using App.Data.Entities;
using App.Data.ViewModels;
using App.Web.Filters;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Data.Extentions;
using System.Data.Entity;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class MoveContributorContributionController : BaseController
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        //private AppDbContext db = new AppDbContext();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public MoveContributorContributionController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }
        //
        // GET: /MoveContributorContribution/
        public ActionResult Index()
        {
            var contributorlist = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Group = x.Employer.EmployerName, Id = x.Id, Name = x.FirstName + " " + x.LastName + " ( " + x.PersonID + " )" }).ToList();
            ViewBag.ContributorId = new SelectList(contributorlist, "Id", "Name", "Group", 0);
            List<MasterContributor> Contributor = new List<MasterContributor>();
            ViewBag.PersonID = new SelectList(Contributor, "Id", "FirstName");
            ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName");
            return View();
        }
        public ActionResult EditDetailSearchAjax(JQueryDataTableParamModel param, string empId, string conId, DateTime fromDate, DateTime to)
        {

            //List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
            List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
            using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
            {
                DataSet ds = new DataSet();
                System.Text.StringBuilder SQL = new System.Text.StringBuilder();
                SQL.Append("select dbo.ContributonSheetHeaderFinalise.ContributonSheetHeaderId as  contributorSheetHeaderId,dbo.ContributonSheetHeaderFinalise.Month, dbo.ContributonSheetHeaderFinalise.Year, dbo.MasterSource.Name AS SourceName, dbo.ContributonSheetDetailsFinalise.SalaryAmount, dbo.ContributonSheetDetailsFinalise.ContributorContribution, dbo.ContributonSheetDetailsFinalise.EmployerContribution , dbo.ContributonSheetDetailsFinalise.Id AS Id, dbo.MasterEmployer.EmployerName, PersonId=dbo.MasterContributor.PersonID,");
                SQL.Append("(isnull(dbo.MasterContributor.FirstName, '') + ' ' + isnull(dbo.MasterContributor.MidName, '') + ' ' + isnull(dbo.MasterContributor.LastName, '')) as ContributorName");
                SQL.Append(",dbo.MasterMonthName.Id As MonthNumber,SystemSalaryAmount = (dbo.MasterContributor.SalaryAmount / 12), SystemContributorContribution = (dbo.MasterContributor.SalaryAmount / 1200) * dbo.MasterContributor.PFRate, SystemEmployerContribution = (dbo.MasterContributor.SalaryAmount / 1200) * dbo.MasterEmployer.PFRate FROM            dbo.ContributonSheetDetailsFinalise INNER JOIN                         dbo.ContributonSheetHeaderFinalise ON dbo.ContributonSheetDetailsFinalise.ContributonSheetHeaderFinaliseID = dbo.ContributonSheetHeaderFinalise.Id   INNER JOIN dbo.MasterSource ON dbo.ContributonSheetDetailsFinalise.SourceID =  dbo.MasterSource.Id INNER JOIN                         dbo.MasterMonthName ON dbo.ContributonSheetHeaderFinalise.Month = dbo.MasterMonthName.Name INNER JOIN                         dbo.MasterContributor ON dbo.ContributonSheetDetailsFinalise.ContributorID = dbo.MasterContributor.Id INNER JOIN                         dbo.MasterEmployer ON dbo.ContributonSheetHeaderFinalise.EmployerId = dbo.MasterEmployer.Id WHERE (convert(datetime, DATEFROMPARTS(dbo.ContributonSheetHeaderFinalise.year, dbo.MasterMonthName.Id, 1)) >= '" + fromDate + "')  AND(convert(datetime, DATEFROMPARTS(dbo.ContributonSheetHeaderFinalise.year, dbo.MasterMonthName.Id, 1)) <= '" + to + "') and (dbo.ContributonSheetHeaderFinalise.IsActive = 1) AND (dbo.ContributonSheetDetailsFinalise.IsActive = 1)");
                //if (!string.IsNullOrWhiteSpace(empId))
                //    SQL.Append("and dbo.ContributonSheetHeaderFinalise.EmployerId=" + empId);
                if (!string.IsNullOrWhiteSpace(conId))
                    SQL.Append("and dbo.ContributonSheetDetailsFinalise.ContributorID=" + conId);
                SQL.Append(" ORDER BY dbo.ContributonSheetHeaderFinalise.Year DESC, MonthNumber DESC");
                //string con = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
                string con = ConnectionStringProvider.GetConnectionString();
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(SQL.ToString(), con);
                da.Fill(ds);
                tempVerify = App.Web.Repository.ListToDataset.ToList<ExcelFileViewModel>(ds.Tables[0]);
            }

            string employerCont = tempVerify.Any() ? "EC$ " + (tempVerify.Sum(x => x.EmployerContribution)) + "" : "";
            string contributorCont = tempVerify.Any() ? "EC$ " + (tempVerify.Sum(x => x.ContributorContribution)) + "" : "";
            string SystemEmployerTotalContribution = "EC$ " + Convert.ToDecimal(tempVerify.Sum(x => x.SystemEmployerContribution)).ToString("#,##0.00");
            string SystemContributorTotalContribution = "EC$ " + Convert.ToDecimal(tempVerify.Sum(x => x.SystemContributorContribution)).ToString("#,##0.00");
            IEnumerable<ExcelFileViewModel> filtered;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = tempVerify.Where(c => c.PersonId.ToLower().Contains(param.sSearch.ToLower())
                                 || c.SalaryAmount.ToString().ToLower().Contains(param.sSearch.ToLower())
                                 || c.ContributorContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                                 || c.EmployerContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                                 || (c.ContributorName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                                 || (c.EmployerName).ToLower().Contains(param.sSearch.ToLower())
                                 );

            }
            else
            {
                filtered = tempVerify;
            }

            //Pagging
            var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            ////Select required columns
            var result = from c in displayed
                         select new[] {
                             c.Id+"#"+c.contributorSheetHeaderId,
                         c.PersonId,
                         c.ContributorName,
                         c.SourceName,
                         Convert.ToDecimal(c.SystemSalaryAmount).ToString("#,##0.00"),
                        Convert.ToDecimal(c.SystemContributorContribution).ToString("#,##0.00"),
                        Convert.ToDecimal(c.SystemEmployerContribution).ToString("#,##0.00"),
                         Convert.ToDecimal(c.SalaryAmount).ToString("#,##0.00"),
                     Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00"),
                     Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00"),
                     c.Month+" - "+c.Year,
                     employerCont,
                     contributorCont,
                     SystemEmployerTotalContribution,
                     SystemContributorTotalContribution
                   };

            return Json(
                                        new
                                        {
                                            sEcho = param.sEcho,
                                            iTotalRecords = tempVerify.Count(),
                                            iTotalDisplayRecords = filtered.Count(),
                                            aaData = result
                                        },
          JsonRequestBehavior.AllowGet);
        }
        public JsonResult MoveContributorAjax(string[] Data, int conId, int oldconId, int oldEmpId)
        {
            MoveContributorContributionViewModel list = new MoveContributorContributionViewModel();
            List<OldAndNewContribution> RecordWithDiffernetContribution = new List<OldAndNewContribution>();
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    int NewEmployerId = Convert.ToInt32(db.MasterContributor.Where(x => x.Id == conId).Select(x => x.EmployerID).FirstOrDefault());
                    var header = db.ContributonSheetHeader.Where(x => x.IsActive == true).ToList();
                    var headerFinalize = db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true).ToList();
                    var detailsFinalize = db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
                    var details = db.ContributonSheetDetails.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
                    int result = 0;
                    for (int i = 0; i < Data.Length; i++)
                    {
                        var data = Data[i].Split('#');
                        int oldheaderId = Convert.ToInt32(data[1]);
                        int olddetailFinalizedId = Convert.ToInt32(data[0]);
                        var oldfinalizeRecord = detailsFinalize.Where(x => x.Id == olddetailFinalizedId).FirstOrDefault();//Get Slected Record
                        #region Move to same employer
                        if (oldfinalizeRecord.ContributonSheetHeaderFinalise.EmployerId == NewEmployerId)
                        {
                            //Check Contribution exist with same source
                            #region When Contribution Exist
                            if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == oldfinalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == oldfinalizeRecord.ContributonSheetHeaderFinaliseID))
                            {
                                //Check Contribution exist with same source and contribution
                                if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == oldfinalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == oldfinalizeRecord.ContributonSheetHeaderFinaliseID && x.SalaryAmount == oldfinalizeRecord.SalaryAmount && x.ContributorContribution == oldfinalizeRecord.ContributorContribution && x.EmployerContribution == oldfinalizeRecord.EmployerContribution))
                                {
                                    #region maintain history when contribution is same & Deactive Previous Record
                                    //Maintain History
                                    ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                    oldfinalizeRecord.MapTo(finaliseHistory);
                                    finaliseHistory.ReplaceContributorID = conId;
                                    finaliseHistory.DetailFinalizeID = oldfinalizeRecord.Id;
                                    finaliseHistory.ReplaceHeaderFinalizeID = oldfinalizeRecord.ContributonSheetHeaderFinaliseID;
                                    db.Entry(finaliseHistory).State = EntityState.Added;
                                    result = db.SaveChanges();
                                    if (result > 0)
                                    {
                                        //Deactive Record
                                        oldfinalizeRecord.IsActive = false;
                                        db.Entry(oldfinalizeRecord).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                    //Maintain History for unfinalized
                                    var oldUnfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == oldheaderId && x.ContributorID == oldconId && x.SourceID == oldfinalizeRecord.SourceID).FirstOrDefault();
                                    ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                    oldUnfinalizeRecord.MapTo(History);
                                    History.ReplaceContributorID = conId;
                                    History.DetailID = oldUnfinalizeRecord.Id;
                                    History.ReplaceHeaderID = oldUnfinalizeRecord.ContributonSheetHeaderID;
                                    db.Entry(History).State = EntityState.Added;
                                    result = db.SaveChanges();
                                    if (result > 0)
                                    {
                                        //Deactive Record
                                        oldUnfinalizeRecord.IsActive = false;
                                        db.Entry(oldUnfinalizeRecord).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region add Record in list
                                    var newFinalizedRecord = detailsFinalize.Where(x => x.ContributorID == conId && x.SourceID == oldfinalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == oldfinalizeRecord.ContributonSheetHeaderFinaliseID).FirstOrDefault();
                                    var oldUnFinalizedDetails = details.Where(x => x.ContributonSheetHeaderID == oldheaderId && x.ContributorID == oldconId && x.SourceID == oldfinalizeRecord.SourceID).FirstOrDefault();
                                    var newUnFinalizedDetails = details.Where(x => x.ContributonSheetHeaderID == oldheaderId && x.ContributorID == conId && x.SourceID == oldfinalizeRecord.SourceID).FirstOrDefault();
                                    OldAndNewContribution contribution = new OldAndNewContribution();
                                    contribution.Month = oldfinalizeRecord.ContributonSheetHeaderFinalise.Month;
                                    contribution.Year = oldfinalizeRecord.ContributonSheetHeaderFinalise.Year;
                                    contribution.SourceName = oldfinalizeRecord.Source.Name;
                                    //New Details on which record move
                                    contribution.NewContributorContribution = newFinalizedRecord.ContributorContribution;
                                    contribution.NewEmployerContribution = newFinalizedRecord.EmployerContribution;
                                    contribution.NewSalaryAmount = newFinalizedRecord.SalaryAmount;
                                    contribution.NewDetailId = newUnFinalizedDetails.Id;
                                    contribution.NewDetailFinalizedId = newFinalizedRecord.Id;
                                    contribution.NewContributorName = newFinalizedRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
                                    contribution.NewEmployerName = newFinalizedRecord.Contributor.Employer.EmployerName;

                                    //old Details whose record move
                                    contribution.OldContributorContribution = oldfinalizeRecord.ContributorContribution;
                                    contribution.OldEmployerContribution = oldfinalizeRecord.EmployerContribution;
                                    contribution.OldSalaryAmount = oldfinalizeRecord.SalaryAmount;
                                    contribution.OldDetailId = oldUnFinalizedDetails.Id;
                                    contribution.OldDetailFinalizedId = oldfinalizeRecord.Id;
                                    contribution.OldContributorName = oldfinalizeRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
                                    contribution.OldEmployerName = oldfinalizeRecord.Contributor.Employer.EmployerName;

                                    RecordWithDiffernetContribution.Add(contribution);
                                    #endregion
                                }
                            }
                            #endregion
                            #region When Contribution not Exist
                            else
                            {
                                #region record not exist for that contributor than update
                                ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                oldfinalizeRecord.MapTo(finaliseHistory);
                                finaliseHistory.ReplaceContributorID = conId;
                                finaliseHistory.DetailFinalizeID = oldfinalizeRecord.Id;
                                finaliseHistory.ReplaceHeaderFinalizeID = oldfinalizeRecord.ContributonSheetHeaderFinaliseID;
                                db.Entry(finaliseHistory).State = EntityState.Added;
                                result = db.SaveChanges();
                                if (result > 0)
                                {
                                    oldfinalizeRecord.ContributorID = conId;
                                    db.Entry(oldfinalizeRecord).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                var oldUnfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == oldheaderId && x.ContributorID == oldconId && x.SourceID == oldfinalizeRecord.SourceID).FirstOrDefault();
                                ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                oldUnfinalizeRecord.MapTo(History);
                                History.ReplaceContributorID = conId;
                                History.DetailID = oldUnfinalizeRecord.Id;
                                History.ReplaceHeaderID = oldUnfinalizeRecord.ContributonSheetHeaderID;
                                db.Entry(History).State = EntityState.Added;
                                result = db.SaveChanges();
                                if (result > 0)
                                {
                                    oldUnfinalizeRecord.ContributorID = conId;
                                    db.Entry(oldUnfinalizeRecord).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        #region Move to different employer
                        else
                        {
                            var OldHeaderRecord = header.Where(x => x.Id == oldheaderId).FirstOrDefault();
                            #region When unFinalized Header Exist

                            if (header.Any(x => x.EmployerId == NewEmployerId && x.Month == OldHeaderRecord.Month && x.Year == OldHeaderRecord.Year))
                            {
                                //If HeaderExist then get that HeaderId
                                var newHeaderId = header.Where(x => x.EmployerId == NewEmployerId && x.Month == OldHeaderRecord.Month && x.Year == OldHeaderRecord.Year).Select(x => x.Id).FirstOrDefault();
                                var oldUnfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == oldheaderId && x.ContributorID == oldconId && x.SourceID == oldfinalizeRecord.SourceID).FirstOrDefault();

                                #region When Contribution Exist

                                //Manage history
                                if (details.Any(x => x.ContributorID == conId && x.SourceID == oldfinalizeRecord.SourceID && x.ContributonSheetHeaderID == newHeaderId))
                                {
                                    if (details.Any(x => x.ContributorID == conId && x.SourceID == oldfinalizeRecord.SourceID && x.ContributonSheetHeaderID == newHeaderId && x.SalaryAmount == oldfinalizeRecord.SalaryAmount && x.ContributorContribution == oldfinalizeRecord.ContributorContribution && x.EmployerContribution == oldfinalizeRecord.EmployerContribution))
                                    {
                                        #region entrywithSameContribution
                                        ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                        oldUnfinalizeRecord.MapTo(History);
                                        History.ReplaceContributorID = conId;
                                        History.DetailID = oldUnfinalizeRecord.Id;
                                        History.ReplaceHeaderID = newHeaderId;
                                        db.Entry(History).State = EntityState.Added;
                                        result = db.SaveChanges();
                                        if (result > 0)
                                        {
                                            oldUnfinalizeRecord.IsActive = false;
                                            db.Entry(oldUnfinalizeRecord).State = EntityState.Modified;
                                            db.SaveChanges();
                                        }
                                        if (headerFinalize.Any(x => x.ContributonSheetHeaderId == newHeaderId && x.EmployerId == NewEmployerId && x.Month == OldHeaderRecord.Month && x.Year == OldHeaderRecord.Year))
                                        {
                                            //Manage history
                                            var newHeaderFinalizedId = headerFinalize.Where(x => x.ContributonSheetHeaderId == newHeaderId && x.EmployerId == NewEmployerId && x.Month == OldHeaderRecord.Month && x.Year == OldHeaderRecord.Year).Select(x => x.Id).FirstOrDefault();
                                            ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                            oldfinalizeRecord.MapTo(finaliseHistory);
                                            finaliseHistory.ReplaceContributorID = conId;
                                            finaliseHistory.DetailFinalizeID = oldfinalizeRecord.Id;
                                            finaliseHistory.ReplaceHeaderFinalizeID = newHeaderFinalizedId;
                                            db.Entry(finaliseHistory).State = EntityState.Added;
                                            result = db.SaveChanges();
                                            if (result > 0)
                                            {
                                                //update Finalized Details
                                                oldfinalizeRecord.IsActive = false;
                                                db.Entry(oldfinalizeRecord).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            //Create new HeaderFinalize
                                            ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
                                            entityFinalise.EmployerId = NewEmployerId;
                                            entityFinalise.Month = OldHeaderRecord.Month;
                                            entityFinalise.Year = OldHeaderRecord.Year;
                                            entityFinalise.IsActive = true;
                                            entityFinalise.ContributonSheetHeaderId = newHeaderId;
                                            db.Entry(entityFinalise).State = EntityState.Added;
                                            var results = db.SaveChanges();
                                            if (result > 0)
                                            {
                                                //Manage history
                                                ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                                oldfinalizeRecord.MapTo(finaliseHistory);
                                                finaliseHistory.ReplaceContributorID = conId;
                                                finaliseHistory.DetailFinalizeID = oldfinalizeRecord.Id;
                                                finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
                                                db.Entry(finaliseHistory).State = EntityState.Added;
                                                result = db.SaveChanges();
                                                if (result > 0)
                                                {
                                                    //Update finalised Details
                                                    oldfinalizeRecord.ContributorID = conId;
                                                    oldfinalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id;
                                                    db.Entry(oldfinalizeRecord).State = EntityState.Modified;
                                                    db.SaveChanges();
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region add Record in list
                                        var newFinalizedRecord = detailsFinalize.Where(x => x.ContributorID == conId && x.SourceID == oldfinalizeRecord.SourceID && x.ContributonSheetHeaderFinalise.ContributonSheetHeaderId == newHeaderId).FirstOrDefault();
                                        var newDetails = details.Where(x => x.ContributonSheetHeaderID == newHeaderId && x.ContributorID == conId && x.SourceID == oldfinalizeRecord.SourceID).FirstOrDefault();
                                        OldAndNewContribution contribution = new OldAndNewContribution();
                                        contribution.Month = OldHeaderRecord.Month;
                                        contribution.Year = OldHeaderRecord.Year;
                                        contribution.SourceName = oldfinalizeRecord.Source.Name;
                                        //New Details on which record move
                                        contribution.NewContributorContribution = newFinalizedRecord.ContributorContribution;
                                        contribution.NewEmployerContribution = newFinalizedRecord.EmployerContribution;
                                        contribution.NewSalaryAmount = newFinalizedRecord.SalaryAmount;
                                        contribution.NewDetailId = newDetails.Id;
                                        contribution.NewDetailFinalizedId = newFinalizedRecord.Id;
                                        contribution.NewContributorName = newFinalizedRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
                                        contribution.NewEmployerName = newFinalizedRecord.Contributor.Employer.EmployerName;

                                        //old Details whose record move
                                        contribution.OldContributorContribution = oldfinalizeRecord.ContributorContribution;
                                        contribution.OldEmployerContribution = oldfinalizeRecord.EmployerContribution;
                                        contribution.OldSalaryAmount = oldfinalizeRecord.SalaryAmount;
                                        contribution.OldDetailId = oldUnfinalizeRecord != null ? oldUnfinalizeRecord.Id : oldconId;//ternary operator added by neeraj on 27Oct2018. One exception issue raised by JKPS team
                                        contribution.OldDetailFinalizedId = oldfinalizeRecord.Id;
                                        contribution.OldContributorName = oldfinalizeRecord.Contributor.FirstName + " " + oldfinalizeRecord.Contributor.LastName;
                                        contribution.OldEmployerName = oldfinalizeRecord.Contributor.Employer.EmployerName;

                                        RecordWithDiffernetContribution.Add(contribution);
                                        #endregion
                                    }

                                }
                                #endregion
                                else
                                {
                                    #region No entry in Unfinalized
                                    //if condition added by neeraj on 27Oct2018. One exception issue raised by JKPS team
                                    if (oldUnfinalizeRecord != null)
                                    {
                                        ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                        oldUnfinalizeRecord.MapTo(History);
                                        History.ReplaceContributorID = conId;
                                        History.DetailID = oldUnfinalizeRecord.Id;
                                        History.ReplaceHeaderID = newHeaderId;
                                        db.Entry(History).State = EntityState.Added;
                                        result = db.SaveChanges();
                                        if (result > 0)
                                        {
                                            //Update unfinalised Details
                                            oldUnfinalizeRecord.ContributorID = conId; oldUnfinalizeRecord.ContributonSheetHeaderID = newHeaderId;
                                            db.Entry(oldUnfinalizeRecord).State = EntityState.Modified;
                                            db.SaveChanges();
                                        }
                                    } //END - if condition added by neeraj on 27Oct2018. One exception issue raised by JKPS team                                  

                                    ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
                                    entityFinalise.EmployerId = NewEmployerId;
                                    entityFinalise.Month = OldHeaderRecord.Month;
                                    entityFinalise.Year = OldHeaderRecord.Year;
                                    entityFinalise.IsActive = true;
                                    entityFinalise.ContributonSheetHeaderId = newHeaderId;
                                    db.Entry(entityFinalise).State = EntityState.Added;
                                    var results = db.SaveChanges();
                                    if (result > 0)
                                    {
                                        //Manage history
                                        ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                        oldfinalizeRecord.MapTo(finaliseHistory);
                                        finaliseHistory.ReplaceContributorID = conId;
                                        finaliseHistory.DetailFinalizeID = oldfinalizeRecord.Id;
                                        finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
                                        db.Entry(finaliseHistory).State = EntityState.Added;
                                        result = db.SaveChanges();
                                        if (result > 0)
                                        {
                                            oldfinalizeRecord.ContributorID = conId; oldfinalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id;
                                            db.Entry(oldfinalizeRecord).State = EntityState.Modified;
                                            db.SaveChanges();
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                            #region When Header Not Exist
                            else
                            {
                                //create New Header
                                ContributonSheetHeader entity = new ContributonSheetHeader();
                                entity.EmployerId = NewEmployerId;
                                entity.Month = OldHeaderRecord.Month;
                                entity.Year = OldHeaderRecord.Year;
                                entity.IsActive = true;
                                entity.Finalized = 1;
                                db.Entry(entity).State = EntityState.Added;
                                result = db.SaveChanges();
                                if (result > 0)
                                {
                                    //Manage history
                                    var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == oldheaderId && x.ContributorID == oldconId && x.SourceID == oldfinalizeRecord.SourceID).FirstOrDefault();
                                    ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                    unfinalizeRecord.MapTo(History);
                                    History.ReplaceContributorID = conId;
                                    History.DetailID = unfinalizeRecord.Id;
                                    History.ReplaceHeaderID = entity.Id;
                                    db.Entry(History).State = EntityState.Added;
                                    result = db.SaveChanges();
                                    if (result > 0)
                                    {
                                        //Update unfinalised Details
                                        unfinalizeRecord.ContributorID = conId; unfinalizeRecord.ContributonSheetHeaderID = entity.Id;
                                        db.Entry(unfinalizeRecord).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                }
                                //Create new HeaderFinalize
                                ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
                                entityFinalise.EmployerId = NewEmployerId;
                                entityFinalise.Month = OldHeaderRecord.Month;
                                entityFinalise.Year = OldHeaderRecord.Year;
                                entityFinalise.IsActive = true;
                                entityFinalise.ContributonSheetHeaderId = entity.Id;
                                db.Entry(entityFinalise).State = EntityState.Added;
                                var results = db.SaveChanges();
                                if (result > 0)
                                {
                                    //Manage history
                                    ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                    oldfinalizeRecord.MapTo(finaliseHistory);
                                    finaliseHistory.ReplaceContributorID = conId;
                                    finaliseHistory.DetailFinalizeID = oldfinalizeRecord.Id;
                                    finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
                                    db.Entry(finaliseHistory).State = EntityState.Added;
                                    result = db.SaveChanges();
                                    if (result > 0)
                                    {
                                        //Update finalised Details
                                        oldfinalizeRecord.ContributorID = conId; oldfinalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id;
                                        db.Entry(oldfinalizeRecord).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                        }
                        #endregion
                    }
                    dbTransaction.Commit();
                    list.Record = RecordWithDiffernetContribution;
                    if (list.Record.Count == 0)
                        list.success = 1;
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    list.Error = ex.Message;
                    dbTransaction.Rollback();
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult MoveContributorAjax1(string[] Data, int conId, int oldconId, int oldEmpId)
        {
            int EmployerId = Convert.ToInt32(db.MasterContributor.Where(x => x.Id == conId).Select(x => x.EmployerID).FirstOrDefault());
            if (EmployerId == oldEmpId)
            {
                var result = mergeWithSameEmployer(Data, conId, oldconId, oldEmpId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = mergeWithDifferentEmployer(Data, conId, oldconId, oldEmpId, EmployerId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public MoveContributorContributionViewModel mergeWithSameEmployer(string[] Data, int conId, int oldconId, int oldEmpId)
        {
            MoveContributorContributionViewModel list = new MoveContributorContributionViewModel();
            List<OldAndNewContribution> RecordWithDiffernetContribution = new List<OldAndNewContribution>();
            int result = 0;
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var detailsFinalize = db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
                    var details = db.ContributonSheetDetails.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
                    for (int i = 0; i < Data.Length; i++)
                    {
                        var data = Data[i].Split('#');
                        int headerId = Convert.ToInt32(data[1]);
                        int detailId = Convert.ToInt32(data[0]);
                        var finalizeRecord = detailsFinalize.Where(x => x.Id == detailId).FirstOrDefault();//Get Slected Record
                        if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
                        {
                            if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID && x.SalaryAmount == finalizeRecord.SalaryAmount && x.ContributorContribution == finalizeRecord.ContributorContribution && x.EmployerContribution == finalizeRecord.EmployerContribution))
                            {
                                #region maintain history when contribution is same & Deactive Previous Record
                                //Maintain History
                                ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                finalizeRecord.MapTo(finaliseHistory);
                                finaliseHistory.ReplaceContributorID = conId;
                                finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
                                finaliseHistory.ReplaceHeaderFinalizeID = finalizeRecord.ContributonSheetHeaderFinaliseID;
                                db.Entry(finaliseHistory).State = EntityState.Added;
                                result = db.SaveChanges();
                                if (result > 0)
                                {
                                    //Deactive Record
                                    finalizeRecord.IsActive = false;
                                    db.Entry(finalizeRecord).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                //Maintain History for unfinalized
                                var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
                                ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                unfinalizeRecord.MapTo(History);
                                History.ReplaceContributorID = conId;
                                History.DetailID = unfinalizeRecord.Id;
                                History.ReplaceHeaderID = unfinalizeRecord.ContributonSheetHeaderID;
                                db.Entry(History).State = EntityState.Added;
                                result = db.SaveChanges();
                                if (result > 0)
                                {
                                    //Deactive Record
                                    unfinalizeRecord.IsActive = false;
                                    db.Entry(unfinalizeRecord).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                #endregion
                            }
                            else
                            {
                                #region add Record in list
                                var newFinalizedRecord = detailsFinalize.Where(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID).FirstOrDefault();
                                var oldDetails = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
                                var newDetails = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
                                OldAndNewContribution contribution = new OldAndNewContribution();
                                contribution.Month = finalizeRecord.ContributonSheetHeaderFinalise.Month;
                                contribution.Year = finalizeRecord.ContributonSheetHeaderFinalise.Year;
                                contribution.SourceName = finalizeRecord.Source.Name;
                                //New Details on which record move
                                contribution.NewContributorContribution = newFinalizedRecord.ContributorContribution;
                                contribution.NewEmployerContribution = newFinalizedRecord.EmployerContribution;
                                contribution.NewSalaryAmount = newFinalizedRecord.SalaryAmount;
                                contribution.NewDetailId = newDetails.Id;
                                contribution.NewDetailFinalizedId = newFinalizedRecord.Id;
                                contribution.NewContributorName = newFinalizedRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
                                contribution.NewEmployerName = newFinalizedRecord.Contributor.Employer.EmployerName;

                                //old Details whose record move
                                contribution.OldContributorContribution = finalizeRecord.ContributorContribution;
                                contribution.OldEmployerContribution = finalizeRecord.EmployerContribution;
                                contribution.OldSalaryAmount = finalizeRecord.SalaryAmount;
                                contribution.OldDetailId = oldDetails.Id;
                                contribution.OldDetailFinalizedId = finalizeRecord.Id;
                                contribution.OldContributorName = finalizeRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
                                contribution.OldEmployerName = finalizeRecord.Contributor.Employer.EmployerName;

                                RecordWithDiffernetContribution.Add(contribution);
                                #endregion
                            }
                        }
                        else
                        {
                            #region record not exist for that contributor than update
                            ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                            finalizeRecord.MapTo(finaliseHistory);
                            finaliseHistory.ReplaceContributorID = conId;
                            finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
                            finaliseHistory.ReplaceHeaderFinalizeID = finalizeRecord.ContributonSheetHeaderFinaliseID;
                            db.Entry(finaliseHistory).State = EntityState.Added;
                            result = db.SaveChanges();
                            if (result > 0)
                            {
                                finalizeRecord.ContributorID = conId;
                                db.Entry(finalizeRecord).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
                            ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                            unfinalizeRecord.MapTo(History);
                            History.ReplaceContributorID = conId;
                            History.DetailID = unfinalizeRecord.Id;
                            History.ReplaceHeaderID = unfinalizeRecord.ContributonSheetHeaderID;
                            db.Entry(History).State = EntityState.Added;
                            result = db.SaveChanges();
                            if (result > 0)
                            {
                                unfinalizeRecord.ContributorID = conId;
                                db.Entry(unfinalizeRecord).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            #endregion
                        }
                    }
                    list.Record = RecordWithDiffernetContribution;
                    dbTransaction.Commit();
                    if (list.Record.Count == 0)
                        list.success = result;
                }
                catch (Exception ex)
                {
                    list.Error = ex.Message;
                    dbTransaction.Rollback();
                    return list;
                }

            }
            return list;
        }


        public MoveContributorContributionViewModel mergeWithDifferentEmployer(string[] Data, int conId, int oldconId, int oldEmpId, int EmployerId)
        {
            MoveContributorContributionViewModel list = new MoveContributorContributionViewModel();
            List<OldAndNewContribution> RecordWithDiffernetContribution = new List<OldAndNewContribution>();
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var header = db.ContributonSheetHeader.Where(x => x.IsActive == true && (x.EmployerId == EmployerId || x.EmployerId == oldEmpId)).ToList();
                    var headerFinalize = db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && (x.EmployerId == EmployerId || x.EmployerId == oldEmpId)).ToList();
                    var detailsFinalize = db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
                    var details = db.ContributonSheetDetails.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
                    for (int i = 0; i < Data.Length; i++)
                    {
                        var data = Data[i].Split('#');
                        int headerId = Convert.ToInt32(data[1]);
                        int detailId = Convert.ToInt32(data[0]);
                        var getHeaderRecord = header.Where(x => x.Id == headerId).FirstOrDefault();
                        var finalizeRecord = detailsFinalize.Where(x => x.Id == detailId).FirstOrDefault();
                        //Check Header Exist For that Month                        
                        if (header.Any(x => x.EmployerId == EmployerId && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year))
                        {
                            //If HeaderExist then get that HeaderId
                            var newHeaderId = header.Where(x => x.EmployerId == EmployerId && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year).Select(x => x.Id).FirstOrDefault();
                            var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();


                            //Manage history
                            if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == newHeaderId))
                            {
                                if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == newHeaderId && x.SalaryAmount == finalizeRecord.SalaryAmount && x.ContributorContribution == finalizeRecord.ContributorContribution && x.EmployerContribution == finalizeRecord.EmployerContribution))
                                {
                                    #region entrywithSameContribution
                                    ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                    unfinalizeRecord.MapTo(History);
                                    History.ReplaceContributorID = conId;
                                    History.DetailID = unfinalizeRecord.Id;
                                    History.ReplaceHeaderID = newHeaderId;
                                    db.Entry(History).State = EntityState.Added;
                                    var result = db.SaveChanges();
                                    if (result > 0)
                                    {
                                        unfinalizeRecord.IsActive = false;
                                        db.Entry(unfinalizeRecord).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                    if (headerFinalize.Any(x => x.ContributonSheetHeaderId == newHeaderId && x.EmployerId == EmployerId && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year))
                                    {
                                        //Manage history
                                        var newHeaderFinalizedId = headerFinalize.Where(x => x.ContributonSheetHeaderId == newHeaderId && x.EmployerId == EmployerId && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year).Select(x => x.Id).FirstOrDefault();
                                        ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                        finalizeRecord.MapTo(finaliseHistory);
                                        finaliseHistory.ReplaceContributorID = conId;
                                        finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
                                        finaliseHistory.ReplaceHeaderFinalizeID = newHeaderFinalizedId;
                                        db.Entry(finaliseHistory).State = EntityState.Added;
                                        result = db.SaveChanges();
                                        if (result > 0)
                                        {
                                            //update Finalized Details
                                            finalizeRecord.IsActive = false;
                                            db.Entry(finalizeRecord).State = EntityState.Modified;
                                            db.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        //Create new HeaderFinalize
                                        ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
                                        entityFinalise.EmployerId = EmployerId;
                                        entityFinalise.Month = getHeaderRecord.Month;
                                        entityFinalise.Year = getHeaderRecord.Year;
                                        entityFinalise.IsActive = true;
                                        entityFinalise.ContributonSheetHeaderId = newHeaderId;
                                        db.Entry(entityFinalise).State = EntityState.Added;
                                        var results = db.SaveChanges();
                                        if (result > 0)
                                        {
                                            //Manage history
                                            ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                            finalizeRecord.MapTo(finaliseHistory);
                                            finaliseHistory.ReplaceContributorID = conId;
                                            finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
                                            finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
                                            db.Entry(finaliseHistory).State = EntityState.Added;
                                            result = db.SaveChanges();
                                            if (result > 0)
                                            {
                                                //Update finalised Details
                                                finalizeRecord.ContributorID = conId;
                                                finalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id;
                                                db.Entry(finalizeRecord).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region add Record in list
                                    var newFinalizedRecord = detailsFinalize.Where(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinalise.ContributonSheetHeaderId == newHeaderId).FirstOrDefault();
                                    var oldDetails = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
                                    var newDetails = details.Where(x => x.ContributonSheetHeaderID == newHeaderId && x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
                                    OldAndNewContribution contribution = new OldAndNewContribution();
                                    contribution.Month = finalizeRecord.ContributonSheetHeaderFinalise.Month;
                                    contribution.Year = finalizeRecord.ContributonSheetHeaderFinalise.Year;
                                    contribution.SourceName = finalizeRecord.Source.Name;
                                    //New Details on which record move
                                    contribution.NewContributorContribution = newFinalizedRecord.ContributorContribution;
                                    contribution.NewEmployerContribution = newFinalizedRecord.EmployerContribution;
                                    contribution.NewSalaryAmount = newFinalizedRecord.SalaryAmount;
                                    contribution.NewDetailId = newDetails.Id;
                                    contribution.NewDetailFinalizedId = newFinalizedRecord.Id;
                                    contribution.NewContributorName = newFinalizedRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
                                    contribution.NewEmployerName = newFinalizedRecord.Contributor.Employer.EmployerName;

                                    //old Details whose record move
                                    contribution.OldContributorContribution = finalizeRecord.ContributorContribution;
                                    contribution.OldEmployerContribution = finalizeRecord.EmployerContribution;
                                    contribution.OldSalaryAmount = finalizeRecord.SalaryAmount;
                                    contribution.OldDetailId = oldDetails.Id;
                                    contribution.OldDetailFinalizedId = finalizeRecord.Id;
                                    contribution.OldContributorName = finalizeRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
                                    contribution.OldEmployerName = finalizeRecord.Contributor.Employer.EmployerName;

                                    RecordWithDiffernetContribution.Add(contribution);
                                    #endregion
                                }

                            }
                            else
                            {
                                #region No entry in Unfinalized
                                ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                unfinalizeRecord.MapTo(History);
                                History.ReplaceContributorID = conId;
                                History.DetailID = unfinalizeRecord.Id;
                                History.ReplaceHeaderID = newHeaderId;
                                db.Entry(History).State = EntityState.Added;
                                var result = db.SaveChanges();
                                if (result > 0)
                                {
                                    //Update unfinalised Details
                                    unfinalizeRecord.ContributorID = conId; unfinalizeRecord.ContributonSheetHeaderID = newHeaderId;
                                    db.Entry(unfinalizeRecord).State = EntityState.Modified;
                                    db.SaveChanges();
                                }

                                ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
                                entityFinalise.EmployerId = EmployerId;
                                entityFinalise.Month = getHeaderRecord.Month;
                                entityFinalise.Year = getHeaderRecord.Year;
                                entityFinalise.IsActive = true;
                                entityFinalise.ContributonSheetHeaderId = newHeaderId;
                                db.Entry(entityFinalise).State = EntityState.Added;
                                var results = db.SaveChanges();
                                if (result > 0)
                                {
                                    //Manage history
                                    ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                    finalizeRecord.MapTo(finaliseHistory);
                                    finaliseHistory.ReplaceContributorID = conId;
                                    finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
                                    finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
                                    db.Entry(finaliseHistory).State = EntityState.Added;
                                    result = db.SaveChanges();
                                    if (result > 0)
                                    {
                                        //Update finalised Details
                                        if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
                                            finalizeRecord.IsActive = false;
                                        else
                                        { finalizeRecord.ContributorID = conId; finalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id; }
                                        db.Entry(finalizeRecord).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            #region create new when no record exist
                            //create New Header
                            ContributonSheetHeader entity = new ContributonSheetHeader();
                            entity.EmployerId = EmployerId;
                            entity.Month = getHeaderRecord.Month;
                            entity.Year = getHeaderRecord.Year;
                            entity.IsActive = true;
                            entity.Finalized = 1;
                            db.Entry(entity).State = EntityState.Added;
                            var result = db.SaveChanges();
                            if (result > 0)
                            {
                                //Manage history
                                var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
                                ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                                unfinalizeRecord.MapTo(History);
                                History.ReplaceContributorID = conId;
                                History.DetailID = unfinalizeRecord.Id;
                                History.ReplaceHeaderID = entity.Id;
                                db.Entry(History).State = EntityState.Added;
                                result = db.SaveChanges();
                                if (result > 0)
                                {
                                    //Update unfinalised Details
                                    if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == unfinalizeRecord.ContributonSheetHeaderID))
                                        unfinalizeRecord.IsActive = false;
                                    else
                                    { unfinalizeRecord.ContributorID = conId; unfinalizeRecord.ContributonSheetHeaderID = entity.Id; }
                                    db.Entry(unfinalizeRecord).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                            //Create new HeaderFinalize
                            ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
                            entityFinalise.EmployerId = EmployerId;
                            entityFinalise.Month = getHeaderRecord.Month;
                            entityFinalise.Year = getHeaderRecord.Year;
                            entityFinalise.IsActive = true;
                            entityFinalise.ContributonSheetHeaderId = entity.Id;
                            db.Entry(entityFinalise).State = EntityState.Added;
                            var results = db.SaveChanges();
                            if (result > 0)
                            {
                                //Manage history
                                ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                                finalizeRecord.MapTo(finaliseHistory);
                                finaliseHistory.ReplaceContributorID = conId;
                                finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
                                finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
                                db.Entry(finaliseHistory).State = EntityState.Added;
                                result = db.SaveChanges();
                                if (result > 0)
                                {
                                    //Update finalised Details
                                    if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
                                        finalizeRecord.IsActive = false;
                                    else
                                    { finalizeRecord.ContributorID = conId; finalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id; }
                                    db.Entry(finalizeRecord).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                            #endregion
                        }
                    }
                    dbTransaction.Commit();
                    list.Record = RecordWithDiffernetContribution;
                    if (list.Record.Count == 0)
                        list.success = 1;
                    return list;
                }
                catch (Exception ex)
                {
                    list.Error = ex.Message;
                    dbTransaction.Rollback();
                    return list;
                }
            }
        }

        //public MoveContributorContributionViewModel mergeWithSameEmployer(string[] Data, int conId, int oldconId, int oldEmpId)
        //{
        //    MoveContributorContributionViewModel list = new MoveContributorContributionViewModel();
        //    List<OldAndNewContribution> RecordWithDiffernetContribution = new List<OldAndNewContribution>();
        //    int result = 0;
        //    using (var dbTransaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var detailsFinalize = db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
        //            var details = db.ContributonSheetDetails.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
        //            for (int i = 0; i < Data.Length; i++)
        //            {
        //                var data = Data[i].Split('#');
        //                int headerId = Convert.ToInt32(data[1]);
        //                int detailId = Convert.ToInt32(data[0]);
        //                var finalizeRecord = detailsFinalize.Where(x => x.Id == detailId).FirstOrDefault();//Get Slected Record
        //                if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
        //                {
        //                    if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID && x.SalaryAmount == finalizeRecord.SalaryAmount && x.ContributorContribution == finalizeRecord.ContributorContribution && x.EmployerContribution == finalizeRecord.EmployerContribution))
        //                    {
        //                        #region maintain history when contribution is same & Deactive Previous Record
        //                        //Maintain History
        //                        ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                        finalizeRecord.MapTo(finaliseHistory);
        //                        finaliseHistory.ReplaceContributorID = conId;
        //                        finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                        finaliseHistory.ReplaceHeaderFinalizeID = finalizeRecord.ContributonSheetHeaderFinaliseID;
        //                        db.Entry(finaliseHistory).State = EntityState.Added;
        //                        result = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Deactive Record
        //                            finalizeRecord.IsActive = false;
        //                            db.Entry(finalizeRecord).State = EntityState.Modified;
        //                            db.SaveChanges();
        //                        }
        //                        //Maintain History for unfinalized
        //                        var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                        ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
        //                        unfinalizeRecord.MapTo(History);
        //                        History.ReplaceContributorID = conId;
        //                        History.DetailID = unfinalizeRecord.Id;
        //                        History.ReplaceHeaderID = unfinalizeRecord.ContributonSheetHeaderID;
        //                        db.Entry(History).State = EntityState.Added;
        //                        result = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Deactive Record
        //                            finalizeRecord.IsActive = false;
        //                            db.Entry(unfinalizeRecord).State = EntityState.Modified;
        //                            db.SaveChanges();
        //                        }
        //                        #endregion
        //                    }
        //                    else
        //                    {
        //                        #region add Record in list
        //                        var newFinalizedRecord = detailsFinalize.Where(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID).FirstOrDefault();
        //                        var oldDetails = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                        var newDetails = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                        OldAndNewContribution contribution = new OldAndNewContribution();
        //                        contribution.Month = finalizeRecord.ContributonSheetHeaderFinalise.Month;
        //                        contribution.Year = finalizeRecord.ContributonSheetHeaderFinalise.Year;
        //                        contribution.SourceName = finalizeRecord.Source.Name;
        //                        //New Details on which record move
        //                        contribution.NewContributorContribution = newFinalizedRecord.ContributorContribution;
        //                        contribution.NewEmployerContribution = newFinalizedRecord.EmployerContribution;
        //                        contribution.NewSalaryAmount = newFinalizedRecord.SalaryAmount;
        //                        contribution.NewDetailId = newDetails.Id;
        //                        contribution.NewDetailFinalizedId = newFinalizedRecord.Id;
        //                        contribution.NewContributorName = newFinalizedRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
        //                        contribution.NewEmployerName = newFinalizedRecord.Contributor.Employer.EmployerName;

        //                        //old Details whose record move
        //                        contribution.OldContributorContribution = finalizeRecord.ContributorContribution;
        //                        contribution.OldEmployerContribution = finalizeRecord.EmployerContribution;
        //                        contribution.OldSalaryAmount = finalizeRecord.SalaryAmount;
        //                        contribution.OldDetailId = oldDetails.Id;
        //                        contribution.OldDetailFinalizedId = finalizeRecord.Id;
        //                        contribution.OldContributorName = finalizeRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
        //                        contribution.OldEmployerName = finalizeRecord.Contributor.Employer.EmployerName;

        //                        RecordWithDiffernetContribution.Add(contribution);
        //                        #endregion
        //                    }
        //                }
        //                else
        //                {
        //                    #region record not exist for that contributor than update
        //                    ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                    finalizeRecord.MapTo(finaliseHistory);
        //                    finaliseHistory.ReplaceContributorID = conId;
        //                    finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                    finaliseHistory.ReplaceHeaderFinalizeID = finalizeRecord.ContributonSheetHeaderFinaliseID;
        //                    db.Entry(finaliseHistory).State = EntityState.Added;
        //                    result = db.SaveChanges();
        //                    if (result > 0)
        //                    {
        //                        finalizeRecord.ContributorID = conId;
        //                        db.Entry(finalizeRecord).State = EntityState.Modified;
        //                        db.SaveChanges();
        //                    }
        //                    var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                    ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
        //                    unfinalizeRecord.MapTo(History);
        //                    History.ReplaceContributorID = conId;
        //                    History.DetailID = unfinalizeRecord.Id;
        //                    History.ReplaceHeaderID = unfinalizeRecord.ContributonSheetHeaderID;
        //                    db.Entry(History).State = EntityState.Added;
        //                    result = db.SaveChanges();
        //                    if (result > 0)
        //                    {
        //                        unfinalizeRecord.ContributorID = conId;
        //                        db.Entry(unfinalizeRecord).State = EntityState.Modified;
        //                        db.SaveChanges();
        //                    }
        //                    #endregion
        //                }
        //            }
        //            list.Record = RecordWithDiffernetContribution;
        //            dbTransaction.Commit();
        //            if (list.Record.Count == 0)
        //                list.success = result;
        //        }
        //        catch (Exception ex)
        //        {
        //            list.Error = ex.Message;
        //            dbTransaction.Rollback();
        //            return list;
        //        }

        //    }
        //    return list;
        //}


        //public MoveContributorContributionViewModel mergeWithDifferentEmployer(string[] Data, int conId, int oldconId, int oldEmpId, int EmployerId)
        //{
        //    MoveContributorContributionViewModel list = new MoveContributorContributionViewModel();
        //    List<OldAndNewContribution> RecordWithDiffernetContribution = new List<OldAndNewContribution>();
        //    using (var dbTransaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var header = db.ContributonSheetHeader.Where(x => x.IsActive == true && (x.EmployerId == EmployerId || x.EmployerId == oldEmpId)).ToList();
        //            var headerFinalize = db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && (x.EmployerId == EmployerId || x.EmployerId == oldEmpId)).ToList();
        //            var detailsFinalize = db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
        //            var details = db.ContributonSheetDetails.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
        //            for (int i = 0; i < Data.Length; i++)
        //            {
        //                var data = Data[i].Split('#');
        //                int headerId = Convert.ToInt32(data[1]);
        //                int detailId = Convert.ToInt32(data[0]);
        //                var getHeaderRecord = header.Where(x => x.Id == headerId).FirstOrDefault();
        //                var finalizeRecord = detailsFinalize.Where(x => x.Id == detailId).FirstOrDefault();
        //                if (header.Any(x => x.EmployerId == EmployerId && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year))
        //                {
        //                    var newHeaderId = header.Where(x => x.EmployerId == EmployerId && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year).Select(x => x.Id).FirstOrDefault();
        //                    var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                    //Manage history
        //                    if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == unfinalizeRecord.ContributonSheetHeaderID))
        //                    {
        //                        if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == unfinalizeRecord.ContributonSheetHeaderID && x.SalaryAmount == finalizeRecord.SalaryAmount && x.ContributorContribution == finalizeRecord.ContributorContribution && x.EmployerContribution == finalizeRecord.EmployerContribution))
        //                        {
        //                            #region entrywithSameContribution
        //                            ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
        //                            unfinalizeRecord.MapTo(History);
        //                            History.ReplaceContributorID = conId;
        //                            History.DetailID = unfinalizeRecord.Id;
        //                            History.ReplaceHeaderID = newHeaderId;
        //                            db.Entry(History).State = EntityState.Added;
        //                            var result = db.SaveChanges();
        //                            if (result > 0)
        //                            {
        //                                unfinalizeRecord.IsActive = false;
        //                                db.Entry(unfinalizeRecord).State = EntityState.Modified;
        //                                db.SaveChanges();
        //                            }
        //                            if (headerFinalize.Any(x => x.ContributonSheetHeaderId == newHeaderId && x.EmployerId == EmployerId && x.ContributonSheetHeaderId == getHeaderRecord.Id && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year))
        //                            {
        //                                //Manage history
        //                                var newHeaderFinalizedId = headerFinalize.Where(x => x.ContributonSheetHeaderId == newHeaderId && x.EmployerId == EmployerId && x.ContributonSheetHeaderId == getHeaderRecord.Id && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year).Select(x => x.Id).FirstOrDefault();
        //                                ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                                finalizeRecord.MapTo(finaliseHistory);
        //                                finaliseHistory.ReplaceContributorID = conId;
        //                                finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                                finaliseHistory.ReplaceHeaderFinalizeID = newHeaderFinalizedId;
        //                                db.Entry(finaliseHistory).State = EntityState.Added;
        //                                result = db.SaveChanges();
        //                                if (result > 0)
        //                                {
        //                                    //update Finalized Details
        //                                    finalizeRecord.IsActive = false;
        //                                    db.Entry(finalizeRecord).State = EntityState.Modified;
        //                                    db.SaveChanges();
        //                                }
        //                            }
        //                            else
        //                            {
        //                                //Create new HeaderFinalize
        //                                ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
        //                                entityFinalise.EmployerId = EmployerId;
        //                                entityFinalise.Month = getHeaderRecord.Month;
        //                                entityFinalise.Year = getHeaderRecord.Year;
        //                                entityFinalise.IsActive = true;
        //                                entityFinalise.ContributonSheetHeaderId = newHeaderId;
        //                                db.Entry(entityFinalise).State = EntityState.Added;
        //                                var results = db.SaveChanges();
        //                                if (result > 0)
        //                                {
        //                                    //Manage history
        //                                    ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                                    finalizeRecord.MapTo(finaliseHistory);
        //                                    finaliseHistory.ReplaceContributorID = conId;
        //                                    finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                                    finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
        //                                    db.Entry(finaliseHistory).State = EntityState.Added;
        //                                    result = db.SaveChanges();
        //                                    if (result > 0)
        //                                    {
        //                                        //Update finalised Details
        //                                        finalizeRecord.ContributorID = conId;
        //                                        finalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id;
        //                                        db.Entry(finalizeRecord).State = EntityState.Modified;
        //                                        db.SaveChanges();
        //                                    }
        //                                }
        //                            }
        //                            #endregion
        //                        }
        //                        else
        //                        {
        //                            #region add Record in list
        //                            var newFinalizedRecord = detailsFinalize.Where(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID).FirstOrDefault();
        //                            var oldDetails = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                            var newDetails = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                            OldAndNewContribution contribution = new OldAndNewContribution();
        //                            contribution.Month = finalizeRecord.ContributonSheetHeaderFinalise.Month;
        //                            contribution.Year = finalizeRecord.ContributonSheetHeaderFinalise.Year;
        //                            contribution.SourceName = finalizeRecord.Source.Name;
        //                            //New Details on which record move
        //                            contribution.NewContributorContribution = newFinalizedRecord.ContributorContribution;
        //                            contribution.NewEmployerContribution = newFinalizedRecord.EmployerContribution;
        //                            contribution.NewSalaryAmount = newFinalizedRecord.SalaryAmount;
        //                            contribution.NewDetailId = newDetails.Id;
        //                            contribution.NewDetailFinalizedId = newFinalizedRecord.Id;
        //                            contribution.NewContributorName = newFinalizedRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
        //                            contribution.NewEmployerName = newFinalizedRecord.Contributor.Employer.EmployerName;

        //                            //old Details whose record move
        //                            contribution.OldContributorContribution = finalizeRecord.ContributorContribution;
        //                            contribution.OldEmployerContribution = finalizeRecord.EmployerContribution;
        //                            contribution.OldSalaryAmount = finalizeRecord.SalaryAmount;
        //                            contribution.OldDetailId = oldDetails.Id;
        //                            contribution.OldDetailFinalizedId = finalizeRecord.Id;
        //                            contribution.OldContributorName = finalizeRecord.Contributor.FirstName + " " + newFinalizedRecord.Contributor.LastName;
        //                            contribution.OldEmployerName = finalizeRecord.Contributor.Employer.EmployerName;

        //                            RecordWithDiffernetContribution.Add(contribution);
        //                            #endregion
        //                        }

        //                    }
        //                    else
        //                    {
        //                        #region No entry in Unfinalized
        //                        ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
        //                        unfinalizeRecord.MapTo(History);
        //                        History.ReplaceContributorID = conId;
        //                        History.DetailID = unfinalizeRecord.Id;
        //                        History.ReplaceHeaderID = newHeaderId;
        //                        db.Entry(History).State = EntityState.Added;
        //                        var result = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Update unfinalised Details
        //                            unfinalizeRecord.ContributorID = conId; unfinalizeRecord.ContributonSheetHeaderID = newHeaderId;
        //                            db.Entry(unfinalizeRecord).State = EntityState.Modified;
        //                            db.SaveChanges();
        //                        }

        //                        ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
        //                        entityFinalise.EmployerId = EmployerId;
        //                        entityFinalise.Month = getHeaderRecord.Month;
        //                        entityFinalise.Year = getHeaderRecord.Year;
        //                        entityFinalise.IsActive = true;
        //                        entityFinalise.ContributonSheetHeaderId = newHeaderId;
        //                        db.Entry(entityFinalise).State = EntityState.Added;
        //                        var results = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Manage history
        //                            ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                            finalizeRecord.MapTo(finaliseHistory);
        //                            finaliseHistory.ReplaceContributorID = conId;
        //                            finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                            finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
        //                            db.Entry(finaliseHistory).State = EntityState.Added;
        //                            result = db.SaveChanges();
        //                            if (result > 0)
        //                            {
        //                                //Update finalised Details
        //                                if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
        //                                    finalizeRecord.IsActive = false;
        //                                else
        //                                { finalizeRecord.ContributorID = conId; finalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id; }
        //                                db.Entry(finalizeRecord).State = EntityState.Modified;
        //                                db.SaveChanges();
        //                            }
        //                        }
        //                        #endregion
        //                    }
        //                }
        //                else
        //                {
        //                    #region create new when no record exist
        //                    //create New Header
        //                    ContributonSheetHeader entity = new ContributonSheetHeader();
        //                    entity.EmployerId = EmployerId;
        //                    entity.Month = getHeaderRecord.Month;
        //                    entity.Year = getHeaderRecord.Year;
        //                    entity.IsActive = true;
        //                    entity.Finalized = 1;
        //                    db.Entry(entity).State = EntityState.Added;
        //                    var result = db.SaveChanges();
        //                    if (result > 0)
        //                    {
        //                        //Manage history
        //                        var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                        ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
        //                        unfinalizeRecord.MapTo(History);
        //                        History.ReplaceContributorID = conId;
        //                        History.DetailID = unfinalizeRecord.Id;
        //                        History.ReplaceHeaderID = entity.Id;
        //                        db.Entry(History).State = EntityState.Added;
        //                        result = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Update unfinalised Details
        //                            if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == unfinalizeRecord.ContributonSheetHeaderID))
        //                                unfinalizeRecord.IsActive = false;
        //                            else
        //                            { unfinalizeRecord.ContributorID = conId; unfinalizeRecord.ContributonSheetHeaderID = entity.Id; }
        //                            db.Entry(unfinalizeRecord).State = EntityState.Modified;
        //                            db.SaveChanges();
        //                        }
        //                    }
        //                    //Create new HeaderFinalize
        //                    ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
        //                    entityFinalise.EmployerId = EmployerId;
        //                    entityFinalise.Month = getHeaderRecord.Month;
        //                    entityFinalise.Year = getHeaderRecord.Year;
        //                    entityFinalise.IsActive = true;
        //                    entityFinalise.ContributonSheetHeaderId = entity.Id;
        //                    db.Entry(entityFinalise).State = EntityState.Added;
        //                    var results = db.SaveChanges();
        //                    if (result > 0)
        //                    {
        //                        //Manage history
        //                        ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                        finalizeRecord.MapTo(finaliseHistory);
        //                        finaliseHistory.ReplaceContributorID = conId;
        //                        finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                        finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
        //                        db.Entry(finaliseHistory).State = EntityState.Added;
        //                        result = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Update finalised Details
        //                            if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
        //                                finalizeRecord.IsActive = false;
        //                            else
        //                            { finalizeRecord.ContributorID = conId; finalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id; }
        //                            db.Entry(finalizeRecord).State = EntityState.Modified;
        //                            db.SaveChanges();
        //                        }
        //                    }
        //                    #endregion
        //                }
        //            }
        //            dbTransaction.Commit();
        //            list.Record = RecordWithDiffernetContribution;
        //            if (list.Record.Count == 0)
        //                list.success = 1;
        //            return list;
        //        }
        //        catch (Exception ex)
        //        {
        //            list.Error = ex.Message;
        //            dbTransaction.Rollback();
        //            return list;
        //        }
        //    }
        //}
        public JsonResult replaceRecordAjax(string Data, int Oper)
        {
            try
            {
                var data = Data.Split(',');
                int detailId = Convert.ToInt32(data[2]);
                int detailfinalizedId = Convert.ToInt32(data[3]);
                int newdetailId = Convert.ToInt32(data[0]);
                int newdetailfinalizedId = Convert.ToInt32(data[1]);
                if (Oper == 1)
                {

                    var entity = db.ContributonSheetDetails.Find(detailId);
                    entity.IsActive = false;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();

                    var entitys = db.ContributonSheetDetailsFinalise.Find(detailfinalizedId);
                    entitys.IsActive = false;
                    db.Entry(entitys).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    var detailsRecord = db.ContributonSheetDetails.Where(x => x.Id == detailId && x.IsActive == true).Select(x => new { x.ContributorID, x.ContributonSheetHeaderID }).FirstOrDefault();
                    var detailsfinalizedRecord = db.ContributonSheetDetailsFinalise.Where(x => x.Id == detailfinalizedId && x.IsActive == true).Select(x => new { x.ContributorID, x.ContributonSheetHeaderFinaliseID }).FirstOrDefault();

                    var entity = db.ContributonSheetDetails.Find(newdetailId);
                    entity.IsActive = false;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();

                    ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
                    entity.MapTo(History);
                    History.ContributorID = detailsRecord.ContributorID;
                    History.ContributonSheetHeaderID = detailsRecord.ContributonSheetHeaderID;
                    History.ReplaceContributorID = Convert.ToInt32(entity.ContributorID);
                    History.DetailID = entity.Id;
                    History.ReplaceHeaderID = entity.ContributonSheetHeaderID;
                    db.Entry(History).State = EntityState.Added;
                    db.SaveChanges();


                    var entitys = db.ContributonSheetDetailsFinalise.Find(newdetailfinalizedId);
                    entitys.IsActive = false;
                    db.Entry(entitys).State = EntityState.Modified;
                    db.SaveChanges();


                    ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
                    finaliseHistory.ContributorID = detailsfinalizedRecord.ContributorID;
                    finaliseHistory.ContributonSheetHeaderFinaliseID = detailsfinalizedRecord.ContributonSheetHeaderFinaliseID;
                    finaliseHistory.ReplaceContributorID = Convert.ToInt32(entitys.ContributorID);
                    finaliseHistory.DetailFinalizeID = entitys.Id;
                    finaliseHistory.ReplaceHeaderFinalizeID = entitys.ContributonSheetHeaderFinaliseID;
                    db.Entry(History).State = EntityState.Added;
                    db.SaveChanges();

                    var entityss = db.ContributonSheetDetails.Find(detailId);
                    entityss.ContributonSheetHeaderID = entity.ContributonSheetHeaderID;
                    entityss.ContributorID = entity.ContributorID;
                    db.Entry(entityss).State = EntityState.Modified;
                    db.SaveChanges();

                    var Entitys = db.ContributonSheetDetailsFinalise.Find(detailfinalizedId);
                    Entitys.ContributonSheetHeaderFinaliseID = entitys.ContributonSheetHeaderFinaliseID;
                    Entitys.ContributorID = entitys.ContributorID;
                    db.Entry(Entitys).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        //public MoveContributorContributionViewModel mergeWithDifferentEmployer(string[] Data, int conId, int oldconId, int oldEmpId, int EmployerId)
        //{
        //    MoveContributorContributionViewModel list = new MoveContributorContributionViewModel();
        //    using (var dbTransaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var header = db.ContributonSheetHeader.Where(x => x.IsActive == true && (x.EmployerId == EmployerId || x.EmployerId == oldEmpId)).ToList();
        //            var headerFinalize = db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && (x.EmployerId == EmployerId || x.EmployerId == oldEmpId)).ToList();
        //            var detailsFinalize = db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
        //            var details = db.ContributonSheetDetails.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
        //            for (int i = 0; i < Data.Length; i++)
        //            {
        //                var data = Data[i].Split('#');
        //                int headerId = Convert.ToInt32(data[1]);
        //                int detailId = Convert.ToInt32(data[0]);
        //                var getHeaderRecord = header.Where(x => x.Id == headerId).FirstOrDefault();
        //                var finalizeRecord = detailsFinalize.Where(x => x.Id == detailId).FirstOrDefault();
        //                if (header.Any(x => x.EmployerId == EmployerId && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year))
        //                {
        //                    var newHeaderId = header.Where(x => x.EmployerId == EmployerId && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year).Select(x => x.Id).FirstOrDefault();
        //                    var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                    //Manage history
        //                    ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
        //                    unfinalizeRecord.MapTo(History);
        //                    History.ReplaceContributorID = conId;
        //                    History.DetailID = unfinalizeRecord.Id;
        //                    History.ReplaceHeaderID = newHeaderId;
        //                    db.Entry(History).State = EntityState.Added;
        //                    var result = db.SaveChanges();
        //                    if (result > 0)
        //                    {
        //                        //Update unfinalised Details
        //                        if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == unfinalizeRecord.ContributonSheetHeaderID))
        //                            unfinalizeRecord.IsActive = false;
        //                        else
        //                        { unfinalizeRecord.ContributorID = conId; unfinalizeRecord.ContributonSheetHeaderID = newHeaderId; }
        //                        db.Entry(unfinalizeRecord).State = EntityState.Modified;
        //                        db.SaveChanges();
        //                    }

        //                    //check record exist in finalized Header
        //                    if (headerFinalize.Any(x => x.ContributonSheetHeaderId == newHeaderId && x.EmployerId == EmployerId && x.ContributonSheetHeaderId == getHeaderRecord.Id && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year))
        //                    {
        //                        //Manage history
        //                        var newHeaderFinalizedId = headerFinalize.Where(x => x.ContributonSheetHeaderId == newHeaderId && x.EmployerId == EmployerId && x.ContributonSheetHeaderId == getHeaderRecord.Id && x.Month == getHeaderRecord.Month && x.Year == getHeaderRecord.Year).Select(x => x.Id).FirstOrDefault();
        //                        ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                        finalizeRecord.MapTo(finaliseHistory);
        //                        finaliseHistory.ReplaceContributorID = conId;
        //                        finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                        finaliseHistory.ReplaceHeaderFinalizeID = newHeaderFinalizedId;
        //                        db.Entry(finaliseHistory).State = EntityState.Added;
        //                        result = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //update Finalized Details
        //                            if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
        //                                finalizeRecord.IsActive = false;
        //                            else
        //                            { finalizeRecord.ContributorID = conId; finalizeRecord.ContributonSheetHeaderFinaliseID = newHeaderFinalizedId; }
        //                            db.Entry(finalizeRecord).State = EntityState.Modified;
        //                            db.SaveChanges();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //Create new HeaderFinalize
        //                        ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
        //                        entityFinalise.EmployerId = EmployerId;
        //                        entityFinalise.Month = getHeaderRecord.Month;
        //                        entityFinalise.Year = getHeaderRecord.Year;
        //                        entityFinalise.IsActive = true;
        //                        entityFinalise.ContributonSheetHeaderId = newHeaderId;
        //                        db.Entry(entityFinalise).State = EntityState.Added;
        //                        var results = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Manage history
        //                            ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                            finalizeRecord.MapTo(finaliseHistory);
        //                            finaliseHistory.ReplaceContributorID = conId;
        //                            finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                            finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
        //                            db.Entry(finaliseHistory).State = EntityState.Added;
        //                            result = db.SaveChanges();
        //                            if (result > 0)
        //                            {
        //                                //Update finalised Details
        //                                if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
        //                                    finalizeRecord.IsActive = false;
        //                                else
        //                                { finalizeRecord.ContributorID = conId; finalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id; }
        //                                db.Entry(finalizeRecord).State = EntityState.Modified;
        //                                db.SaveChanges();
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    //create New Header
        //                    ContributonSheetHeader entity = new ContributonSheetHeader();
        //                    entity.EmployerId = EmployerId;
        //                    entity.Month = getHeaderRecord.Month;
        //                    entity.Year = getHeaderRecord.Year;
        //                    entity.IsActive = true;
        //                    entity.Finalized = 1;
        //                    db.Entry(entity).State = EntityState.Added;
        //                    var result = db.SaveChanges();
        //                    if (result > 0)
        //                    {
        //                        //Manage history
        //                        var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                        ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
        //                        unfinalizeRecord.MapTo(History);
        //                        History.ReplaceContributorID = conId;
        //                        History.DetailID = unfinalizeRecord.Id;
        //                        History.ReplaceHeaderID = entity.Id;
        //                        db.Entry(History).State = EntityState.Added;
        //                        result = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Update unfinalised Details
        //                            if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == unfinalizeRecord.ContributonSheetHeaderID))
        //                                unfinalizeRecord.IsActive = false;
        //                            else
        //                            { unfinalizeRecord.ContributorID = conId; unfinalizeRecord.ContributonSheetHeaderID = entity.Id; }
        //                            db.Entry(unfinalizeRecord).State = EntityState.Modified;
        //                            db.SaveChanges();
        //                        }
        //                    }
        //                    //Create new HeaderFinalize
        //                    ContributonSheetHeaderFinalise entityFinalise = new ContributonSheetHeaderFinalise();
        //                    entityFinalise.EmployerId = EmployerId;
        //                    entityFinalise.Month = getHeaderRecord.Month;
        //                    entityFinalise.Year = getHeaderRecord.Year;
        //                    entityFinalise.IsActive = true;
        //                    entityFinalise.ContributonSheetHeaderId = entity.Id;
        //                    db.Entry(entityFinalise).State = EntityState.Added;
        //                    var results = db.SaveChanges();
        //                    if (result > 0)
        //                    {
        //                        //Manage history
        //                        ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                        finalizeRecord.MapTo(finaliseHistory);
        //                        finaliseHistory.ReplaceContributorID = conId;
        //                        finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                        finaliseHistory.ReplaceHeaderFinalizeID = entityFinalise.Id;
        //                        db.Entry(finaliseHistory).State = EntityState.Added;
        //                        result = db.SaveChanges();
        //                        if (result > 0)
        //                        {
        //                            //Update finalised Details
        //                            if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
        //                                finalizeRecord.IsActive = false;
        //                            else
        //                            { finalizeRecord.ContributorID = conId; finalizeRecord.ContributonSheetHeaderFinaliseID = entityFinalise.Id; }
        //                            db.Entry(finalizeRecord).State = EntityState.Modified;
        //                            db.SaveChanges();
        //                        }
        //                    }
        //                }
        //            }
        //            dbTransaction.Commit();
        //        }
        //        catch (Exception ex)
        //        { dbTransaction.Rollback(); }
        //    }
        //    return "";
        //}
        //public MoveContributorContributionViewModel mergeWithSameEmployer(string[] Data, int conId, int oldconId)
        //{
        //    MoveContributorContributionViewModel list = new MoveContributorContributionViewModel();            
        //    int result = 0;
        //    using (var dbTransaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {

        //            var detailsFinalize = db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
        //            var details = db.ContributonSheetDetails.Where(x => x.IsActive == true && (x.ContributorID == conId || x.ContributorID == oldconId)).ToList();
        //            for (int i = 0; i < Data.Length; i++)
        //            {
        //                var data = Data[i].Split('#');
        //                int headerId = Convert.ToInt32(data[1]);
        //                int detailId = Convert.ToInt32(data[0]);
        //                var finalizeRecord = detailsFinalize.Where(x => x.Id == detailId).FirstOrDefault();
        //                ContributonSheetDetailsFinaliseHistory finaliseHistory = new ContributonSheetDetailsFinaliseHistory();
        //                finalizeRecord.MapTo(finaliseHistory);
        //                finaliseHistory.ReplaceContributorID = conId;
        //                finaliseHistory.DetailFinalizeID = finalizeRecord.Id;
        //                finaliseHistory.ReplaceHeaderFinalizeID = finalizeRecord.ContributonSheetHeaderFinaliseID;
        //                db.Entry(finaliseHistory).State = EntityState.Added;
        //                result = db.SaveChanges();
        //                if (result > 0)
        //                {
        //                    if (detailsFinalize.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderFinaliseID == finalizeRecord.ContributonSheetHeaderFinaliseID))
        //                        finalizeRecord.IsActive = false;
        //                    else
        //                        finalizeRecord.ContributorID = conId;
        //                    db.Entry(finalizeRecord).State = EntityState.Modified;
        //                    db.SaveChanges();
        //                }

        //                var unfinalizeRecord = details.Where(x => x.ContributonSheetHeaderID == headerId && x.ContributorID == oldconId && x.SourceID == finalizeRecord.SourceID).FirstOrDefault();
        //                ContributonSheetDetailsHistory History = new ContributonSheetDetailsHistory();
        //                unfinalizeRecord.MapTo(History);
        //                History.ReplaceContributorID = conId;
        //                History.DetailID = unfinalizeRecord.Id;
        //                History.ReplaceHeaderID = unfinalizeRecord.ContributonSheetHeaderID;
        //                db.Entry(History).State = EntityState.Added;
        //                result = db.SaveChanges();
        //                if (result > 0)
        //                {
        //                    if (details.Any(x => x.ContributorID == conId && x.SourceID == finalizeRecord.SourceID && x.ContributonSheetHeaderID == unfinalizeRecord.ContributonSheetHeaderID))
        //                        unfinalizeRecord.IsActive = false;
        //                    else
        //                        unfinalizeRecord.ContributorID = conId;
        //                    db.Entry(unfinalizeRecord).State = EntityState.Modified;
        //                    db.SaveChanges();
        //                }
        //            }
        //            dbTransaction.Commit();
        //            if (list.Record.Count == 0)
        //                list.success = result;
        //            return list;
        //        }
        //        catch (Exception ex)
        //        {
        //            list.Error = ex.Message;
        //            dbTransaction.Rollback();
        //            return list;
        //        }
        //    }
        //}
        public JsonResult GetContributorAjax(int EmpId)
        {
            var result = db.MasterContributor.Where(x => x.Employer.Id == EmpId).Select(x => new { x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName + " ( " + x.PersonID + " )" });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}