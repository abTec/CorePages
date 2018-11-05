using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class DataSet
    {
        public int ID { get; set; }

        public string Hash { get; set; }

        public string Name { get; set; }

        [Display(Name = "Total Users")]
        public int NumberOfUsers { get; set; }

        [Display(Name = "Created Date")]
        public DateTime InseretedDateUtc { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
