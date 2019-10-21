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
        private readonly int _lineHeight = 20;
        ConsoleGraphics _graphics;

        public Message(ConsoleGraphics graphics)
        {
            _graphics = graphics;
        }

        public void ShowMessage(string str, int indent)
        {
            while (!Input.IsKeyDown(Keys.ESCAPE))
            {
                _graphics.FillRectangle(0xff000000, indent, _lineHeight, _graphics.ClientWidth / 2, _graphics.ClientHeight - _lineHeight * 2);
                _graphics.DrawString($"{str}", "Arial", 0xffffffff, indent, _lineHeight, 12);
                _graphics.DrawString("Press Escape to Exit...", "Arial", 0xffffffff, indent, _graphics.ClientHeight / 2, 10);

                _graphics.FlipPages();
                Thread.Sleep(100);
            }
        }

        public void MessageHalper(int indent)
        {
            _graphics.FillRectangle(0xff000000, indent, _lineHeight, _graphics.ClientWidth / 2, _graphics.ClientHeight - _lineHeight * 2);
            _graphics.FlipPages();
        }

        public void MessageCopy(int indent)
        {
            _graphics.FillRectangle(0xff000000, indent, _lineHeight, _graphics.ClientWidth / 2, _graphics.ClientHeight - _lineHeight * 2);
            _graphics.DrawString("Copying in progress! Please wait....", "Arial", 0xffffffff, indent, _lineHeight, 10);
            _graphics.FlipPages();
        }
    }
}
