using NConsoleGraphics;
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

            Console.CursorVisible = false;

           
            TotalComanderEngine handler = new TotalComanderEngine();

            handler.Start();

        }
    }
}
