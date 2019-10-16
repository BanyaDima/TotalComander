using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    public class RightWindow : IWindow
    {
        public int Indent { get; private set; }
        ConsoleGraphics _graphics;
        FolderView _view;

        public RightWindow(ConsoleGraphics graphics, FolderView view)
        {
            _graphics = graphics;
            _view = view;
            Indent = _graphics.ClientWidth / 2;
        }

        public void ShowContents()
        {
            DrawDatails();

            _view.Show(Indent);
        }
        public void DrawDatails()
        {
            _graphics.FillRectangle(0xff000000, (_graphics.ClientWidth / 2) + 1, 0, (_graphics.ClientWidth / 2) + 1, _graphics.ClientHeight);

            _graphics.DrawString("D:\\", "Arial", 0xffffffff, _graphics.ClientWidth / 2, 0, 12);

            _graphics.DrawLine(0xffffffff, _graphics.ClientWidth / 2, _graphics.ClientHeight - _view.LineHeight, _graphics.ClientWidth, _graphics.ClientHeight - _view.LineHeight);
            _graphics.DrawLine(0xffffffff, _graphics.ClientWidth / 2, _view.LineHeight, _graphics.ClientWidth / 2, _view.LineHeight);
        }

    }
}
