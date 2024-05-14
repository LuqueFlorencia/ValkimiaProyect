using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tennis.Models.Entity
{
    public class Match
    {
        public int IdMatch { get; set; }
        public int TournamentId { get; set; }
        public DateOnly Date { get; set; }
        public int MatchType { get; set; } //8vo, 4to, Semi, Final [4,3,2,1]
        public int IdPlayer1 { get; set; }
        public int IdPlayer2 { get; set; }
        public int ? WinnerId { get; set; }
        public virtual Tournament Tournament { get; set; }
        public virtual Player Player1 { get; set; }
        public virtual Player Player2 { get; set; }
        public virtual Player? PlayerWinner { get; set; }
    }
    public class MatchConfig : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Match");
            builder.HasKey(pm => new { pm.IdPlayer1, pm.IdPlayer2, pm.IdMatch });
            builder.Property(m => m.IdMatch)
                .HasColumnName("IdMatch")
                .ValueGeneratedOnAdd()
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(x => x.IdPlayer1)
                .HasColumnName("IdPlayer1")
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(x => x.IdPlayer2)
                .HasColumnName("IdPlayer2")
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(m => m.TournamentId)
                .HasColumnName("IdTournament")
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(m => m.Date)
                .HasColumnName("Date")
                .IsRequired();
            builder.Property(m => m.MatchType)
                .HasColumnName("MatchType")
                .HasMaxLength(1)
                .IsRequired();
            builder.Property(x => x.WinnerId)
                .HasColumnName("WinnerId");

            builder.HasOne(pm => pm.Player1)
                .WithMany(p => p.MatchesAsP1)
                .HasForeignKey(pm => pm.IdPlayer1)
                .HasConstraintName("FK_Match_Player1")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pm => pm.Player2)
                .WithMany(p => p.MatchesAsP2)
                .HasForeignKey(pm => pm.IdPlayer2)
                .HasConstraintName("FK_Match_Player2")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pm => pm.PlayerWinner)
                .WithMany(p => p.MatchesWinner)
                .HasForeignKey(pm => pm.WinnerId)
                .HasConstraintName("FK_Match_Player");

            builder.HasOne(m => m.Tournament)
                .WithMany(t => t.Matches)
                .HasForeignKey(m => m.TournamentId);
 
        }
    }
}
