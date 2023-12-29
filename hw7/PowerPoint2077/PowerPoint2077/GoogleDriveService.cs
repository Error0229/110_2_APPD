using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WindowPowerPoint
{
    public class GoogleDriveService
    {
        private static readonly string[] SCOPES = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        private DriveService _service;
        private IMessageBox _messageBox;
        const string CONNECTION_FAILED_MESSAGE = "Fail to connect Google Drive";
        public GoogleDriveService(string applicationName, string clientSecretFileName, IMessageBox messageBox)
        {
            _messageBox = messageBox;
            CreateNewService(applicationName, clientSecretFileName);
        }

        // dependcy injection
        public void SetDriveService(DriveService service)
        {
            _service = service;
        }


        // create new service
        protected virtual void CreateNewService(string applicationName, string clientSecretFileName)
        {
            const string USER = "user";
            const string CREDENTIAL_FOLDER = ".credential/";
            UserCredential credential;

            using (FileStream stream = new FileStream(clientSecretFileName, FileMode.Open, FileAccess.Read))
            {
                string credentialPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credentialPath = Path.Combine(credentialPath, CREDENTIAL_FOLDER + applicationName);
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets, SCOPES, USER, CancellationToken.None, new FileDataStore(credentialPath, true)).Result;
            }

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
            _service = service;
        }

        // search file
        public virtual async Task<IList<Google.Apis.Drive.v3.Data.File>> SearchFile(string fileName)
        {
            string query = $"name = '{fileName}' and 'root' in parents and trashed = false";

            Google.Apis.Drive.v3.Data.FileList response;
            try
            {
                var request = _service.Files.List();
                request.Q = query;
                request.Fields = "files(id, name)";
                response = await request.ExecuteAsync();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                _messageBox.Show(CONNECTION_FAILED_MESSAGE);
                return null;
            }
            return response.Files;
        }

        // load file
        public virtual bool Load(string fileId, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                try
                {
                    _service.Files.Get(fileId).Download(stream);
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    _messageBox.Show(CONNECTION_FAILED_MESSAGE);
                    return false;
                }
            }
            return true;
        }

        // save file
        public virtual async Task<string> Save(string fileName, string remoteName)
        {
            var parentFolderId = "root";
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = remoteName,
                Parents = new List<string> { parentFolderId }
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                try
                {
                    request = _service.Files.Create(fileMetadata, stream, "text/plain");
                    await request.UploadAsync();
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    _messageBox.Show(CONNECTION_FAILED_MESSAGE);
                    return null;
                }
            }
            return request.ResponseBody.Id;
        }

        // update file
        public virtual async Task<bool> UpdateFile(string fileName, string remoteName, string fileId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = remoteName
            };
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                try
                {
                    await _service.Files.Update(fileMetadata, fileId, stream, "text/plain").UploadAsync();
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    _messageBox.Show(CONNECTION_FAILED_MESSAGE);
                    return false;
                }
            }
            return true;
        }

        // delete file
        public virtual async Task<bool> DeleteFile(string fileId)
        {
            try
            {
                await _service.Files.Delete(fileId).ExecuteAsync();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                _messageBox.Show(CONNECTION_FAILED_MESSAGE);
                return false;
            }
            return true;
        }
    }
}
