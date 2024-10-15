using AutoMapper;
using FormMaster.BLL.DTOs;
using FormMaster.DAL.Entities;

namespace FormMaster.BLL.Utilities.AutoMapperProfiles;

public class FormMasterAutoMapperProfile : Profile
{
    public FormMasterAutoMapperProfile()
    {
        CreateMap<UserRegistrationDto, User>();
    }
}
