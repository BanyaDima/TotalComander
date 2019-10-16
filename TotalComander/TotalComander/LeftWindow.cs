using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    public class LeftWindow : IWindow
    {
        public int Indent { get; private set; }
        FolderView _view;
        ConsoleGraphics _graphic;

        public LeftWindow(ConsoleGraphics graphics, FolderView view)
        {
            _view = view;
            _graphic = graphics;
            Indent = 0;
        }

        public void ShowContents()
        {
            DrawDatails();

            _view.Show(Indent);
        }

        public void DrawDatails()
        {
            _graphic.FillRectangle(0xff000000, 0, 0, _graphic.ClientWidth / 2, _graphic.ClientHeight);

            _graphic.DrawString("C:\\", "Arial", 0xffffffff, 0, 0, 12);

            _graphic.DrawLine(0xffffffff, _graphic.ClientHeight, 0, _graphic.ClientHeight, _graphic.ClientHeight - _view.LineHeight);
            _graphic.DrawLine(0xffffffff, 0, _graphic.ClientHeight - _view.LineHeight, _graphic.ClientWidth, _graphic.ClientHeight - _view.LineHeight);
            _graphic.DrawLine(0xffffffff, 0, _view.LineHeight, _graphic.ClientWidth / 2, _view.LineHeight);


        }
    }
}
