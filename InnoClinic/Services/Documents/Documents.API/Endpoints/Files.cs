using Microsoft.AspNetCore.Mvc;

public class Files : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/document", async (IFileFacade fileFacade, [AsParameters] UploadDocumentRequest request) =>
        {
            var response = await fileFacade
                .UploadDocumentAsync(request.File, request.ResultId);

            return response.Match(
                url => Results.Ok(url),
                errors => Results.Extensions.Problem(errors));
        })
        .WithName("UploadDocument")
        .RequireAuthorization("Patient, Doctor, Receptionist");

        app.MapGet("/api/document/{fileName}", async (IFileFacade fileFacade, string fileName) =>
        {
            var response = await fileFacade.DownloadDocumentAsync(fileName);

            return response.Match(
                document => Results.Ok(document),
                errors => Results.Extensions.Problem(errors));
        })
        .WithName("DownloadDocument")
        .RequireAuthorization("Patient, Doctor, Receptionist");

        app.MapDelete("/api/document/{fileName}", async (IFileFacade fileFacade, string fileName) =>
        {
            var response = await fileFacade.DeleteDocumentAsync(fileName);

            return response.Match(
                _ => Results.NoContent(),
                errors => Results.Extensions.Problem(errors));
        })
        .WithName("DeleteDocument")
        .RequireAuthorization("Patient, Doctor, Receptionist");

        app.MapPost("api/photos", async (IFileFacade fileFacade, [AsParameters] UploadPhotoRequest request) =>
        {
            var response = await fileFacade
                .UploadPhotoAsync(request.File);

            return response.Match(
                url => Results.Ok(url),
                errors => Results.Extensions.Problem(errors));
        })
        .WithName("UploadPhoto")
        .RequireAuthorization("Patient, Doctor, Receptionist");

        app.MapGet("/api/photo/{fileName}", async (IFileFacade fileFacade, string fileName) =>
        {
            var response = await fileFacade.DownloadPhotoAsync(fileName);

            return response.Match(
                document => Results.Ok(document),
                errors => Results.Extensions.Problem(errors));
        })
        .WithName("DownloadPhoto")
        .RequireAuthorization("Patient, Doctor, Receptionist");

        app.MapDelete("/api/photo/{fileName}", async (IFileFacade fileFacade, string fileName) =>
        {
            var response = await fileFacade.DeletePhotoAsync(fileName);

            return response.Match(
                _ => Results.NoContent(),
                errors => Results.Extensions.Problem(errors));
        })
        .WithName("DeletePhoto")
        .RequireAuthorization("Patient, Doctor, Receptionist");
    }
}
    
