using Illuminus.Core.Entity;

namespace Illuminus.Core.DomainService
{
    public interface ICustomerRepository
    {
        void CreateCustomer(string name, string password);
        Customer GetCustomer(string name, string password);
    }
}
