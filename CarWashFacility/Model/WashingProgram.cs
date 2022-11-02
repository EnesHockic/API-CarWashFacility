namespace CarWashFacility.Model
{
    public class WashingProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<ProgramStep> ProgramSteps { get; set; }
    }
}
