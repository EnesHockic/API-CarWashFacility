using CarWashFacility.Data;
using CarWashFacility.Interfaces;
using CarWashFacility.Model;
using Microsoft.EntityFrameworkCore;

namespace CarWashFacility.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
