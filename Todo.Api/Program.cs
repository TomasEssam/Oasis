using Todo.Domain.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Todo.Domain.Entities.Identity;
using Todo.Api.IServices.Identity;
using Todo.Api.Services.Identity;
using Todo.Api.Helpers;
using Todo.Domain.IPresistance;
using Todo.Domain.Presistance;
using Todo.Domain.IRepository;
using Todo.Domain.Repository;
using Todo.Domain.Entities;
using Todo.Api.Services;
using Todo.Domain.Seeds;
using Todo.Api.IServices;
namespace Todo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<TodoContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            // Add Identity services
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<TodoContext>()
                .AddDefaultTokenProviders();

            //Add DI 
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDbFactory,DbFactory>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IRepository<TodoEntity>, Repository<TodoEntity>>();
            builder.Services.AddScoped<ITodoService, TodoService>();


            var app = builder.Build();
            // Run the database seeding
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedApplication.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
