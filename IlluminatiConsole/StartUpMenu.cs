using System;


namespace IlluminatiConsole
{
    class StartUpMenu
    {
        private readonly string FILEPATHLOGO = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\LogoText.txt";


        public void Initialize()
        {
            Console.Clear();
            ShowLogo();
            Console.ReadLine();
        }

        private void ShowLogo()
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHLOGO, 25, 2, 5, true);
            LogInMenu logInMenu = new LogInMenu();
            logInMenu.Initialize();
        }
    }
}
