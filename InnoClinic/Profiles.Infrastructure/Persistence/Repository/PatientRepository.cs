using Microsoft.EntityFrameworkCore;
using Profiles.Infrastructure.Persistence.Data;
using System.Linq.Expressions;

namespace Profiles.Infrastructure.Persistence.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ProfilesDbContext _dbContext;
        public PatientRepository(ProfilesDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Patient> AddPatientAsync(Patient patient, CancellationToken cancellationToken = default)
        {
            var newPatient = await _dbContext.Patients.AddAsync(patient);
            return newPatient.Entity;
        }

        public async Task DeletePatientAsync(int id, CancellationToken cancellationToken = default)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient != null)
            {
                _dbContext.Entry(patient).State = EntityState.Deleted;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Patient>> ListPatientsAsync(Expression<Func<Patient, bool>> filter, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Patients.AsQueryable();

            if(filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);

            return await query.ToListAsync();
        }
        public async Task<List<Patient>> GetListPatientsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Patients
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        public Task<List<Patient>> SearchPatientByNameAsync(string firstName, string lastName, string middleName, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Patients.AsQueryable();

            query = query.Where(p => p.FirstName == firstName && p.LastName == lastName && p.MiddleName == middleName);

            return query.ToListAsync();
        }

        public Task<Patient> UpdatePatientAsync(Patient patient, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(patient).State = EntityState.Modified;
            return Task.FromResult(patient);
        }
    }
}
