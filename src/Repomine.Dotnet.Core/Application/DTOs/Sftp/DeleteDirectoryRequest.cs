using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.DTOs.Sftp;

public class DeleteDirectoryRequest
{
    public SftpSettings SftpSettings { get; set; }
    public string RemoteDirectoryPath { get; set; }
}