﻿using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 40;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            ConsoleGraphics graphics = new ConsoleGraphics();

            FolderViewLeft view = new FolderViewLeft(graphics);
            FolderViewRight viewRight = new FolderViewRight(graphics);

            Console.CursorVisible = false;

            while (true)
            {
                view.Show();
                //viewRight.Show();
                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.Tab:
                        break;
                    case ConsoleKey.Enter:
                        view.InFolder();
                        break;
                    case ConsoleKey.Backspace:
                        view.InFolder("..");
                        break;
                    case ConsoleKey.UpArrow:
                        view.MuveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        view.MuveDown();
                        break;
                    case ConsoleKey.F1:
                        break;
                    case ConsoleKey.F2:
                        break;
                    case ConsoleKey.F3:
                        break;
                    case ConsoleKey.F4:
                        break;
                    case ConsoleKey.F5:
                        break;
                    case ConsoleKey.F6:
                        break;
                    case ConsoleKey.F7:
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