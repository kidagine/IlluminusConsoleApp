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
        private readonly string FILEPATHWELCOMEBACK = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\LogInText.txt";
        private readonly string FILEPATHDISPLAYNAME = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\DisplayNameText.txt";

        public void Initialize(string name)
        {
            Console.Clear();
            ShowWelcome(name);
            Console.ReadLine();
        }

        private void ShowWelcome(string name)
        {
            using (StreamReader srLogo = new StreamReader(FILEPATHWELCOMEBACK))
            {
                string logoLine;
                while ((logoLine = srLogo.ReadLine()) != null)
                {
                    string[] logoLines = logoLine.Split('|');
                    for (int i = 0; i < logoLines.Length; i++)
                    {
                        Console.SetCursorPosition(35, Console.CursorTop);
                        foreach (char c in logoLines[i])
                        {
                            Console.Write(c);
                            if (!Char.IsWhiteSpace(c))
                            {
                                System.Threading.Thread.Sleep(5);
                            }
                        }
                        if (logoLines[i].Length > 0)
                        {
                            Console.WriteLine();
                        }
                    }
                }
                srLogo.Close();
                System.Threading.Thread.Sleep(500);
                ShowDisplayName(name);
                Console.SetCursorPosition(35, Console.CursorTop);
            }
        }

        private void ShowDisplayName(string name)
        {
            using (StreamReader srLogo = new StreamReader(FILEPATHDISPLAYNAME))
            {
                int count = 12;
                string logoLine;
                while ((logoLine = srLogo.ReadLine()) != null)
                {
                    string[] logoLines = logoLine.Split('Σ');
                    for (int i = 0; i < logoLines.Length; i++)
                    {
                        Console.SetCursorPosition(35,count);
                        foreach (char c in logoLines[i])
                        {
                            Console.Write(c);
                        }
                        if (logoLines[i].Length > 0)
                        {
                            Console.WriteLine();
                        }
                        count++;
                    }
                }
                srLogo.Close();
                System.Threading.Thread.Sleep(500);
                Console.SetCursorPosition(35, 11);
                Console.WriteLine(name);
            }
        }
    }
}
