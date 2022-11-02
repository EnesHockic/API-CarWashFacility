using CarWashFacility.DTO;
using CarWashFacility.Interfaces;
using CarWashFacility.Model;

namespace CarWashFacility.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal GetDiscount(ICollection<Activity> customerActivities)
        {
            return DiscountFor10thVisit(customerActivities);
        }
        private decimal DiscountFor10thVisit(ICollection<Activity> customerActivities)
        {
            return customerActivities.Count() % 10 == 9 ? (decimal)0.2 : 0;
        }
    }
}
