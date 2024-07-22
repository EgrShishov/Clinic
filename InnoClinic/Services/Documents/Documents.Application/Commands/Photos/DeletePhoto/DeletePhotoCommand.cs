public sealed record DeletePhotoCommand(string fileName) : IRequest<ErrorOr<Unit>>
{
}
