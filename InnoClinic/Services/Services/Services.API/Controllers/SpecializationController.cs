using Microsoft.AspNetCore.Authorization;
public class SpecializationController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public SpecializationController(IMediator mediator, ILogger logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("all-specializations")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetAllSpecializations()
    {
        var result = await _mediator.Send(new ViewSpecializationsListQuery());
        return result.Match(
            services => Ok(_mapper.Map<List<SpecializationInfoResponse>>(services)),
            errors => Problem(errors));
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> GetSpecializationInfo(int id)
    {
        var result = await _mediator.Send(new ViewServicesInfoQuery(id));
        return result.Match(
            services => Ok(_mapper.Map<SpecializationInfoResponse>(services)),
            errors => Problem(errors));
    }

    [HttpPost("create-specialization")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> CreateSpecialization(CreateSpecializationRequest request)
    {
        var command = _mapper.Map<CreateSpecializationCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
            services => Ok(_mapper.Map<SpecializationInfoResponse>(services)),
            errors => Problem(errors));
    }

    [HttpPut("update-specialization")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> UpdateSpecializationInfo(int id, UpdateSpecializationRequest request)
    {
        var command = _mapper.Map<UpdateSpecializationCommand>((id, request));
        var result = await _mediator.Send(command);

        return result.Match(
            service => Ok(_mapper.Map<SpecializationInfoResponse>(service)),
            errors => Problem(errors));
    }

    [HttpPost("change-status")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> ChangeSpecializationStatus(int id, bool status)
    {
        var result = await _mediator.Send(new ChangeSpecializationStatusCommand(id, status));

        return result.Match(
            service => Ok(_mapper.Map<ServiceInfoResponse>(service)),
            errors => Problem(errors));
    }
}
