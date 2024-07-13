public record SpecializationInfoResponse(
    string SpecializationName,
    string SpecializationStatus,
    List<ServiceInfoResponse> relatedServices
    );