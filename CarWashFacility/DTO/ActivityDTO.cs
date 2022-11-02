namespace CarWashFacility.DTO
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int CustomerId { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountFormatted { get => 100 * Discount; }
        public decimal? TotalPrice { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Status { get; set; }
        public CustomerDTO Customer { get; set; }
        public ProgramDTO Program { get; set; }
    }
}
