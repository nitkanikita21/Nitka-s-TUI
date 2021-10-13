using System.Numerics;

namespace CS_TUI.UI.Graphics
{
    public class UIPositioned
    {
        protected Vector2 position;

        public Vector2 getPosition()
        {
            return position;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public void addPosition(Vector2 position)
        {
            this.position += position;
        }
    }
}