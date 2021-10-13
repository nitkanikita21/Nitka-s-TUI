using System.Collections.Generic;
using System.Numerics;

namespace CS_TUI.UI.Graphics.Elements
{
    public class Container: Element
    {
        protected List<Element> items = new List<Element>();

        public void addElement(Element e)
        {
            items.Add(e);
        }

        public void removeElement(Element e)
        {
            items.Remove(e);
        }

        public Element getElement(int index)
        {
            return items[index];
        }


        protected virtual void onAddedElement(){}
        protected virtual void onRemovedElement(){}

        public Container(Vector2 position) : base(position)
        {
        }
    }
}