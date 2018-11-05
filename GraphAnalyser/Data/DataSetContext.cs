using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataSetContext : DbContext
    {
        public DataSetContext (DbContextOptions<DataSetContext> options)
            : base(options)
        {
        }

        public DbSet<DataSet> DataSet { get; set; }
        public DbSet<UserFriendship> UserFriendship { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.ID, x.DataSetID });

            modelBuilder.Entity<UserFriendship>()
                .HasKey(x => new { x.DataSetID, x.UserID, x.FriendId });

            modelBuilder.Entity<UserFriendship>()
                .HasOne(x => x.User)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => new { x.UserID, x.DataSetID });
        }
    }
}
