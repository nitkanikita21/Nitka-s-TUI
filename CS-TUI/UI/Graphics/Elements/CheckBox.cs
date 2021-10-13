using System;
using System.Numerics;

namespace CS_TUI.UI.Graphics.Elements
{
    public class CheckBox : Text
    {
        private static string checkedChar = "+";
        private static string uncheckedChar = "x";
        

        public bool check = false;

        public CheckBox(Vector2 position, string text, ConsoleColor color, bool check = false) : base(position,text,color)
        {
            this.check = check;

            Keyboard.keyPressEvent += keyPress;
        }

        private void keyPress(ConsoleKeyInfo key)
        {
            Console.Beep();
            check = !check;
            onUpdate();
        }
        
        protected override void onRender()
        {
            Console.ForegroundColor = color;
            Console.Write(getText());
        }
        
        public override string getText()
        {
            return $"[{(check ? checkedChar : uncheckedChar)}] {text}";
        }

        protected override void onUpdate()
        {
            Console.SetCursorPosition((int)position.X, (int)position.Y+1);
            onRender();
        }
    }
}