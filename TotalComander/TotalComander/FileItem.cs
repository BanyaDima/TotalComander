using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    class FileItem : IDirectoryItem
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string Extansion { get; set; }

        public void Show(ConsoleGraphics graphics, int index, int indent, int lineHeight)
        {
            graphics.DrawString($"{Name}\t\t<{Extansion}>\t{Size}", "Arial", 0xffffffff, indent, lineHeight + lineHeight * index, 10);
        }

        public void TextSelection(ConsoleGraphics graphics, int index, int indent, int lineHeight)
        {
            graphics.FillRectangle(0xff615f5d, indent, lineHeight + lineHeight * index, graphics.ClientWidth / 2, lineHeight);
        }
    }
}
