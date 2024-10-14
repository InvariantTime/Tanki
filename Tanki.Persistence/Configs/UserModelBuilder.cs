using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tanki.Domain.Models;

namespace Tanki.Persistence.Configs
{
    class UserModelBuilder : IEntityTypeConfiguration<User>
    {
        private const int _nameMaxLength = 50;

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasAlternateKey(x => x.Name);
            builder.Property(x => x.Name).HasMaxLength(_nameMaxLength);

            builder.HasOne(x => x.Room)
                .WithOne(x => x.Host)
                .HasForeignKey<User>(x => x.RoomId);
        }
    }
}