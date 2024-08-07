using Microsoft.AspNetCore.Http;

public class FileFacade : IFileFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private readonly string DocumentsContainerName = "documents";
    private readonly string PhotosContainerName = "photos";
    public FileFacade(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
    {
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
    }

    public async Task<ErrorOr<Unit>> DeleteDocumentAsync(string fileName)
    {
        try
        {
            var result = await _blobStorageService.DeleteAsync(fileName, DocumentsContainerName);

            if (!result)
            {
                return Errors.Documents.NotFound;
            }

            var document = await _unitOfWork.Documents.GetDocumentAsync(fileName);
            
            if (document is null)
            {
                return Errors.Documents.NotFound;
            }

            await _unitOfWork.Documents.DeleteDocumentAsync(fileName);

            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            return Error.Failure(code: "File.UnhandledException", description: ex.Message);
        }

        return Unit.Value;
    }

    public async Task<ErrorOr<Unit>> DeletePhotoAsync(string fileName)
    {
        try
        {
            var result = await _blobStorageService.DeleteAsync(fileName, PhotosContainerName);

            if (!result)
            {
                return Errors.Photos.NotFound;
            }

            var photo = await _unitOfWork.Photos.GetPhotoAsync(fileName);

            if (photo is null)
            {
                return Errors.Photos.NotFound;
            }

            await _unitOfWork.Photos.DeletePhotoAsync(fileName);

            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            return Error.Failure(code: "File.UnhandledException", description: ex.Message);
        }

        return Unit.Value;
    }

    public async Task<ErrorOr<FileResponse>> DownloadDocumentAsync(string fileName)
    {
        return await _blobStorageService.DownloadAsync(fileName, DocumentsContainerName);
    }

    public async Task<ErrorOr<FileResponse>> DownloadPhotoAsync(string fileName)
    {
        return await _blobStorageService.DownloadAsync(fileName, PhotosContainerName);
    }

    public async Task<ErrorOr<string>> UploadDocumentAsync(IFormFile file, int resultId)
    {
        if (file == null || file.Length == 0)
        {
            return Errors.Documents.NotFound;
        }

        var url = await _blobStorageService
            .UploadAsync(file.OpenReadStream(), file.FileName, DocumentsContainerName, file.ContentType);

        var document = new Document
        {
            Url = url,
            ResultId = resultId
        };

        await _unitOfWork.Documents.SaveDocumentAsync(document);

        await _unitOfWork.CompleteAsync();

        return url;
    }

    public async Task<ErrorOr<string>> UploadPhotoAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Errors.Photos.NotFound;
        }

        var url = await _blobStorageService
            .UploadAsync(file.OpenReadStream(), file.FileName, PhotosContainerName, file.ContentType);

        var photo = new Photo
        {
            Url = url
        };

        await _unitOfWork.Photos.SavePhotoAsync(photo);

        await _unitOfWork.CompleteAsync();

        return url;
    }
}
