using Microsoft.AspNetCore.Mvc;

public class DocumentsController : ApiController
{
    private readonly IBlobStorageService _storageService;

    public DocumentsController(IBlobStorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpGet("{container}/{id:guid}")]
    public async Task<IActionResult> DownloadFile(string container, Guid fileId)
    {
        var response = await _storageService.DownloadAsync(fileId, container);

        if (response == null)
        {
            return NoContent();
        }

        return File(response.stream, response.contentType, response.filename);
    }

    [HttpPost("{container}/{blob}")]
    public async Task<IActionResult> UploadFile(IFormFile file, string container)
    {
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        var id = await _storageService.UploadAsync(stream, Request.ContentType, container);
        
        if (!id.HasValue)
        {
            return Problem();
        }
        return Ok(id);
    }

    [HttpDelete("{container}/{id:guid}")]
    public async Task<IActionResult> DeleteFile(string container, Guid fileId)
    {
        await _storageService.DeleteAsync(fileId, container);
        return NoContent();
    }
}
