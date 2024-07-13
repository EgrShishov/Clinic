
public class DeletePhotoCommandHandler(IFileFacade fileFacade) : IRequestHandler<DeletePhotoCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        await fileFacade.DeletePhotoAsync(request.fileName);
        return Unit.Value;
    }
}
