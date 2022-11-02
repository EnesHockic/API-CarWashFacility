using CarWashFacility.DTO;
using CarWashFacility.Model;

namespace CarWashFacility.Interfaces
{
    public interface IStepRepository : IGenericRepository<Step>
    {
        public ICollection<StepProgramRelDataDTO> GetStepsByProgramId(int programId);
        public ICollection<Step> GetUnassignedSteps();
    }
}
