using CarWashFacility.Data;
using CarWashFacility.DTO;
using CarWashFacility.Interfaces;
using CarWashFacility.Model;

using System.Data.Entity;

namespace CarWashFacility.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public IDiscountService _discountService { get; }

        public ActivityRepository(ApplicationDbContext applicationDbContext, IDiscountService discountService)
            :base(applicationDbContext)
        {
            _discountService = discountService;
        }
        public List<Activity> GetActivitiesByStatus(string status)
        {
            return _applicationDbContext.Activities
                .Include(x => x.Customer)
                .Include(x => x.Program)
                .Select(x => new Activity
                 {
                     Id = x.Id,
                     DateTime = x.DateTime,
                     Discount = x.Discount,
                     TotalPrice = x.TotalPrice,
                     Status = x.Status,
                     CustomerId = x.CustomerId,
                     Customer = _applicationDbContext.Customers.FirstOrDefault(y => y.Id == x.CustomerId),
                     ProgramId = x.ProgramId,
                     Program = _applicationDbContext.Programs.FirstOrDefault(y => y.Id == x.ProgramId)
                })
                .Where(x => x.Status == status).ToList(); ;
        }
        public ActivityDTO PrepareActivity(CustomerDTO customer, ProgramDTO program)
        {
            var discount = _discountService.GetDiscount(GetCustomerVisits(customer.Id));
            var activity = new ActivityDTO()
            {
                CustomerId = customer.Id,
                ProgramId = program.Id,
                Discount = discount,
                TotalPrice = program.Price - program.Price * discount,
                Status = "Active",
                DateTime = DateTime.Now,
                Customer = customer,
                Program = program
            };
            return activity;
        }
        public ICollection<Activity> GetCustomerVisits(int customerId)
        {
            return _applicationDbContext.Activities
                .Where(x => x.CustomerId == customerId && x.Status == "Done").ToList();
        }
    }
}
