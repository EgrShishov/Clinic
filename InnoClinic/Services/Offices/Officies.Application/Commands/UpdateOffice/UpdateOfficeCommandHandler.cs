public class UpdateOfficeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateOfficeCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.OfficeRepository.UpdateOfficeAsync(request.office, unitOfWork.Session);
        return Unit.Value;
    }
}