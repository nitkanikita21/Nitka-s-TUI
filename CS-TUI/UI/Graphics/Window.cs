using System;
using System.Collections.Generic;
using System.Numerics;

namespace CS_TUI.UI.Graphics
{
    public class Window
    {
        private static ConsoleColor barBackground = ConsoleColor.DarkGray;
        private static ConsoleColor barForeground = ConsoleColor.Blue;

        public Window(string title, ConsoleColor background = ConsoleColor.Black, int w = 64, int h = 32)
        {
            this.background = background;
            this.title = title;
            this.w = w;
            this.h = h;
            
            Console.SetWindowSize(w,h);
            Console.SetBufferSize(w,h);
        }

        private string title;

        public void setTitle(string t)
        {
            title = t;
            renderBar();
        }

        public string getTitle()
        {
            return title;
        }
        
        List<UIElement> elements = new List<UIElement>();
        ConsoleColor background = ConsoleColor.Black;
        private int w;
        private int h;

        private void renderBar()
        {
            Console.BackgroundColor = barBackground;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(new string(' ',Console.BufferWidth));
            Console.SetCursorPosition(0,0);
            Console.Write(title);
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
            Console.Title = $"{UICore.getAppName()}: {title}";
            Console.ResetColor();
            Console.BackgroundColor = background;
            foreach (var element in elements)
            {
                element.render();
            }
        }

        public void addElement(UIElement element)
        {
            elements.Add(element);
        }

        public int getWidth()
        {
            return w;
        }

        public int getHeigth()
        {
            return h-1;
        }

        public void exit()
        {
            render();
            UICore.exit(-1);
        }
    }
}