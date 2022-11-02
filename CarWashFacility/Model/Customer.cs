namespace CarWashFacility.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Car { get; set; }
        public string? CarType { get; set; }
        public bool? Active { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
