using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class DoctorRepository : IDoctorRepository
{
    private readonly ProfilesDbContext _dbContext;
    public DoctorRepository(ProfilesDbContext dbContext) 
    {
        _dbContext = dbContext;
    }

    public async Task<Doctor> AddDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        var newDoctor = await _dbContext.Doctors.AddAsync(doctor);
        return newDoctor.Entity;
    }

    public async Task DeleteDoctorAsync(int id, CancellationToken cancellationToken = default)
    {
        var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        if (doctor != null)
        {
            _dbContext.Entry(doctor).State = EntityState.Deleted;
        }
    }

    public async Task<List<Doctor>> FilterByOfficeAsync(int officeId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Doctors.AsQueryable();

        query = query.Where(d => d.OfficeId == officeId) //offset pagination
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<List<Doctor>> FilterByOfficeOnMapAsync(int officeId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Doctors.AsQueryable(); // what does it mean?

        query = query.Where(d => d.OfficeId == officeId)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<List<Doctor>> FilterBySpecializationAsync(int specializationId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Doctors.AsQueryable();

        query = query.Where(d => d.SpecializationId == specializationId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<Doctor> GetDoctorByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Doctor>> ListDoctorsAsync(Expression<Func<Doctor, bool>> filter, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Doctors.AsQueryable();

        if(filter != null)
        {
            query = query.Where(filter);
        }

        query = query.Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);

        return await query.ToListAsync();
    } 
    public async Task<List<Doctor>> GetListDoctorsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Doctor>> SearchByNameAsync(string firstName, string lastName, string middleName, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Doctors.AsQueryable();

        query = query.Where(d => d.LastName == lastName && d.FirstName == firstName && d.MiddleName == middleName);

        return await query.ToListAsync();
    }

    public Task<Doctor> UpdateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(doctor).State = EntityState.Modified;
        return Task.FromResult(doctor);
    }

    public async Task UpdateStatusAsync(int doctorId, ProfileStatus status, CancellationToken cancellationToken = default)
    {
        var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
        if(doctor != null)
        {
            doctor.Status = status;
        }

        _dbContext.Entry(doctor).State = EntityState.Modified;
    }
}
