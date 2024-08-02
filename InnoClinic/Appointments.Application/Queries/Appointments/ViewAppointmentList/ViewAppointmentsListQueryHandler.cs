public class ViewAppointmentsListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewAppointmentsListQuery, ErrorOr<List<Appointment>>>
{
    public async Task<ErrorOr<List<Appointment>>> Handle(ViewAppointmentsListQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.AppointmentsRepository.GetAllAsync(cancellationToken)
            .ContinueWith(task =>
            {
                var appointments = task.Result;
                var query = appointments.AsQueryable();

                if (request.AppointmentDate.HasValue)
                {
                    query = query.Where(a => a.Date == request.AppointmentDate);
                }

                if (request.OfficeId.HasValue)
                {
                    //where should i get officeId?
                }

                query = query.Where(a => a.IsApproved == request.AppointmentStatus);

                if (request.DoctorId.HasValue)
                {
                    query = query.Where(a => a.DoctorId == request.DoctorId);
                }

                if (request.ServiceId.HasValue)
                {
                    query = query.Where(a => a.ServiceId == request.ServiceId);
                }

                return query.ToList();
            });
    }
}
