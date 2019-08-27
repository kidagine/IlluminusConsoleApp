using Illuminus.Core.Entity;

namespace Illuminus.Core.ApplicationService
{
    public interface ICustomerService
    {
        void CreateCustomer(string name, string password);
        Customer GetCustomer(string name, string password);
    }
}
