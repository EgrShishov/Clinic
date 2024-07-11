
public class DeleteFileCommandHandler(IBlobStorageService storageService) : IRequestHandler<DeleteFileCommand, ErrorOr<Unit>>
{
    public Task<ErrorOr<Unit>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
