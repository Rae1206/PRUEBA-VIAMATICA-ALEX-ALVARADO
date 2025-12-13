using Application.Extensions;
using Repository.Persistencias.Extensions;
namespace API
{

    public partial class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var Configuration = builder.Configuration;

            // Add services to the container
            var Cors = "Cors";
            builder.Services.AddInjectionInfrastructure(Configuration);
            builder.Services.AddInjectionApplication(Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddHttpContextAccessor();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: Cors,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200");
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();
                        builder.SetIsOriginAllowed(origin => true);
                    });

            });
            var app = builder.Build();
            app.UseCors(Cors);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
public partial class Program
{ }