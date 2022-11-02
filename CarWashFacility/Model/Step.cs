namespace CarWashFacility.Model
{
    public class Step
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProgramStep> ProgramSteps { get; set; }
    }
}
