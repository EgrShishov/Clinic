using InnoProfileslinic.Domain.Entities;

namespace Profiles.Application.Querires.Receptionists.ViewReceptionistById
{
    public class ViewReceptonistsByIdQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<ViewReceptionistByIdQuery, ErrorOr<Receptionist>>
    {
        public async Task<ErrorOr<Receptionist>> Handle(ViewReceptionistByIdQuery request, CancellationToken cancellationToken)
        {
            var receptionist = await unitOfWork.ReceptionistsRepository.GetReceptionistByIdAsync(request.ReceptionistId);

            if (receptionist is null)
            {
                return Errors.Receptionists.NotFound;
            }

            return receptionist;
        }
    }
}
