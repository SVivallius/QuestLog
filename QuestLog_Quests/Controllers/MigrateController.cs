using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data;
namespace QuestLog_Quests.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MigrateController : ControllerBase
{
    private readonly QuestLog_QuestContext _db;
    private readonly ILogger<MigrateController> _logger;
    public MigrateController(QuestLog_QuestContext db, ILogger<MigrateController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        _logger.LogInformation($"Recieved request from: {Request.HttpContext.Connection.RemoteIpAddress}");

        var pending = await _db.Database.GetPendingMigrationsAsync();
        if (pending.Count() < 1)
            return Results.Ok("No pending migrations");

        await _db.Database.BeginTransactionAsync();
        try
        {
            await _db.Database.MigrateAsync();
            await _db.Database.CommitTransactionAsync();
            _logger.LogInformation("Pending migrations applied.");
            return Results.Ok("Pending migrations applied.");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            await _db.Database.RollbackTransactionAsync();
            return Results.StatusCode(500);
        }
    }
}
