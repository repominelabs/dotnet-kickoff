using Renci.SshNet.Sftp;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.Interfaces.Shared;

public interface ISftpService
{
    bool CreateDirectory(SftpSettings sftpSettings, string remoteDirectoryPath);
    bool DeleteDirectory(SftpSettings sftpSettings, string remoteDirectoryPath);
    bool DeleteFile(SftpSettings sftpSettings, string remoteFilePath);
    Stream DownloadFile(SftpSettings sftpSettings, string remoteFilePath, string localFilePath);
    IEnumerable<SftpFile> ListAllFiles(SftpSettings sftpSettings, string remoteDirectory);
    bool UploadFile(SftpSettings sftpSettings, Stream fs, string remoteFilePath);
}