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
        FolderActions _leftFolderActions;
        FolderActions _rightFolderActions;
        Window _leftWindow;
        Window _rightWindow;
        IWindowActions _activWindow;
        IWindowActions _inactiveWindow;

        public TotalComanderEngine()
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 40;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;

            _graphics = new ConsoleGraphics();
            _leftFolderActions = new FolderActions(_graphics);
            _rightFolderActions = new FolderActions(_graphics);
            _leftWindow = new Window(_graphics, _leftFolderActions, 0);
            _rightWindow = new Window(_graphics, _rightFolderActions, _graphics.ClientWidth / 2 + 2);
        }

        public void Start()
        {
            _activWindow = _leftWindow;
            _inactiveWindow = _rightWindow;

            while (true)
            {
                _inactiveWindow.HideWindow();
                _activWindow.ShowContents();

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.Tab:
                        ChangeActivWindow();
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
                        _activWindow.ListOfDisks();
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

        private void ChangeActivWindow()
        {
            if (_leftWindow == _activWindow)
            {
                _activWindow = (IWindowActions)_rightWindow;
                _inactiveWindow = (IWindowActions)_leftWindow;
            }
            else
            {
                _activWindow = (IWindowActions)_leftWindow;
                _inactiveWindow = (IWindowActions)_rightWindow;
            }
        }
    }
}
