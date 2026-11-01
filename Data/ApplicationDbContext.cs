using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Theatre> Theatres { get; set; }
    public DbSet<Screen> Screens { get; set; }
    public DbSet<Seat> Seats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(m => m.MovieId);

            entity.Property(m => m.Title)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(m => m.Description)
                .HasMaxLength(500);

            entity.Property(m => m.DurationMinutes)
                .IsRequired();

            entity.Property(m => m.ReleaseDate)
                .HasColumnType("date")
                .IsRequired();

            entity.Property(m => m.Language)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(m => m.Genre)
                .HasMaxLength(50)
                .IsRequired();

            entity.ToTable(table =>
            {
                table.HasCheckConstraint(
                    "CK_Movies_DurationMinutes",
                    "[DurationMinutes] > 0");
            });
        });

        modelBuilder.Entity<Theatre>(entity =>
        {
            entity.HasKey(t => t.TheatreId);

            entity.Property(t => t.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(t => t.Location)
                .HasMaxLength(200)
                .IsRequired();
        });
        // Screen configuration
        modelBuilder.Entity<Screen>(entity =>
        {
            entity.HasKey(s => s.ScreenId);

            entity.Property(s => s.ScreenName)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasOne(s => s.Theatre)
                .WithMany(t => t.Screens)
                .HasForeignKey(s => s.TheatreId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(s => new { s.TheatreId, s.ScreenName })
                .IsUnique();
        });
        // Seat configuration
        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(s => s.SeatId);

            entity.Property(s => s.RowName)
                .HasMaxLength(5)
                .IsRequired();

            entity.Property(s => s.SeatNumber)
                .IsRequired();

            entity.Property(s => s.SeatType)
                .HasMaxLength(30)
                .IsRequired();

            entity.HasOne(s => s.Screen)
                .WithMany(s => s.Seats)
                .HasForeignKey(s => s.ScreenId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.ToTable(table =>
                table.HasCheckConstraint(
                    "CK_Seats_SeatNumber",
                    "[SeatNumber] > 0"));

            entity.HasIndex(s => new
            {
                s.ScreenId,
                s.RowName,
                s.SeatNumber
            }).IsUnique();
        });
    }
}