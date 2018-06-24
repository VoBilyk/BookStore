using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class Client
    {
        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string SecondName { get; private set; }
        

        public Client(string firstName, string secondName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            SecondName = secondName;
        }

        public override string ToString()
        {
            return $"Client id: {Id}, Fullname: {FirstName} {SecondName}";
        }
    }
}
