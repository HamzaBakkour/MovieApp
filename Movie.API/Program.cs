
using Movie.API.Extensions;
using Movie.Data.DataConfigurations;
using Movie.Presentation;

namespace Movie.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureSql(builder.Configuration);



            builder.Services.AddControllers()
                            //.AddNewtonsoftJson()
                            .AddApplicationPart(typeof(AssemblyReference).Assembly);

            builder.Services.AddOpenApi();

            builder.Services.AddRepositories();
            builder.Services.AddServiceLayer();

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());
            builder.Services.ConfigureCors();

            var app = builder.Build();











            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
