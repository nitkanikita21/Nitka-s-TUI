using System;
using System.Numerics;

namespace CS_TUI.UI.Graphics.Elements
{
    public class Button : Text
    {
        protected ConsoleKey key;
        
        public delegate void PressButtonHandler(ConsoleKeyInfo key, Button button);
        public event PressButtonHandler pressButtonEvent;
        
        
        public Button(Vector2 position, string text, ConsoleKey key, ConsoleColor color = ConsoleColor.Cyan, ConsoleColor back = ConsoleColor.DarkBlue) : base(position, text, color, back)
        {
            this.key = key;
            Keyboard.keyPressEvent += onUse;
        }

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