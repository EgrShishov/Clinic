using Microsoft.AspNetCore.Authorization;

public class ServiceController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ServiceController(IMediator mediator, ILogger logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("all-services")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetAllServices()
    {
        var result = await _mediator.Send(new ViewServicesQuery());
        return result.Match(
            services => Ok(_mapper.Map<List<ServiceInfoResponse>>(services)),
            errors => Problem(errors));
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> GetServiceInfo(int id)
    {
        var result = await _mediator.Send(new ViewServicesInfoQuery(id));
        return result.Match(
            services => Ok(_mapper.Map<ServiceInfoResponse>(services)),
            errors => Problem(errors));
    }

    [HttpPost("create-service")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> CreateService(CreateServiceRequest request)
    {
        var command = _mapper.Map<CreateServiceCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
            services => Ok(_mapper.Map<ServiceInfoResponse>(services)),
            errors => Problem(errors));
    }

    [HttpPut("update-service")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> UpdateServiceInfo(int id, UpdateServiceInfoRequest request)
    {
        var command = _mapper.Map<UpdateServiceCommand>((id, request));
        var result = await _mediator.Send(command);

        return result.Match(
            service => Ok(_mapper.Map<ServiceInfoResponse>(service)),
            errors => Problem(errors));
    }

    [HttpPost("change-status")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> ChangeServiceStatus(int id, bool status)
    {
        var result = await _mediator.Send(new ChangeServiceStatusCommand(id, status));

        return result.Match(
            service => Ok(_mapper.Map<ServiceInfoResponse>(service)),
            errors => Problem(errors));
    }
}
