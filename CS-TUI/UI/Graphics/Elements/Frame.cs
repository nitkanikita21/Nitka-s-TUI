using System;
using System.Numerics;
using CS_TUI.UI.Graphics.Styles;

namespace CS_TUI.UI.Graphics.Elements
{
    public class Frame: Container, IBackground
    {
        protected int w, h;
        protected ConsoleColor background;

        public Frame(Vector2 position) : base(position)
        {
        }

        protected override void onRender()
        {
            Console.BackgroundColor = background;
            for (int i = 0; i < h; i++)
            {
                Console.Write(new string(' ',w));
                Console.SetCursorPosition((int) position.X,(int) position.Y+1+i);
            }
            
            
            foreach (Element element in items)
            {
                Element clone = (Element) element.Clone();
                if (clone is IBackground)
                {
                    ((IBackground)clone).setBackground(background);
                }
                clone.setPosition(position + element.getPosition());
                
                clone.render();
            }
        }

        #region Styles

        public void setWidth(int s)
        {
            w = s;
        }
        public void setHeigth(int s)
        {
            h = s;
        }
        public void setSize(Vector2 s)
        {
            w = (int) s.X;
            h = (int) s.Y;
        }

        public int getWidth()
        {
            return w;
        }

        public int getHeigth()
        {
            return h;
        }

        public Vector2 getSize()
        {
            return new Vector2(w, h);
        }

        public void setBackground(ConsoleColor s)
        {
            background = s;
        }

        public ConsoleColor getBackground()
        {
            return background;
        }

        #endregion
    }
}