using NConsoleGraphics;
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
        FolderActions _actions;

        public Window(ConsoleGraphics graphics, FolderActions view, int ident)
        {
            _graphic = graphics;
            _actions = view;
            Indent = ident;
        }

        public void ShowContents()
        {
            DrawDatails(_actions.DiscName);
            _actions.Show(Indent);
        }

        private void DrawDatails(string diskName)
        {
            _graphic.FillRectangle(0xff000000, Indent + 1, 0, (_graphic.ClientWidth / 2), _graphic.ClientHeight);

            _graphic.DrawString($"{diskName}", "Arial", 0xffffffff, Indent, 0, 12);
            _graphic.DrawString("F1-copy  :  F2-cut  :  F3-paste  :  F4-list of disks  :  F5-properties  :  F6-rename  :  F7-criate folder",
                                                            "Arial", 0xffffffff, 0, _graphic.ClientHeight - _actions.LineHeight, 10);

            _graphic.DrawLine(0xffffffff, _graphic.ClientWidth / 2, 0, _graphic.ClientWidth / 2, _graphic.ClientHeight - _actions.LineHeight);
            _graphic.DrawLine(0xffffffff, 0, _graphic.ClientHeight - _actions.LineHeight, _graphic.ClientWidth, _graphic.ClientHeight - _actions.LineHeight);
            _graphic.DrawLine(0xffffffff, 0, _actions.LineHeight - 1, _graphic.ClientWidth, _actions.LineHeight - 1);
        }

        public void HideWindow()
        {
            _graphic.FillRectangle(0xff615f5d, Indent, 0, _graphic.ClientWidth / 2, _graphic.ClientHeight);
            _actions.Show(Indent);
        }

        public void InFolder(string folder = "") => _actions.InFolder(folder);

        public void MuveDown() => _actions.MuveDown();

        public void MuveUp() => _actions.MuveUp();

        public void Copy() => _actions.Copy();

        public void Cut() => _actions.Cut();

        public void Rename() => _actions.Rename(Indent);

        public void Paste() => _actions.Paste(Indent);

        public void ListOfDisks() => _actions.ListOfDisks(Indent);

        public void CriateFolder() => _actions.CriateFolder();

        public void Properties() => _actions.Properties(Indent);
    }
}
