using System;


namespace IlluminatiConsole
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ILLUMINUS";
            StartUpMenu startUpMenu = new StartUpMenu();
            startUpMenu.Initialize();
        }
    }
}
