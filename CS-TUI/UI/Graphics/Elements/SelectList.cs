using System;
using System.Collections.Generic;
using System.Numerics;

namespace CS_TUI.UI.Graphics.Elements
{
    public class Select : Form
    {
        public List<Text> items;
        private int selectedItem = 0;

        public int getSelectedItem()
        {
            return selectedItem;
        }

        protected void down()
        {
            if (selectedItem - 1 >= 0) selectedItem--;
        }
        protected void up()
        {
            if (selectedItem + 1 < items.Count) selectedItem++;
        }

        public Select(Vector2 position, ConsoleColor bg = ConsoleColor.Gray, int width = 6, int heigth = 3) : base(position, bg, width, heigth)
        {
            for (var i = 0; i < items.Count; i++)
            { 
                Console.SetCursorPosition((int)this.position.X, (int)this.position.Y+i);
                Console.BackgroundColor = bg;
            }
        }
    }
}