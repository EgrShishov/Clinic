public class ServiceCategoryRepository : IServiceCategoryRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public ServiceCategoryRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<ServiceCategory> AddServiceCategoryAsync(ServiceCategory category, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO ServiceCategories (CategoryName, TimeSlotSize)
            VALUES (@{nameof(ServiceCategory.CategoryName}, @{nameof(ServiceCategory.TimeSlotSize)});
            RETURNING Id;";

        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        var id = await sqlConnection.QuerySingleAsync<int>(sql, new
        {
            category.CategoryName,
            category.TimeSlotSize
        });

        category.Id = id;
        return category;
    }

    public async Task<IEnumerable<ServiceCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string query = @"SELECT * FROM ServiceCategories;";

        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        return await sqlConnection.QueryAsync<ServiceCategory>(query);
    }

    public async Task<ServiceCategory> GetServiceCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string query = @"SELECT * FROM ServiceCategories WHERE Id = @Id;";

        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        return await sqlConnection.QuerySingleOrDefaultAsync<ServiceCategory>(query, new { Id = id });
    }

    public async Task UpdateServiceCategoryAsync(ServiceCategory category, CancellationToken cancellationToken = default)
    {
        const string query = @"
            UPDATE ServiceCategories 
            SET CategoryName = @{nameof(ServiceCategory.CategoryName)}, TimeSlotSize = @{nameof(ServiceCategory.TimeSlotSize)}
            WHERE Id = @Id;";

        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        await sqlConnection.ExecuteAsync(query, new
        {
            category.CategoryName,
            category.TimeSlotSize,
            category.Id
        });
    }
}
