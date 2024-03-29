﻿using System;

namespace Illuminus.Application.Util
{
    class ASCIIAnimator
    {
        private static ASCIIAnimator instance = null;


        public static ASCIIAnimator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ASCIIAnimator();
                }
                return instance;
            }
        }

        public void CreateASCIIAnimation(string path, int startingPositionX, int startingPositionY, int speed = 5, bool willHide = false, int delay = 500)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                Console.SetCursorPosition(startingPositionX, startingPositionY);
                foreach (char c in lines[i])
                {
                    Console.Write(c);
                    if (!Char.IsWhiteSpace(c))
                    {
                        System.Threading.Thread.Sleep(speed);
                    }
                }
                Console.WriteLine();
                startingPositionY++;
            }
            System.Threading.Thread.Sleep(500);
            if (willHide)
            {
                HideASCIIAnimation(path, startingPositionY, speed);
            }
        }

        public void HideASCIIAnimation(string path, int startingPositionY, int speed)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                Console.SetCursorPosition(i, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
                System.Threading.Thread.Sleep(speed * 10);
                startingPositionY++;
            }
        }

        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
