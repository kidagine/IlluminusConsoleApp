namespace Illuminus.Core.Entity
{
    public class Customer
    {
        public Customer(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
