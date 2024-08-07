﻿public record OfficeCreatedEvent
{
    public string Id { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string OfficeNumber { get; set; }
    public string PhotoId { get; set; }
    public string RegistryPhoneNumber { get; set; }
}