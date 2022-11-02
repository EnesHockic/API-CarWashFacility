using AutoMapper;
using CarWashFacility.DTO;
using CarWashFacility.Model;


namespace CarWashFacility.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<WashingProgram, ProgramDTO>();
            CreateMap<ProgramDTO, WashingProgram>();
            CreateMap<ProgramStepDTO, ProgramStep>();
            CreateMap<Step, StepDTO>();
            CreateMap<StepDTO, Step>();
            CreateMap<Activity, ActivityDTO>();
            CreateMap<AddActivityDTO, Activity>();
        }
    }
}
