using AutoMapper;
using CarWashFacility.DTO;
using CarWashFacility.Interfaces;
using CarWashFacility.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarWashFacility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        //private readonly IStepRepository _StepRepository;
        //private readonly IMapper _mapper;
        public IStepRepository _StepRepository { get; set; }
        public IMapper _mapper { get; set; }

        public StepController(IStepRepository StepRepository, IMapper mapper)
        {
            _StepRepository = StepRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof (ICollection<StepDTO>))]
        public IActionResult GetSteps()
        {
            var Steps = _StepRepository.GetAll();
            var StepsOutput = _mapper.Map<ICollection<StepDTO>>(Steps);
            return Ok(StepsOutput);
        }
        [HttpGet("{stepId}")]
        [ProducesResponseType(200, Type = typeof(StepDTO))]
        public IActionResult GetStepById(int stepId)
        {
            var Step = _StepRepository.GetById(stepId);
            if(Step == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(_mapper.Map<StepDTO>(Step));
        }
        [HttpGet("ProgramSteps/{programId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<StepProgramRelDataDTO>))]
        public IActionResult GetStepsByProgramId(int programId)
        {
            var Steps = _StepRepository.GetStepsByProgramId(programId);
            return Ok(Steps);
        }
        [HttpGet("UnassignedSteps")]
        [ProducesResponseType(200, Type = typeof(ICollection<StepProgramRelDataDTO>))]
        public IActionResult GetUnassignedStepsByProgramId()
        {
            var Steps = _StepRepository.GetUnassignedSteps();
            var StepsOutput = _mapper.Map<ICollection<StepDTO>>(Steps);
            return Ok(StepsOutput);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateStep([FromBody] StepDTO newStep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Step = _mapper.Map<Step>(newStep);
            var result = _StepRepository.Add(Step);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
