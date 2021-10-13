using System;
using System.Threading;
using System.Windows.Input;

namespace CS_TUI.UI
{
    public class Keyboard
    {
        public delegate void KeyPressHandler(ConsoleKeyInfo key);
        public static event KeyPressHandler keyPressEvent;

        private Thread keyEventThread;
        
        public void init()
        {
            keyEventThread = new Thread(new ThreadStart(keyPressCheck));
            keyEventThread.Start();
        }

        private void keyPressCheck()
        {
            while (true)
            {
                var consoleKeyInfo = Console.ReadKey(true);
                keyPressEvent?.Invoke(consoleKeyInfo);
            }
        }
    }
}