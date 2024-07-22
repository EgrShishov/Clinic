public class ResultsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ResultsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Patient, Doctor")]
    public async Task<IActionResult> GetAppointmentResult(int id)
    {
        var result = await _mediator.Send(new ViewAppointmentResultQuery(id));
        return result.Match(
            appointmentResult => Ok(_mapper.Map<ResultResponse>(appointmentResult)),
            errors => Problem(errors));
    }

    [HttpGet("download/{id:int}")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> DownloadAppointmentsResults(int id)
    {
        var result = await _mediator.Send(new DownloadAppointmentResultsQuery(id));
        var filename = $"appointments-{id}";
        var mimeType = "application/octet-stream";

        return result.Match(
            fileBytes => Ok(new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = filename
            }),
            errors => Problem(errors));
    }

    [HttpPost("create-result")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> CreateAppointmentResult(int doctorId, CreateAppointmentResultRequest request)
    {
        var command = _mapper.Map<CreateAppointmentsResultCommand>((doctorId, request));
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<ResultResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> UpdateAppointmentResultInfo(int id, UpdateAppointmentResultRequest request)
    {
        var command = _mapper.Map<EditAppointmentsResultCommand>((id, request));
        var result = await _mediator.Send(command);

        return result.Match(
            appointmentResult => Ok(_mapper.Map<ResultResponse>(appointmentResult)),
            errors => Problem(errors));
    }
}
