using InnoClinic.Contracts.Profiles.Receptionists.Requests;
using InnoClinic.Contracts.Profiles.Receptionists.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Commands.Receptionists.CreateReceptionist;
using Profiles.Application.Commands.Receptionists.DeleteReceptionist;
using Profiles.Application.Commands.Receptionists.UpdateReceptionist;
using Profiles.Application.Querires.Receptionists.ViewAllReceptionists;
using Profiles.Application.Querires.Receptionists.ViewReceptionistById;

namespace Profiles.API.Controllers
{
    [Authorize(Roles = "Receptionist")]
    public class ReceptionistsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ReceptionistsController(IMediator mediator, ILogger logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create-profile")]
        public async Task<IActionResult> CreateReceptionsitProfile(CreateReceptionistRequest request)
        {
            var command = _mapper.Map<CreateReceptionistCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<ReceptionistResponse>(value)),
                errors => Problem(errors));
        }

        [HttpPut("edit-profile/{id:int}")]
        public async Task<IActionResult> UpdateReceptionsitProfile(int id, UpdateReceptionistRequest request)
        {
            var command = _mapper.Map<UpdateReceptionistCommand>((id, request));
            var result = await _mediator.Send(command);

            return result.Match(
                value => Ok(_mapper.Map<ReceptionistResponse>(value)),
                errors => Problem(errors));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReceptionsitProfile(int id)
        {
            var result = await _mediator.Send(new DeleteReceptionistCommand(id));
            
            if (result.IsError)
            {
                return BadRequest(result.FirstError.Description);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReceptionist(int id)
        {
            var result = await _mediator.Send(new ViewReceptionistByIdQuery(id));

            return result.Match(
                value => Ok(_mapper.Map<ReceptionistResponse>(value)),
                errors => Problem(errors));
        }

        [HttpGet("all-receptionists")]
        public async Task<IActionResult> GetAllReceptionist(int pageNumber, int pageSize)
        {
            var result = await _mediator.Send(new ViewAllReceptionistsQuery(pageNumber, pageSize));

            return result.Match(
                value => Ok(_mapper.Map<List<ReceptionistResponse>>(value)),
                errors => Problem(errors));
        }
    }
}
