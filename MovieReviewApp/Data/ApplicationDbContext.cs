using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Models;

namespace MovieReviewApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActorMovie>()
                .HasKey(am => new { am.ActorId, am.MovieId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(a => a.Actor)
                .WithMany(am => am.Cast)
                .HasForeignKey(a => a.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(m => m.Movie)
                .WithMany(am => am.Cast)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<Review>()
                .HasKey(r => new { r.UserId, r.MovieId });

            modelBuilder.Entity<Review>()
               .HasOne(m => m.Movie)
               .WithMany(r => r.Reviews)
               .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<Review>()
                .HasOne(u => u.User)
                .WithMany(r => r.Reviews)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<MovieGenre>()
                 .HasKey(mg => new { mg.GenreId, mg.MovieId });

            modelBuilder.Entity<MovieGenre>()
               .HasOne(m => m.Movie)
               .WithMany(mg => mg.Genres)
               .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(g => g.Genre)
                .WithMany(mg => mg.Movies)
                .HasForeignKey(g => g.GenreId);
        }
    }
}
