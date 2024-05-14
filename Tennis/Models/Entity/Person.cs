using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Tennis.Models.Entity
{
    public class Person
    {
        public int IdPerson { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Player ? Player { get; set; }
        public User ? User { get; set; }
    }
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");
            builder.HasKey(p => p.IdPerson);
            builder.Property(x => x.IdPerson)
                .HasColumnName("IdPerson")
                .ValueGeneratedOnAdd()
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(50)
                .IsRequired();

            //builder.HasData(new Person
            //{
            //    IdPerson = 1,
            //    FirstName = "Gustavo",
            //    LastName = "Lucci"
            //},
            //new Person
            //{
            //    IdPerson = 2,
            //    FirstName = "Facundo",
            //    LastName = "Villalobo"
            //},
            //new Person
            //{
            //    IdPerson = 3,
            //    FirstName = "Matias",
            //    LastName = "Corredera"
            //},
            //new Person
            //{
            //    IdPerson = 4,
            //    FirstName = "Lautaro",
            //    LastName = "De Simeone"
            //},
            //new Person
            //{
            //    IdPerson = 5,
            //    FirstName = "Carlos",
            //    LastName = "Palladino"
            //},
            //new Person
            //{
            //    IdPerson = 6,
            //    FirstName = "Emiliano",
            //    LastName = "Caballero"
            //},
            //new Person
            //{
            //    IdPerson = 7,
            //    FirstName = "Nicolas",
            //    LastName = "Jimenez"
            //},
            //new Person
            //{
            //    IdPerson = 8,
            //    FirstName = "Nicolas",
            //    LastName = "Silva"
            //});
        }
    }
}
