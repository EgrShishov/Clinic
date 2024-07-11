public sealed record DeleteFileCommand(Guid fileId) : IRequest<ErrorOr<Unit>>
{
}
