using System;
using System.Numerics;
using CS_TUI.UI;
using CS_TUI.UI.Graphics;
using CS_TUI.UI.Graphics.Elements;

namespace CS_TUI.Other
{
    public class InterfaceTests
    {
        public static Window widnow = new Window(
            "Tests", ConsoleColor.DarkBlue,
            w: 80, h:40);
        public static void Start(string[] args)
        {
            UICore.init("InterfaceTests");
            
            
            Frame frame = new Frame(Vector2.One);
            frame.setBackground(ConsoleColor.Gray);
            frame.setSize(new Vector2(24,38));

            Text text = new Text(Vector2.One);
            text.setText("Test text");
            text.setColor(ConsoleColor.Black);
            
            frame.addElement(text);
            
            
            
            
            widnow.addElement(frame);
            widnow.render();
            while (true)
            {
                ;
            }
        }
    }
}