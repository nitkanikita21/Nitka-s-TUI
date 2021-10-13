using System;
using System.Numerics;

namespace CS_TUI.UI.Graphics
{
    public class Element : Component
    {
        public Element(Vector2 position)
        {
            this.position = position;
        }

        protected Vector2 position;

        public virtual Vector2 getPosition()
        {
            return position;
        }

        public virtual void setPosition(Vector2 pos)
        {
            position = pos;
        }

        public virtual void addPosition(Vector2 pos)
        {
            position += pos;
        }
        

        protected virtual void onRender()
        {
        }
        protected virtual void onUpdate()
        {
        }
        
        public void render(bool setPos = true)
        {
            if(setPos)Console.SetCursorPosition((int) position.X,(int)position.Y+1);
            onRender();
        }

        public void update(bool setPos = true)
        {
            if(setPos)Console.SetCursorPosition((int) position.X,(int)position.Y+1);
            onUpdate();
        }
    }
}