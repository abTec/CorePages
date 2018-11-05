using System;
using System.Collections.Generic;

namespace Domain
{
    public class DataSet
    {
        public int ID { get; set; }

        public string Hash { get; set; }

        public string Name { get; set; }

        public int NumberOfUsers { get; set; }

        public DateTime InseretedDateUtc { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
