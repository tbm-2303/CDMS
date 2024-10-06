using ChemicalDepotManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ChemicalDepotManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure the in-memory database context
            builder.Services.AddDbContext<DepotContext>(options =>
                options.UseInMemoryDatabase("DepotDB"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DepotContext>();
                SeedDatabase(context);
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

        // Seed the database with initial data
        private static void SeedDatabase(DepotContext context)
        {
            if (context.Warehouses.Any() || context.Chemicals.Any() || context.Jobs.Any())
            {
                return; // Database has been seeded
            }

            // Seed warehouses
            var warehouse1 = new Warehouse { Id = 1, Capacity = 10 };
            var warehouse2 = new Warehouse { Id = 2, Capacity = 12 };
            context.Warehouses.AddRange(warehouse1, warehouse2);

            // Seed jobs
            var job1 = new Job { Id = 1, Description = "Job for Chemical A", Status = "Pending" };
            var job2 = new Job { Id = 2, Description = "Job for Chemical B and C", Status = "Confirmed" };

            // Seed chemicals with warehouse association
            var chemicalA = new Chemical { Id = 1, Name = "Chemical A", Class = "A", Quantity = 5, Job = job1, Warehouse = warehouse1 };
            var chemicalB = new Chemical { Id = 2, Name = "Chemical B", Class = "B", Quantity = 10, Job = job2, Warehouse = warehouse2 };
            var chemicalC = new Chemical { Id = 3, Name = "Chemical C", Class = "C", Quantity = 8, Job = job2, Warehouse = warehouse1 };

            // Add jobs and their chemicals to the context
            context.Jobs.AddRange(job1, job2);
            context.Chemicals.AddRange(chemicalA, chemicalB, chemicalC);

            context.SaveChanges();
        }


    }
}
