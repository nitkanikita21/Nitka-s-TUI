using System;
using System.Numerics;

namespace CS_TUI.UI.Graphics
{
    public class UIElement
    {
        public UIElement(Vector2 position)
        {
            this.position = position;
        }
        
        public Vector2 position
        {
            get;
            set;
        }

        protected virtual void onRender()
        {
        }
        protected virtual void onUpdate()
        {
        }
        
        public void render()
        {
            Console.SetCursorPosition((int) position.X,(int)position.Y+1);
            onRender();
        }

        public void update()
        {
            Console.SetCursorPosition((int) position.X,(int)position.Y);
            onUpdate();
        }
    }
}