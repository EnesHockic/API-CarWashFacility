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
    public class ProgramController : ControllerBase
    {
        public IProgramRepository _ProgramRepository { get; set; }
        public IMapper _mapper { get; set; }
        public ProgramController(IProgramRepository ProgramRepository,
                                        IMapper mapper)
        {
            _ProgramRepository = ProgramRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<ProgramDTO>))]
        public IActionResult GetPrograms()
        {
            var programs = _ProgramRepository.GetAll();
            var programsOutput = _mapper.Map<List<ProgramDTO>>(programs);
            return Ok(programsOutput);
        }
        [HttpGet("{programId}")]
        [ProducesResponseType(200, Type = typeof(ProgramDTO))]
        public IActionResult GetProgramById(int programId)
        {
            var program = _ProgramRepository.GetById(programId);
            var programOutput = _mapper.Map<ProgramDTO>(program);
            return Ok(programOutput);
        }
        [HttpGet("{programId}/Steps")]
        [ProducesResponseType(200, Type=typeof(ICollection<StepDTO>))]
        public IActionResult GetProgramSteps(int programId)
        {
            var steps = _ProgramRepository.GetProgramSteps(programId);
            var stepsOutput = _mapper.Map<List<StepDTO>>(steps);
            return Ok(stepsOutput);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProgram([FromBody] ProgramDTO newProgram)
        {
            if (newProgram == null)
            {
                return BadRequest(ModelState);
            }
            var programMapped = _mapper.Map<WashingProgram>(newProgram);
            var newProgramId = _ProgramRepository.Add(programMapped);

            if (newProgramId != null)
                return Ok("Program successfuly added. Program Id: " + newProgramId);
            return BadRequest("Internal error");
        }
        [HttpPut("{programId}/Edit")]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult UpdateProgram(int programId, [FromBody] ProgramDTO updatedProgram)
        {
            var existingProgram = _ProgramRepository.GetById(programId);
            if (existingProgram == null)
            {
                return BadRequest("Customer doesn't exist");
            }
            var program = _mapper.Map(updatedProgram,existingProgram);
            program.Id = programId;
            var result = _ProgramRepository.Update(program);

            if (result)
            {
                return Ok("Program successfuly updated!");
            }
            return BadRequest("Something went wrong!");
        }
        [HttpPost("AddStep")]
        public IActionResult AddStepToProgram([FromBody] ProgramStepDTO newProgramStep)
        {
            if(_ProgramRepository.GetById(newProgramStep.ProgramId) == null)
            {
                return BadRequest("Program is not valid (" + newProgramStep.ProgramId +")");
            }
            //Check if step exists
            var result = _ProgramRepository.AddStepToProgram(_mapper.Map<ProgramStep>(newProgramStep));
            //check result
            return Ok("Step successfuly added to the Program");
        }
        [HttpDelete("{ProgramId}/DeleteStep")]
        [ProducesResponseType(200)]
        public IActionResult RemoveStepFromProgram(int ProgramId, [FromQuery] int stepId)
        {
            var existingProgramSteps = _ProgramRepository.GetProgramSteps(ProgramId);
            var targetProgramStep = existingProgramSteps.FirstOrDefault(x => x.Id == stepId);
            if (targetProgramStep == null)
            {
                BadRequest("This Program doesn't have selected step");
            }
            _ProgramRepository.RemoveStepFromProgram(ProgramId, stepId);
            return Ok("Step was successfuly removed from the Program");
        }
    }
}
