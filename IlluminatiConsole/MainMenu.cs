using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlluminatiConsole
{
    class MainMenu
    {
        private readonly string FILEPATHWELCOMEBACK = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\WelcomeBackText.txt";


        public void Initialize(string name)
        {
            Console.Clear();
            ShowWelcome(name);
            Console.ReadLine();
        }

        private void ShowWelcome(string name)
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHWELCOMEBACK, 10, 2, 2);
            Console.SetCursorPosition(57, 18);
            Console.WriteLine(name);
            System.Threading.Thread.Sleep(600);
            Console.Clear();
        }
    }
}
