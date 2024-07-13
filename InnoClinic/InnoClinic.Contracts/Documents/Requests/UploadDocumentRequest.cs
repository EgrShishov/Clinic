using Microsoft.AspNetCore.Http;

public record UploadDocumentRequest(IFormFile file, int resultId);