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
        public void Start()
        {
            ConsoleGraphics graphics = new ConsoleGraphics();            

            FolderView viewLeftWindow = new FolderView(graphics);
            FolderView viewRightWindow = new FolderView(graphics, "D:\\");

            Window leftWindow = new Window(graphics, viewLeftWindow, 0);
            Window rightWindow = new Window(graphics, viewRightWindow, graphics.ClientWidth / 2 + 2);
           

            IWindow activWindow = leftWindow;
            

            while (true)
            {            
                rightWindow.ShowContents();
                leftWindow.ShowContents();
                                
                var key = Console.ReadKey();

                

                switch (key.Key)
                {
                    case ConsoleKey.Tab:
                        activWindow = leftWindow == activWindow ? (IWindow)rightWindow : (IWindow)leftWindow;                        
                        break;
                    case ConsoleKey.Enter:
                        activWindow.InFolder();
                        break;
                    case ConsoleKey.Backspace:
                        activWindow.InFolder("..");
                        break;
                    case ConsoleKey.UpArrow:
                        activWindow.MuveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        activWindow.MuveDown();
                        break;
                    case ConsoleKey.F1:
                       viewRightWindow.Copy();
                        break;
                    case ConsoleKey.F2:
                        viewRightWindow.Cut();
                        break;
                    case ConsoleKey.F3:
                        viewRightWindow.Paste();
                        break;
                    case ConsoleKey.F4:
                        viewRightWindow.ListOfDisks();
                        break;
                    case ConsoleKey.F5:
                        break;
                    case ConsoleKey.F6:
                        break;
                    case ConsoleKey.F7:
                        viewRightWindow.CriateFolder();
                        break;
                    case ConsoleKey.F8:
                        break;
                    case ConsoleKey.F9:
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }
        }
    }
}
