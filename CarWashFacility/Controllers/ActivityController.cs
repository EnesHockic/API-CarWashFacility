using AutoMapper;
using CarWashFacility.DTO;
using CarWashFacility.Interfaces;
using CarWashFacility.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CarWashFacility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public IActivityRepository _activityRepository { get; set; }
        public IProgramRepository _programRepository { get; set; }
        public ICustomerRepository _customerRepository { get; set; }
        public IMapper _mapper { get; set; }
        public ActivityController(IActivityRepository ActivityRepository,
                                         IProgramRepository ProgramRepository,
                                         ICustomerRepository customerRepository,
                                         IMapper mapper)
        {
            _activityRepository = ActivityRepository;
            _programRepository = ProgramRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        [HttpGet("GetByStatus")]
        [ProducesResponseType(200, Type = typeof(ICollection<ActivityDTO>))]
        public IActionResult GetActivitiesByStatus([FromQuery]string status)
        {
            var Activities = _activityRepository.GetActivitiesByStatus(status);
            var mapper = _mapper.Map<ICollection<ActivityDTO>>(Activities);
            return Ok(mapper);
        }
        [HttpGet("Prepare")]
        [ProducesResponseType(200, Type = typeof(ActivityDTO))]
        [ProducesResponseType(500)]
        public IActionResult GetPreparedActivity([FromQuery] int programId, [FromQuery] int customerId)
        {
            var program = _programRepository.GetById(programId);
            var customer = _customerRepository.GetById(customerId);
            if(program == null)
            {
                ModelState.AddModelError("", "Program does not exist!");
                return BadRequest(ModelState);
            }
            if (customer == null)
            {
                ModelState.AddModelError("", "Customer does not exist!");
                return BadRequest(ModelState);
            }
            var activity = _activityRepository.PrepareActivity(
                _mapper.Map<CustomerDTO>(customer),
                _mapper.Map<ProgramDTO>(program));
            return Ok(activity);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult CreateActivity([FromBody] AddActivityDTO newActivity)
        {
            var program = _programRepository.GetById(newActivity.ProgramId);
            var customer = _customerRepository.GetById(newActivity.CustomerId);
            if (program == null)
            {
                ModelState.AddModelError("", "Program does not exist!");
                return BadRequest(ModelState);
            }
            if (customer == null)
            {
                ModelState.AddModelError("", "Customer does not exist!");
                return BadRequest(ModelState);
            }
            var activity = _mapper.Map<Activity>(newActivity);
            _activityRepository.Add(activity);
            return Ok("Activity successfuly created!");
        }
        [HttpPut]
        public IActionResult UpdateActivity([FromBody] AddActivityDTO updatedActivity)
        {
            var existingActivity = _activityRepository.GetById(updatedActivity.Id);
            if(existingActivity == null)
            {
                ModelState.AddModelError("", "Activity does not exist!");
                return BadRequest(ModelState);
            }
            _mapper.Map(updatedActivity, existingActivity);
            _activityRepository.Update(existingActivity);
            return Ok(updatedActivity);
        }
    }
}
