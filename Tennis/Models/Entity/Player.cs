using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tennis.Helpers;

namespace Tennis.Models.Entity
{
    public class Player
    {   
        public int IdPlayer { get; set; }
        public int IdPerson { get; set; }
        public Gender Gender {get; set; }
        public Hand Hand { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int ReactionTime { get; set; }
        public double AbilityLevel
        {
            get
            {
                return ((Strength + Speed + ReactionTime) / 3);
            }
        }
        public Person Person { get; set; } = null!;
        public Tournament Tournament { get; set; }
        public ICollection<RegisteredPlayer> ? RegisteredPlayers { get; set; }
        public virtual ICollection<Match> ? MatchesWinner { get; set; }
        public virtual ICollection<Match> ? MatchesAsP1 { get; set; }
        public virtual ICollection<Match> ? MatchesAsP2 { get; set; }
    }
    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Player");
            builder.HasKey(x => x.IdPlayer);
            builder.Property(x => x.IdPlayer)
                .HasColumnName("IdPlayer")
                .ValueGeneratedOnAdd()
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(x => x.IdPerson)
                .HasColumnName("IdPerson")
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(x => x.Gender)
                .HasColumnName("Gender")
                .IsRequired();
            builder.Property(x => x.Hand)
                .HasColumnName("Hand")
                .IsRequired();
            builder.Property(x => x.Strength)
                .HasColumnName("Strength")
                .HasMaxLength(2)
                .IsRequired();
            builder.Property(x => x.Speed)
                .HasColumnName("Speed")
                .HasMaxLength(2)
                .IsRequired();
            builder.Property(x => x.ReactionTime)
                .HasColumnName("ReactionTime")
                .HasMaxLength(2)
                .IsRequired();

            builder.HasMany(p => p.MatchesAsP1)
                .WithOne(p => p.Player1);

            builder.HasMany(p => p.MatchesAsP2)
                .WithOne(p => p.Player2);

            builder.HasOne(p => p.Person)
                .WithOne(p => p.Player)
                .HasForeignKey<Player>(p => p.IdPerson)
                .IsRequired();
        }
    }
}