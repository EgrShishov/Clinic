public sealed record CreateSpecializationCommand(string SpecializatioName, bool IsActive) : IRequest<ErrorOr<SpecializationResponse>>
{
}

