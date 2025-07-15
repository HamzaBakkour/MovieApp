using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Movie.Data.DataConfigurations;

public class MovieConfiguration : IEntityTypeConfiguration<Core.DomainEntities.Movie>
{
    public void Configure(EntityTypeBuilder<Core.DomainEntities.Movie> builder)
    {
        builder.HasOne(m => m.Detailes)
                .WithOne(d => d.Movie)
                .HasForeignKey<Core.DomainEntities.MovieDetailes>(d => d.MovieId);

        builder.Property(m => m.Title)
            .HasMaxLength(150);

    }
}
