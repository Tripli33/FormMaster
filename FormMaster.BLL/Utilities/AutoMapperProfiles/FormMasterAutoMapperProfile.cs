using AutoMapper;
using FormMaster.BLL.DTOs;
using FormMaster.DAL.Entities;

namespace FormMaster.BLL.Utilities.AutoMapperProfiles;

public class FormMasterAutoMapperProfile : Profile
{
    public FormMasterAutoMapperProfile()
    {
        CreateMap<UserRegistrationDto, User>();
        CreateMap<Topic, TopicTemplateManipulationDto>();
        CreateMap<QuestionRegistrationDto, Question>();
        CreateMap<Question, QuestionRegistrationDto>();
        CreateMap<Template, TemplateManipulationDto>();
        CreateMap<Question, QuestionManipulationDto>();
        CreateMap<FormAnswerDto, Answer>();
        CreateMap<Form, UserFormManipulationDto>();
        CreateMap<Template, UserTemplateManipulationDto>();
        CreateMap<Form, TemplateFormDto>();
        CreateMap<Template, HomeTemplateManipulationDto>();
    }
}
