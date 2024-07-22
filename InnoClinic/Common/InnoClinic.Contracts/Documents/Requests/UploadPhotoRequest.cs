using Microsoft.AspNetCore.Http;

public record UploadPhotoRequest(IFormFile file);