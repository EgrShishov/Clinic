﻿using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
public class OfficeRepository : IOfficeRepository
{
    private readonly OfficesDbContext _dbContext;

    public OfficeRepository(OfficesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Office>> GetOfficesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Offices.ToListAsync(cancellationToken);
    }

    public async Task<Office> GetOfficeByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Offices.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<string> AddOfficeAsync(Office office, CancellationToken cancellationToken = default)
    {
        var addedOffice = await _dbContext.Offices.AddAsync(office, cancellationToken);
        return addedOffice.Entity.Id;
    }

    public Task UpdateOfficeAsync(Office office, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(office).State = EntityState.Modified;
        return Task.FromResult(office);
    }

    public async Task DeleteOfficeAsync(string id, CancellationToken cancellationToken = default)
    {
        var office = await _dbContext.Offices.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if (office is null)
        {
            return;
        }
        _dbContext.Offices.Remove(office);
    }

    public Task<List<Office>> ListOfficesAsync(Expression<Func<Office, bool>> filter, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Offices.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.ToListAsync(cancellationToken);
    }
}   