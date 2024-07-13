public class DeleteDocumentCommandHandler(IFileFacade fileFacade) : IRequestHandler<DeleteDocumentCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        await fileFacade.DeleteDocumentAsync(request.fileName);
        return Unit.Value;
    }
}
