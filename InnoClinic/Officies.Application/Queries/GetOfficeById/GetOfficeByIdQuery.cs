namespace Officies.Application.Queries.GetOfficeById
{
    public sealed record GetOfficeByIdQuery(string Id) : IRequest<Office>
    {
    }
}
