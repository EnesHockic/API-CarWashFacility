using CarWashFacility.Model;

namespace CarWashFacility.Interfaces
{
    public interface IProgramRepository : IGenericRepository<WashingProgram>
    {
        //public ICollection<Program> GetPrograms();
        //public Program GetPrograms(int programId);
        //public bool ProgramExists(int programId);
        //public bool AddProgram(Program newprogram);
        //public bool UpdateProgram(Program program);
        public ICollection<Step> GetProgramSteps(int programId);
        public bool AddStepToProgram(ProgramStep programStep);
        public bool RemoveStepFromProgram(int programId, int stepId);
    }
}