using Illuminus.Application.Util;
using Illuminus.Core.ApplicationService;
using Illuminus.Core.ApplicationService.Services;
using Illuminus.Core.DomainService;
using Illuminus.Infastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Illuminus.Application.Menus
{
    class SignUpMenu: IMenu
    {
        private readonly ICustomerService customerService;
        private readonly string FILEPATHSIGNUP = AppContext.BaseDirectory + "\\TxtFiles\\SignUpText.txt";
        private readonly int txtNamePosition = 13;
        private readonly int txtPasswordPosition = 16;
        private readonly string placeholderNameText = "Enter name";
        private readonly string placeholderPasswordText = "Enter password";
        private readonly string textField = "__________________";


        public SignUpMenu(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public void Initialize()
        {
            Console.Clear();
            ShowSignUp();
            Console.ReadLine();
        }

        private void ShowSignUp()
        {
            ShowSignUpASCII();
            ShowSignUpFields();
            UserSignUp();
        }

        private void ShowSignUpASCII()
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHSIGNUP, 37, 2);
        }

        private void ShowSignUpFields()
        {
            Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
            Console.Write(placeholderNameText);
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtNamePosition + 1);
            Console.Write(textField);
            Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
            Console.Write(placeholderPasswordText);
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtPasswordPosition + 1);
            Console.Write(textField);
        }

        private void UserSignUp()
        {
            Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
            ASCIIAnimator.Instance.ClearCurrentConsoleLine();
            Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
            string name = Console.ReadLine();
            Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
            ASCIIAnimator.Instance.ClearCurrentConsoleLine();
            Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
            string password = Console.ReadLine();
            customerService.CreateCustomer(name, password);
            ChangeToLogInMenu();
        }

        private void ChangeToLogInMenu()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<IMenu, LogInMenu>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IMenu logInMenu = serviceProvider.GetRequiredService<IMenu>();
            logInMenu.Initialize();
        }
    }
}
