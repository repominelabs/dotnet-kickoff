using Microsoft.AspNetCore.Http;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.DTOs.Sftp;

public class UploadFileRequest
{
    public SftpSettings SftpSettings { get; set; }
    public IEnumerable<IFormFile> Files { get; set; }
    public string RemoteFilePath { get; set; }
}