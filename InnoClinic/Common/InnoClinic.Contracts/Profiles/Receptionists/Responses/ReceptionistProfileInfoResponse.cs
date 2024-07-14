public record ReceptionistProfileInfoResponse(
        int id,
        byte[] Photo,
        string FirstName,
        string LastName,
        string MiddleName,
        string Email,
        string City,
        string Street,
        string HouseNumber,
        string OfficeNumber);
