using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    class FolderViewLeft
    {
        protected string curentFolder;
        protected int selectedIndex = 0;
        List<IDirectoryItem> directoryItems = new List<IDirectoryItem>();
        protected ConsoleGraphics _graphics;


        public FolderViewLeft(ConsoleGraphics graphics, string path = @"C:\")
        {
            curentFolder = path;
            _graphics = graphics;
            InitCurentDir();
        }

        protected IEnumerable<DirectoryInfo> GetDiractory() => Directory.GetDirectories(curentFolder)
                                                                       .Select(s => new DirectoryInfo(s));

        protected IEnumerable<FileInfo> GetFile() => Directory.GetFiles(curentFolder)
                                                                  .Select(a => new FileInfo(a));

        public virtual void Show()
        {
            DrawDatails();

            for (int i = 0; i < directoryItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    directoryItems[i].TextSelection(_graphics, selectedIndex);
                    directoryItems[i].Show(_graphics, selectedIndex);
                }
                else
                {
                    directoryItems[i].Show(_graphics, i);
                }

            }
            _graphics.FlipPages();
        }

        private void DrawDatails()
        {
            _graphics.FillRectangle(0xff000000, 0, 0, _graphics.ClientWidth, _graphics.ClientHeight);

            _graphics.DrawString("C:\\", "Arial", 0xffffffff, 0, 0, 12);
            _graphics.DrawString("D:\\", "Arial", 0xffffffff, _graphics.ClientWidth / 2, 0, 12);

            _graphics.DrawLine(0xffffffff, _graphics.ClientHeight, 0, _graphics.ClientHeight, _graphics.ClientWidth);
            _graphics.DrawLine(0xffffffff, 0, _graphics.ClientHeight - 20, _graphics.ClientWidth, _graphics.ClientWidth - 20);
            _graphics.DrawLine(0xffffffff, 0, 20, _graphics.ClientWidth, 20);


        }

        protected void InitCurentDir()
        {
            directoryItems.Clear();
            directoryItems.AddRange(GetDiractory()
                          .Select(a => new FolderItem() { Name = a.Name }));

            directoryItems.AddRange(GetFile()
                          .Select(a => new FileItem() { Name = a.Name, Size = a.Length, Extansion = a.Extension }));

            selectedIndex = 0;
        }

        public void InFolder(string folder = "")
        {
            curentFolder = Path.Combine(curentFolder, !string.IsNullOrEmpty(folder) ? folder : directoryItems[selectedIndex].Name);
            curentFolder = new DirectoryInfo(curentFolder).FullName;
            InitCurentDir();
        }

        public void MuveDown()
        {
            selectedIndex++;
            if (selectedIndex == directoryItems.Count)
                selectedIndex = directoryItems.Count-1;
        }

        public void MuveUp()
        {
            selectedIndex--;

            if (selectedIndex < 0)
                selectedIndex = 0;
        }
    }
}
