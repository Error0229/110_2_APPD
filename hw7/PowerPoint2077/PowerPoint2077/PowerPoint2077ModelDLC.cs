using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowPowerPoint
{
    public partial class PowerPointModel
    {
        private CursorManager _cursorManager;
        public virtual Size CanvasSize
        {
            get; set;
        }
        public int SlideIndex
        {
            get; set;
        }
        public virtual CursorManager ModelCursorManager
        {
            get
            {
                return _cursorManager;
            }
            set
            {
                _cursorManager = value;
            }
        }
        private CommandManager _commandManager;
        public virtual CommandManager ModelCommandManager
        {
            set
            {
                _commandManager = value;
            }
        }
        private GoogleDriveService _drive;
        private string _fileID;

        // save pages to drive
        public async Task<bool> HandleSave()
        {
            var fileName = Guid.NewGuid().ToString() + Constant.TEXT_FILE;
            System.IO.File.WriteAllText(fileName, _slides.Convert());
            if (string.IsNullOrEmpty(_fileID))
            {
                _fileID = await _drive.Save(fileName, Constant.SAVE_FILE_NAME);
                if (string.IsNullOrEmpty(_fileID))
                    return false;
            }
            else if (!await _drive.UpdateFile(fileName, Constant.SAVE_FILE_NAME, _fileID))
            {
                return false;
            }
            System.IO.File.Delete(fileName);
            await Task.Delay(Constant.WHY_I_NEED_TO_ADD_THIS_IN_MY_CODE_JUST_FOR_TEST);
            return true;
        }

        // load pages from drive
        public void HandleLoad()
        {
            if (_fileID == string.Empty || !_drive.Load(_fileID, Constant.LOAD_FILE_NAME))
            {
                MessageBox.Show(Constant.ERROR_NO_LOAD_FILE);
                return;
            }
            ClearSlide();
            _commandManager.Clear();
            string data = System.IO.File.ReadAllText(Constant.LOAD_FILE_NAME);
            var rawData = InterpretPages(data);
            for (int i = 0; i < rawData.Count - 1; i++)
            {
                var page = new Page();
                page.Interpret(rawData[i], CanvasSize);
                AddPage(_slides.Count, page);
                NotifyPageChanged(_slides.Count - 1, Page.Action.Add);
            }
            System.IO.File.Delete(Constant.LOAD_FILE_NAME);
        }

        // clear slides
        public void ClearSlide()
        {
            while (SlideIndex >= 0)
            {
                HandleDeletePage(SlideIndex);
            }
        }

        // Interpret pages
        public List<string> InterpretPages(string data)
        {
            return new List<string>(data.Split(Constant.NEW_LINE));
        }

        // check save file exist
        public async void CheckSavesExist()
        {
            var results = await _drive.SearchFile(Constant.SAVE_FILE_NAME);
            if (results != null && results.Count != 0)
            {
                _fileID = results[0].Id; // get first match file
            }
            else
            {
                _fileID = string.Empty;
            }
        }

        // generate random number securely
        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[Constant.INTEGER32_BYTES];
                rng.GetBytes(randomNumber);
                int generatedValue = BitConverter.ToInt32(randomNumber, 0);
                return Math.Abs(generatedValue % (maxValue - minValue + 1)) + minValue;
            }
        }

    }

}
