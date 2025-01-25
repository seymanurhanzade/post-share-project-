using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using react.server.DataManagement.server.entitys;
using server.entitys;

namespace react.server.ApplicationContext
{
    public class IdentityContext: IdentityDbContext<Users>
    {
        public IdentityContext() { }
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options){
            
        }
        public DbSet<InstantShare> InstantShares { get;set;}
        public DbSet<DeletePost> DeletePost { get;set;}
        public DbSet<LikesTable> LikesTables { get;set;}
        public DbSet<PostCommunt> PostCommunts { get;set;}

        //****
        public DbSet<FollowedTable> FollowedTable { get; set; }
        //****

        public DbSet<FollowerTable> FollowerTable { get; set; }
        public DbSet<DeleteFollowerTable> DeleteFollowerTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // LikesTable -> InstantShare ilişkisi
            modelBuilder.Entity<LikesTable>()
                .HasOne(l => l.PostTable)
                .WithMany()
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            // LikesTable -> Users ilişkisi
            modelBuilder.Entity<LikesTable>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //PostCommunt -> InstantShare ilişkisi
            modelBuilder.Entity<PostCommunt>()
                .HasOne(l=>l.PostTable)
                .WithMany()
                .HasForeignKey(l=>l.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostCommunt>()
                .HasOne(l=>l.User)
                .WithMany()
                .HasForeignKey(l=>l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FollowedTable>()
                .HasOne(i=>i.User)
                .WithMany()
                .HasForeignKey(l=>l.FollowedUserId)
                .HasForeignKey(l=>l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FollowerTable>()
                .HasOne(i=>i.User)
                .WithMany()
                .HasForeignKey(l=>l.FollowerUserId)
                .HasForeignKey(l=>l.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}