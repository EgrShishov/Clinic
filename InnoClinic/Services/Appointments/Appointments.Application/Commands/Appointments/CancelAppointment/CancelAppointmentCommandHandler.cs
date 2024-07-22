public class CancelAppointmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelAppointmentCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var appointment = await unitOfWork.AppointmentsRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment is null)
            {
                return Errors.Appointments.NotFound;
            }

            await unitOfWork.AppointmentsRepository.DeleteAppointmentAsync(request.AppointmentId, cancellationToken);
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