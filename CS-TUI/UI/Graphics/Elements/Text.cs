using System;
using System.Numerics;

namespace CS_TUI.UI.Graphics.Elements
{
    public class Text : Element
    {
        protected string text
        {
            get;
            set;
        }

        public ConsoleColor color
        {
            get;
            set;
        }
        public ConsoleColor back
        {
            get;
            set;
        }

        public Text(Vector2 position, string text, ConsoleColor color = ConsoleColor.White, ConsoleColor back = ConsoleColor.Black) : base(position)
        {
            this.text = text;
            this.color = color;
            this.back = back;
        }

        protected override void onRender()
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = back;
            Console.Write(text);
        }

        public virtual string getText()
        {
            return text;
        }

        public virtual void setText(string str)
        {
            text = str;
        }
        
        public int centerOffsetX()
        {
            return getWidth() / 2;
        }

        protected int getWidth()
        {
            return getText().Length;
        }
    }
}