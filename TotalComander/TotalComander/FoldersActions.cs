﻿using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalComander
{
    public class FolderActions
    {
        public int LineHeight { get; private set; }
        public string DiscName { get; private set; }
        private string _curentFolder;
        private string _sourcePath;
        private int _selectedIndex = 0;
        private char _lastKeyPress;
        private string _nameFileOrDirectory = "";
        List<IDirectoryItem> directoryItems = new List<IDirectoryItem>();
        List<DiskItem> driveItems = new List<DiskItem>();
        ConsoleGraphics _graphics;
        Message _message;

        public FolderActions(ConsoleGraphics graphics, string path = "C:\\")
        {
            LineHeight = 20;
            _curentFolder = path;
            _graphics = graphics;
            _message = new Message(graphics);
            DiscName = _curentFolder;
            InitCurentDir();
        }

        private IEnumerable<DirectoryInfo> GetDiractory() => Directory.GetDirectories(_curentFolder)
                                                                       .Select(s => new DirectoryInfo(s));
        private IEnumerable<FileInfo> GetFile() => Directory.GetFiles(_curentFolder)
                                                                  .Select(a => new FileInfo(a));

        public virtual void Show(int indent)
        {
            for (int i = 0; i < directoryItems.Count; i++)
            {
                if (_selectedIndex == i)
                {
                    directoryItems[i].TextSelection(_graphics, _selectedIndex, indent, LineHeight);
                    directoryItems[i].Show(_graphics, _selectedIndex, indent, LineHeight);
                }
                else
                {
                    directoryItems[i].Show(_graphics, i, indent, LineHeight);
                }
            }
            _graphics.FlipPages();
            Thread.Sleep(100);
        }

        private void InitCurentDir()
        {
            try
            {
                directoryItems.Clear();
                directoryItems.AddRange(GetDiractory()
                              .Select(a => new FolderItem() { Name = a.Name, SoursePath = a.FullName }));

                directoryItems.AddRange(GetFile()
                              .Select(a => new FileItem() { Name = a.Name, Size = a.Length, Extansion = a.Extension, SoursePath = a.FullName }));

                _selectedIndex = 0;
            }
            catch (Exception ex)
            {
                _message.ShowMessage($"{ex.Message}", 0);
                InFolder("..");
            }
        }

        public void InFolder(string folder = "")
        {
            _curentFolder = Path.Combine(_curentFolder, !string.IsNullOrEmpty(folder) ? folder : directoryItems[_selectedIndex].Name);
            _curentFolder = new DirectoryInfo(_curentFolder).FullName;
            InitCurentDir();
        }

        public void MuveDown()
        {
            _selectedIndex++;
            if (_selectedIndex == directoryItems.Count)
                _selectedIndex = directoryItems.Count - 1;
        }

        public void MuveUp()
        {
            _selectedIndex--;
            if (_selectedIndex < 0)
                _selectedIndex = 0;
        }

        public void ListOfDisks(int indent)
        {

            DriveInfo[] drives = DriveInfo.GetDrives();
            driveItems.Clear();
            driveItems.AddRange(drives.Select(d => new DiskItem() { Name = d.Name, IsReady = d.IsReady }));
            _selectedIndex = 0;

            while (!Input.IsKeyDown(Keys.RETURN))
            {
                ShowListDisks(indent);

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        MuveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        MuveDown();
                        break;
                    default:
                        break;
                }
            }
            if (!driveItems[_selectedIndex].IsReady)
            {
                _message.ShowMessage("Disk is not ready for use...Select another drive!", indent);
                ListOfDisks(indent);
            }
            InitCurentDir();
        }

        private void ShowListDisks(int indent)
        {
            _message.MessageHalper(indent);

            for (int i = 0; i < driveItems.Count; i++)
            {
                if (_selectedIndex == i)
                {
                    driveItems[i].TextSelection(_graphics, _selectedIndex, indent, LineHeight);
                    driveItems[i].Show(_graphics, _selectedIndex, indent, LineHeight);
                    _curentFolder = driveItems[i].Name;
                    DiscName = driveItems[i].Name;
                }
                else
                {
                    driveItems[i].Show(_graphics, i, indent, LineHeight);
                }
            }
            _graphics.FlipPages();
        }

        public void Rename(int indent)
        {
            _sourcePath = directoryItems[_selectedIndex].SoursePath;

            _message.MessageHalper(indent);

            Console.CursorVisible = true;

            Console.Write("\n\nEnter new Name:");
            string newName = Console.ReadLine();
            string targetPaht = Path.Combine(_curentFolder, newName);

            if (File.Exists(targetPaht))
            {
                _message.ShowMessage("Such name already exists! Try again...", indent);
            }
            else
            {
                if (File.Exists(_sourcePath))
                {
                    string extention = Path.GetExtension(_sourcePath);
                    newName = $"{newName}{extention}";
                    targetPaht = Path.Combine(_curentFolder, newName);

                    File.Move(_sourcePath, targetPaht);
                }
                else
                {
                    Directory.Move(_sourcePath, targetPaht);
                }
            }
            Console.CursorVisible = false;
            InitCurentDir();
        }

        public void Copy()
        {
            _lastKeyPress = (char)Keys.F1;
            _sourcePath = directoryItems[_selectedIndex].SoursePath;
            _nameFileOrDirectory = directoryItems[_selectedIndex].Name;
        }

        public void Cut()
        {
            _lastKeyPress = (char)Keys.F2;
            _sourcePath = directoryItems[_selectedIndex].SoursePath;
            _nameFileOrDirectory = directoryItems[_selectedIndex].Name;
        }

        public void Paste(int indent)
        {
            string targetPaht = Path.Combine(_curentFolder, _nameFileOrDirectory);
            try
            {
                if (File.Exists(_sourcePath) && _sourcePath != null && _nameFileOrDirectory != null)
                {
                    if (_lastKeyPress == (char)Keys.F1)
                    {
                        _message.MessageCopy(indent);
                        File.Copy(_sourcePath, targetPaht);
                        _message.ShowMessage("Copying completed!", indent);
                    }
                    else
                    {
                        _message.MessageCopy(indent);
                        File.Move(_sourcePath, targetPaht);
                        _message.ShowMessage("Copying completed!", indent);
                    }
                }
                else if (_sourcePath != null && _nameFileOrDirectory != null)
                {
                    if (_lastKeyPress == (char)Keys.F1)
                    {
                        DirectoryInfo diSource = new DirectoryInfo(_sourcePath);
                        DirectoryInfo diTarget = new DirectoryInfo(targetPaht);

                        _message.MessageCopy(indent);
                        CopyDir(diSource, diTarget, indent);
                        _message.ShowMessage("Copying completed!", indent);
                    }
                    else
                    {
                        _message.MessageCopy(indent);
                        Directory.Move(_sourcePath, targetPaht);
                        _message.ShowMessage("Copying completed!", indent);
                    }
                }
                InitCurentDir();
            }
            catch (Exception ex)
            {
                _message.ShowMessage($"{ex.Message}", indent);
            }
        }

        private void CopyDir(DirectoryInfo source, DirectoryInfo target, int indent)
        {
            try
            {
                Directory.CreateDirectory(target.FullName);

                foreach (FileInfo file in source.GetFiles())
                {
                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                }

                foreach (DirectoryInfo subDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(subDir.Name);
                    CopyDir(subDir, nextTargetSubDir, indent);
                }
            }
            catch (Exception ex)
            {
                _message.ShowMessage($"{ex.Message}", indent);
            }
        }

        public void CriateFolder()
        {
            int _folderCounter = 0;
            string folderName = "New Folder";
            string targetPath = Path.Combine(_curentFolder, folderName);

            if (Directory.Exists(targetPath))
            {
                do
                {
                    _folderCounter++;
                    folderName = $"New Folder({_folderCounter})";
                    targetPath = Path.Combine(_curentFolder, folderName);
                }
                while (Directory.Exists(targetPath));
            }
            _folderCounter = 0;

            Directory.CreateDirectory(targetPath);
            InitCurentDir();
        }

        public void Properties(int indent)
        {
            _sourcePath = directoryItems[_selectedIndex].SoursePath;
            try
            {
                if (File.Exists(_sourcePath))
                {
                    _message.ShowMessage($"Name: {Path.GetFileName(_sourcePath)}\nRoot directori: {Path.GetPathRoot(_sourcePath)}\n" +
                                         $"Last access time: {File.GetLastAccessTime(_sourcePath)}\nLast write time: { File.GetLastWriteTime(_sourcePath)}\n" +
                                         $"Size: {directoryItems[_selectedIndex].Size} byte", indent);
                }
                else
                {
                    _message.ShowMessage($"Name: {directoryItems[_selectedIndex].Name}\nRoot directori: {Path.GetPathRoot(_sourcePath)}\n" +
                                         $"Last read time: {Directory.GetLastAccessTime(_sourcePath)}\nLast write time: {Directory.GetLastWriteTime(_sourcePath)}\n" +
                                         $"Size: {Directory.GetFiles(_sourcePath, "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length))} byte\n" +
                                         $"Files: {Directory.GetFiles(_sourcePath, "*", SearchOption.AllDirectories).Count()}\n" +
                                         $"Directoris: {Directory.GetDirectories(_sourcePath, "*", SearchOption.AllDirectories).Count()}", indent);
                }
            }
            catch (Exception ex)
            {
                _message.ShowMessage($"{ex.Message}", indent);
            }
        }
    }
}
