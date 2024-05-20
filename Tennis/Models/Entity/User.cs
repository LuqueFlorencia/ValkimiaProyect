using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Tennis.Models.Entity
{
    public class User
    {
        public int IdUser { get; set; }
        public int IdPerson { get; set; }
        public string Rol {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ? RefreshToken { get; set; }
        public DateTime ? RefreshTokenExpiration { get; set; }
        public Person Person { get; set; } = null!;
    }
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.IdUser);
            builder.Property(x => x.IdUser)
                .HasColumnName("IdUser")
                .ValueGeneratedOnAdd()
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(x => x.IdPerson)
                .HasColumnName("IdPerson")
                .HasMaxLength(4)
                .IsRequired();
            builder.Property(x => x.Rol)
                .HasColumnName("Rol")
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(x => x.UserName)
                .HasColumnName("Username")
                .HasMaxLength (20)
                .IsRequired();
            builder.Property(x => x.Password)
                .HasColumnName("Password")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(x => x.RefreshToken)
                .HasColumnName("RefreshToken");
            builder.Property(x => x.RefreshTokenExpiration)
                .HasColumnName("RefreshTokenExpiration");

            builder.HasOne(p => p.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(p => p.IdPerson)
                .IsRequired();
        }
    }
}