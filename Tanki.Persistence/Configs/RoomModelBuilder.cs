using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tanki.Domain.Models;

namespace Tanki.Persistence.Configs
{
    public class RoomModelBuilder : IEntityTypeConfiguration<Room>
    {
        private const int maxNameLength = 50;

        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(maxNameLength)
                .IsRequired();

            builder.HasAlternateKey(x => x.Name);
        }
    }
}