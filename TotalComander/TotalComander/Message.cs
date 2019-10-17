using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalComander
{
    public class Message
    {
        ConsoleGraphics _graphics;

        public Message(ConsoleGraphics graphics)
        {
            _graphics = graphics;
        }

        public void ShowMessage(string str)
        {
            while (!Input.IsKeyDown(Keys.ESCAPE))
            {
                _graphics.FillRectangle(0xff000000, 0, 0, _graphics.ClientWidth, _graphics.ClientHeight);
                _graphics.DrawString($"{str}", "Arial", 0xffffffff, 0, 0, 12);
                _graphics.DrawString("Press Escape to Exit...", "Arial", 0xffffffff, 0, _graphics.ClientHeight / 2, 10);

                _graphics.FlipPages();
                Thread.Sleep(100);
            }
        }


    }
}
