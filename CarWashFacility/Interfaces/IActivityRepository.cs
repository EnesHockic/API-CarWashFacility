using CarWashFacility.DTO;
using CarWashFacility.Model;


namespace CarWashFacility.Interfaces
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        public List<Activity> GetActivitiesByStatus(string status);
        public ActivityDTO PrepareActivity(CustomerDTO customer, ProgramDTO program);
    }
}
