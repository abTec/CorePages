namespace Domain
{
    public class UserFriendship
    {
        public int DataSetID { get; set; }

        public int UserID { get; set; }

        public int FriendId { get; set; }
        public virtual User User { get; set; }
    }
}
