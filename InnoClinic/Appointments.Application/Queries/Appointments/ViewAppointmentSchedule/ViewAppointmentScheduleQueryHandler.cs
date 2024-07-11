public class ViewAppointmentScheduleQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewAppointmentScheduleQuery, ErrorOr<List<Appointment>>>
{
    public async Task<ErrorOr<List<Appointment>>> Handle(ViewAppointmentScheduleQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentsRepository.
            ListAsync(a => a.DoctorId == request.DoctorId && a.Date == request.AppointmentDate.Date);
        if (appointments is null)
        {
            return Errors.Appointments.NotFound;
        }

        return appointments.OrderBy(a => a.Time).ToList();
    }
}
