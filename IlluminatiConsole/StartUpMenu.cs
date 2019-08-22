using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlluminatiConsole
{
    class StartUpMenu
    {
        private readonly string FILEPATHLOGO = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\LogoText.txt";

        //Console.SetCursorPosition((Console.WindowWidth - logoLines[i].Length) / 2, Console.CursorTop);
        public void Initialize()
        {
            Console.Title = "ILLUMINUS";
            Console.Clear();
            ShowLogo();
            Console.ReadLine();
        }

        private void ShowLogo()
        {
            using (StreamReader srLogo = new StreamReader(FILEPATHLOGO))
            {
                string logoLine;
                while ((logoLine = srLogo.ReadLine()) != null)
                {
                    string[] logoLines = logoLine.Split('|');
                    for (int i = 0; i < logoLines.Length; i++)
                    {
                        Console.SetCursorPosition(20, Console.CursorTop);
                        foreach (char c in logoLines[i])
                        {
                            Console.Write(c);
                            if (!Char.IsWhiteSpace(c))
                            {
                                System.Threading.Thread.Sleep(5);
                            }
                        }
                        if (logoLines[i].Length > 14)
                        {
                            Console.WriteLine();
                        }
                    }
                }
                srLogo.Close();
                System.Threading.Thread.Sleep(500);
                HideLogo();
            }
        }

        private void HideLogo()
        {
            using (StreamReader srLogo = new StreamReader(FILEPATHLOGO))
            {
                string logoLine;
                while ((logoLine = srLogo.ReadLine()) != null)
                {
                    string[] logoLines = logoLine.Split('|');
                    for (int i = 0; i < logoLines.Length; i++)
                    {
                        if (logoLines[i].Length > 14)
                        {
                            Console.SetCursorPosition(i, Console.CursorTop - 1);
                            ClearCurrentConsoleLine();
                            System.Threading.Thread.Sleep(50);
                        }
                    }
                }
                srLogo.Close();
                LogInMenu logInMenu = new LogInMenu();
                logInMenu.Initialize();
            }
        }

        private void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
