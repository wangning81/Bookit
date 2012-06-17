using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Bookit.Domain;

namespace Bookit.Data
{
    public class BookitDB : DbContext
    {
        public DbSet<MapNode> MapNodes { get; set; }
        public DbSet<MapPath> MapPathes { get; set; }
        public DbSet<Cube> Cubes { get; set; }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MapNode>().HasKey(x => x.Id);
            modelBuilder.Entity<MapNode>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<MeetingRoom>().ToTable("MeetingRoom");
        }
    }
}
