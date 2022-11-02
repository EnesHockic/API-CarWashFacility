namespace CarWashFacility.Model
{
    public class ProgramStep
    {
        public int Id { get; set; }
        public int? ProgramId { get; set; }
        public int? StepId { get; set; }
        public int? StepOrder { get; set; }
        public WashingProgram Program { get; set; }
        public Step Step { get; set; }
    }
}
