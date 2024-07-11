public class ViewAppointmentsHistoryQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ViewAppointmentsHistoryQuery, ErrorOr<List<Appointment>>>
{
    public async Task<ErrorOr<List<Appointment>>> Handle(ViewAppointmentsHistoryQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentsRepository.ListAsync(a => a.PatientId == request.PatientId);
        if (appointments is null)
        {
            return Errors.Appointments.NotFound;
        }

        return appointments.ToList();
    }
}
