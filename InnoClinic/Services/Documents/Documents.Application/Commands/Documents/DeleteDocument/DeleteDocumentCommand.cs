public sealed record DeleteDocumentCommand(string fileName) : IRequest<ErrorOr<Unit>>
{
}
