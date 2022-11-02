using CarWashFacility.Data;
using CarWashFacility.DTO;
using CarWashFacility.Interfaces;
using CarWashFacility.Model;
using Microsoft.EntityFrameworkCore;

namespace CarWashFacility.Repositories
{
    public class StepRepository : GenericRepository<Step>, IStepRepository
    {
        public StepRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {

        }

        public ICollection<StepProgramRelDataDTO> GetStepsByProgramId(int programId)
        {
            return _applicationDbContext.ProgramSteps
                .Include(x => x.Step)
                .Where(x => x.ProgramId == programId && x.StepId != null)
                .Select(x => new StepProgramRelDataDTO
                {
                    Id = x.Step.Id,
                    Name = x.Step.Name,
                    StepOrder = x.StepOrder
                }).OrderBy(x => x.StepOrder).ToList();
        }

        public ICollection<Step> GetUnassignedSteps()
        {
            return _applicationDbContext.Steps
                .Where(x => !_applicationDbContext.ProgramSteps.Any(s => s.StepId == x.Id)).ToList(); ;
        }
    }
}
