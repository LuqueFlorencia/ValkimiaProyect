using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tennis.Models.Entity
{
    public class RegisteredPlayer
    {
        public int TournamentId {get;set;}
        public int PlayerId { get;set;}
        public Tournament Tournament { get;set;}
        public Player Player { get;set;}
    }
    public class RegisteredPlayersConfig : IEntityTypeConfiguration<RegisteredPlayer>
    {
        public void Configure(EntityTypeBuilder<RegisteredPlayer> builder)
        {
            builder.ToTable("RegisteredPlayer");
            builder.HasKey(rp => new { rp.PlayerId, rp.TournamentId });
            builder.Property<int>(rp => rp.PlayerId)
                .HasColumnName("PlayerId")
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(rp => rp.TournamentId)
                .HasColumnName("TournamentId")
                .HasMaxLength(4)
                .IsRequired();

            builder.HasOne(p => p.Player)
                .WithMany(rp => rp.RegisteredPlayers)
                .HasForeignKey(rp => rp.PlayerId)
                .IsRequired();

            builder.HasOne(t => t.Tournament)
                .WithMany(rp => rp.RegisteredPlayers)
                .HasForeignKey(m => m.TournamentId)
                .IsRequired();
        }
    }
}
