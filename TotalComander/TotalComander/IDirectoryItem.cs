using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    interface IDirectoryItem
    {
        string Name { get; set; }
        long Size { get; set; }

        void Show(ConsoleGraphics graphics, int index);

        void TextSelection(ConsoleGraphics graphics, int index);
    }
}
