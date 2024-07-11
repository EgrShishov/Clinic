using MediatR;
using Microsoft.AspNetCore.Mvc;

public class DocumentsController : ApiController
{
    private readonly IMediator _mediator;

    public DocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("download/{id:guid}")]
    public async Task<IActionResult> DownloadFile(Guid fileId)
    {
        throw new NotImplementedException();
    }
}
