using System;
using System.Numerics;

namespace CS_TUI.UI.Graphics.Elements
{
    public class CheckBox : Label
    {
        private static string checkedChar = "+";
        private static string uncheckedChar = "x";
        

        private bool check = false;

        public CheckBox(Vector2 position, string text, ConsoleColor color, bool check = false) : base(position,text,color)
        {
            this.check = check;
        }

        protected override void onRender()
        {
            Console.ForegroundColor = color;
            Console.Write("["+(check ? checkedChar : uncheckedChar)+ "] " + text);
        }

        protected override void onUpdate()
        {
            onRender();
        }
    }
}