public record ServiceResponse(
    int ServiceCategoryId,
    string ServiceCategoryName,
    List<AppointmentInfoResponse> appointments
    //List<Analyzez> //wtf should i do here?
    );
