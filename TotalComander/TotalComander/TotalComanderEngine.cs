using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    public class TotalComanderEngine
    {
        ConsoleGraphics _graphics;
        FolderActions _viewLeftWindow;
        FolderActions _viewRightWindow;
        Window _leftWindow;
        Window _rightWindow;
        IWindowActions _activWindow;

        public TotalComanderEngine()
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 40;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;

            _graphics = new ConsoleGraphics();
            _viewLeftWindow = new FolderActions(_graphics);
            _viewRightWindow = new FolderActions(_graphics, "D:\\");
            _leftWindow = new Window(_graphics, _viewLeftWindow, 0);
            _rightWindow = new Window(_graphics, _viewRightWindow, _graphics.ClientWidth / 2 + 2);
        }

        public void Start()
        {
            _activWindow = _leftWindow;

            while (true)
            {
                _leftWindow.ShowContents();
                _rightWindow.ShowContents();

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.Tab:
                        _activWindow = _leftWindow == _activWindow ? (IWindowActions)_rightWindow : (IWindowActions)_leftWindow;
                        break;
                    case ConsoleKey.Enter:
                        _activWindow.InFolder();
                        break;
                    case ConsoleKey.Backspace:
                        _activWindow.InFolder("..");
                        break;
                    case ConsoleKey.UpArrow:
                        _activWindow.MuveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        _activWindow.MuveDown();
                        break;
                    case ConsoleKey.F1:
                        _activWindow.Copy();
                        break;
                    case ConsoleKey.F2:
                        _activWindow.Cut();
                        break;
                    case ConsoleKey.F3:                      
                        _activWindow.Paste();
                        break;
                    case ConsoleKey.F4:
                       // _activWindow.ListOfDisks();
                        break;
                    case ConsoleKey.F5:
                        _activWindow.Properties();
                        break;
                    case ConsoleKey.F6:
                        _activWindow.Rename();
                        break;
                    case ConsoleKey.F7:
                        _activWindow.CriateFolder();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
