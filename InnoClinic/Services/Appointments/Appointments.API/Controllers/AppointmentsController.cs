public class AppointmentsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AppointmentsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("schedule/{id:int}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetAppointmentSchedule(int id, DateTime appointmentDate)
    {
        var result = await _mediator.Send(new ViewAppointmentScheduleQuery(id, appointmentDate));

        return result.Match(
            schedule => Ok(_mapper.Map<List<AppointmentHistoryResponse>>(schedule)),
            errors => Problem(errors));
    }

    [HttpGet("all")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> GetAppointmentList(
        DateTime? appointmentDate, 
        int? doctorId, 
        int? serviceId, 
        bool appointmentStatus,
        int? officeId)
    {
        var command = _mapper.Map<ViewAppointmentsListQuery>((appointmentDate, doctorId, serviceId, appointmentStatus, officeId));
        var result = await _mediator.Send(command);

        return result.Match(
            list => Ok(_mapper.Map<List<AppointmentHistoryResponse>>(list)),
            errors => Problem(errors));
    }

    [HttpGet("history")]
    [Authorize(Roles = "Doctor, Patient")]
    public async Task<IActionResult> GetAppointmentHistory(int id)
    {
        var result = await _mediator.Send(new ViewAppointmentsHistoryQuery(id));

        return result.Match(
            history => Ok(_mapper.Map<List<AppointmentHistoryResponse>>(history)),
            errors => Problem(errors));
    }

    [HttpPost("create-appointment")]
    [Authorize(Roles = "Patient, Receptionist")]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentRequest request)
    {
        var command = _mapper.Map<CreateAppointmentCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            appointment => Ok(_mapper.Map<AppointmentHistoryResponse>(appointment)),
            errors => Problem(errors));
    }

    [HttpPost("approve/{id:int}")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> ApproveAppointment(int id)
    {
        var result = await _mediator.Send(new ApproveAppointmentCommand(id));
        return result.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpPost("cancel/{id:int}")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> CancelAppointment(int id)
    {
        var result = await _mediator.Send(new CancelAppointmentCommand(id));
        return result.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpPost("select-slots")]
    [Authorize(Roles = "Receptionist, Patient")]
    public async Task<IActionResult> SelectDateAndTimeSlot(DateTime appointmentDate, TimeSpan appointmentTime)
    {
        throw new NotImplementedException();
    }

    [HttpPut("reschedule/{id:int}")]
    [Authorize(Roles = "Receptionist, Patient")]
    public async Task<IActionResult> RescheduleAppointment(int id, int doctorId, DateTime appointmentDate, TimeSpan appointmentTime)
    {
        var command = _mapper.Map<RescheduleAppointmentCommand>((id, doctorId, appointmentDate, appointmentTime));
        var result = await _mediator.Send(command);
        return result.Match(
            rescheduledAppointment => Ok(_mapper.Map<AppointmentHistoryResponse>(rescheduledAppointment)),
            errors => Problem(errors));
    }


}
