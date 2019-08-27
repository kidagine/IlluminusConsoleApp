using Illuminus.Core.DomainService;
using Illuminus.Core.Entity;

namespace Illuminus.Core.ApplicationService.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository customerRepository;


        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void CreateCustomer(string name, string password)
        {
            customerRepository.CreateCustomer(name, password);
        }

        public Customer GetCustomer(string name, string password)
        {
            return customerRepository.GetCustomer(name, password);
        }
    }
}
