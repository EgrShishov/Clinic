public class FileFacade : IFileFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileRepository _fileRepository;
    public FileFacade(IUnitOfWork unitOfWork, IFileRepository fileRepository)
    {
        _unitOfWork = unitOfWork;
        _fileRepository = fileRepository;
    }

    public async Task DeleteDocumentAsync(string fileName)
    {
        string containerName = "Documents";
        try
        {
            var url = await _fileRepository.DeleteAsync(fileName, containerName);
            var document = await _unitOfWork.Documents.GetDocumentByUrlAsync(url);
            if (document != null)
            {
                await _unitOfWork.Documents.DeleteDocumentByUrlAsync(url);
                await _unitOfWork.CompleteAsync();
            }
        }
        catch (Exception)
        {
            throw; // TODO : change to Errors and handle exception
        }
    }

    public async Task DeletePhotoAsync(string fileName)
    {
        string containerName = "Photos";
        try
        {
            var url = await _fileRepository.DeleteAsync(fileName, containerName);
            var document = await _unitOfWork.Photos.GetPhotoByUrlAsync(url);
            if (document != null)
            {
                await _unitOfWork.Photos.DeletePhotoByUrlAsync(url);
                await _unitOfWork.CompleteAsync();
            }
        }
        catch (Exception)
        {
            throw; // TODO : change to Errors and handle exception
        }
    }

    public async Task<FileResponse> DownloadDocumentAsync(string fileName)
    {
        string containerName = "Documents";
        var document = await _unitOfWork.Documents.GetDocumentByUrlAsync(fileName);
        if (document == null)
        {
            return null;
        }

        return await _fileRepository.DownloadAsync(fileName, containerName);
    }

    public async Task<FileResponse> DownloadPhotoAsync(string fileName)
    {
        string containerName = "Photos";
        var photo = await _unitOfWork.Photos.GetPhotoByUrlAsync(fileName);
        if (photo == null)
        {
            return null;
        }

        return await _fileRepository.DownloadAsync(fileName, containerName);
    }

    public async Task<string> UploadDocumentAsync(Stream fileStream, string contentType, string fileName, int resultId)
    {
        var url = string.Empty;
        string containerName = "Documents";

        try
        {
            url = await _fileRepository.UploadAsync(fileStream, fileName, contentType, containerName);

            if (url == null)
            {
                return null; //TODO : change to Errors Class
            }
            var docuemnt = new Document
            {
                Url = url,
                ResultId = resultId
            };

            await _unitOfWork.Documents.SaveDocumentAsync(docuemnt);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception)
        {
            if (!string.IsNullOrEmpty(url))
            {
                await _fileRepository.DeleteAsync(fileName, containerName); //TODO: implement some deletion logic
            }
            throw;
        }

        return url;
    }

    public async Task<string> UploadPhotoAsync(Stream fileStream, string fileName, string contentType)
    {
        var url = string.Empty;
        string containerName = "Photos";

        try
        {
            url = await _fileRepository.UploadAsync(fileStream, fileName, contentType, containerName);

            if (url == null)
            {
                return null; //TODO : change to Errors Class
            }
            var photo = new Photo
            {
                Url = url
            };

            await _unitOfWork.Photos.SavePhotoAsync(photo);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception)
        {
            if (!string.IsNullOrEmpty(url))
            {
                await _fileRepository.DeleteAsync(fileName, containerName); //TODO: implement some deletion logic
            }
            throw;
        }

        return url;
    }
}
