using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.DTOs.Sftp;

public class DeleteFileRequest
{
    public SftpSettings SftpSettings { get; set; }
    public string RemoteFilePath { get; set; }
}