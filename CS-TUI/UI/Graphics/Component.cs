using System;

namespace CS_TUI.UI.Graphics
{
    public class Component : ICloneable
    {
        protected virtual void onInit()
        {
            
        }
        protected virtual void onDestroy()
        {
            
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}