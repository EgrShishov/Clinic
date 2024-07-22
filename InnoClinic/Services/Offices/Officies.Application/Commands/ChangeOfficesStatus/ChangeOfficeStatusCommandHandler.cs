public class ChangeOfficesStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeOfficesStatusCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(ChangeOfficesStatusCommand request, CancellationToken cancellationToken)
    {
        var office = await unitOfWork.OfficeRepository.GetOfficeByIdAsync(request.Id, unitOfWork.Session);
        if (office == null)
        {
            return Errors.Offices.NotFound;
        }

        office.IsActive = request.isActive;

        await unitOfWork.OfficeRepository.UpdateOfficeAsync(office, unitOfWork.Session);
        return Unit.Value;
    }
}
