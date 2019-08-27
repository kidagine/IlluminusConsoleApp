using Illuminus.Application.Util;
using Illuminus.Core.ApplicationService;
using Illuminus.Core.Entity;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Illuminus.Application.Menus
{
    class MainMenu: IMenu
    {
        private readonly IVideoService videoService;
        private readonly string FILEPATHWELCOMEBACK = AppContext.BaseDirectory + "\\TxtFiles\\WelcomeBackText.txt";
        private readonly string FILEPATHMENULAYOUT = AppContext.BaseDirectory + "\\TxtFiles\\MainMenuLayoutText.txt";


        public MainMenu(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        public void Initialize()
        {
            Console.Clear();
            ShowWelcome("skelly");
            Console.ReadLine();
        }

        private void ShowWelcome(string name)
        {
            ShowWelcomeASCII(name);
            ShowMenuLayoutASCII();
            ShowVideosList();
        }

        private void ShowWelcomeASCII(string name)
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHWELCOMEBACK, 10, 2, 2);
            Console.SetCursorPosition(57, 18);
            Console.WriteLine(name);
            Thread.Sleep(600);
            Console.Clear();
        }

        private void ShowMenuLayoutASCII()
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHMENULAYOUT, 0, 0);
        }

        private void ShowVideosList()
        {
            ShowVideoListLayout();
            ShowVideoListData(videoService.ReadAllVideos());
            ClearUserConsole();
        }

        private void ShowVideoListLayout()
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
        }

        private void ShowVideoListData(List<Video> videosList)
        {
            int countTop = 9;
            int maxSpacing = 46;
            string spacingBetween = "";
            foreach (Video v in videosList)
            {
                int spacingToAdd = maxSpacing - v.Name.Length;
                for (int i = 0; i < spacingToAdd; i++)
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
            Console.WriteLine("");
        }

        private void ClearUserConsole()
        {
            int topCount = 9;
            for (int i = 0; i < 21; i++)
            {
                Console.SetCursorPosition(0, topCount);
                Console.Write(new string(' ', 18));
                topCount++;
            }
            UserMainMenu();
        }

        private void UserMainMenu()
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
                    DeleteVideo();
                }
                else if (cki.Key == ConsoleKey.D2)
                {
                    allowOptionChoosing = false;
                    EditVideo();
                }
                else if (cki.Key == ConsoleKey.D3)
                {
                    allowOptionChoosing = false;
                    SearchVideos();
                }
                else if (cki.Key == ConsoleKey.D4)
                {
                    allowOptionChoosing = false;
                    ShowVideosList();
                }
            } while (allowOptionChoosing);
        }

        private void AddVideo()
        {
            Console.WriteLine("-Add-");
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Genre:");
            string genre = Console.ReadLine();
            videoService.CreateVideo(name, genre);
            ShowVideosList();
            ClearUserConsole();
        }

        private void DeleteVideo()
        {
            Console.WriteLine("-Delete-");
            Console.WriteLine("Select id of video");
            int id = int.Parse(Console.ReadLine());
            videoService.RemoveVideo(id);
            ClearVideoList();
            ShowVideosList();
            ClearUserConsole();
        }

        private void EditVideo()
        {
            Console.WriteLine("-Edit-");
            Console.WriteLine("Select id of video");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Edit Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Edit Genre:");
            string genre = Console.ReadLine();
            videoService.UpdateVideo(id, name, genre);
            ClearVideoList();
            ShowVideosList();
            ClearUserConsole();
        }

        private void SearchVideos()
        {
            string exitSearchText = "69";
            Console.WriteLine("-Search-");
            Console.WriteLine("search by anything");
            Console.WriteLine("type 69 reset");
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("");
            Console.SetCursorPosition(0, 14);
            string filter = Console.ReadLine();
            if (filter != exitSearchText)
            {
                List<Video> allVideos = videoService.ReadAllVideos();
                List<Video> filteredVideos = videoService.SearchVideos(allVideos, filter);
                ClearVideoList(allVideos.Count * 2);
                ShowVideoListData(filteredVideos);
            }
            else
            {
                ShowVideosList();
            }
            Console.SetCursorPosition(0, 9);
            Console.WriteLine("");
            SearchVideos();
        }

        private void ClearVideoList(int lengthToDelete = 24)
        {
            int topCount = 9;
            for (int i = 0; i < lengthToDelete; i++)
            {
                Console.SetCursorPosition(22, topCount);
                Console.Write(new string(' ', 500));
                topCount++;
            }

            topCount = 9;
            int listDefaultLength = 21;
            for (int i = 0; i < listDefaultLength; i++)
            {
                Console.SetCursorPosition(18, topCount);
                Console.Write('|');
                topCount++;
            }
        }
    }
}
