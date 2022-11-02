namespace CarWashFacility.DTO
{
    public class AddActivityDTO
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int CustomerId { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Status { get; set; }
    }
}
