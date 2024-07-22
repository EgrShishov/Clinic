using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Patient, Doctor, Receptionist")]
public class FileController : ApiController
{
    private readonly IFileFacade _fileFacade;
    public FileController(IFileFacade fileFacade)
    {
        _fileFacade = fileFacade;
    }

    [HttpPost("document")]
    public async Task<IActionResult> UploadDocument(UploadDocumentRequest request)
    {
        if (request.file == null || request.file.Length == 0)
            return BadRequest("Invalid file.");

        var fileName = Path.GetFileName(request.file.FileName);
        var contentType = request.file.ContentType;

        using var stream = request.file.OpenReadStream();

        var url = await _fileFacade.UploadDocumentAsync(stream, fileName, contentType, request.resultId);
        return Ok(new { Url = url });
    }

    [HttpGet("document/{fileName}")]
    public async Task<IActionResult> DownloadDocument(string fileName)
    {
        var response = await _fileFacade.DownloadDocumentAsync(fileName);
        return File(response.content, response.contentType, response.filename);
    }

    [HttpDelete("document/{fileName}")]
    public async Task<IActionResult> DeleteDocument(string fileName)
    {
        await _fileFacade.DeleteDocumentAsync(fileName);
        return NoContent();
    }

    [HttpPost("photo")]
    public async Task<IActionResult> UploadPhoto(UploadPhotoRequest request)
    {
        if (request.file == null || request.file.Length == 0)
            return BadRequest("Invalid file.");

        var fileName = Path.GetFileName(request.file.FileName);
        var contentType = request.file.ContentType;

        using var stream = request.file.OpenReadStream();

        var url = await _fileFacade.UploadPhotoAsync(stream, fileName, contentType);
        return Ok(new { Url = url });
    }

    [HttpGet("photo/{fileName}")]
    public async Task<IActionResult> DownloadPhoto(string fileName)
    {
        var response = await _fileFacade.DownloadPhotoAsync(fileName);
        return File(response.content, response.contentType, response.filename);
    }

    [HttpDelete("photo/{fileName}")]
    public async Task<IActionResult> DeletePhoto(string fileName)
    {
        await _fileFacade.DeletePhotoAsync(fileName);
        return NoContent();
    }
}
