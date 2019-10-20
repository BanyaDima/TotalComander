﻿using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalComander
{
    public class Window : IWindowActions
    {
        public int Indent { get; private set; }
        ConsoleGraphics _graphic;
        FolderActions _view;

        public Window(ConsoleGraphics graphics, FolderActions view, int ident)
        {
            _graphic = graphics;
            _view = view;
            Indent = ident;
        }

        public void ShowContents()
        {
            DrawDatails();

            _view.Show(Indent);
        }

        private void DrawDatails()
        {
            _graphic.FillRectangle(0xff000000, Indent, 0, _graphic.ClientWidth / 2, _graphic.ClientHeight);
            _graphic.FillRectangle(0xff000000, Indent + 1, 0, (_graphic.ClientWidth / 2), _graphic.ClientHeight);

            _graphic.DrawString("C:\\", "Arial", 0xffffffff, 0, 0, 12);
            _graphic.DrawString("D:\\", "Arial", 0xffffffff, _graphic.ClientWidth / 2, 0, 12);
            _graphic.DrawString("F1-copy  :  F2-cut  :  F3-paste  :  F4-list of disks  :  F5-properties  :  F6-rename  :  F7-criate folder",
                                                            "Arial", 0xffffffff, 0, _graphic.ClientHeight - _view.LineHeight, 10);

            _graphic.DrawLine(0xffffffff, _graphic.ClientWidth / 2, 0, _graphic.ClientWidth / 2, _graphic.ClientHeight - _view.LineHeight);
            _graphic.DrawLine(0xffffffff, 0, _graphic.ClientHeight - _view.LineHeight, _graphic.ClientWidth, _graphic.ClientHeight - _view.LineHeight);
            _graphic.DrawLine(0xffffffff, 0, _view.LineHeight - 1, _graphic.ClientWidth, _view.LineHeight - 1);


        }

        public void InFolder(string folder = "") => _view.InFolder(folder);

        public void MuveDown() => _view.MuveDown();

        public void MuveUp() => _view.MuveUp();

        public void Copy() => _view.Copy();

        public void Cut() => _view.Cut();

        public void Rename() => _view.Rename();

        public void Paste() => _view.Paste();

        public void ListOfDisks() => _view.ListOfDisks();

        public void CriateFolder() => _view.CriateFolder();

        public void Properties() => _view.Properties();
    }
}
