using Profiles.Domain.Entities;
using System.Linq.Expressions;

namespace Profiles.Domain.Abstractions
{
    public interface IDoctorRepository
    {
        public Task<Doctor> AddDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default);
        public Task<Doctor> UpdateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default);
        public Task DeleteDoctorAsync(int id, CancellationToken cancellationToken = default);
        public Task<Doctor> GetDoctorByIdAsync(int id, CancellationToken cancellationToken = default);
        public Task<List<Doctor>> ListDoctorsAsync(Expression<Func<Doctor, bool>> filter, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        public Task<List<Doctor>> GetListDoctorsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        public Task<List<Doctor>> FilterBySpecializationAsync(int specializationId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        public Task<List<Doctor>> SearchByNameAsync(string firstName, string lastName, string middleName, CancellationToken cancellationToken = default);
        public Task<List<Doctor>> FilterByOfficeOnMapAsync(int officeId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        public Task<List<Doctor>> FilterByOfficeAsync(int officeId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        public Task UpdateStatusAsync(int doctorId, ProfileStatus status, CancellationToken cancellationToken = default);
    }
}
