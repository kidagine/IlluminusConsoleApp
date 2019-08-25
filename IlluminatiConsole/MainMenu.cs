using IlluminatiConsole.BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IlluminatiConsole
{
    class MainMenu
    {
        private readonly string FILEPATHWELCOMEBACK = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\WelcomeBackText.txt";
        private readonly string FILEPATHMENULAYOUT = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\MainMenuLayoutText.txt";
        private readonly string FILEPATHVIDEOS = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\VideosText.txt";


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
            ShowMenuLayout();
            ShowVideosList();
        }

        private void ShowMenuLayout()
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHMENULAYOUT, 0, 0);
        }

        private void ShowVideosList()
        {
            string listOfVideosText = "LIST OF VIDEOS";
            Console.SetCursorPosition((Console.WindowWidth - listOfVideosText.Length) / 2, 4);
            Console.WriteLine(listOfVideosText);
            string listTabs = "|  #         VIDEO                                         GENRE                                 ";
            Console.SetCursorPosition(18, 7);
            Console.WriteLine(listTabs);
            string spacingTabText = "|----------------------------------------------------------------------------------------------------";
            Console.SetCursorPosition(18, 8);
            Console.WriteLine(spacingTabText);

            int countTop = 9;
            int maxSpacing = 46;
            string spacingBetween = "";
            List<Video> videosList = MainModel.Instance.LoadVideosList();
            foreach (Video v in videosList)
            {
                int spacingToAdd = maxSpacing - v.Name.Length;
                for (int i = 0; i <spacingToAdd; i++)
                {
                    spacingBetween += " ";
                }
                string videoListing = ($"|  {v.Id}         {v.Name}{spacingBetween}{v.Genre}");
                Console.SetCursorPosition(18, countTop);
                Console.WriteLine(videoListing);
                countTop++;
                spacingBetween = "";

                string spacingText = "|----------------------------------------------------------------------------------------------------";
                Console.SetCursorPosition(18, countTop);
                Console.WriteLine(spacingText);
                countTop++;
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(" ");
            ClearGivenConsoleLine();
        }

        private void ClearVideoList()
        {
            int topCount = 9;
            for (int i = 0; i < 21; i++)
            {
                Console.SetCursorPosition(22, topCount);
                Console.Write(new string(' ', 200));
                topCount++;
            }

            topCount = 9;
            int listDefaultLength = 30;
            for (int i = 0; i < listDefaultLength; i++)
            {
                Console.SetCursorPosition(18, topCount);
                Console.Write('|');
                topCount++;
            }
        }

        private void ClearGivenConsoleLine()
        {
            int topCount = 9;
            for (int i = 0; i < 21; i++)
            {
                Console.SetCursorPosition(0, topCount);
                Console.Write(new string(' ', 18));
                topCount++;
            }
            AllowUserInput();
        }

        private void AllowUserInput()
        {
            bool allowOptionChoosing = true;
            Console.SetCursorPosition(0, 10);
            ConsoleKeyInfo cki;
            do
            {
                while (Console.KeyAvailable == false)
                    Thread.Sleep(250); 

                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.D0)
                {
                    allowOptionChoosing = false;
                    AddVideo();
                }
                else if (cki.Key == ConsoleKey.D1)
                {
                    allowOptionChoosing = false;
                    RemoveVideo();
                }
                else if (cki.Key == ConsoleKey.D2)
                {
                    allowOptionChoosing = false;
                    EditVideo();
                }
            } while (allowOptionChoosing);
        }

        private void AddVideo()
        {
            Console.WriteLine("-Add video-");
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Genre:");
            string genre = Console.ReadLine();
            MainModel.Instance.AddVideo(name, genre);
            ShowVideosList();
        }

        private void RemoveVideo()
        {
            Console.WriteLine("Select id of video");
            int id = int.Parse(Console.ReadLine());
            MainModel.Instance.RemoveVideo(id);
            ClearVideoList();
            ShowVideosList();
        }

        private void EditVideo()
        {
            Console.WriteLine("Select id of video");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Edit Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Edit Genre:");
            string genre = Console.ReadLine();
            MainModel.Instance.EditVideo(id, name, genre);
            ClearVideoList();
            ShowVideosList();
        }
    }
}
