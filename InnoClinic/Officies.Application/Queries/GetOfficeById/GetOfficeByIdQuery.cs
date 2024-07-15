public sealed record GetOfficeByIdQuery(string Id) : IRequest<Office>
{
}
