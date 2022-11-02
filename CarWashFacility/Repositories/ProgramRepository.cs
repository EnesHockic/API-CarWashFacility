using CarWashFacility.Data;
using CarWashFacility.Interfaces;
using CarWashFacility.Model;
using Microsoft.EntityFrameworkCore;

namespace CarWashFacility.Repositories
{
    public class ProgramRepository : GenericRepository<WashingProgram>, IProgramRepository
    {
        public ProgramRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        { }

        public bool AddStepToProgram(ProgramStep programStep)
        {
            _applicationDbContext.ProgramSteps.Add(programStep);
            return Save();
        }

        public ICollection<Step> GetProgramSteps(int programId)
        {
            return _applicationDbContext.ProgramSteps.Where(w => w.ProgramId == programId)
                .Include(w => w.Step).Select(w => w.Step).ToList();
        }

        public bool RemoveStepFromProgram(int programId, int stepId)
        {
            var programStep = _applicationDbContext.ProgramSteps.FirstOrDefault(x => x.ProgramId == programId && x.StepId == stepId);
            _applicationDbContext.ProgramSteps.Remove(programStep);
            return Save();
        }
    }
}
