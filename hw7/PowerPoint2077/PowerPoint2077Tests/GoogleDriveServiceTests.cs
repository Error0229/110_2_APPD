using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class GoogleDriveServiceTests
    {
        GoogleDriveService _service;

        const string TEST_FILE_NAME = "ILOVEUNITTEST.txt";
        // create local test file
        public void CreateLocalTestFile()
        {
            string data = "test data";
            System.IO.File.WriteAllText(TEST_FILE_NAME, data);
        }

        // initialize
        [TestInitialize]
        public void Initialize()
        {
            _service = new GoogleDriveService(Constant.PROJECT_NAME, Constant.SECRET_FILE_NAME);
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
        }

        [TestMethod()]
        public void LoadTest()
        {
            CreateLocalTestFile();
            string fileId = _service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result;
            Assert.IsTrue(_service.Load(fileId, TEST_FILE_NAME));
        }

        // save test
        [TestMethod()]
        public void SaveTest()
        {
            CreateLocalTestFile();
            string fileId = _service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result;
            Assert.AreNotEqual(string.Empty, fileId);
        }

        [TestMethod()]
        public void UpdateFileTest()
        {
            CreateLocalTestFile();
            string fileId = _service.Save(TEST_FILE_NAME, TEST_FILE_NAME).Result;
            Assert.IsTrue(_service.UpdateFile(TEST_FILE_NAME, TEST_FILE_NAME, fileId).Result);
        }
    }
}