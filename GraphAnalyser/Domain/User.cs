using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int DataSetID { get; set; }

        public string Name { get; set; }

        public int NumberOfFriends { get; set; }

        public ICollection<UserFriendship> Friends { get; set; }
    }
}
