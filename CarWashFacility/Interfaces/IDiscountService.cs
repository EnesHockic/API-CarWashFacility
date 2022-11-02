using CarWashFacility.DTO;
using CarWashFacility.Model;

namespace CarWashFacility.Interfaces
{
    public interface IDiscountService
    {
        public decimal GetDiscount(ICollection<Activity> customerActivities);
    }
}
