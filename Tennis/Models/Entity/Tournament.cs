﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tennis.Helpers;

namespace Tennis.Models.Entity
{
    public class Tournament
    {
        public int IdTournament { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int Capacity { get; set; }   
        public int Prize { get; set; }
        public int ? WinnerId { get; set; }
        public ICollection<Match> ? Matches { get; set; } 
        public ICollection<RegisteredPlayer> ? RegisteredPlayers {  get; set; } 
    }
    public class TournamentConfig : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("Tournament");
            builder.HasKey(t => t.IdTournament);
            builder.Property(t => t.IdTournament)
                .HasColumnName("IdTournament")
                .ValueGeneratedOnAdd()
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Gender)
                .HasColumnName("Gender")
                .IsRequired();
            builder.Property(t => t.StartDate)
                .HasColumnName("StartDate")
                .IsRequired();
            builder.Property(t => t.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();
            builder.Property(t => t.Capacity)
                .HasColumnName("Capacity")
                .HasMaxLength(3)
                .IsRequired();
            builder.Property(t => t.Prize)
                .HasColumnName("Prize")
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(t => t.WinnerId)
                .HasColumnName("WinnerId");

            builder.HasMany(t => t.Matches)
                .WithOne(m => m.Tournament);

            builder.HasMany(t => t.RegisteredPlayers)
                .WithOne(rp => rp.Tournament);

            //builder.HasData(new Tournament
            //{
            //    IdTournament = 1,
            //    Name = "MaleTennisTour",
            //    Gender = Gender.Male,
            //    StartDate = new DateOnly(2024, 05, 01),
            //    EndDate = new DateOnly(2024, 05, 07),
            //    Capacity = 8,
            //    Prize = 100000,
            //    WinnerId = null
            //},
            //new Tournament
            //{
            //    IdTournament = 2,
            //    Name = "FemaleTennisTour",
            //    Gender = Gender.Female,
            //    StartDate = new DateOnly(2024, 05, 01),
            //    EndDate = new DateOnly(2024, 05, 07),
            //    Capacity = 8,
            //    Prize = 100000,
            //    WinnerId = null
            //});
        }
    }   

}