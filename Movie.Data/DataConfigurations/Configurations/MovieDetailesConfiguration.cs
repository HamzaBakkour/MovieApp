using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movie.Data.DataConfigurations;


public class MovieDetailesConfiguration : IEntityTypeConfiguration<Core.DomainEntities.MovieDetailes>
{
    public void Configure(EntityTypeBuilder<Core.DomainEntities.MovieDetailes> builder)
    {

        builder.HasIndex(d => d.MovieId)
                .IsUnique(); //This ensures one-to-one
    }
}
