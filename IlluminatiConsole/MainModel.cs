using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlluminatiConsole
{
    class MainModel
    {
        private readonly string FILEPATHCUSTOMERS = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\CustomersText.txt";
        private static MainModel instance = null;
        private List<Customer> customersList = new List<Customer>();


        public static MainModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainModel();
                }
                return instance;
            }
        }

        public void LoadCustomer(Customer customer)
        {
            foreach (Customer c in customersList)
            {
                if (c.Id.ToString().Equals(customer.Id))
                {
                    customersList.Remove(c);
                }
            }
            customersList.Add(customer);
        }

        public void AddCustomer(string name, string password)
        {
            Customer customer = new Customer(GetNextId(), name, password);
            string customerLine = customer.Id.ToString() + "|" + customer.Name + "|" + customer.Password;
            File.AppendAllText(FILEPATHCUSTOMERS, customerLine + Environment.NewLine);
        }

        private int GetNextId()
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
