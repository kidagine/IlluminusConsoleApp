using Illuminus.Application.Menus;
using Illuminus.Core.ApplicationService;
using Illuminus.Core.ApplicationService.Services;
using Illuminus.Core.DomainService;
using Illuminus.Infastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Illuminus.Application
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ILLUMINUS";
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<IMenu, StartUpMenu>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IMenu startUpMenu = serviceProvider.GetRequiredService<IMenu>();
            startUpMenu.Initialize();
        }   
    }
}
