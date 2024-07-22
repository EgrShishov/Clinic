public record ServiceCreatedEvent
{
    public int Id { get; set; }
    public string ServiceName { get; set; }
    public int ServiceCategoryId { get; set; }
}

