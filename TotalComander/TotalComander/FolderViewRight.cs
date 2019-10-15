using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    class FolderViewRight : FolderViewLeft
    {
        List<IDirectoryItem> directoryItems = new List<IDirectoryItem>();

        public FolderViewRight(ConsoleGraphics graphics, string path = @"D:\") : base(graphics, path)
        {
            curentFolder = path;
        
        }


    }
}
