using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class PatientsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PatientsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [Authorize(Roles = "Patient, Receptionist")]
    [HttpPost("create-profile")]
    public async Task<IActionResult> CreatePatientProfile(CreatePatientRequest request)
    {
        var command = _mapper.Map<CreatePatientCommand>(request);
        var result = await _mediator.Send(command);
        if (result.IsError)
        {
            return BadRequest(result.FirstError.Description);
        }
        return Ok(result.Value);
    }

    [Authorize(Roles = "Patient, Receptionist")]
    [HttpPut("edit-profile/{id:int}")]
    public async Task<IActionResult> EditProfile(int id, UpdatePatientRequest request)
    {
        var command = _mapper.Map<UpdatePatientCommand>((id, request));
        var result = await _mediator.Send(command);

        if (result.IsError)
        {
            return BadRequest(result.FirstError.Description);
        }

        return Ok(result.Value);
    }

    [Authorize(Roles = "Receptionist")]
    [HttpDelete]
    public async Task<IActionResult> DeletePatientProfile(int id)
    {
        var result = await _mediator.Send(new DeletePatientCommand(id));

        if (result.IsError)
        {
            return BadRequest(result.FirstError.Description);
        }

        return Ok(result.Value);
    }

    [Authorize(Roles = "Doctor, Receptionist, Patient")] //difference between roles -> receptionist doesnt see appoitments
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientProfile(int id)
    {
        var result = await _mediator.Send(new ViewPatientProfileQuery(id));

        if (result.IsError)
        {
            return BadRequest(result.FirstError.Description);
        }

        return Ok(result.Value);
    }

    [Authorize(Roles = "Receptionist")]
    [HttpGet("search-by-name")]
    public async Task<IActionResult> GetPatientByName(string FirstName, string LastName, string MiddleName)
    {
        var result = await _mediator.Send(new SearchPatientByNameQuery(FirstName, LastName, MiddleName));

        if (result.IsError)
        {
            return BadRequest(result.FirstError.Description);
        }

        return Ok(result.Value);
    }

    [Authorize(Roles = "Receptionist")]
    [HttpGet("all-patients")]
    public async Task<IActionResult> GetAllPatients(int pageNumber, int pageSize)
    {
        var result = await _mediator.Send(new ViewAllPatientsQuery(pageNumber, pageSize));

        if (result.IsError)
        {
            return BadRequest(result.FirstError.Description);
        }

        return Ok(result.Value);
    }
}
