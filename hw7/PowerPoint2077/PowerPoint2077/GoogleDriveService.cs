using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public class GoogleDriveService
    {
        private static readonly string[] SCOPES = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        private DriveService _service;
        private int _timeStamp;
        private string _applicationName;
        private string _clientSecretFileName;

        public GoogleDriveService(string applicationName, string clientSecretFileName)
        {
            _applicationName = applicationName;
            _clientSecretFileName = clientSecretFileName;
            CreateNewService(applicationName, clientSecretFileName);
        }


        // create new service
        private void CreateNewService(string applicationName, string clientSecretFileName)
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

            _timeStamp = UNIXNowTimeStamp;
            _service = service;
        }

        // search file
        public async Task<IList<Google.Apis.Drive.v3.Data.File>> SearchFile(string fileName)
        {
            string query = $"name = '{fileName}' and 'root' in parents and trashed = false";

            var request = _service.Files.List();

            request.Q = query;
            request.Fields = "files(id, name)";
            Google.Apis.Drive.v3.Data.FileList response;
            try
            {
                response = await request.ExecuteAsync();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Fail to connect Google Drive");
                return null;
            }
            return response.Files;
        }

        private int UNIXNowTimeStamp
        {
            get
            {
                const int UNIX_START_YEAR = 1970;
                DateTime unixStartTime = new DateTime(UNIX_START_YEAR, 1, 1);
                return Convert.ToInt32((DateTime.Now.Subtract(unixStartTime).TotalSeconds));
            }
        }

        //Check and refresh the credential if credential is out-of-date
        private void CheckCredentialTimeStamp()
        {
            const int ONE_HOUR_SECOND = 3600;
            int nowTimeStamp = UNIXNowTimeStamp;

            if ((nowTimeStamp - _timeStamp) > ONE_HOUR_SECOND)
                CreateNewService(_applicationName, _clientSecretFileName);
        }

        // load file
        public bool Load(string fileId, string fileName)
        {
            CheckCredentialTimeStamp();
            var request = _service.Files.Get(fileId);
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                try
                {
                    request.Download(stream);
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    MessageBox.Show("Fail to connect Google Drive");
                    return false;
                }
            }
            return true;
        }

        // save file
        public async Task<string> Save(string fileName, string remoteName)
        {
            CheckCredentialTimeStamp();
            var parentFolderId = "root";
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = remoteName,
                Parents = new List<string> { parentFolderId }
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                request = _service.Files.Create(fileMetadata, stream, "text/plain");
                request.Fields = "id";
                try
                {
                    await request.UploadAsync();
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    MessageBox.Show("Fail to connect Google Drive");
                    return string.Empty;
                }
            }
            return request.ResponseBody.Id;
        }

        // update file
        public async Task<bool> UpdateFile(string fileName, string remoteName, string fileId)
        {
            CheckCredentialTimeStamp();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = remoteName
            };
            FilesResource.UpdateMediaUpload request;
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                request = _service.Files.Update(fileMetadata, fileId, stream, "text/plain");
                request.Fields = "id";
                try
                {
                    await request.UploadAsync();
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    MessageBox.Show("Fail to connect Google Drive");
                    return false;
                }
            }
            return true;
        }
    }
}
