using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem
{

    public class Person
    {
        public int PersonId { get; }
        public string FullName { get; }
        public string Phone { get; }

        protected Person(int personId, string fullName, string phone)
        {
            if (personId <= 0)
                throw new ArgumentException("Person ID must be greater than zero.", nameof(personId));

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty.", nameof(phone));

            PersonId = personId;
            FullName = fullName;
            Phone = phone;
        }
    }
}
