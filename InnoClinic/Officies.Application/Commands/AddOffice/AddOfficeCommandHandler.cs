public class AddOfficeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddOfficeCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(AddOfficeCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.OfficeRepository.AddOfficeAsync(request.office, unitOfWork.Session);
        return Unit.Value;
    }
}
