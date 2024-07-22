using Microsoft.AspNetCore.Mvc;

public class OfficesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OfficesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetOffices()
    {
        var response = await _mediator.Send(new GetOfficesQuery());
            return response.Match(
                value => Ok(response),
                errors => Problem(errors));
    }
        
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOffice(string id)
    {
        var response = await _mediator.Send(new GetOfficeByIdQuery(id));
        return response.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOffice(CreateOfficeRequest request)
    {
        var command = _mapper.Map<CreateOfficeCommand>(request);
        var response = await _mediator.Send(command);
            return response.Match(
                value => Ok(value),
                errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOffice(string id, UpdateOfficeRequest request)
    {
        var command = _mapper.Map<UpdateOfficeCommand>((id, request));

        var response = await _mediator.Send(command);
            return response.Match(
                value => Ok(value),
                errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOffice(string id)
    {
        await _mediator.Send(new DeleteOfficeCommand(id));
        return NoContent();
    }
}
