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

        public Text(Vector2 position) : base(position)
        {
            this.text = text;
        }

        #region Styles

        public void setColor(ConsoleColor s)
        {
            color = s; 
        }
        public void setBackground(ConsoleColor s)
        {
            back = s;
        }
        public ConsoleColor getColor()
        {
            return color;
        }
        public ConsoleColor getBackground()
        {
            return back;
        }

        
        
        
        public virtual string getText()
        {
            return text;
        }
        public virtual Text setText(string s)
        {
            text = s;
            return this;
        }
        #endregion

        protected override void onRender()
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = back;
            Console.Write(text);
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