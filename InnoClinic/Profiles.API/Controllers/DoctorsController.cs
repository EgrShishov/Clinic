using InnoClinic.Contracts.Profiles.Doctors.Requests;
using InnoClinic.Contracts.Profiles.Doctors.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Commands.Doctors.CreateDoctor;
using Profiles.Application.Commands.Doctors.DeleteDoctor;
using Profiles.Application.Commands.Doctors.UpdateDoctor;
using Profiles.Application.Querires.Doctors.FilterByOffice;
using Profiles.Application.Querires.Doctors.FilterBySpecialization;
using Profiles.Application.Querires.Doctors.SearchByName;
using Profiles.Application.Querires.Doctors.ViewById;
using Profiles.Application.Querires.Doctors.ViewDoctors;

namespace Profiles.API.Controllers
{
    [Route("api/[controller]")]
    public class DoctorsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DoctorsController(IMediator mediator, IMapper mapper, ILogger logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("create-profile")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> CreateDoctorProfile(CreateDoctorRequest request)
        {
            var command = _mapper.Map<CreateDoctorCommand>(request);
            var result = await _mediator.Send(command);
            if (result.IsError)
            {
                return BadRequest(result.FirstError);
            }
            return Ok(_mapper.Map<DoctorResponse>(result.Value));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var command = new DeleteDoctorCommand(id);
            var result = await _mediator.Send(command);
            if (result.IsError)
            {
                return BadRequest(result.FirstError);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Receptionist, Doctor")]
        public async Task<IActionResult> UpdateDoctor(int id, UpdateDoctorRequest request)
        {
            var command = _mapper.Map<UpdateDoctorCommand>((id, request));
            var result = await _mediator.Send(command);
            if (result.IsError)
            {
                return BadRequest(result.FirstError);
            }
            return Ok(_mapper.Map<DoctorResponse>(result.Value));
        }

        [HttpGet("by-office")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetDoctorsByOffice(int officeId, int pageNumber = 1, int pageSize = 10)
        {
            var query = new FilterByOfficeQuery(officeId, pageNumber, pageSize);
            var result = await _mediator.Send(query);
            if (result.IsError)
            {
                return BadRequest(result.FirstError);
            }
            return Ok(_mapper.Map<List<DoctorResponse>>(result.Value));
        }

        [HttpGet("by-specialization")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetDoctorsBySpecialization(int specializationId, int pageNumber = 1, int pageSize = 10)
        {
            var query = new FilterBySpecializationQuery(specializationId, pageNumber, pageSize);
            var result = await _mediator.Send(query);
            if (result.IsError)
            {
                return BadRequest(result.FirstError);
            }
            return Ok(_mapper.Map<List<DoctorResponse>>(result.Value));
        }

        [HttpGet("search-by-name")]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> SearchDoctorsByName(SearchByNameRequest request, int pageNumber = 1, int pageSize = 10)
        {
            var query = _mapper.Map<SearchByNameQuery>((request, pageNumber, pageSize));
            var result = await _mediator.Send(query);
            if (result.IsError)
            {
                return BadRequest(result.FirstError);
            }
            return Ok(_mapper.Map<List<DoctorResponse>>(result.Value));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var query = new ViewByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result.IsError)
            {
                return BadRequest(result.FirstError);
            }
            return Ok(_mapper.Map<DoctorResponse>(result.Value));
        }

        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAllDoctors(int pageNumber = 1, int pageSize = 10)
        {
            var query = new ViewDoctorsQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            if (result.IsError)
            {
                return BadRequest(result.FirstError);
            }
            return Ok(_mapper.Map<List<DoctorResponse>>(result.Value));
        }
    }
}
