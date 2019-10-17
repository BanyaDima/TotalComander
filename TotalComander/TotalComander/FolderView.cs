using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalComander
{
    public class FolderView
    {
        public int LineHeight { get; private set; }
        string curentFolder;
        int selectedIndex = 0;
        List<IDirectoryItem> directoryItems = new List<IDirectoryItem>();
        ConsoleGraphics _graphics;
        Message _message;

        public FolderView(ConsoleGraphics graphics, string path = @"C:\")
        {
            LineHeight = 20;
            curentFolder = path;
            _graphics = graphics;
            _message = new Message(graphics);
            InitCurentDir();
        }

        private IEnumerable<DirectoryInfo> GetDiractory() => Directory.GetDirectories(curentFolder)
                                                                       .Select(s => new DirectoryInfo(s));
        private IEnumerable<FileInfo> GetFile() => Directory.GetFiles(curentFolder)
                                                                  .Select(a => new FileInfo(a));
        public virtual void Show(int indent)
        {
            for (int i = 0; i < directoryItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    directoryItems[i].TextSelection(_graphics, selectedIndex, indent, LineHeight);
                    directoryItems[i].Show(_graphics, selectedIndex, indent, LineHeight);
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
                              .Select(a => new FolderItem() { Name = a.Name }));

                directoryItems.AddRange(GetFile()
                              .Select(a => new FileItem() { Name = a.Name, Size = a.Length, Extansion = a.Extension }));

                selectedIndex = 0;
            }
            catch (Exception ex)
            {
                _message.ShowMessage($"{ex.Message}");
                
                InFolder("..");
            }            
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
                selectedIndex = directoryItems.Count - 1;
        }

        public void MuveUp()
        {
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = 0;
        }
    }
}
