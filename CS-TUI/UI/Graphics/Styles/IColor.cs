using System;

namespace CS_TUI.UI.Graphics.Styles
{
    public interface IColor
    {
        public void setColor(ConsoleColor s);
        public ConsoleColor getColor();
    }
}