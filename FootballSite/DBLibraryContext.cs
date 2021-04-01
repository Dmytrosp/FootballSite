using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FootballSite
{
    public partial class DBLibraryContext : DbContext
    {
        public DBLibraryContext()
        {
        }

        public DBLibraryContext(DbContextOptions<DBLibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<ClubMatch> ClubMatches { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamMatch> TeamMatches { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-3C96GSB; Database=DBLibrary; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Ukrainian_CI_AS");

            modelBuilder.Entity<Club>(entity =>
            {
                entity.Property(e => e.ClubName).HasMaxLength(50);

                entity.Property(e => e.CoachDateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.CoachFirstName).HasMaxLength(50);

                entity.Property(e => e.CoachLastName).HasMaxLength(50);

                entity.Property(e => e.StadiumName).HasMaxLength(50);
            });

            modelBuilder.Entity<ClubMatch>(entity =>
            {
                entity.HasKey(e => new { e.MatchId, e.FirstClubId, e.SecondClubId });

                entity.Property(e => e.MatchId).ValueGeneratedOnAdd();

                entity.Property(e => e.MatchDate).HasColumnType("datetime");

                entity.Property(e => e.MatchName).HasMaxLength(50);

                entity.Property(e => e.MatchResult).HasMaxLength(50);

                entity.HasOne(d => d.FirstClub)
                    .WithMany(p => p.ClubMatchFirstClubs)
                    .HasForeignKey(d => d.FirstClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClubMatches_ClubMatches");

                entity.HasOne(d => d.SecondClub)
                    .WithMany(p => p.ClubMatchSecondClubs)
                    .HasForeignKey(d => d.SecondClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClubMatches_Clubs");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Players)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(d => d.ClubId);



                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.CountryId);



                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(d => d.TeamId);
                  
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.CoachDateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.CoachFirstName).HasMaxLength(50);

                entity.Property(e => e.CoachLastName).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Teams_Countries");
            });

            modelBuilder.Entity<TeamMatch>(entity =>
            {
                entity.HasKey(e => new { e.MatchId, e.FirstTeamId, e.SecondTeamId })
                    .HasName("PK_Matches_1");

                entity.Property(e => e.MatchId).ValueGeneratedOnAdd();

                entity.Property(e => e.MatchDate).HasColumnType("datetime");

                entity.Property(e => e.MatchName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MatchResult).HasMaxLength(50);

                entity.HasOne(d => d.FirstTeam)
                    .WithMany(p => p.TeamMatchFirstTeams)
                    .HasForeignKey(d => d.FirstTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamMatches_TeamMatches");

                entity.HasOne(d => d.SecondTeam)
                    .WithMany(p => p.TeamMatchSecondTeams)
                    .HasForeignKey(d => d.SecondTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamMatches_Teams");
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.HasKey(e => new { e.TransferId, e.SellerId, e.BuyerId });

                entity.Property(e => e.TransferId).ValueGeneratedOnAdd();

                entity.Property(e => e.CostOfPlayer).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.TransferBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfers_Clubs1");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Transfers)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_Transfers_Players");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.TransferSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfers_Clubs");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
