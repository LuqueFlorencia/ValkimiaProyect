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

            //// Male Players
            //builder.HasData(new Player
            //{
            //    IdPlayer = 1,
            //    IdPerson = 1,
            //    Gender = Gender.Male,
            //    Hand = Hand.Left,
            //    Strength = 50,
            //    Speed = 40,
            //    ReactionTime = 40,
            //},
            //new Player
            //{
            //    IdPlayer = 2,
            //    IdPerson = 2,
            //    Gender = Gender.Male,
            //    Hand = Hand.Right,
            //    Strength = 55,
            //    Speed = 55,
            //    ReactionTime = 30,
            //},
            //new Player
            //{
            //    IdPlayer = 3,
            //    IdPerson = 3,
            //    Gender = Gender.Male,
            //    Hand = Hand.Left,
            //    Strength = 55,
            //    Speed = 40,
            //    ReactionTime = 40,
            //},
            //new Player
            //{
            //    IdPlayer = 4,
            //    IdPerson = 4,
            //    Gender = Gender.Male,
            //    Hand = Hand.Right,
            //    Strength = 60,
            //    Speed = 50,
            //    ReactionTime = 35,
            //},
            //new Player
            //{
            //    IdPlayer = 5,
            //    IdPerson = 5,
            //    Gender = Gender.Male,
            //    Hand = Hand.Right,
            //    Strength = 45,
            //    Speed = 45,
            //    ReactionTime = 45,
            //},
            //new Player
            //{
            //    IdPlayer = 6,
            //    IdPerson = 6,
            //    Gender = Gender.Male,
            //    Hand = Hand.Right,
            //    Strength = 60,
            //    Speed = 40,
            //    ReactionTime = 45,
            //},
            //new Player
            //{
            //    IdPlayer = 7,
            //    IdPerson = 7,
            //    Gender = Gender.Male,
            //    Hand = Hand.Left,
            //    Strength = 50,
            //    Speed = 50,
            //    ReactionTime = 35,
            //},
            //new Player
            //{
            //    IdPlayer = 8,
            //    IdPerson = 8,
            //    Gender = Gender.Male,
            //    Hand = Hand.Right,
            //    Strength = 45,
            //    Speed = 50,
            //    ReactionTime = 45,
            //});
        }
    }
}