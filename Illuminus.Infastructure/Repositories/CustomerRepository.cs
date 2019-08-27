using System;
using System.IO;
using Illuminus.Core.DomainService;
using Illuminus.Core.Entity;

namespace Illuminus.Infastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string FILEPATHCUSTOMERS = AppContext.BaseDirectory + "\\TxtFiles\\CustomersText.txt";


        public void CreateCustomer(string name, string password)
        {
            Customer customer = new Customer(GetNextCustomerId(), name, password);
            string customerLine = customer.Id.ToString() + "|" + customer.Name + "|" + customer.Password;
            File.AppendAllText(FILEPATHCUSTOMERS, customerLine + Environment.NewLine);

        }

        public Customer GetCustomer(string name, string password)
        {
            using (StreamReader srCustomers = new StreamReader(FILEPATHCUSTOMERS))
            {
                string customerLine = "";
                while ((customerLine = srCustomers.ReadLine()) != null)
                {
                    if (!String.IsNullOrEmpty(customerLine))
                    {
                        string[] customerLines = customerLine.Split('|');
                        if (customerLines[1].Equals(name) && customerLines[2].Equals(password))
                        {
                            Customer customerToReturn = new Customer(int.Parse(customerLines[0]), name, password);
                            return customerToReturn;
                        }
                    }
                }
                return null;
            }
        }

        private int GetNextCustomerId()
        {
            int id = 0;
            using (StreamReader srCustomers = new StreamReader(FILEPATHCUSTOMERS))
            {
                string customerLine = "";
                while ((customerLine = srCustomers.ReadLine()) != null)
                {
                    if (!String.IsNullOrEmpty(customerLine))
                    {
                        string[] customerLines = customerLine.Split('|');
                        int customerId = int.Parse(customerLines[0]);
                        if (customerId >= id)
                        {
                            id = customerId;
                        }
                    }
                }
            }
            id += 1;
            return id;
        }
    }
}
