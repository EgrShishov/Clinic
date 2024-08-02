﻿using MongoDB.Driver;
using System.Linq.Expressions;
public interface IOfficeRepository
{
    Task<List<Office>> GetOfficesAsync();
    Task<List<Office>> ListOfficesAsync(Expression<Func<Office, bool>> filter);
    Task<Office> GetOfficeByIdAsync(string id, IClientSessionHandle session);
    Task AddOfficeAsync(Office office, IClientSessionHandle session);
    Task UpdateOfficeAsync(Office office, IClientSessionHandle session);
    Task DeleteOfficeAsync(string id, IClientSessionHandle session);
}