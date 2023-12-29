using Google.Apis.Drive.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class GoogleDriveServiceTests
    {
        GoogleDriveService _service;
        PrivateObject _privateService;
        Mock<IMessageBox> _messageBox;
        const string TEST_FILE_NAME = "ILOVEUNITTEST.txt";
        const string CONNECTION_FAILED_MESSAGE = "Fail to connect Google Drive";
        // create local test file
        public void CreateLocalTestFile()
        {
            string data = "I love unit testing as much as I love the world.";
            System.IO.File.WriteAllText(TEST_FILE_NAME, data);
        }

        // initialize
        [TestInitialize]
        public void Initialize()
        {
            _messageBox = new Mock<IMessageBox>();
            _messageBox.Setup(messageBox => messageBox.Show(It.IsAny<string>()));
            _service = new GoogleDriveService(Constant.PROJECT_NAME, Constant.SECRET_FILE_NAME, _messageBox.Object);
            _privateService = new PrivateObject(_service);
        }

        // set drive service test
        [TestMethod]
        public void SetDriveServiceTest()
        {
            var mockDriveService = new Mock<DriveService>();
            _service.SetDriveService(mockDriveService.Object);
            Assert.IsNotNull(_privateService.GetField("_service"));
        }

        // test constructor
        [TestMethod()]
        public void GoogleDriveServiceTest()
        {
            Assert.IsNotNull(_service);
        }

        // test search file
        [TestMethod()]
        public void SearchFileTest()
        {
            CreateLocalTestFile();
            string fileId = _service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result;
            var result = _service.SearchFile(TEST_FILE_NAME).Result;
            Assert.AreEqual(fileId, result[0].Id);
            Assert.IsTrue(_service.DeleteFile(fileId).Result);
        }

        // test search file exception
        [TestMethod()]
        public void SearchFileExceptionTest()
        {
            var mockDriveService = new Mock<DriveService>();
            _service.SetDriveService(mockDriveService.Object);
            mockDriveService.Setup(service => service.Files).Throws(new System.Net.Http.HttpRequestException());
            Assert.IsNull(_service.SearchFile(TEST_FILE_NAME).Result);
            _messageBox.Verify(messageBox => messageBox.Show(CONNECTION_FAILED_MESSAGE), Times.Once);
        }

        // load file test
        [TestMethod()]
        public void LoadTest()
        {
            CreateLocalTestFile();
            string fileId = _service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result;
            Assert.IsTrue(_service.Load(fileId, TEST_FILE_NAME));
            Assert.IsTrue(_service.DeleteFile(fileId).Result);
        }

        // load file exception test
        [TestMethod()]
        public void LoadExceptionTest()
        {
            var mockDriveService = new Mock<DriveService>();
            _service.SetDriveService(mockDriveService.Object);
            mockDriveService.Setup(service => service.Files).Throws(new System.Net.Http.HttpRequestException());
            Assert.IsFalse(_service.Load("1", TEST_FILE_NAME));
            _messageBox.Verify(messageBox => messageBox.Show(CONNECTION_FAILED_MESSAGE), Times.Once);
        }

        // save test
        [TestMethod()]
        public void SaveTest()
        {
            CreateLocalTestFile();
            string fileId = _service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result;
            Assert.AreNotEqual(string.Empty, fileId);
            Assert.IsTrue(_service.DeleteFile(fileId).Result);
        }

        // save exception test
        [TestMethod()]
        public void SaveExceptionTest()
        {
            var mockDriveService = new Mock<DriveService>();
            _service.SetDriveService(mockDriveService.Object);
            mockDriveService.Setup(service => service.Files).Throws(new System.Net.Http.HttpRequestException());
            Assert.IsNull(_service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result);
            _messageBox.Verify(messageBox => messageBox.Show(CONNECTION_FAILED_MESSAGE), Times.Once);
        }

        // update file test
        [TestMethod()]
        public void UpdateFileTest()
        {
            CreateLocalTestFile();
            string fileId = _service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result;
            Assert.IsTrue(_service.UpdateFile(TEST_FILE_NAME, TEST_FILE_NAME, fileId).Result);
            Assert.IsTrue(_service.DeleteFile(fileId).Result);
        }

        // update file exception test
        [TestMethod()]
        public void UpdateFileExceptionTest()
        {
            var mockDriveService = new Mock<DriveService>();
            _service.SetDriveService(mockDriveService.Object);
            mockDriveService.Setup(service => service.Files).Throws(new System.Net.Http.HttpRequestException());
            Assert.IsFalse(_service.UpdateFile(TEST_FILE_NAME, TEST_FILE_NAME, "1").Result);
            _messageBox.Verify(messageBox => messageBox.Show(CONNECTION_FAILED_MESSAGE), Times.Once);
        }

        // delete file test
        [TestMethod()]
        public void DeleteFileTest()
        {
            CreateLocalTestFile();
            string fileId = _service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result;
            Assert.IsTrue(_service.DeleteFile(fileId).Result);
        }

        // delete file exception test
        [TestMethod()]
        public void DeleteFileExceptionTest()
        {
            var mockDriveService = new Mock<DriveService>();
            _service.SetDriveService(mockDriveService.Object);
            mockDriveService.Setup(service => service.Files).Throws(new System.Net.Http.HttpRequestException());
            Assert.IsFalse(_service.DeleteFile("1").Result);
            _messageBox.Verify(messageBox => messageBox.Show(CONNECTION_FAILED_MESSAGE), Times.Once);
        }
    }
}