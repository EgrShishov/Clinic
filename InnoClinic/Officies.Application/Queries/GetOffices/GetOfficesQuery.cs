namespace Officies.Application.Queries.GetOffices
{
    public sealed record GetOfficesQuery() : IRequest<ErrorOr<List<Office>>>
    {
    }
}
