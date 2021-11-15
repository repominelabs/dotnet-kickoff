using Microsoft.AspNetCore.Mvc;
using Renci.SshNet.Sftp;
using Repomine.Dotnet.Core.Application.DTOs.Sftp;
using Repomine.Dotnet.Core.Application.Interfaces.Shared;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Presentation.Controllers.Integration;

[ApiController]
[Route("[controller]")]
public class SftpController : ControllerBase
{
    private readonly ISftpService _sftpService;

    public SftpController(ISftpService sftpService)
    {
        _sftpService = sftpService;
    }

    [HttpPost("create-directory")]
    public IActionResult CreateDirectory([FromBody] CreateDirectoryRequest request)
    {
        Response<bool> response = new()
        {
            Succeeded = _sftpService.CreateDirectory(request.SftpSettings, request.RemoteDirectoryPath),
        };
        
        return Ok(response);
    }

    [HttpPost("delete-directory")]
    public IActionResult DeleteDirectory([FromBody] DeleteDirectoryRequest request)
    {
        Response<bool> response = new()
        {
            Succeeded = _sftpService.DeleteDirectory(request.SftpSettings, request.RemoteDirectoryPath),
        };

        return Ok(response);
    }

    [HttpPost("delete-file")]
    public IActionResult DeleteFile([FromBody] DeleteFileRequest request)
    {
        Response<bool> response = new()
        {
            Succeeded = _sftpService.DeleteFile(request.SftpSettings, request.RemoteFilePath),
        };

        return Ok(response);
    }

    [HttpPost("download-file")]
    public IActionResult DownloadFile([FromBody] DownloadFileRequest request)
    {
        Stream stream = _sftpService.DownloadFile(request.SftpSettings, request.RemoteFilePath, request.LocalFilePath);
        Response<Stream> response = new()
        {
            Data = stream,
            Succeeded = stream != null,
        };

        return Ok(response);
    }

    [HttpPost("list-all-files")]
    public IActionResult ListAllFiles([FromBody] ListAllFilesRequest request)
    {
        IEnumerable<SftpFile> files = _sftpService.ListAllFiles(request.SftpSettings, request.RemoteDirectory);
        Response<IEnumerable<SftpFile>> response = new()
        {
            Data = files,
            Succeeded = files != null,
        };

        return Ok(response);
    }

    [HttpPost("upload-file")]
    public IActionResult UploadFile([FromBody] UploadFileRequest request)
    {
        List<object> data = new();
        foreach (IFormFile file in request.Files)
        {
            if (file.Length > 0)
            {
                using var fs = file.OpenReadStream();
                bool result = _sftpService.UploadFile(request.SftpSettings, fs, request.RemoteFilePath);
                data.Add(new { Succeeded = result, Name = file.FileName });
            }
        }

        Response<List<object>> response = new()
        {
            Data = data,
            Succeeded = true,
        };

        return Ok(response);
    }
}