using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    class FolderItem : IDirectoryItem
    {
        public string Name { get; set; }
        public long Size { get; set; }
        private int lineHeight = 20;


        public void Show(ConsoleGraphics graphics, int index)
        {              
            graphics.DrawString($"{Name}\t\t<dir>", "Arial", 0xffffffff, 0, lineHeight + lineHeight  * index, 10);
        }

        public void TextSelection(ConsoleGraphics graphics, int index)
        {
            graphics.FillRectangle(0xff615f5d, 0, lineHeight + lineHeight * index, graphics.ClientWidth / 2, lineHeight);
        }       
    }
}
