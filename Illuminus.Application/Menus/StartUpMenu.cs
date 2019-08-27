using System;
using Illuminus.Application.Util;
using Illuminus.Core.ApplicationService;
using Illuminus.Core.ApplicationService.Services;
using Illuminus.Core.DomainService;
using Illuminus.Infastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Illuminus.Application.Menus
{
    class StartUpMenu: IMenu
    {
        private readonly string FILEPATHLOGO = AppContext.BaseDirectory + "\\TxtFiles\\LogoText.txt";

        public void Initialize()
        {
            Console.Clear();
            ShowLogoASCII();
        }

        private void ShowLogoASCII()
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHLOGO, 25, 2, 5, true);
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
