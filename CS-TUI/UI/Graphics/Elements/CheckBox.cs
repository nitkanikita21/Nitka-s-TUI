using System;
using System.Numerics;
using CS_TUI.UI.Graphics.Styles;

namespace CS_TUI.UI.Graphics.Elements
{
    public class CheckBox : Text, IBackground , IColor
    {
        protected string checkedChar = "+";
        protected string uncheckedChar = "x";
        

        public bool check = false;

        public CheckBox(Vector2 position, string text, ConsoleColor color, bool check = false) : base(position)
        {
            this.check = check;

            Keyboard.keyPressEvent += keyPress;
        }

        #region Styles
        public virtual ConsoleColor getColor()
        {
            return color;
        }
        public virtual ConsoleColor getBackground()
        {
            return back;
        }



        public void setIcons(string checkedIcon, string uncheckedIcon)
        {
            uncheckedChar = uncheckedIcon;
            checkedChar = checkedIcon;
        }

        #endregion

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