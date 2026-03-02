using App.Data.Entities;
using App.Data.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
  public static class AutoMapperConfig
  {
    public static void Register()
    {
      Mapper.CreateMap<UserProfile, ProfileViewModel>();
      Mapper.CreateMap<MasterContributor, ContributionSummaryViewModel>().ReverseMap();
      Mapper.CreateMap<UserRegisterViewModel,UserProfile>();
      Mapper.CreateMap<ContributonSheetHeader, ExcelFileViewModel>().ReverseMap();
      Mapper.CreateMap<ContributonSheetDetails, ExcelFileViewModel>().ReverseMap();
      Mapper.CreateMap<ContributonSheetHeaderFinalise, ExcelFileViewModel>().ReverseMap();
      Mapper.CreateMap<ContributonSheetDetailsFinalise, ExcelFileViewModel>().ReverseMap();
      Mapper.CreateMap<ContributonSheetHeaderFinalise, ContributonSheetHeader>().ReverseMap();
      Mapper.CreateMap<ContributonSheetDetailsFinalise, ContributonSheetDetails>().ReverseMap();
      Mapper.CreateMap<MasterContributor, MasterPensioner>();
      Mapper.CreateMap<PensionProcessDetails, PensionProcessViewModel>();
      Mapper.CreateMap<PensionProcessHeader, PensionProcessViewModel>();
      Mapper.CreateMap<MasterContributor, PensionCalculationViewModel>();
      Mapper.CreateMap<MasterContributor, MasterContributorUpdLog>();
      Mapper.CreateMap<ContributonSheetDetailsFinalise, ContributonSheetDetailsFinaliseHistory>();
      Mapper.CreateMap<ContributonSheetDetails, ContributonSheetDetailsHistory>();
      //Mapper.CreateMap<SecModule, RoleModuleViewModel>();
      //Mapper.CreateMap<SecRoleModule, RoleModuleViewModel>();      
      //Mapper.CreateMap<MasterEmployer, EmployerViewModel>();
    }
  }
}

