namespace App.Data.Migrations
{
  using App.Data.Entities;
  using Microsoft.AspNet.Identity;
  using Microsoft.AspNet.Identity.EntityFramework;
  using Microsoft.Owin.Security;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<App.Data.AppDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(App.Data.AppDbContext context)
    {
      var roleStore = new RoleStore<AppRole, int, AppUserRole>(context);
      var roleManager = new RoleManager<AppRole, int>(roleStore);
      var userStore = new UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>(context);
      var userManager = new UserManager<AppUser, int>(userStore);

      //See Roles
      if (!roleManager.RoleExists("Admin"))
      {
        roleManager.Create(new AppRole { Name = "Admin" });
      }

      if (!roleManager.RoleExists("User"))
      {
        roleManager.Create(new AppRole { Name = "User" });
      }

      //add default entries in tables

      //MasterStatus
      #region MasterStatus
      if (!context.MasterStatus.Any())
      {
        MasterStatus masterStatus;
        if (!context.MasterStatus.Where(x => x.Name == "Active Contributor").Any())
        {
          masterStatus = new MasterStatus();
          masterStatus.Name = "Active Contributor";
          masterStatus.IsActive = true;
          context.Entry(masterStatus).State = EntityState.Added;
        }
        if (!context.MasterStatus.Where(x => x.Name == "Expired").Any())
        {
          masterStatus = new MasterStatus();
          masterStatus.Name = "Expired";
          masterStatus.IsActive = true;
          context.Entry(masterStatus).State = EntityState.Added;
        }

        if (!context.MasterStatus.Where(x => x.Name == "Pensioner").Any())
        {
          masterStatus = new MasterStatus();
          masterStatus.Name = "Pensioner";
          masterStatus.IsActive = true;
          context.Entry(masterStatus).State = EntityState.Added;
        }

        if (!context.MasterStatus.Where(x => x.Name == "Resignee").Any())
        {
          masterStatus = new MasterStatus();
          masterStatus.Name = "Resignee";
          masterStatus.IsActive = true;
          context.Entry(masterStatus).State = EntityState.Added;
        }

        if (!context.MasterStatus.Where(x => x.Name == "Supernumerary").Any())
        {
          masterStatus = new MasterStatus();
          masterStatus.Name = "Supernumerary";
          masterStatus.IsActive = true;
          context.Entry(masterStatus).State = EntityState.Added;
        }

        context.SaveChanges();
      }
      #endregion
      //MasterPensionerType
      #region MasterPensionerType
      if (!context.MasterPensionerType.Any())
      {
        MasterPensionerType masterPensionerTypes;
        if (!context.MasterPensionerType.Where(x => x.Name == "Full Time").Any())
        {
          masterPensionerTypes = new MasterPensionerType();
          masterPensionerTypes.Name = "Full Time";
          masterPensionerTypes.IsActive = true;
          context.Entry(masterPensionerTypes).State = EntityState.Added;
        }
        if (!context.MasterPensionerType.Where(x => x.Name == "Part Time").Any())
        {
          masterPensionerTypes = new MasterPensionerType();
          masterPensionerTypes.Name = "Part Time";
          masterPensionerTypes.IsActive = true;
          context.Entry(masterPensionerTypes).State = EntityState.Added;
        }
        if (!context.MasterPensionerType.Where(x => x.Name == "Unemployed").Any())
        {
          masterPensionerTypes = new MasterPensionerType();
          masterPensionerTypes.Name = "Unemployed";
          masterPensionerTypes.IsActive = true;
          context.Entry(masterPensionerTypes).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterEmployerType
      #region MasterEmployerType
      if (!context.MasterEmployerType.Any())
      {
        MasterEmployerType masterEmployerType;
        if (!context.MasterEmployerType.Where(x => x.Name == "Royal Anguilla Police Force ").Any())
        {
          masterEmployerType = new MasterEmployerType();
          masterEmployerType.Name = "Royal Anguilla Police Force ";
          masterEmployerType.RetirementAge = 55;
          masterEmployerType.IsActive = true;
          context.Entry(masterEmployerType).State = EntityState.Added;
        }
        if (!context.MasterEmployerType.Where(x => x.Name == "Public Service").Any())
        {
          masterEmployerType = new MasterEmployerType();
          masterEmployerType.Name = "Public Service";
          masterEmployerType.RetirementAge = 60;
          masterEmployerType.IsActive = true;
          context.Entry(masterEmployerType).State = EntityState.Added;
        }

        context.SaveChanges();
      }
      #endregion
      #region MasterMonthName
      if (!context.MasterMonthName.Any())
      {
        MasterMonthName masterMonthName;
        if (!context.MasterMonthName.Where(x => x.Name == "January").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "January";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "February").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "February";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "March").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "March";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "April").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "April";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "May").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "May";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "June").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "June";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "July").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "July";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "August").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "August";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "September").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "September";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "October").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "October";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "November").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "November";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        if (!context.MasterMonthName.Where(x => x.Name == "December").Any())
        {
          masterMonthName = new MasterMonthName();
          masterMonthName.Name = "December";
          masterMonthName.IsActive = true;
          context.Entry(masterMonthName).State = EntityState.Added;
        }

        context.SaveChanges();
      }
      #endregion
      //MasterCountry
      #region Countries
      if (!context.MasterCountry.Any())
      {
        MasterCountry masterCountry;
        if (!context.MasterCountry.Where(x => x.Name == "Anguilla").Any())
        {
          masterCountry = new MasterCountry();
          masterCountry.Name = "Anguilla";
          masterCountry.CountryCode = "ANG";
          masterCountry.ISDCode = "+1";
          masterCountry.IsActive = true;
          context.Entry(masterCountry).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterState
      #region MasterState
      if (!context.MasterState.Any())
      {
        MasterState masterState;
        if (!context.MasterState.Where(x => x.Name == "Anguilla").Any())
        {
          masterState = new MasterState();
          masterState.Name = "Anguilla";
          masterState.CountryId = 1;
          masterState.IsActive = true;
          context.Entry(masterState).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterCity
      #region MasterCity
      if (!context.MasterCity.Any())
      {
        MasterCity masterCity;
        if (!context.MasterCity.Where(x => x.Name == "Betty Hill").Any())
        {
          masterCity = new MasterCity();
          masterCity.Name = "Betty Hill";
          masterCity.DistrictId = 1;
          masterCity.IsActive = true;
          context.Entry(masterCity).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterDepartment
      #region MasterDepartment
      if (!context.MasterDepartment.Any())
      {
        MasterDepartment masterDepartment;
        if (!context.MasterDepartment.Where(x => x.Name == "Marketing").Any())
        {
          masterDepartment = new MasterDepartment();
          masterDepartment.Name = "Marketing";
          masterDepartment.IsActive = true;
          context.Entry(masterDepartment).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterDesignation
      #region MasterDesignation
      if (!context.MasterDesignation.Any())
      {
        MasterDesignation masterDesignation;
        if (!context.MasterDesignation.Where(x => x.Name == "Managing Director").Any())
        {
          masterDesignation = new MasterDesignation();
          masterDesignation.Name = "Managing Director";
          masterDesignation.IsActive = true;
          context.Entry(masterDesignation).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterPrefixes
      #region MasterPrefix
      if (!context.MasterPrefix.Any())
      {
        MasterPrefix masterPrefix;
        if (!context.MasterPrefix.Where(x => x.Name == "Mr").Any())
        {
          masterPrefix = new MasterPrefix();
          masterPrefix.Name = "Mr";
          masterPrefix.IsActive = true;
          context.Entry(masterPrefix).State = EntityState.Added;
        }
        if (!context.MasterPrefix.Where(x => x.Name == "Mrs").Any())
        {
          masterPrefix = new MasterPrefix();
          masterPrefix.Name = "Mrs";
          masterPrefix.IsActive = true;
          context.Entry(masterPrefix).State = EntityState.Added;
        }
        if (!context.MasterPrefix.Where(x => x.Name == "Miss").Any())
        {
          masterPrefix = new MasterPrefix();
          masterPrefix.Name = "Miss";
          masterPrefix.IsActive = true;
          context.Entry(masterPrefix).State = EntityState.Added;
        }
        if (!context.MasterPrefix.Where(x => x.Name == "Dr").Any())
        {
          masterPrefix = new MasterPrefix();
          masterPrefix.Name = "Dr";
          masterPrefix.IsActive = true;
          context.Entry(masterPrefix).State = EntityState.Added;
        }
        if (!context.MasterPrefix.Where(x => x.Name == "Sir").Any())
        {
          masterPrefix = new MasterPrefix();
          masterPrefix.Name = "Sir";
          masterPrefix.IsActive = true;
          context.Entry(masterPrefix).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //masterSuffixes
      #region MasterSuffix

      if (!context.MasterSuffix.Any())
      {
        MasterSuffix masterSuffix;
        if (!context.MasterSuffix.Where(x => x.Name == "JR").Any())
        {
          masterSuffix = new MasterSuffix();
          masterSuffix.Name = "JR";
          masterSuffix.IsActive = true;
          context.Entry(masterSuffix).State = EntityState.Added;
        }
        if (!context.MasterSuffix.Where(x => x.Name == "JR").Any())
        {
          masterSuffix = new MasterSuffix();
          masterSuffix.Name = "JR";
          masterSuffix.IsActive = true;
          context.Entry(masterSuffix).State = EntityState.Added;
        }
        if (!context.MasterSuffix.Where(x => x.Name == "SR").Any())
        {
          masterSuffix = new MasterSuffix();
          masterSuffix.Name = "SR";
          masterSuffix.IsActive = true;
          context.Entry(masterSuffix).State = EntityState.Added;
        }
        if (!context.MasterSuffix.Where(x => x.Name == "I").Any())
        {
          masterSuffix = new MasterSuffix();
          masterSuffix.Name = "I";
          masterSuffix.IsActive = true;
          context.Entry(masterSuffix).State = EntityState.Added;
        }
        if (!context.MasterSuffix.Where(x => x.Name == "II").Any())
        {
          masterSuffix = new MasterSuffix();
          masterSuffix.Name = "II";
          masterSuffix.IsActive = true;
          context.Entry(masterSuffix).State = EntityState.Added;
        }
        if (!context.MasterSuffix.Where(x => x.Name == "III").Any())
        {
          masterSuffix = new MasterSuffix();
          masterSuffix.Name = "III";
          masterSuffix.IsActive = true;
          context.Entry(masterSuffix).State = EntityState.Added;
        }
        if (!context.MasterSuffix.Where(x => x.Name == "IV").Any())
        {
          masterSuffix = new MasterSuffix();
          masterSuffix.Name = "IV";
          masterSuffix.IsActive = true;
          context.Entry(masterSuffix).State = EntityState.Added;
        }
        if (!context.MasterSuffix.Where(x => x.Name == "V").Any())
        {
          masterSuffix = new MasterSuffix();
          masterSuffix.Name = "V";
          masterSuffix.IsActive = true;
          context.Entry(masterSuffix).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterBank
      #region MasterBank

      if (!context.MasterBanks.Any())
      {
        MasterBanks MasterBanks;
        if (!context.MasterBanks.Where(x => x.bank_desc == "BNS").Any())
        {
          MasterBanks = new MasterBanks();
          MasterBanks.bank_desc = "BNS";
          MasterBanks.bank_code = "BNS";
          //MasterBanks.IsActive = true;
          context.Entry(MasterBanks).State = EntityState.Added;
        }
        if (!context.MasterBanks.Where(x => x.bank_desc == "CCB").Any())
        {
          MasterBanks = new MasterBanks();
          MasterBanks.bank_desc = "CCB";
          MasterBanks.bank_code = "BNS";
          //MasterBanks.IsActive = true;
          context.Entry(MasterBanks).State = EntityState.Added;
        }
        if (!context.MasterBanks.Where(x => x.bank_desc == "FCIB").Any())
        {
          MasterBanks = new MasterBanks();
          MasterBanks.bank_desc = "FCIB";
          MasterBanks.bank_code = "FCIB";
          //MasterBanks.IsActive = true;
          context.Entry(MasterBanks).State = EntityState.Added;
        }
        if (!context.MasterBanks.Where(x => x.bank_desc == "NBA").Any())
        {
          MasterBanks = new MasterBanks();
          MasterBanks.bank_desc = "NBA";
          MasterBanks.bank_code = "NBA";
          //MasterBanks.IsActive = true;
          context.Entry(MasterBanks).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterGrade
      #region MasterGrade
      if (!context.MasterGrade.Any())
      {        
        MasterGrade masterGrade;
        if (!context.MasterGrade.Where(x => x.Name == "A").Any())
        {
          masterGrade = new MasterGrade();
          masterGrade.Name = "A";
          masterGrade.IsActive = true;
          context.Entry(masterGrade).State = EntityState.Added;
        }
        if (!context.MasterGrade.Where(x => x.Name == "B").Any())
        {
          masterGrade = new MasterGrade();
          masterGrade.Name = "B";
          masterGrade.IsActive = true;
          context.Entry(masterGrade).State = EntityState.Added;
        }
        if (!context.MasterGrade.Where(x => x.Name == "C").Any())
        {
          masterGrade = new MasterGrade();
          masterGrade.Name = "C";
          masterGrade.IsActive = true;
          context.Entry(masterGrade).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterNationalities
      #region MasterNationality
      if (!context.MasterNationality.Any())
      {        
        MasterNationality masterNationality;
        if (!context.MasterNationality.Where(x => x.Name == "ANGUILLIAN").Any())
        {
          masterNationality = new MasterNationality();
          masterNationality.Name = "ANGUILLIAN";
          masterNationality.IsActive = true;
          context.Entry(masterNationality).State = EntityState.Added;
        }
        if (!context.MasterNationality.Where(x => x.Name == "B.O.T CITIZEN").Any())
        {
          masterNationality = new MasterNationality();
          masterNationality.Name = "B.O.T CITIZEN";
          masterNationality.IsActive = true;
          context.Entry(masterNationality).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterMaritalStatus
      #region MasterMaritalStatus
      if (!context.MasterMaritalStatus.Any())
      {
        MasterMaritalStatus masterMaritalStatus;
        if (!context.MasterMaritalStatus.Where(x => x.Name == "Spinster").Any())
        {
          masterMaritalStatus = new MasterMaritalStatus();
          masterMaritalStatus.Name = "Spinster";
          masterMaritalStatus.IsActive = true;
          context.Entry(masterMaritalStatus).State = EntityState.Added;
        }
        if (!context.MasterMaritalStatus.Where(x => x.Name == "Married").Any())
        {
          masterMaritalStatus = new MasterMaritalStatus();
          masterMaritalStatus.Name = "Married";
          masterMaritalStatus.IsActive = true;
          context.Entry(masterMaritalStatus).State = EntityState.Added;
        }
        if (!context.MasterMaritalStatus.Where(x => x.Name == "Divorced").Any())
        {
          masterMaritalStatus = new MasterMaritalStatus();
          masterMaritalStatus.Name = "Divorced";
          masterMaritalStatus.IsActive = true;
          context.Entry(masterMaritalStatus).State = EntityState.Added;
        }
        if (!context.MasterMaritalStatus.Where(x => x.Name == "Widowed").Any())
        {
          masterMaritalStatus = new MasterMaritalStatus();
          masterMaritalStatus.Name = "Widowed";
          masterMaritalStatus.IsActive = true;
          context.Entry(masterMaritalStatus).State = EntityState.Added;
        }
        if (!context.MasterMaritalStatus.Where(x => x.Name == "Separated").Any())
        {
          masterMaritalStatus = new MasterMaritalStatus();
          masterMaritalStatus.Name = "Separated";
          masterMaritalStatus.IsActive = true;
          context.Entry(masterMaritalStatus).State = EntityState.Added;
        }
        if (!context.MasterMaritalStatus.Where(x => x.Name == "Legally Separated").Any())
        {
          masterMaritalStatus = new MasterMaritalStatus();
          masterMaritalStatus.Name = "Legally Separated";
          masterMaritalStatus.IsActive = true;
          context.Entry(masterMaritalStatus).State = EntityState.Added;
        }
        context.SaveChanges();
      }      
      #endregion
      //MasterRelationshipType
      #region MasterRelationshipType
      if (!context.MasterRelationshipType.Any())
      {
        MasterRelationshipType masterRelationshipType;
        if (!context.MasterRelationshipType.Where(x => x.Name == "Son").Any())
        {
          masterRelationshipType = new MasterRelationshipType();
          masterRelationshipType.Name = "Son";
          masterRelationshipType.IsActive = true;
          context.Entry(masterRelationshipType).State = EntityState.Added;
        }
        if (!context.MasterRelationshipType.Where(x => x.Name == "Daughter").Any())
        {
          masterRelationshipType = new MasterRelationshipType();
          masterRelationshipType.Name = "Daughter";
          masterRelationshipType.IsActive = true;
          context.Entry(masterRelationshipType).State = EntityState.Added;
        }
        if (!context.MasterRelationshipType.Where(x => x.Name == "Mother").Any())
        {
          masterRelationshipType = new MasterRelationshipType();
          masterRelationshipType.Name = "Mother";
          masterRelationshipType.IsActive = true;
          context.Entry(masterRelationshipType).State = EntityState.Added;
        }
        if (!context.MasterRelationshipType.Where(x => x.Name == "Father").Any())
        {
          masterRelationshipType = new MasterRelationshipType();
          masterRelationshipType.Name = "Father";
          masterRelationshipType.IsActive = true;
          context.Entry(masterRelationshipType).State = EntityState.Added;
        }
        if (!context.MasterRelationshipType.Where(x => x.Name == "Grandfather").Any())
        {
          masterRelationshipType = new MasterRelationshipType();
          masterRelationshipType.Name = "Grandfather";
          masterRelationshipType.IsActive = true;
          context.Entry(masterRelationshipType).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterPFRate
      #region MasterPFRate
      if (!context.MasterPFRate.Any())
      {
        MasterPFRate MasterPFRate;
        if (!context.MasterPFRate.Where(x => x.Name == "Royal Anguilla Police Force ").Any())
        {
          MasterPFRate = new MasterPFRate();
          MasterPFRate.Name = "Royal Anguilla Police Force ";
          MasterPFRate.PFRate = 4;
          //MasterPFRate.EmplrPFRate = 4;
          MasterPFRate.EffectiveDate = DateTime.Now;
          MasterPFRate.IsActive = true;
          context.Entry(MasterPFRate).State = EntityState.Added;
        }
        if (!context.MasterPFRate.Where(x => x.Name == "Public Service").Any())
        {
          MasterPFRate = new MasterPFRate();
          MasterPFRate.Name = "Public Service";
          MasterPFRate.PFRate = 3;
          //MasterPFRate.EmplrPFRate = 3;
          MasterPFRate.EffectiveDate = DateTime.Now;
          MasterPFRate.IsActive = true;
          context.Entry(MasterPFRate).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterJobTitle
      #region MasterJobTitle
      if (!context.MasterJobTitle.Any())
      {
        MasterJobTitle masterJobTitle;
        if (!context.MasterJobTitle.Where(x => x.Name == "Training Administrator").Any())
        {
          masterJobTitle = new MasterJobTitle();
          masterJobTitle.Name = "Training Administrator";
          masterJobTitle.IsActive = true;
          context.Entry(masterJobTitle).State = EntityState.Added;
        }
        if (!context.MasterJobTitle.Where(x => x.Name == "Client Operations Manager").Any())
        {
          masterJobTitle = new MasterJobTitle();
          masterJobTitle.Name = "Client Operations Manager";
          masterJobTitle.IsActive = true;
          context.Entry(masterJobTitle).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterDiscountForGratuity
      #region MasterDiscountForGratuity
      if (!context.MasterDiscountForGratuity.Any())
      {
        MasterDiscountForGratuity masterDiscountForGratuity;
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 1).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 1;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.9615);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 2).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 2;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.9246);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 3).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 3;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.8890);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 4).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 4;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.8548);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 5).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 5;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.8219);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 6).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 6;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.7903);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 7).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 7;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.7599);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 8).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 8;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.7307);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 9).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 9;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.7026);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 10).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 10;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.6756);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 11).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 11;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.6496);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 12).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 12;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.6246);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 13).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 13;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.6006);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 14).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 14;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.5775);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 15).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 15;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.5553);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 16).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 16;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.5339);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 17).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 17;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.5134);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 18).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 18;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.4936);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 19).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 19;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.4746);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 20).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 20;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.4564);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 21).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 21;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.4388);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 22).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 22;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.4220);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 23).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 23;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.4057);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 24).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 24;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.3901);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 25).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 25;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.3751);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 26).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 26;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.3607);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 27).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 27;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.3468);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 28).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 28;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.3335);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 29).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 29;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.3207);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 30).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 30;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.3083);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 31).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 31;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2965);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 32).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 32;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2851);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 33).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 33;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2741);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 34).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 34;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2636);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 35).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 35;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2534);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 36).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 36;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2437);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 37).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 37;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2343);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 38).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 38;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2253);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 39).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 39;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2166);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        if (!context.MasterDiscountForGratuity.Where(x => x.YearsToNormalRetirement == 40).Any())
        {
          masterDiscountForGratuity = new MasterDiscountForGratuity();
          masterDiscountForGratuity.YearsToNormalRetirement = 40;
          masterDiscountForGratuity.DiscountFactorPercent = 4;
          masterDiscountForGratuity.DiscountFactor = Convert.ToDecimal(0.2083);
          masterDiscountForGratuity.IsActive = true;
          context.Entry(masterDiscountForGratuity).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //MasterEmployer
      #region MasterEmployer
      /*
      if (!context.MasterEmployer.Any())
      {
        MasterEmployer masterEmployer;
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000001").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Government of Anguilla").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000001";
          masterEmployer.EmployerName = "Government of Anguilla";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000002").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Water Corporation of Anguilla").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000002";
          masterEmployer.EmployerName = "Water Corporation of Anguilla";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000003").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Health Authority of Anguilla").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000003";
          masterEmployer.EmployerName = "Health Authority of Anguilla";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000004").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Anguilla Development Board").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000004";
          masterEmployer.EmployerName = "Anguilla Development Board";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000005").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Anguilla Tourist Board").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000005";
          masterEmployer.EmployerName = "Anguilla Tourist Board";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000006").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Public Utilities Commission").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000006";
          masterEmployer.EmployerName = "Public Utilities Commission";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000007").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Financial Services Commission").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000007";
          masterEmployer.EmployerName = "Financial Services Commission";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000008").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Anguilla Community College ").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000008";
          masterEmployer.EmployerName = "Anguilla Community College ";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000009").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Anguilla Air & Sea Ports Authority ").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000009";
          masterEmployer.EmployerName = "Anguilla Air & Sea Ports Authority ";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000010").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Public Service Pension Fund").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000010";
          masterEmployer.EmployerName = "Public Service Pension Fund";
          masterEmployer.EmployerTypeID = 2;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 2;
          masterEmployer.PFRate = Convert.ToDecimal(3);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
        if (!context.MasterEmployer.Where(x => x.UniqueID == "E000011").Any() || !context.MasterEmployer.Where(x => x.EmployerName == "Royal Anguilla Police Force ").Any())
        {
          masterEmployer = new MasterEmployer();
          masterEmployer.UniqueID = "E000011";
          masterEmployer.EmployerName = "Royal Anguilla Police Force ";
          masterEmployer.EmployerTypeID = 1;
          masterEmployer.EmployerAddress = "Anguilla";
          masterEmployer.CountryID = 1;
          masterEmployer.CityID = 1;
          masterEmployer.POBoxNo = "PO BOX";
          masterEmployer.ContactPerson = "";
          masterEmployer.Mobile = "";
          masterEmployer.PFRateID = 1;
          masterEmployer.PFRate = Convert.ToDecimal(4);
          masterEmployer.IsActive = true;
          context.Entry(masterEmployer).State = EntityState.Added;
        }
      */
      #endregion
      //MasterIncomeCode
      #region MasterIncomeCode
      if (!context.MasterIncCodes.Any())
      {
        MasterIncCodes masterIncomeCode;
        if (!context.MasterIncCodes.Where(x => x.inc_code == "REGPY").Any())
        {
          masterIncomeCode = new MasterIncCodes();
          masterIncomeCode.inc_code = "REGPY";
          masterIncomeCode.description = "REGULAR PENSION";
          masterIncomeCode.dflt_dept = "REGULAR PENSION";
          masterIncomeCode.inc_type = "H";
          masterIncomeCode.non_qual = "N";
          masterIncomeCode.dfltaccounttype = "EXPENS";
          //masterIncomeCode.IsActive = true;
          context.Entry(masterIncomeCode).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion

      #region MasterInterestRate
      if (!context.MasterInterestRate.Any())
      {
        MasterInterestRate masterInterestRate;
        if (!context.MasterInterestRate.Where(x => x.InterestRate == 3).Any())
        {
          masterInterestRate = new MasterInterestRate();
          masterInterestRate.InterestRate = Convert.ToDecimal(3);
          masterInterestRate.IsActive = true;
          context.Entry(masterInterestRate).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion

      //MasterDeductionCode
      #region MasterDeductionCode
      if (!context.MasterDedcodes.Any())
      {
        MasterDedcodes masterDeductionCode;
        if (!context.MasterDedcodes.Where(x => x.ded_code == "MEDINS").Any())
        {
          masterDeductionCode = new MasterDedcodes();
          masterDeductionCode.ded_code = "MEDINS";
          masterDeductionCode.description = "(Medical Insurance";
          //masterDeductionCode.IsActive = true;
          context.Entry(masterDeductionCode).State = EntityState.Added;
        }
        if (!context.MasterDedcodes.Where(x => x.ded_code == "LIFEINS").Any())
        {
          masterDeductionCode = new MasterDedcodes();
          masterDeductionCode.ded_code = "LIFEINS";
          masterDeductionCode.description = "Life Insurance";
          //masterDeductionCode.IsActive = true;
          context.Entry(masterDeductionCode).State = EntityState.Added;
        }
        if (!context.MasterDedcodes.Where(x => x.ded_code == "MASA").Any())
        {
          masterDeductionCode = new MasterDedcodes();
          masterDeductionCode.ded_code = "MASA";
          masterDeductionCode.description = "MASA";
          //masterDeductionCode.IsActive = true;
          context.Entry(masterDeductionCode).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion

      //MasterObligationCode
      #region MasterObligationCode
      if (!context.MasterOblCodes.Any())
      {
        MasterOblCodes MasterObligationCode;
        if (!context.MasterOblCodes.Where(x => x.Obl_code == "PROLI").Any())
        {
          MasterObligationCode = new MasterOblCodes();
          MasterObligationCode.Obl_code = "PROLI";
          MasterObligationCode.description = "Processing Life Insurance";
          //MasterObligationCode.IsActive = true;
          context.Entry(MasterObligationCode).State = EntityState.Added;
        }
        context.SaveChanges();
      }
      #endregion
      //Seed users
      #region userManager
      var user = userManager.FindByName("Admin");
      if (user == null)
      {
        var adminUser = new AppUser
        {
          UserName = "neeraj.p@mishainfotech.com",
          Email = "neeraj.p@mishainfotech.com",
          EmailConfirmed = true,
          SecurityStamp = Guid.NewGuid().ToString(),

        };


        userManager.Create(adminUser, "Admin@123");
        userManager.AddToRole(adminUser.Id, "Admin");

        var UserProfile = new UserProfile
        {
          FirstName = "Admin",
          LastName = "A"
        };

        context.UserProfiles.Add(UserProfile);
        context.SaveChanges();
      }
      #endregion
    }
  }
}

