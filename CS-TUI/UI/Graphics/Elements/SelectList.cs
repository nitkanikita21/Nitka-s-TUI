using System;
using System.Collections.Generic;
using System.Numerics;

namespace CS_TUI.UI.Graphics.Elements
{
    public class SelectList : Form
    {
        public delegate void ClickItemHandler(int selectedItem, Text label);
        public event ClickItemHandler clickItemEvent;
        
        public delegate void SelectItemHandler(int selectedItem, Text label);
        public event SelectItemHandler selectItemEvent;
        
        protected List<Text> items = new List<Text>();
        private int selectedItem = 0;
        private int offset = 0;

        private ConsoleColor fgUnselected;
        private ConsoleColor fgSelected;

        protected ConsoleKey upKey;
        protected ConsoleKey downKey;
        protected ConsoleKey enter;
        
        string selector;
        private ConsoleColor selectorColor;
        private ConsoleColor selectorBack;
        
        public int getSelectedItem()
        {
            return selectedItem;
        }

        protected void up()
        {
            if (selectedItem <= offset && offset != 0)
            {
                selectedItem--;
                offset--;
            }
            else if(selectedItem - 1 >= 0)
            {
                selectedItem--;
            }
            
            selectItemEvent?.Invoke(selectedItem,items[selectedItem]);
        }
        protected void down()
        {
            if (selectedItem + 1 < items.Count) selectedItem++;
            if (selectedItem + 1 > (heigth - 1)+offset)
            {
                //selectedItem++;
                offset++;
            }
            selectItemEvent?.Invoke(selectedItem,items[selectedItem]);
        }

        public void addItem(Text label)
        {
            items.Add(label);
        }
        public void removeItem(Text label)
        {
            items.Remove(label);
        }

        public void clear()
        {
            items = new List<Text>();
            offset = 0;
            selectedItem = 0;
        }

        public SelectList(
            Vector2 position, int width = 6, int heigth = 3, 
            ConsoleColor bg = ConsoleColor.Gray,
            ConsoleColor fgUnselected = ConsoleColor.DarkGray,
            ConsoleColor fgSelected = ConsoleColor.Blue,
            ConsoleKey upKey = ConsoleKey.UpArrow,
            ConsoleKey downKey = ConsoleKey.DownArrow,
            ConsoleKey enter = ConsoleKey.Enter,
            string selector = "",
            ConsoleColor selectorColor = ConsoleColor.DarkGreen,
            ConsoleColor selectorBack = ConsoleColor.DarkBlue

            
            ) : base(position, bg, width, heigth)
        {
            this.fgUnselected = fgUnselected;
            this.fgSelected = fgSelected;

            this.upKey = upKey;
            this.downKey = downKey;
            this.enter = enter;
            this.selector = selector;

            this.selectorColor = selectorColor;
            this.selectorBack = selectorBack;
            
            Keyboard.keyPressEvent += onKeyInterract;
        }

        private void onKeyInterract(ConsoleKeyInfo key)
        {
            if (key.Key == upKey)
            {
                up();
                render();
            };
            if (key.Key == downKey)
            {
                down();
                render();
            };
            if (key.Key == enter)
            {
                clickItemEvent?.Invoke(selectedItem, items[selectedItem]);
                render();
            }
            //UICore.setAppName(offset+"");
        }

        protected override void onRender()
        {
            base.onRender();
            int y = 0;
            for (var i = offset; i < (heigth - 1)+offset && i < items.Count; i++)
            {
                int posY = (int) this.position.Y + 1 + y;
                int posX = (int) this.position.X;
                Console.SetCursorPosition(posX, posY);
                Console.BackgroundColor = background;
                Console.ForegroundColor = i == selectedItem ? fgSelected : fgUnselected;
                string sel = (i == selectedItem ? selector : "");
                string txt = (
                    (sel + items[i].getText()).Length >= width ?
                    (sel + items[i].getText()).Substring(0,width-3)+"..." :
                    (sel + items[i].getText())
                    );
                Console.Write(txt+new string(' ',selector.Length-2));
                y++;
            }
        }
    }
}