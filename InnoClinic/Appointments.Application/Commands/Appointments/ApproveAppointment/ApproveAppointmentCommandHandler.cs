public class ApproveAppointmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ApproveAppointmentCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var appointment = await unitOfWork.AppointmentsRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment is null)
            {
                return Errors.Appointments.NotFound;
            }

            appointment.IsApproved = true;
            await unitOfWork.AppointmentsRepository.UpdateAppointmentAsync(appointment, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
