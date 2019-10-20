using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    interface IWindowActions
    {
        void ShowContents();
        void InFolder(string folder = "");
        void MuveDown();
        void MuveUp();
        void Copy();
        void Cut();
        void Rename();
        void Paste();
        void ListOfDisks();
        void CriateFolder();
        void Properties();
    }
}
