using System;
using System.Numerics;

namespace CS_TUI.UI.Graphics.Elements
{
    public class Form : Element
    {
        public int width
        {
            get;
            set;
        }
        public int heigth
        {
            get;
            set;
        }

        protected ConsoleColor background
        {
            get;
            set;
        }
        
        public Form(Vector2 position, ConsoleColor bg = ConsoleColor.Gray, int width = 6, int heigth = 3) : base(position)
        {
            this.heigth = heigth;
            this.width = width;
            background = bg;
        }

        public Vector2 getCenter(bool w = true, bool h = true)
        {
            return new Vector2(
                (w ? width / 2 : position.X), 
                (h ? heigth / 2 : position.Y)
            );
        }

        protected override void onRender()
        {
            Console.BackgroundColor = background;
            for (int i = 0; i < heigth; i++)
            {
                Console.Write(new string(' ',width));
                Console.SetCursorPosition((int) position.X,(int) position.Y+1+i);
            }
            Console.ResetColor();
        }
    }
}