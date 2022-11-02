using AutoMapper;
using CarWashFacility.DTO;
using CarWashFacility.Interfaces;
using CarWashFacility.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarWashFacility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerRepository _customerRepository { get; set; }
        public IMapper _mapper { get; set; }
        public CustomerController(ICustomerRepository customerRepository,
                                  IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        } 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<CustomerDTO>))]
        public IActionResult GetCustomers()
        {
            var customers = _customerRepository.GetAll();
            var customersOutput = _mapper.Map<List<CustomerDTO>>(customers);
            return Ok(customersOutput);
        }
        [HttpGet("{customerId}")]
        [ProducesResponseType(200, Type = typeof(CustomerDTO))]
        public IActionResult GetCustomerById(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            var customerOutput = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerOutput);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult CreateCustomer([FromBody] CustomerDTO newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest(ModelState);
            }
            var customerMapped = _mapper.Map<Customer>(newCustomer);
            var successfulSave = _customerRepository.Add(customerMapped);

            if (successfulSave != null)
                return Ok("Customer successfuly added");
            return BadRequest("Internal error");
        }
        [HttpPut("{customerId}/edit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCustomer(int customerId, [FromBody] CustomerDTO updatedCustomer)
        {
            var existingCustomer = _customerRepository.GetById(customerId);
            if (existingCustomer == null)
            {
                return BadRequest("Customer doesn't exist");
            }
            var customer = _mapper.Map(updatedCustomer, existingCustomer);
            customer.Id = customerId;
            var result = _customerRepository.Update(customer);

            if (result != null)
            {
                return Ok("Customer successfuly updated!");
            }
            return BadRequest("Something went wrong!");
        }
    }
}
