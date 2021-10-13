using System;
using System.Numerics;
using CS_TUI.UI.Graphics.Styles;

namespace CS_TUI.UI.Graphics.Elements
{
    public class Button : Text, IBackground , IColor
    {
        protected ConsoleKey key;
        
        public delegate void PressButtonHandler(ConsoleKeyInfo key, Button button);
        public event PressButtonHandler pressButtonEvent;
        
        
        public Button(Vector2 position, ConsoleKey key, ConsoleColor color = ConsoleColor.Cyan, ConsoleColor back = ConsoleColor.DarkBlue) : base(position)
        {
            this.key = key;
            Keyboard.keyPressEvent += onUse;
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

        #endregion

        protected override void onRender()
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = back;
            Console.Write(getText());
        }
        protected override void onUpdate()
        {
            Console.SetCursorPosition((int)position.X, (int)position.Y+1);
            onRender();
        }

        public override string getText()
        {
            return $"[{key.ToString()}] {text}";
        }

        protected virtual void onUse(ConsoleKeyInfo key)
        {
            if (key.Key == this.key)
            {
                pressButtonEvent?.Invoke(key,this);
                update();
            }
        }
    }
}