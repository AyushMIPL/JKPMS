using App.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Collections.Generic;
using App.Data.ViewModels;
using System.Web.Routing;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading.Tasks;

namespace App.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Int32, AppUserLogin, AppUserRole, AppUserClaim>
    {
        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public AppDbContext(string connectionString)
          :
            //base(ConnectionStringProvider.ConnectionName)
            base(connectionString)
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            // Get the ObjectContext related to this DbContext
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 0;
        }
        public static AppDbContext Create()
        {
            ConnectionStringProvider connectionStringProvider = new ConnectionStringProvider();
            return new AppDbContext(connectionStringProvider.GetConnectionString());
        }

        public DbSet<UserProfile> UserProfiles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("AppUser");
            modelBuilder.Entity<AppRole>().ToTable("AppRole");
            modelBuilder.Entity<AppUserClaim>().ToTable("AppUserClaim");
            modelBuilder.Entity<AppUserLogin>().ToTable("AppUserLogin");
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRole");

            // Set AutoIncrement-Properties
            modelBuilder.Entity<AppUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppUserClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MasterDiscountForGratuityDetails>().Property(x => x.DiscountFactor).HasPrecision(18, 9);

            modelBuilder.Entity<MasterPensioner>().ToTable("MasterPensioner");
            modelBuilder.Entity<MasterCountry>().ToTable("MasterCountry");
            modelBuilder.Entity<MasterDesignation>().ToTable("MasterDesignation");
            modelBuilder.Entity<MasterDepartment>().ToTable("MasterDepartment");
            modelBuilder.Entity<MasterSuffix>().ToTable("MasterSuffix");
            modelBuilder.Entity<MasterPrefix>().ToTable("MasterPrefix");
            modelBuilder.Entity<MasterGrade>().ToTable("MasterGrade");
            modelBuilder.Entity<MasterPensionerType>().ToTable("MasterPensionerType");
            modelBuilder.Entity<MasterMaritalStatus>().ToTable("MasterMaritalStatus");
            modelBuilder.Entity<MasterMonthName>().ToTable("MasterMonthName");
            modelBuilder.Entity<MasterNationality>().ToTable("MasterNationality");
            modelBuilder.Entity<MasterPFRate>().ToTable("MasterPFRate");
            modelBuilder.Entity<MasterState>().ToTable("MasterState");
            modelBuilder.Entity<MasterCity>().ToTable("MasterCity");
            modelBuilder.Entity<MasterEmployer>().ToTable("MasterEmployer");
            modelBuilder.Entity<MasterEmployerPFRateDetails>().ToTable("MasterEmployerPFRateDetails");
            modelBuilder.Entity<MasterContributorPFRateDetails>().ToTable("MasterContributorPFRateDetails");
            modelBuilder.Entity<MasterContributor>().ToTable("MasterContributor");
            modelBuilder.Entity<MasterDependantDetails>().ToTable("MasterDependantDetails");
            modelBuilder.Entity<MasterRelationshipType>().ToTable("MasterRelationshipType");
            modelBuilder.Entity<MasterContributorMarriageDetails>().ToTable("MasterContributorMarriageDetails");
            modelBuilder.Entity<MasterContributorJobDetails>().ToTable("MasterContributorJobDetails");
            modelBuilder.Entity<MasterStatus>().ToTable("MasterStatus");
            modelBuilder.Entity<MasterSource>().ToTable("MasterSource");
            modelBuilder.Entity<MasterJobTitle>().ToTable("MasterJobTitle");
            modelBuilder.Entity<MasterBanks>().ToTable("MasterBanks");
            modelBuilder.Entity<MasterBankDetails>().ToTable("MasterBankDetails");
            modelBuilder.Entity<MasterDiscountForGratuity>().ToTable("MasterDiscountForGratuity");
            modelBuilder.Entity<MasterEmpBankDetails>().ToTable("MasterEmpBankDetails");
            modelBuilder.Entity<MasterEmployeeDeductions>().ToTable("MasterEmployeeDeductions");
            modelBuilder.Entity<MasterEmployeeIncomes>().ToTable("MasterEmployeeIncomes");
            modelBuilder.Entity<MasterEmployeeObligations>().ToTable("MasterEmployeeObligations");
            modelBuilder.Entity<MasterIncCodes>().ToTable("MasterIncCodes");
            modelBuilder.Entity<MasterDedcodes>().ToTable("MasterDedcodes");
            modelBuilder.Entity<MasterOblCodes>().ToTable("MasterOblCodes");
            modelBuilder.Entity<PensionApplications>().ToTable("PensionApplications");
            modelBuilder.Entity<RefundApplications>().ToTable("RefundApplications");
            modelBuilder.Entity<ContributonSheetDetails>().ToTable("ContributonSheetDetails");
            modelBuilder.Entity<ContributonSheetHeader>().ToTable("ContributonSheetHeader");
            modelBuilder.Entity<ContributonSheetDetailsFinalise>().ToTable("ContributonSheetDetailsFinalise");
            modelBuilder.Entity<ContributonSheetHeaderFinalise>().ToTable("ContributonSheetHeaderFinalise");
            modelBuilder.Entity<ContributonTransactions>().ToTable("ContributonTransactions");
            modelBuilder.Entity<ProcessDirectDepositHeader>().ToTable("ProcessDirectDepositHeader");
            modelBuilder.Entity<Process_DirectDeposit_Header>().ToTable("Process_DirectDeposit_Header");
            modelBuilder.Entity<ProcessDirectDepositDetails>().ToTable("ProcessDirectDepositDetails");
            modelBuilder.Entity<ProcessPayIncome>().ToTable("ProcessPayIncome");
            modelBuilder.Entity<ProcessPayDeductions>().ToTable("ProcessPayDeductions");
            modelBuilder.Entity<ProcessPayObligations>().ToTable("ProcessPayObligations");
            modelBuilder.Entity<ProcessPayPensioner>().ToTable("ProcessPayPensioner");
            modelBuilder.Entity<MasterEmployerType>().ToTable("MasterEmployerType");
            modelBuilder.Entity<PensionProcessDetails>().ToTable("PensionProcessDetails");
            modelBuilder.Entity<PensionProcessHeader>().ToTable("PensionProcessHeader ");
            modelBuilder.Entity<PensionerComments>().ToTable("PensionerComments");
            modelBuilder.Entity<ContributorComments>().ToTable("ContributorComments");
            modelBuilder.Entity<ContributorSalaryHistory>().ToTable("ContributorSalaryHistory");
            modelBuilder.Entity<FlexSegmentReference>().ToTable("FlexSegmentReference ");
            modelBuilder.Entity<FlexSegmentValueDetails>().ToTable("FlexSegmentValueDetails");
            modelBuilder.Entity<FlexStructDetails>().ToTable("FlexStructDetails");
            modelBuilder.Entity<FlexStructHeader>().ToTable("FlexStructHeader");
            modelBuilder.Entity<MasterInterestRate>().ToTable("MasterInterestRate");
            modelBuilder.Entity<PayControl>().ToTable("PayControl");
            modelBuilder.Entity<PayrollGLAccounts>().ToTable("PayrollGLAccounts");
            modelBuilder.Entity<DeductionsTaxTableHeader>().ToTable("DeductionsTaxTableHeader");
            modelBuilder.Entity<DeductionsTaxTableDetails>().ToTable("DeductionsTaxTableDetails");
            modelBuilder.Entity<SecModule>().ToTable("SecModule");
            modelBuilder.Entity<SecRoleModule>().ToTable("SecRoleModule");
            modelBuilder.Entity<GratuityDetails>().ToTable("GratuityDetails");
            modelBuilder.Entity<RefundPaidDetails>().ToTable("RefundPaidDetails");
            modelBuilder.Entity<ApprovalProcessLevel>().HasKey(r => new { r.ApprovalProcessLevelId }).ToTable("ApprovalProcessLevel");
            modelBuilder.Entity<ApplicationApprovalStatus>().HasKey(r => new { r.ApplicationApprovalStatusId }).ToTable("ApplicationApprovalStatus");
            modelBuilder.Entity<ApprovalProcessAssignedUser>().HasKey(r => new { r.ApprovalProcessId }).ToTable("ApprovalProcessAssignedUser");
            modelBuilder.Entity<MasterMultiplerForPoliceForce>().ToTable("MasterMultiplerForPoliceForce");
            //modelBuilder.Entity<MasterContributorUpdLog>().ToTable("MasterContributorUpdLog");
            modelBuilder.Entity<MasterContributorUpdLog>().HasKey(r => new { r.LogId }).ToTable("MasterContributorUpdLog");
            modelBuilder.Entity<ContributorDependantPensionDetails>().ToTable("ContributorDependantPensionDetails");
            modelBuilder.Entity<ApplicationUpdLog>().ToTable("ApplicationUpdLog");
            modelBuilder.Entity<MasterDiscountForGratuityMain>().ToTable("MasterDiscountForGratuityMain");
            modelBuilder.Entity<MasterDiscountForGratuityDetails>().ToTable("MasterDiscountForGratuityDetails");
            modelBuilder.Entity<ContributonSheetDetailsFinaliseHistory>().ToTable("ContributonSheetDetailsFinaliseHistory");
            modelBuilder.Entity<ContributonSheetDetailsHistory>().ToTable("ContributonSheetDetailsHistory");
            modelBuilder.Entity<ReportText>().ToTable("ReportText");
            modelBuilder.Entity<MasterDistrict>().ToTable("MasterDistrict");
            modelBuilder.Entity<MasterRegion>().ToTable("MasterRegion");
            modelBuilder.Entity<SecRoleLocationModule>().ToTable("SecRoleLocationModule");
            modelBuilder.Entity<MasterBeneficiariesDetails>().ToTable("MasterBeneficiariesDetails");
            modelBuilder.Entity<MasterBeneficiaries>().ToTable("MasterBeneficiaries");
            modelBuilder.Entity<Exception_log>().ToTable("ExceptionLog");
            modelBuilder.Entity<ConExceptions>().ToTable("ConExceptions");
            modelBuilder.Entity<Media_Queue>().ToTable("Media_Queue");
            modelBuilder.Entity<MediaDownloads>().ToTable("MediaDownloads");
            modelBuilder.Entity<EditApplicantFields>().ToTable("EditApplicantFields");
            modelBuilder.Entity<MasterFinancialYears>().ToTable("MasterFinancialYears");
            modelBuilder.Entity<LoginLog>().ToTable("LoginLog");
            modelBuilder.Entity<ClientTestTable>().ToTable("ClientTestTable");
            modelBuilder.Entity<TxnHeader>().ToTable("txnHeader");
            modelBuilder.Entity<TxnDetail>().ToTable("txnDetail");
            modelBuilder.Entity<MailTemplate>().ToTable("MailTemplates");
            modelBuilder.Entity<PensionFileUploadHistory>().ToTable("PensionFileUploadHistory");
        }

        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                int userId = 0;
                try
                {
                    userId = AppUserManager.GetUserId();
                }
                catch (Exception)
                {
                }
                var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
                foreach (DbEntityEntry item in modified)
                {
                    var changedOrAddedItem = item.Entity as BaseEntity;
                    if (changedOrAddedItem != null)
                    {
                        if (item.State == EntityState.Added)
                        {
                            changedOrAddedItem.CreatedBy = userId;
                            changedOrAddedItem.CreatedOn = DateTime.Now;
                        }
                        changedOrAddedItem.ModifiedBy = userId;
                        changedOrAddedItem.ModifiedOn = DateTime.Now;
                    }
                }

                #region For Audit Log
                ////    Get all Added / Deleted / Modified entities(not Unmodified or Detached)
                foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted || p.State == EntityState.Modified)) /*p.State == EntityState.Added ||*/
                {
                    // For each changed record, get the audit record entries and add them
                    foreach (AuditLogs x in GetAuditRecordsForChange(ent, userId))
                    {
                        this.AuditLogs.Add(x);
                    }
                }


                #endregion

                int result = await base.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                //TODO: Track db errors
            }
            return 0;
        }

        /// <summary>
        /// Hooks into the Save process to get a last-minute chance to look at the entities and change them. Also intercepts exceptions and 
        /// wraps them in a new Exception type.
        /// </summary>
        /// <returns>The number of affected rows.</returns>
        public override int SaveChanges()
        {
            try
            {
                int userId = 0;
                try
                {
                    userId = AppUserManager.GetUserId();
                }
                catch (Exception)
                {
                }
                var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
                foreach (DbEntityEntry item in modified)
                {
                    var changedOrAddedItem = item.Entity as BaseEntity;
                    if (changedOrAddedItem != null)
                    {
                        if (item.State == EntityState.Added)
                        {
                            changedOrAddedItem.CreatedBy = userId;
                            changedOrAddedItem.CreatedOn = DateTime.Now;
                        }
                        changedOrAddedItem.ModifiedBy = userId;
                        changedOrAddedItem.ModifiedOn = DateTime.Now;
                    }
                }

                #region For Audit Log
                ////    Get all Added / Deleted / Modified entities(not Unmodified or Detached)
                foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted || p.State == EntityState.Modified)) /*p.State == EntityState.Added ||*/
                {
                    // For each changed record, get the audit record entries and add them
                    foreach (AuditLogs x in GetAuditRecordsForChange(ent, userId))
                    {
                        this.AuditLogs.Add(x);
                    }
                }
                #endregion
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: Track db errors
            }
            return 0;
        }
        #region Logs in all tables by Audit Log
        private List<AuditLogs> GetAuditRecordsForChange(DbEntityEntry dbEntry, int userId)
        {
            try
            {
                ///Check auditLog
                List<AuditLogs> result = new List<AuditLogs>();
                string area = string.Empty;
                string action = string.Empty;
                string controller = string.Empty;
                Int32 keyName = 0;
                var IpAddressCheck = "";
                string Url = string.Empty;
                DateTime changeTime = DateTime.UtcNow;
                var m = HttpContext.Current;
                if (m == null)
                {
                    area = string.Empty;
                    action = string.Empty;
                    controller = string.Empty;
                    IpAddressCheck = "";
                }
                else
                {
                    var route = HttpContext.Current.Request.RequestContext.RouteData.Values;
                    action = route["action"].ToString();
                    controller = route["controller"].ToString();
                    var routingValues = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).Values;
                    area = (string)routingValues["area"] ?? string.Empty;
                    Url = HttpContext.Current.Request.RawUrl;
                    IpAddressCheck = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.UserHostAddress;
                }
                // Get the Table() attribute, if one exists
                TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
                // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
                string tableName = GetTableName(dbEntry);// tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().FullName;
                if (tableName == "Users")
                {
                    tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
                }
                if (tableName.Contains("_") && (tableName.Contains("UserProfile") || tableName.Contains("AppUser") || tableName.Contains("AppUserClaim")))
                { tableName = tableName.Split('_')[0]; }

                if (tableName == "MasterIncCodes")
                {
                    keyName = ((App.Data.Entities.MasterIncCodes)dbEntry.Entity).inc_code_id;
                    //Convert.ToInt32(dbEntry.OriginalValues.GetValue<object>("Id"));
                    //keyName = Convert.ToInt32(dbEntry.OriginalValues.GetValue<object>("Id"));
                }
                else if (tableName == "MasterBanks")
                {
                    keyName = ((App.Data.Entities.MasterBanks)dbEntry.Entity).Bank_Code_ID;
                }
                else if (tableName == "EditApplicantFields")
                {
                    keyName = ((App.Data.Entities.EditApplicantFields)dbEntry.Entity).RoleID;
                }
                else if (tableName != "AppUser")
                {
                    keyName = ((App.Data.Entities.BaseEntity)dbEntry.Entity).Id;
                }
                else
                {
                    keyName = Convert.ToInt32(dbEntry.OriginalValues.GetValue<object>("Id"));
                }


                if (dbEntry.State == EntityState.Deleted)
                {
                    string Json = string.Empty;
                    foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                    {
                        //if (propertyName != "CreatedBy" && propertyName != "CreatedOn" && propertyName != "ModifiedBy" && propertyName != "ModifiedOn" && propertyName != "Id")
                        if (string.IsNullOrWhiteSpace(Json))
                            Json += propertyName + " : '" + dbEntry.OriginalValues.GetValue<object>(propertyName) + "'";
                        else
                            Json += " , " + propertyName + " : '" + dbEntry.OriginalValues.GetValue<object>(propertyName) + "'";
                    }
                    Json = "{ " + Json + " }";
                    // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                    result.Add(new Entities.AuditLogs()
                    {
                        //UserId = userId,
                        EventType = "Deleted", // Deleted
                        TableName = tableName,
                        RecordId = keyName,
                        ColumnName = "*ALL",
                        NewValue = Json,// (dbEntry.OriginalValues.ToObject() is IDescribableEntity) ? (dbEntry.OriginalValues.ToObject() as IDescribableEntity).Describe() : dbEntry.OriginalValues.ToObject().ToString(),
                                        //CreatedMachineInfo = System.Environment.MachineName,
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now,
                        Action = action,
                        Controller = controller,
                        Area = area,
                        IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.UserHostAddress,
                        Url = Url,
                        IsActive = true
                    }
                        );
                }
                else if (dbEntry.State == EntityState.Modified)
                {

                    foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                    {
                        // For updates, we only want to capture the columns that actually changed
                        if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                        {

                            result.Add(new AuditLogs()
                            {
                                //    UserId = userId,
                                EventType = "Modified",    // Modified
                                TableName = tableName,
                                RecordId = keyName,
                                ColumnName = propertyName,
                                OldValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
                                NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString(),
                                //CreatedMachineInfo = System.Environment.MachineName,
                                CreatedBy = userId,
                                CreatedOn = DateTime.Now,
                                Action = action,
                                Controller = controller,
                                Area = area,
                                IPAddress = IpAddressCheck,
                                Url = Url,
                                IsActive = true


                            }
                                );
                        }
                    }
                }
                // Otherwise, don't do anything, we don't care about Unchanged or Detached entities
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string GetTableName(DbEntityEntry ent)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
            Type entityType = ent.Entity.GetType();

            if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                entityType = entityType.BaseType;

            string entityTypeName = entityType.Name;

            EntityContainer container =
                objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = (from meta in container.BaseEntitySets
                                    where meta.ElementType.Name == entityTypeName
                                    select meta.Name).First();
            return entitySetName;
        }
        #endregion

        public DbSet<AppUserRole> UserRole { get; set; }
        public DbSet<MasterPensioner> MasterPensioner { get; set; }
        public DbSet<App.Data.Entities.MasterCountry> MasterCountry { get; set; }
        public DbSet<App.Data.Entities.MasterDesignation> MasterDesignation { get; set; }
        public DbSet<App.Data.Entities.MasterDepartment> MasterDepartment { get; set; }
        public DbSet<App.Data.Entities.MasterSuffix> MasterSuffix { get; set; }
        public DbSet<App.Data.Entities.MasterPrefix> MasterPrefix { get; set; }
        public DbSet<App.Data.Entities.MasterGrade> MasterGrade { get; set; }
        public DbSet<App.Data.Entities.MasterPensionerType> MasterPensionerType { get; set; }
        public DbSet<App.Data.Entities.MasterMaritalStatus> MasterMaritalStatus { get; set; }
        public DbSet<App.Data.Entities.MasterMonthName> MasterMonthName { get; set; }
        public DbSet<App.Data.Entities.MasterNationality> MasterNationality { get; set; }
        public DbSet<App.Data.Entities.MasterPFRate> MasterPFRate { get; set; }
        public DbSet<App.Data.Entities.MasterState> MasterState { get; set; }
        public DbSet<App.Data.Entities.MasterCity> MasterCity { get; set; }
        public DbSet<App.Data.Entities.MasterEmployer> MasterEmployer { get; set; }
        public DbSet<App.Data.Entities.MasterEmployerPFRateDetails> MasterEmployerPFRateDetails { get; set; }
        public DbSet<App.Data.Entities.MasterContributorPFRateDetails> MasterContributorPFRateDetails { get; set; }
        public DbSet<App.Data.Entities.MasterContributor> MasterContributor { get; set; }
        public DbSet<App.Data.Entities.MasterDependantDetails> MasterDependantDetails { get; set; }
        public DbSet<App.Data.Entities.MasterRelationshipType> MasterRelationshipType { get; set; }
        public DbSet<App.Data.Entities.MasterContributorMarriageDetails> MasterContributorMarriageDetails { get; set; }
        public DbSet<App.Data.Entities.MasterContributorJobDetails> MasterContributorJobDetails { get; set; }
        public DbSet<App.Data.Entities.MasterStatus> MasterStatus { get; set; }
        public DbSet<App.Data.Entities.MasterJobTitle> MasterJobTitle { get; set; }
        public DbSet<App.Data.Entities.MasterBanks> MasterBanks { get; set; }
        public DbSet<MasterBankDetails> MasterBankDetails { get; set; }
        public DbSet<MasterEmployee> MasterEmployees { get; set; }
        public DbSet<MasterDiscountForGratuity> MasterDiscountForGratuity { get; set; }
        public DbSet<MasterEmpBankDetails> MasterEmpBankDetails { get; set; }
        public DbSet<MasterEmployeeDeductions> MasterEmployeeDeductions { get; set; }
        public DbSet<MasterEmployeeIncomes> MasterEmployeeIncomes { get; set; }
        public DbSet<MasterEmployeeObligations> MasterEmployeeObligations { get; set; }
        public DbSet<MasterIncCodes> MasterIncCodes { get; set; }
        public DbSet<MasterDedcodes> MasterDedcodes { get; set; }
        public DbSet<MasterRegion> MasterRegion { get; set; }

        public DbSet<MasterDistrict> MasterDistrict { get; set; }
        public DbSet<SecRoleLocationModule> SecRoleLocationModule { get; set; }
        public DbSet<MasterOblCodes> MasterOblCodes { get; set; }
        public DbSet<PensionApplications> PensionApplications { get; set; }
        public DbSet<RefundApplications> RefundApplications { get; set; }
        public DbSet<ContributonSheetDetails> ContributonSheetDetails { get; set; }
        public DbSet<ContributonSheetHeader> ContributonSheetHeader { get; set; }
        public DbSet<ContributonSheetDetailsFinalise> ContributonSheetDetailsFinalise { get; set; }
        public DbSet<ContributonSheetHeaderFinalise> ContributonSheetHeaderFinalise { get; set; }
        public DbSet<ContributonTransactions> ContributonTransactions { get; set; }
        public DbSet<ProcessDirectDepositHeader> ProcessDirectDepositHeader { get; set; }
        public DbSet<Process_DirectDeposit_Header> Process_DirectDeposit_Header { get; set; }//
        public DbSet<ProcessDirectDepositDetails> ProcessDirectDepositDetails { get; set; }
        public DbSet<ProcessPayIncome> ProcessPayIncome { get; set; }
        public DbSet<ProcessPayDeductions> ProcessPayDeductions { get; set; }
        public DbSet<ProcessPayObligations> ProcessPayObligations { get; set; }
        public DbSet<ProcessPayPensioner> ProcessPayPensioner { get; set; }
        public DbSet<MasterEmployerType> MasterEmployerType { get; set; }
        public DbSet<MasterInterestRate> MasterInterestRate { get; set; }
        public DbSet<PensionProcessDetails> PensionProcessDetails { get; set; }
        public DbSet<PensionProcessHeader> PensionProcessHeader { get; set; }
        public DbSet<PensionerComments> PensionerComments { get; set; }
        public DbSet<ContributorComments> ContributorComments { get; set; }
        public DbSet<ContributorSalaryHistory> ContributorSalaryHistory { get; set; }
        public DbSet<PayControl> PayControl { get; set; }
        public DbSet<PayrollGLAccounts> PayrollGLAccounts { get; set; }

        public DbSet<FlexStructHeader> FlexStructHeader { get; set; }
        public DbSet<FlexStructDetails> FlexStructDetails { get; set; }
        public DbSet<FlexSegmentReference> FlexSegmentReference { get; set; }
        public DbSet<FlexSegmentValueDetails> FlexSegmentValueDetails { get; set; }

        public DbSet<DeductionsTaxTableHeader> DeductionsTaxTableHeader { get; set; }
        public DbSet<DeductionsTaxTableDetails> DeductionsTaxTableDetails { get; set; }

        public DbSet<SecModule> SecModule { get; set; }
        public DbSet<SecRoleModule> SecRoleModule { get; set; }

        public DbSet<ApprovalProcessLevel> ApprovalProcessLevel { get; set; }
        public DbSet<ApprovalProcessAssignedUser> ApprovalProcessAssignedUser { get; set; }
        public DbSet<ApplicationApprovalStatus> ApplicationApprovalStatus { get; set; }
        public DbSet<GratuityDetails> GratuityDetails { get; set; }
        public DbSet<RefundPaidDetails> RefundPaidDetails { get; set; }
        public DbSet<MasterMultiplerForPoliceForce> MasterMultiplerForPoliceForce { get; set; }
        public DbSet<MasterSource> MasterSource { get; set; }
        public DbSet<MasterContributorUpdLog> MasterContributorUpdLog { get; set; }
        public DbSet<ContributorDependantPensionDetails> ContributorDependantPensionDetails { get; set; }
        public DbSet<ApplicationUpdLog> ApplicationUpdLog { get; set; }
        public DbSet<MasterDiscountForGratuityMain> MasterDiscountForGratuityMain { get; set; }
        public DbSet<MasterDiscountForGratuityDetails> MasterDiscountForGratuityDetails { get; set; }
        public DbSet<ContributonSheetDetailsFinaliseHistory> ContributonSheetDetailsFinaliseHistory { get; set; }
        public DbSet<ContributonSheetDetailsHistory> ContributonSheetDetailsHistory { get; set; }
        public DbSet<MasterBeneficiariesDetails> MasterBeneficiariesDetails { get; set; }
        public DbSet<MasterBeneficiaries> MasterBeneficiaries { get; set; }
        public DbSet<Exception_log> ExceptionLog { get; set; }
        public DbSet<ReportText> ReportText { get; set; }
        public DbSet<MasterEmpTypeIncomes> MasterEmpTypeIncomes { get; set; }
        public DbSet<MasterTehsil> MasterTehsil { get; set; }

        public DbSet<MasterEmpType> MasterEmpType { get; set; }
        public DbSet<Media_Queue> Media_Queue { get; set; }
        public DbSet<MediaDownloads> MediaDownloads { get; set; }
        public DbSet<ConExceptions> ConExceptions { get; set; }
        public DbSet<MailSettings> MailSettings { get; set; }
        public DbSet<EditApplicantFields> EditApplicantFields { get; set; }
        public DbSet<MasterFinancialYears> MasterFinancialYears { get; set; }

        public DbSet<AuditLogs> AuditLogs { get; set; }
        public DbSet<LoginLog> LoginLog { get; set; }
        public DbSet<ClientTestTable> ClientTestTable { get; set; }
        public DbSet<TxnHeader> TxnHeaders { get; set; }
        public DbSet<TxnDetail> TxnDetails { get; set; }
        public DbSet<Widgets> Widgets { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }
        public DbSet<PensionFileUploadHistory> PensionFileUploadHistory { get; set; }
        public DbSet<MasterEmail> MasterEmails { get; set; }
        public DbSet<MasterProcessKey> MasterProcessKeys { get; set; }
        public IEnumerable<object> AppUser { get; set; }
        public DbSet Set(string name)
        {
            return base.Set(Type.GetType(name));
        }


    }
}