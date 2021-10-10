using System;
using System.Collections.Generic;

namespace CS_TUI.UI.Graphics
{
    public class Window
    {
        private static ConsoleColor barBackground = ConsoleColor.DarkGray;
        private static ConsoleColor barForeground = ConsoleColor.Blue;

        public Window(string name, ConsoleColor background = ConsoleColor.Black)
        {
            this.background = background;
            this.windowName = name;
        }

        string windowName;
        List<UIElement> elements = new List<UIElement>();
        ConsoleColor background = ConsoleColor.Black;

        private void renderBar()
        {
            Console.BackgroundColor = barBackground;
            Console.Write(new string(' ',Console.BufferWidth));
            Console.SetCursorPosition(0,0);
            Console.Write(windowName);
            Console.BackgroundColor = background;
        }

        private void clear()
        {
            Console.BackgroundColor = background;
            Console.Clear();
            renderBar();
        }
        
        public void render()
        {
            clear();
            Console.Title = windowName;
            foreach (var element in elements)
            {
                element.render();
            }
        }

        public void addElement(UIElement element)
        {
            elements.Add(element);
        }
        
        
    }
}