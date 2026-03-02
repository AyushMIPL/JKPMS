using App.Data;
using App.Data.Entities;
using App.Data.ViewModels;
using JKPS.BLL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace App.Web.Repository
{
    public class PensionAndRefundRepo
    {
        //static AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        static AppDbContext db;

        public PensionAndRefundRepo()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        //to Calculate Refund Estimate, if estimateStatus is Yes than Calculation on Estimation
        public static refundViewModel GetRefundEstimated(int? PersonID, DateTime startDate, DateTime endDate, string estimateStatus)
        {
            var contributorDetails = db.MasterContributor.Where(x => x.Id == PersonID).FirstOrDefault();
            var tempCheckPoliceForce = contributorDetails.Employer.EmployerTypeID;
            refundViewModel model = new refundViewModel();
            //DateTime startDate = Convert.ToDateTime(StartDate);
            //DateTime endDate = Convert.ToDateTime(EndDate);
            //int totalservicelengthinMonth = Convert.ToInt32(endDate.Subtract(startDate).Days / (365.25 / 12));
            //int totalservicelengthinYear = totalservicelengthinMonth / 12;
            double totalservicelengthinYear = (endDate.Subtract(startDate).Days + 1) / (365.25);
            if ((totalservicelengthinYear < 10 && tempCheckPoliceForce == (int)PensionType.Public) || (totalservicelengthinYear < 2 && tempCheckPoliceForce == (int)PensionType.Police))
            {
                double interestRatetemp = 0;
                if (!db.MasterInterestRate.Where(x => x.DateFrom <= endDate && (!x.DateTo.HasValue ? endDate : x.DateTo) >= endDate && x.EmployerTypeID == tempCheckPoliceForce && x.IsActive == true).Any())
                {
                    model.error = "InterestRate Not defined for that period";
                    return model;
                }
                else
                {
                    var interestRate = db.MasterInterestRate.Where(x => x.DateFrom <= endDate && (!x.DateTo.HasValue ? endDate : x.DateTo) >= endDate && x.EmployerTypeID == tempCheckPoliceForce && x.IsActive == true).Select(x => x.InterestRate).FirstOrDefault();
                    interestRatetemp = Convert.ToDouble(interestRate / 100);
                    model.interestRate = interestRate.ToString();
                }
                //double interestRatetemp = (db.MasterInterestRate.Any() ? (double)db.MasterInterestRate.FirstOrDefault().InterestRate : 0) / 100;
                double carriedfwd = 0.0;
                int ServiceLength = 0;
                double TotalContributionPerYear = 0.0;
                double TotalInterestPerYear = 0.0;
                double TotalRefundAmountPerYear = 0.0;
                int highestMonth = 0;

                ContributonSheetDetailsFinalise finalizeSheet = new ContributonSheetDetailsFinalise();
                List<ContributonSheetDetailsFinalise> finalizeSheetList = new List<ContributonSheetDetailsFinalise>();

                ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
                using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
                {
                    List<ContributonSheetDetailsFinalise> _ContributonSheetDetailsFinalise = dbContext.ContributonSheetDetailsFinalise.Where(x => x.ContributorID == (PersonID == null ? 0 : PersonID) && x.IsActive == true).ToList();
                    var allYearList = from a in _ContributonSheetDetailsFinalise
                                      join b in dbContext.ContributonSheetHeaderFinalise on a.ContributonSheetHeaderFinaliseID equals b.Id
                                      where a.ContributorID == (PersonID == null ? 0 : PersonID) && a.IsActive == true && b.IsActive == true
                                      select new
                                      {
                                          b.Id,
                                          b.ContributonSheetHeaderId,
                                          a.EmployerContribution,
                                          b.Month,
                                          b.Year,
                                          a.ContributorContribution,
                                          a.EntryDate,
                                      };

                    foreach (var item in allYearList)
                    {
                        finalizeSheet = new ContributonSheetDetailsFinalise();
                        int Month = DateTime.ParseExact(item.Month, "MMMM", CultureInfo.CurrentCulture).Month;
                        int monthLastDate = DateTime.DaysInMonth(item.Year, Month);
                        DateTime date = Convert.ToDateTime(Month + "/" + monthLastDate + "/" + item.Year);
                        if (item.Year == DateTime.Now.Year && Month == DateTime.Now.Month)
                        {
                            date = DateTime.Now;
                        }
                        finalizeSheet.EntryDate = date;
                        //finalizeSheet.ContributonSheetHeaderFinaliseID = item.ContributonSheetHeaderId;//This is commented by Neeraj on date 21/08/2015
                        finalizeSheet.ContributonSheetHeaderFinaliseID = item.Id;
                        finalizeSheet.ContributorContribution = item.ContributorContribution;
                        finalizeSheetList.Add(finalizeSheet);
                    }
                }
                //add Estimate Contribution
                #region checkTheNumberOfYear
                if (estimateStatus == "Yes")
                {
                    //interestRatetemp = Convert.ToDouble(db.MasterInterestRate.Where(x => x.DateFrom >= endDate && (!x.DateTo.HasValue ? endDate : x.DateTo) <= endDate && x.EmployerTypeID == tempCheckPoliceForce).Select(x => new { interestRate = (x.InterestRate == null ? 0 : (x.InterestRate / 100)) }).FirstOrDefault());
                    DateTime currentDate = DateTime.Now;
                    if (endDate > currentDate)
                    {

                        for (var dt = currentDate; dt <= endDate; dt = dt.AddMonths(1))
                        {
                            var contributiondate = dt.Date;
                            //get employer pf rate from employer history table
                            var AllPfRateHistoryContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate <= contributiondate && x.EffectiveEndDate >= contributiondate && x.PersonID == contributorDetails.PersonID).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
                            if (AllPfRateHistoryContributor != null)
                            {
                                var RateToBeAppliedOnContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryContributor.EffectiveDate && x.PersonID == contributorDetails.PersonID).FirstOrDefault();
                                ContributonSheetDetailsFinalise tempListEstimates = new ContributonSheetDetailsFinalise();
                                tempListEstimates.ContributonSheetHeaderFinaliseID = 0;
                                tempListEstimates.ContributorContribution = Math.Round(((contributorDetails.SalaryAmount * RateToBeAppliedOnContributor.PFRate) / 1200), 2);
                                tempListEstimates.EntryDate = dt;
                                finalizeSheetList.Add(tempListEstimates);
                            }
                            //ContributonSheetDetailsFinalise tempListEstimates = new ContributonSheetDetailsFinalise();
                            //tempListEstimates.ContributonSheetHeaderFinaliseID = 0;
                            //tempListEstimates.ContributorContribution = Math.Round(((contributorDetails.SalaryAmount * contributorDetails.PFRate) / 1200), 2);
                            //tempListEstimates.EntryDate = dt;
                            //finalizeSheetList.Add(tempListEstimates);
                        }
                    }
                }
                #endregion
                var newfinalizeSheetList = finalizeSheetList.Where(x => (x.EntryDate >= startDate && x.EntryDate <= endDate) || (x.EntryDate.Month == endDate.Month && x.EntryDate.Year == endDate.Year)).ToList();
                var newfinalizeSheetListfinal = newfinalizeSheetList.OrderBy(x => x.EntryDate);


                for (int i = startDate.Year; i <= endDate.Year; i++)
                {
                    //List Per Year
                    var perYearList = (from perYear in newfinalizeSheetList
                                       where Convert.ToDateTime(perYear.EntryDate).Year == i
                                       orderby Convert.ToDateTime(perYear.EntryDate).Month
                                       select perYear).ToList();

                    if (perYearList.Any())
                    {
                        //Find Highest Month
                        //var highestMonth = (from h in perYearList
                        //										select Convert.ToDateTime(h.EntryDate).Month).Max();


                        //Action Date Contribution
                        if (i == endDate.Year)
                        {
                            if (newfinalizeSheetListfinal.Where(x => x.EntryDate.Month == endDate.Month && x.EntryDate.Year == endDate.Year).Any())
                                highestMonth = endDate.Month;
                            else
                                highestMonth = endDate.Month - 1;
                            //var monthEndDate = DateTime.DaysInMonth(endDate.Year, endDate.Month);
                            //if (monthEndDate == endDate.Day)
                            //  highestMonth = endDate.Month;
                            //else
                            //  highestMonth = endDate.Month - 1;
                        }
                        else
                        {
                            highestMonth = 12;
                        }
                        //Find Lowest Month
                        var lowestMonth = (from l in perYearList
                                           select Convert.ToDateTime(l.EntryDate).Month).Min();

                        if (i == startDate.Year)
                        {
                            //Carried Fwd 0
                            carriedfwd = 0.0;
                        }
                        else
                        {
                            carriedfwd = 0.0;
                            carriedfwd = (TotalRefundAmountPerYear) * (interestRatetemp) * ((double)(highestMonth) / 12);
                            TotalRefundAmountPerYear = TotalRefundAmountPerYear + carriedfwd;
                            TotalInterestPerYear = TotalInterestPerYear + carriedfwd;
                        }

                        //Loop on Month 

                        double InterestMonthly = 0.0;
                        double RefundMonthly = 0.0;
                        for (int month = lowestMonth; month <= highestMonth; month++)
                        {
                            //Get Monthly Contribution



                            var ContributorContribution = from x in perYearList
                                                          where x.EntryDate.Year == i && x.EntryDate.Month == month
                                                          select new { x.ContributorContribution };



                            if (ContributorContribution.Any())
                            {
                                decimal sum = ContributorContribution.Select(t => t.ContributorContribution).Sum();
                                //Monthly Calculation
                                //ServiceLength++;
                                //InterestMonthly = (Convert.ToDouble(ContributorContribution.First().ContributorContribution)) * (interestRatetemp) * ((double)(highestMonth - month) / 12);
                                //RefundMonthly = Convert.ToDouble(ContributorContribution.First().ContributorContribution) + InterestMonthly;
                                ServiceLength++;
                                InterestMonthly = (Convert.ToDouble(sum)) * (interestRatetemp) * ((double)(highestMonth - month) / 12);
                                RefundMonthly = Convert.ToDouble(sum) + InterestMonthly;


                                //Add Monthly Calculation
                                //TotalContributionPerYear = TotalContributionPerYear + Convert.ToDouble(ContributorContribution.First().ContributorContribution);
                                //TotalInterestPerYear = TotalInterestPerYear + InterestMonthly;
                                //TotalRefundAmountPerYear = TotalRefundAmountPerYear + RefundMonthly;
                                TotalContributionPerYear = TotalContributionPerYear + Convert.ToDouble(sum);
                                TotalInterestPerYear = TotalInterestPerYear + InterestMonthly;
                                TotalRefundAmountPerYear = TotalRefundAmountPerYear + RefundMonthly;
                            }
                        }
                    }
                    else
                    {
                        if (i != endDate.Year)
                        {
                            if (i != startDate.Year)
                            {
                                carriedfwd = 0.0;
                                carriedfwd = (TotalRefundAmountPerYear) * (interestRatetemp);
                                TotalRefundAmountPerYear = TotalRefundAmountPerYear + carriedfwd;
                                TotalInterestPerYear = TotalInterestPerYear + carriedfwd;
                            }
                        }
                        else
                        {
                            highestMonth = endDate.Month - 1;
                            carriedfwd = 0.0;
                            carriedfwd = (TotalRefundAmountPerYear) * (interestRatetemp) * ((double)(highestMonth) / 12);
                            TotalRefundAmountPerYear = TotalRefundAmountPerYear + carriedfwd;
                            TotalInterestPerYear = TotalInterestPerYear + carriedfwd;
                        }
                    }


                }

                model.ServiceLength = ServiceLength.ToString();
                model.TotalInterestPerYear = Math.Round(TotalInterestPerYear, 2).ToString();
                model.TotalContributionPerYear = Math.Round(TotalContributionPerYear, 2).ToString();
                model.TotalRefundAmountPerYear = Math.Round(TotalRefundAmountPerYear, 2).ToString();
                return model;
            }
            else
            {
                if (tempCheckPoliceForce == (int)PensionType.Police)
                {
                    model.error = "Service Length Should be Less than 2 Years";
                    return model;
                }
                model.error = "Service Length Should be Less than 10 Years";
                return model;
            }

        }

        //to Calculate Pension    
        public static PensionCalculationViewModel CalculatePolicePensionDetails(DateTime retirementOrResignationDate, int PersonID, string status, string estimateStatus)
        {
            var model = (from a in db.MasterContributor
                         join b in db.ContributorSalaryHistory on a.PersonID equals b.PersonID
                         join e in db.MasterEmployer on a.EmployerID equals e.Id
                         join t in db.MasterEmployerType on e.EmployerTypeID equals t.Id
                         where (a.Id == PersonID && b.Default == true)
                         select new { b.SalaryAmount, a.FirstAppointmentDate, a.ExpectedRetirementDate, employerType = t.Id, a.DateOfBirth, JoinBefore2004 = t.RetirementAge, JoinAfter2004 = t.RetirementAgeAfter2004 }).FirstOrDefault();
            PensionCalculationViewModel vmodel = new PensionCalculationViewModel();
            DateTime FirstAppointmentDate = Convert.ToDateTime(model.FirstAppointmentDate);
            //DateTime retirementOrResignationDate = Convert.ToDateTime(RetirementOrResignationDate);
            decimal salary1 = 0;
            decimal salary2 = 0;
            decimal MaxPension = Math.Round((2 * model.SalaryAmount) / 3);
            var startday = FirstAppointmentDate.Day;
            var monthEndDate = DateTime.DaysInMonth(retirementOrResignationDate.Year, retirementOrResignationDate.Month);
            var endday = retirementOrResignationDate.Day;
            if (startday != 1)
            {
                FirstAppointmentDate = FirstAppointmentDate.AddMonths(+1).AddDays(-(startday - 1));
            }
            if (endday != monthEndDate)
            {
                retirementOrResignationDate = retirementOrResignationDate.AddDays(-endday);
            }
            DateTime DateOfBirth = Convert.ToDateTime(model.DateOfBirth);
            DateTime expectedRetirementDate = Convert.ToDateTime(model.ExpectedRetirementDate);
            decimal fullPensionAnnually = 0;
            decimal fullPensionMonthly = 0;
            decimal reducedPEnsionAnnually = 0;
            decimal reducedPensionMonthly = 0;
            decimal Gratuitydue = 0;
            int remainingMonth = 0;
            string serviceType = "";
            int serviceLengthBefore20YearOfService = 0;
            int serviceLengthAfter20halfTo30Years = 0;
            int yearsRemainingtoEarlyRetirement = 0;
            var years = Convert.ToInt32(Math.Truncate((retirementOrResignationDate.Subtract(model.DateOfBirth.Value).Days + 1) / (365.25)));
            if (model.FirstAppointmentDate.Value.Year >= 2004)
                yearsRemainingtoEarlyRetirement = model.JoinAfter2004 - years;
            else
                yearsRemainingtoEarlyRetirement = model.JoinBefore2004 - years;

            int totalservicelengthinMonth = Convert.ToInt32(retirementOrResignationDate.Subtract(FirstAppointmentDate).Days / (365.25 / 12));
            //int totalservicelengthinYear = totalservicelengthinMonth / 12;
            double totalservicelengthinYear = (retirementOrResignationDate.Subtract(FirstAppointmentDate).Days + 1) / (365.25);
            #region ServiceLengthGreaterThan2
            if (totalservicelengthinYear >= 2)
            {
                int retirementAge = Convert.ToInt32(Math.Truncate((retirementOrResignationDate.Subtract(DateOfBirth).Days + 1) / 365.25));
                decimal multiplier = db.MasterMultiplerForPoliceForce.Where(x => x.ContributorAge == retirementAge).Select(x => x.MultiplierFactor).FirstOrDefault();

                #region FullRetirement
                if (totalservicelengthinYear >= 30 || retirementAge == 55)
                {
                    serviceLengthBefore20YearOfService = Convert.ToInt32(totalservicelengthinYear);
                    serviceType = "FullRetirement";
                    if (totalservicelengthinYear > 30)
                    {
                        totalservicelengthinYear = 30;
                    }
                    fullPensionAnnually = (model.SalaryAmount * Convert.ToInt32(totalservicelengthinYear)) / 60;
                    if (totalservicelengthinYear > 20)
                    {
                        remainingMonth = ((totalservicelengthinMonth) - ((20 * 12) + 6)) / 6;
                        fullPensionAnnually = (((model.SalaryAmount * 20) / 60) + ((model.SalaryAmount * remainingMonth) / 60));
                        serviceLengthBefore20YearOfService = 20;
                        serviceLengthAfter20halfTo30Years = remainingMonth;
                    }

                    fullPensionMonthly = Math.Round((fullPensionAnnually / 12), 2);
                    reducedPEnsionAnnually = Math.Round((fullPensionAnnually * 3 / 4), 2);
                    reducedPensionMonthly = Math.Round((reducedPEnsionAnnually / 12), 2);
                    Gratuitydue = Math.Round(((fullPensionAnnually / 400) * multiplier), 2);
                    salary1 = Math.Round(model.SalaryAmount, 2);
                }

                #endregion

                #region EarlyRetirement
                else if (totalservicelengthinYear >= 10 && totalservicelengthinYear < 30)
                {

                    serviceLengthBefore20YearOfService = Convert.ToInt32(totalservicelengthinYear);
                    serviceType = "EarlyRetirement";
                    //yearsRemainingtoEarlyRetirement = Convert.ToInt32(Math.Round((expectedRetirementDate.Subtract(retirementOrResignationDate).Days / (365.25 / 12)) / 12, 2));
                    fullPensionAnnually = ((model.SalaryAmount * Convert.ToInt32(totalservicelengthinYear)) / 60);
                    if (totalservicelengthinYear > 20)
                    {
                        remainingMonth = ((Convert.ToInt32(totalservicelengthinYear) * 12) - ((20 * 12) + 6)) / 6;
                        fullPensionAnnually = (((model.SalaryAmount * 20) / 60) + ((model.SalaryAmount * remainingMonth) / 60));
                        serviceLengthBefore20YearOfService = 20;
                        serviceLengthAfter20halfTo30Years = remainingMonth;
                    }
                    fullPensionMonthly = Math.Round((fullPensionAnnually / 12), 2);
                    reducedPEnsionAnnually = Math.Round((fullPensionAnnually * 3 / 4), 2);
                    reducedPensionMonthly = Math.Round((reducedPEnsionAnnually / 12), 2);
                    int discountRateId = db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= retirementOrResignationDate && (!x.DateTo.HasValue ? (DateTime.Now > retirementOrResignationDate ? DateTime.Now : retirementOrResignationDate) : x.DateTo) >= retirementOrResignationDate && x.EmployerTypeID == (int)PensionType.Police && x.IsActive == true).Select(x => x.Id).FirstOrDefault();
                    decimal discountGratuityPolice = 0;
                    if (discountRateId != 0)
                        discountGratuityPolice = db.MasterDiscountForGratuityDetails.Where(x => x.MainId == discountRateId && x.YearsToNormalRetirement == yearsRemainingtoEarlyRetirement && x.IsActive == true).Select(x => x.DiscountFactor).FirstOrDefault();

                    //var discountGratuityPolice = db.MasterDiscountForGratuity.Where(x => x.EmployerTypeID == model.employerType && x.YearsToNormalRetirement == yearsRemainingtoEarlyRetirement && x.IsActive == true).Select(x => x.DiscountFactor).FirstOrDefault();
                    Gratuitydue = Math.Round(((fullPensionAnnually * Convert.ToDecimal(0.25 * 12.5)) * discountGratuityPolice), 2);
                    salary1 = Math.Round(model.SalaryAmount, 2);
                }
                #endregion

                #region ShortRetirement
                else if (totalservicelengthinYear >= 2 && totalservicelengthinYear < 10)
                {
                    salary1 = Math.Round(model.SalaryAmount, 2);
                    if ((totalservicelengthinMonth / 12) < 3)
                    {
                        salary1 = 0;
                        salary2 = Math.Round(model.SalaryAmount, 2);
                    }
                    serviceType = "ShortRetirement";
                    serviceLengthBefore20YearOfService = Convert.ToInt32(totalservicelengthinYear);
                    //yearsRemainingtoEarlyRetirement = Convert.ToInt32(Math.Round((expectedRetirementDate.Subtract(retirementOrResignationDate).Days / (365.25 / 12)) / 12, 2));
                    fullPensionAnnually = ((model.SalaryAmount * Convert.ToInt32(totalservicelengthinYear)) / 60);
                    fullPensionMonthly = Math.Round((fullPensionAnnually / 12), 2);
                    reducedPEnsionAnnually = Math.Round((fullPensionAnnually * 3 / 4), 2);
                    reducedPensionMonthly = Math.Round((reducedPEnsionAnnually / 12), 2);

                    int discountRateId = db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= retirementOrResignationDate && (!x.DateTo.HasValue ? (DateTime.Now > retirementOrResignationDate ? DateTime.Now : retirementOrResignationDate) : x.DateTo) >= retirementOrResignationDate && x.EmployerTypeID == (int)PensionType.Police && x.IsActive == true).Select(x => x.Id).FirstOrDefault();
                    decimal discountGratuityPolice = 0;
                    if (discountRateId != 0)
                        discountGratuityPolice = db.MasterDiscountForGratuityDetails.Where(x => x.MainId == discountRateId && x.YearsToNormalRetirement == yearsRemainingtoEarlyRetirement && x.IsActive == true).Select(x => x.DiscountFactor).FirstOrDefault();

                    //var discountGratuityPolice = db.MasterDiscountForGratuity.Where(x => x.EmployerTypeID == model.employerType && x.YearsToNormalRetirement == yearsRemainingtoEarlyRetirement && x.IsActive == true).Select(x => x.DiscountFactor).FirstOrDefault();
                    int monthsBatch = (totalservicelengthinMonth) / 6;
                    Gratuitydue = Math.Round((((monthsBatch * model.SalaryAmount / 24)) * discountGratuityPolice), 2);
                }
                #endregion
                #region Injury
                if (status == "True")
                {

                    fullPensionAnnually = (model.SalaryAmount * 70 / 100);
                    Gratuitydue = Math.Round(model.SalaryAmount * 375 / 1000, 2);
                    serviceType = "Injured";
                }
                #endregion

                vmodel.SalaryAmount = salary1;
                vmodel.SalaryAmount2 = salary2;
                vmodel.PensionType = serviceType;
                vmodel.LengthOfQualifyingServiceInMonthsTo31Dec2003 = serviceLengthBefore20YearOfService;
                vmodel.LengthOfQualifyingServiceInMonthsFrom1Jan2014 = serviceLengthAfter20halfTo30Years;
                vmodel.FullPension = fullPensionAnnually;
                vmodel.Gratuity = Gratuitydue;
                vmodel.ReducedPension = reducedPEnsionAnnually;
                vmodel.MaxPension = MaxPension;
                return vmodel;
            }
            else
            {
                vmodel.error = "Service Length should be Greater than 2 Years";
                return vmodel;
            }
            #endregion
        }

        public static string GetMonthNumberFromAbbreviation(string mmm)
        {
            string[] monthAbbrev = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

            // Creates a TextInfo based on the "en-US" culture.
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            string monthname = myTI.ToTitleCase(mmm.Trim().ToLower());
            int index = Array.IndexOf(monthAbbrev, monthname) + 1;
            return index.ToString("0#");
        }

        public static PensionCalculationViewModel CalculatePublicPensionDetails(DateTime retirementOrResignationDate, int PersonID, string estimateStatus)
        {
            var model = (from a in db.MasterContributor
                         join b in db.ContributorSalaryHistory on a.PersonID equals b.PersonID
                         join e in db.MasterEmployer on a.EmployerID equals e.Id
                         join t in db.MasterEmployerType on e.EmployerTypeID equals t.Id
                         where (a.Id == PersonID && b.Default == true)
                         select new { b.SalaryAmount, a.FirstAppointmentDate, a.ExpectedRetirementDate, employerType = t.Id, a.DateOfBirth, JoinBefore2004 = t.RetirementAge, JoinAfter2004 = t.RetirementAgeAfter2004 }).FirstOrDefault();

            PensionCalculationViewModel vmodel = new PensionCalculationViewModel();
            //BLLPayrollFunctions PayrollFunctions = new BLLPayrollFunctions();

            if (model.ExpectedRetirementDate == null)
            {
                vmodel.error = "Expected Retirement date for this Contributor is not set, Please set Expected Retirement date first. Contributor Details -> Edit -> Job Details ";
                return vmodel;
            }

            DateTime FirstAppointmentDate = Convert.ToDateTime(model.FirstAppointmentDate);
            int totalservicelengthinMonth = Convert.ToInt32(retirementOrResignationDate.Subtract(FirstAppointmentDate).Days / (365.25 / 12));
            //int totalservicelengthinYear = Convert.ToInt32(retirementOrResignationDate.Subtract(FirstAppointmentDate).Days / (365.25));
            int totalservicelengthinYear = Convert.ToInt32(Math.Truncate((retirementOrResignationDate.Date.Subtract(FirstAppointmentDate.Date).Days + 1) / (365.25)));
            #region ServiceLengthGreaterThan10Year
            if (totalservicelengthinYear >= 10)
            {
                int currentMonthCont = 0;
                int CurrentmonthEndDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                DateTime CurrentmonthEndDate = Convert.ToDateTime(DateTime.Now.Month + "/" + CurrentmonthEndDay + "/" + DateTime.Now.Year);
                decimal ReducedPension = 0;
                decimal Gratuity;
                decimal DiscountedGratuity;
                decimal FullPension;
                decimal FullPension1 = 0;
                decimal FullPension2;
                decimal MaxPensionPre;
                int LengthOfQualifyingServiceInMonthsFrom1Jan2014;
                int LengthOfQualifyingServiceInMonthsTo31Dec2003 = 0;
                decimal MaxPension = Math.Round((2 * model.SalaryAmount) / 3);
                MaxPensionPre = MaxPension;
                var startday = FirstAppointmentDate.Day;
                var monthEndDate = DateTime.DaysInMonth(retirementOrResignationDate.Year, retirementOrResignationDate.Month);
                var endday = retirementOrResignationDate.Day;
                //var yearToNormalRetirement = (model.ExpectedRetirementDate.Value.Year - retirementOrResignationDate.Year);
                //var yearToNormalRetirement = Convert.ToInt32(Math.Truncate((model.ExpectedRetirementDate.Value.Subtract(retirementOrResignationDate).Days + 1) / (365.25)));//Commented as change requested by calculating year to normal retirement on dob
                var years = Convert.ToInt32(Math.Truncate((retirementOrResignationDate.Subtract(model.DateOfBirth.Value).Days + 1) / (365.25)));
                int yearToNormalRetirement = 0;
                if (model.FirstAppointmentDate.Value.Year >= 2004)
                    yearToNormalRetirement = model.JoinAfter2004 - years;
                else
                    yearToNormalRetirement = model.JoinBefore2004 - years;
                var no = new List<ContributonSheetHeaderFinalise>();

                ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
                using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
                {
                    no = (from a in dbContext.ContributonSheetHeaderFinalise
                          join b in dbContext.ContributonSheetDetailsFinalise on a.Id equals b.ContributonSheetHeaderFinaliseID
                          where (b.ContributorID == PersonID && a.IsActive == true && b.IsActive == true)
                          select a).ToList();
                }
                List<DateTime> dateArray = new List<DateTime>();
                foreach (var item in no)
                {
                    int Month = DateTime.ParseExact(item.Month.Trim(), "MMMM", CultureInfo.CurrentCulture).Month;
                    int monthLastDate = DateTime.DaysInMonth(item.Year, Month);
                    DateTime date = Convert.ToDateTime(Month + "/" + monthLastDate + "/" + item.Year);
                    if (item.Year == DateTime.Now.Year && Month == DateTime.Now.Month)
                    {
                        date = DateTime.Now;
                    }

                    if (!dateArray.Where(x => x == date).Any())
                    {
                        dateArray.Add(date);
                    }
                }
                if (CurrentmonthEndDate < retirementOrResignationDate.Date && !dateArray.Where(x => x.Month == CurrentmonthEndDate.Month && x.Year == CurrentmonthEndDate.Year).Any())
                {
                    currentMonthCont = 1;
                }
                int discountRateId = 0;
                discountRateId = db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= retirementOrResignationDate && (!x.DateTo.HasValue ? DateTime.Now : x.DateTo) >= retirementOrResignationDate && x.EmployerTypeID == (int)PensionType.Public && x.IsActive == true).Select(x => x.Id).FirstOrDefault();
                if (estimateStatus == "Yes")
                {
                    discountRateId = db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= retirementOrResignationDate && (!x.DateTo.HasValue ? retirementOrResignationDate : x.DateTo) >= retirementOrResignationDate && x.EmployerTypeID == (int)PensionType.Public && x.IsActive == true).Select(x => x.Id).FirstOrDefault();
                }
                decimal discount = 0;
                if (discountRateId != 0)
                {
                    discount = db.MasterDiscountForGratuityDetails.Where(x => x.MainId == discountRateId && x.YearsToNormalRetirement == yearToNormalRetirement && x.IsActive == true).Select(x => x.DiscountFactor).FirstOrDefault();
                    //double dFactor = Convert.ToDouble(1 + (masterDiscountForGratuityMain.DiscountFactorPercent / 100));
                    //discount = Convert.ToDecimal(1 / Math.Pow(dFactor, power));         
                }
                //discount = db.MasterDiscountForGratuityDetails.Where(x => x.MainId == discountRateId && x.YearsToNormalRetirement == yearToNormalRetirement && x.IsActive == true).Select(x => x.DiscountFactor).FirstOrDefault();

                //var discount = db.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == yearToNormalRetirement && x.EmployerTypeID != 1 && x.IsActive == true).Select(x => x.DiscountFactor).FirstOrDefault();
                if (startday != 1)
                {
                    FirstAppointmentDate = FirstAppointmentDate.AddMonths(+1).AddDays(-(startday - 1));
                }
                if (endday != monthEndDate)
                {
                    retirementOrResignationDate = retirementOrResignationDate.AddDays(-endday);
                }

                int ExtraMonths = Convert.ToInt32(retirementOrResignationDate.Subtract(DateTime.Now).Days / (365.25 / 12)) + currentMonthCont;
                if (ExtraMonths < 0 || estimateStatus != "Yes")
                {
                    ExtraMonths = 0;
                }
                var noOfMonthsWithContribution = dateArray.Where(x => x >= FirstAppointmentDate && x <= retirementOrResignationDate).Count();
                var pdate = new DateTime(2003, 12, 31);
                var sdate = new DateTime(2004, 01, 01);
                #region ifjoiningdateLessThan2004
                if (FirstAppointmentDate.Date < pdate.Date)
                {

                    LengthOfQualifyingServiceInMonthsTo31Dec2003 = Convert.ToInt32(pdate.Subtract(FirstAppointmentDate).Days / (365.25 / 12));

                    FullPension1 = Math.Round((LengthOfQualifyingServiceInMonthsTo31Dec2003 / 600M) * model.SalaryAmount);
                    LengthOfQualifyingServiceInMonthsFrom1Jan2014 = Convert.ToInt32(retirementOrResignationDate.Subtract(sdate).Days / (365.25 / 12));
                    noOfMonthsWithContribution = dateArray.Where(x => x >= pdate && x <= retirementOrResignationDate).Count();
                    LengthOfQualifyingServiceInMonthsFrom1Jan2014 = LengthOfQualifyingServiceInMonthsFrom1Jan2014 - (LengthOfQualifyingServiceInMonthsFrom1Jan2014 - noOfMonthsWithContribution) + (ExtraMonths < 0 ? 0 : ExtraMonths);
                    FullPension2 = Math.Round((LengthOfQualifyingServiceInMonthsFrom1Jan2014 / 960M) * model.SalaryAmount);

                    FullPension = FullPension1 + FullPension2;
                    Gratuity = Math.Round((MaxPension < FullPension ? MaxPension : FullPension) * 12.5M * 0.25M);
                    DiscountedGratuity = Math.Truncate((Gratuity * discount));
                    //var r=MaxPension < FullPension ? MaxPension : FullPension * 0.75M;
                    ReducedPension = Math.Round((MaxPension < FullPension ? MaxPension : FullPension) * 0.75M);
                }
                #endregion
                #region ifjoiningdateGreaterThan2004
                else
                {
                    LengthOfQualifyingServiceInMonthsFrom1Jan2014 = Convert.ToInt32(retirementOrResignationDate.Subtract(FirstAppointmentDate).Days / (365.25 / 12));
                    LengthOfQualifyingServiceInMonthsFrom1Jan2014 = LengthOfQualifyingServiceInMonthsFrom1Jan2014 - (LengthOfQualifyingServiceInMonthsFrom1Jan2014 - noOfMonthsWithContribution) + (ExtraMonths < 0 ? 0 : ExtraMonths);
                    FullPension2 = Math.Round((LengthOfQualifyingServiceInMonthsFrom1Jan2014 / 960M) * model.SalaryAmount);
                    FullPension = FullPension2;

                    Gratuity = Math.Round(Convert.ToDecimal(Convert.ToDouble(MaxPension < FullPension ? MaxPension : FullPension) * 12.5 * 0.25));
                    DiscountedGratuity = Math.Round(Gratuity * discount);
                    ReducedPension = Math.Round(Convert.ToDecimal(Convert.ToDouble(MaxPension < FullPension ? MaxPension : FullPension) * 0.75));
                }
                #endregion


                vmodel.LengthOfQualifyingServiceInMonthsFrom1Jan2014 = LengthOfQualifyingServiceInMonthsFrom1Jan2014;
                vmodel.LengthOfQualifyingServiceInMonthsTo31Dec2003 = LengthOfQualifyingServiceInMonthsTo31Dec2003;
                vmodel.FullPension = FullPension;
                vmodel.MaxPension = Math.Round(MaxPension, 2);
                vmodel.Gratuity = Gratuity;
                vmodel.SalaryAmount = model.SalaryAmount;
                vmodel.FullPension1 = FullPension1;
                vmodel.FullPension2 = FullPension2;
                vmodel.DiscountedGratuity = DiscountedGratuity;
                vmodel.ReducedPension = ReducedPension;
                return vmodel;
            }
            #endregion
            vmodel.error = "Service Length Should be Greater Than 10 Years";
            return vmodel;

        }


    }
}