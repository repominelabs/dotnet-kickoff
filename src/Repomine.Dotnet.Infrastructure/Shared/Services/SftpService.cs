using Renci.SshNet;
using Renci.SshNet.Sftp;
using Repomine.Dotnet.Core.Application.Interfaces.Shared;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Infrastructure.Shared.Services;

public class SftpService : ISftpService
{
    public bool CreateDirectory(SftpSettings sftpSettings, string remoteDirectoryPath)
    {
        using var client = new SftpClient(sftpSettings.Host, sftpSettings.Port == 0 ? 22 : sftpSettings.Port, sftpSettings.UserName, sftpSettings.Password);
        try
        {
            client.Connect();
            if (!client.Exists(remoteDirectoryPath))
            {
                client.CreateDirectory(remoteDirectoryPath);
                return true;
            }
            return false;
        }
        catch (Exception exception)
        {
            // Todo : log
            return false;
        }
        finally
        {
            client.Disconnect();
        }
    }

    public bool DeleteDirectory(SftpSettings sftpSettings, string remoteDirectoryPath)
    {
        using var client = new SftpClient(sftpSettings.Host, sftpSettings.Port == 0 ? 22 : sftpSettings.Port, sftpSettings.UserName, sftpSettings.Password);
        try
        {
            client.Connect();
            if (!client.Exists(remoteDirectoryPath))
            {
                client.DeleteDirectory(remoteDirectoryPath);
                return true;
            }
            return false;
        }
        catch (Exception exception)
        {
            // Todo : log
            return false;
        }
        finally
        {
            client.Disconnect();
        }
    }

    public bool DeleteFile(SftpSettings sftpSettings, string remoteFilePath)
    {
        using var client = new SftpClient(sftpSettings.Host, sftpSettings.Port == 0 ? 22 : sftpSettings.Port, sftpSettings.UserName, sftpSettings.Password);
        try
        {
            client.Connect();
            client.DeleteFile(remoteFilePath);
            return true;
        }
        catch (Exception exception)
        {
            // Todo : log
            return false;
        }
        finally
        {
            client.Disconnect();
        }
    }

    public Stream DownloadFile(SftpSettings sftpSettings, string remoteFilePath, string localFilePath)
    {
        using var client = new SftpClient(sftpSettings.Host, sftpSettings.Port == 0 ? 22 : sftpSettings.Port, sftpSettings.UserName, sftpSettings.Password);
        try
        {
            client.Connect();
            using Stream fs = File.OpenWrite(localFilePath);
            client.DownloadFile(remoteFilePath, fs);
            return fs;
        }
        catch (Exception exception)
        {
            // Todo : log
            return null;
        }
        finally
        {
            client.Disconnect();
        }
    }

    public IEnumerable<SftpFile> ListAllFiles(SftpSettings sftpSettings, string remoteDirectory)
    {
        using var client = new SftpClient(sftpSettings.Host, sftpSettings.Port == 0 ? 22 : sftpSettings.Port, sftpSettings.UserName, sftpSettings.Password);
        try
        {
            client.Connect();
            if (client.IsConnected)
            {
                return client.ListDirectory(remoteDirectory);
            }
            return null;
        }
        catch (Exception exception)
        {
            // Todo : log
            return null;
        }
        finally
        {
            client.Disconnect();
        }
    }

    public bool UploadFile(SftpSettings sftpSettings, Stream fs, string remoteFilePath)
    {
        using var client = new SftpClient(sftpSettings.Host, sftpSettings.Port == 0 ? 22 : sftpSettings.Port, sftpSettings.UserName, sftpSettings.Password);
        try
        {
            client.Connect();
            if (client.IsConnected)
            {
                client.BufferSize = 4 * 1024;
                client.UploadFile(fs, remoteFilePath);

                return true;
            }

            return false;
        }
        catch (Exception exception)
        {
            // Todo : log
            return false;
        }
        finally
        {
            client.Disconnect();
        }
    }
}