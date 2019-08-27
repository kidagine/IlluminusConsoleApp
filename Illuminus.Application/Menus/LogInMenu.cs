using Illuminus.Application.Util;
using Illuminus.Core.ApplicationService;
using Illuminus.Core.ApplicationService.Services;
using Illuminus.Core.DomainService;
using Illuminus.Core.Entity;
using Illuminus.Infastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Illuminus.Application.Menus
{
    class LogInMenu: IMenu
    {
        private readonly ICustomerService customerService;
        private readonly string FILEPATHLOGIN = AppContext.BaseDirectory + "\\TxtFiles\\LogInText.txt";
        private readonly int txtNamePosition = 13;
        private readonly int txtPasswordPosition = 16;
        private readonly string placeholderNameText = "Enter name";
        private readonly string placeholderPasswordText = "Enter password";
        private readonly string textField = "__________________";


        public LogInMenu(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public void Initialize()
        {
            Console.Clear();
            ShowLogIn();
            Console.ReadLine();
        }

        private void ShowLogIn()
        {
            ShowLogInASCII();
            ShowLogInFields();
            UserLogIn();
        }

        private void ShowLogInASCII()
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHLOGIN, 37, 2);
        }

        private void ShowLogInFields()
        {
            Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
            Console.Write(placeholderNameText);
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtNamePosition + 1);
            Console.Write(textField);
            Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
            Console.Write(placeholderPasswordText);
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtPasswordPosition + 1);
            Console.Write(textField);
            Console.SetCursorPosition(45, 20);
            Console.WriteLine("If you want to sign up type: 420");
        }

        private void UserLogIn()
        {
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtNamePosition);
            ASCIIAnimator.Instance.ClearCurrentConsoleLine();
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtNamePosition);
            string name = Console.ReadLine();
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtPasswordPosition);
            ASCIIAnimator.Instance.ClearCurrentConsoleLine();
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtPasswordPosition);
            string password = Console.ReadLine();

            Customer customer = customerService.GetCustomer(name, password);
            if (!name.Equals("420") && !password.Equals("420"))
            {
                if (customer != null)
                {
                    ChangeToMainMenu(name);
                }
                else
                {
                    UserLogIn();
                }
            }
            else
            {
                ChangeToSignUpMenu();
            }
        }

        private void ChangeToMainMenu(string name)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IVideoRepository, VideoRepository>();
            serviceCollection.AddScoped<IVideoService, VideoService>();
            serviceCollection.AddScoped<IMenu, MainMenu>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IMenu mainMenu = serviceProvider.GetRequiredService<IMenu>();
            mainMenu.Initialize();
        }

        private void ChangeToSignUpMenu()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<IMenu, SignUpMenu>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IMenu signUpMenu = serviceProvider.GetRequiredService<IMenu>();
            signUpMenu.Initialize();
        }
    }
}
