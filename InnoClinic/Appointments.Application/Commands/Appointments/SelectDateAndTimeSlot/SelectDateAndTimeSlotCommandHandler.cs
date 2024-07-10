
public class SelectDateAndTimeSlotCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<SelectDateAndTimeSlotCommand, ErrorOr<Appointment>>
{
    public async Task<ErrorOr<Appointment>> Handle(SelectDateAndTimeSlotCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
