using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data;

namespace QuestLog_Quests;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders().AddConsole();

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Setup SQL connection
        var connString = builder.Configuration.GetValue<string>("ConnectionString");
        builder.Services.AddDbContext<QuestLog_QuestContext>(opt => opt.UseMySql(connString, ServerVersion.AutoDetect(connString)));

        var app = builder.Build();

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
