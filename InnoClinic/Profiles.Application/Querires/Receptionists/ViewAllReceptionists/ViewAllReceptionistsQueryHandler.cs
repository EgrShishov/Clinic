using InnoProfileslinic.Domain.Entities;

namespace Profiles.Application.Querires.Receptionists.ViewAllReceptionists
{
    public class ViewAllReceptionistsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<ViewAllReceptionistsQuery, ErrorOr<List<Receptionist>>>
    {
        public async Task<ErrorOr<List<Receptionist>>> Handle(ViewAllReceptionistsQuery request, CancellationToken cancellationToken)
        {
            var receptionists = await unitOfWork.ReceptionistsRepository.GetListReceptionistAsync(request.PageNumber, request.PageSize);
            
            if (receptionists is null)
            {
                return Errors.Receptionists.NotFound;
            }
            return receptionists;
        }
    }
}
