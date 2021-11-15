using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.DTOs.Sftp;

public class ListAllFilesRequest
{
    public SftpSettings SftpSettings { get; set; }
    public string RemoteDirectory { get; set; }
}