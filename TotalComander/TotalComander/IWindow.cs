using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    interface IWindow
    {
        void ShowContents();

        void DrawDatails();

        void InFolder(string folder = "");

        void MuveDown();

        void MuveUp();

    }
}
