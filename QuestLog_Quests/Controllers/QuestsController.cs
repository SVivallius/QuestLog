using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data;
using QuestLog_Quests.Data.Entities;
using System.Text.Json;
namespace QuestLog_Quests.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestsController : ControllerBase
{
    private readonly QuestLog_QuestContext _db;
    private readonly ILogger<QuestsController> _logger;
    public QuestsController(QuestLog_QuestContext db, ILogger<QuestsController> logger)
    {
        _db = db;
        _logger = logger;
    }

    // GET: api/<QuestsController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        var entities = await _db.Quests.ToListAsync();
        return Results.Ok(entities);
    }

    // GET api/<QuestsController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var entity = await _db.Quests
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        if (entity == null)
            return Results.NotFound();
        return Results.Ok(entity);
    }

    // POST api/<QuestsController>
    [HttpPost]
    public async Task<IResult> Post(Quest payload)
    {
        try
        {
            //var quest = JsonSerializer.Deserialize<Quest>(payload);
            var result = _db.Quests
                .Add(payload);

            if (await _db.SaveChangesAsync() < 1)
            {
                _logger.LogError("Unable to save to database.");
                return Results.StatusCode(500);
            }
            return Results.Created();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return Results.StatusCode(500);
        }
    }

    // PUT api/<QuestsController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, Quest payload)
    {
        try
        {
            var entity = await _db.Quests
                .AnyAsync(e => e.Id.Equals(id));

            if (entity == false)
                return Results.NotFound();

            _db.Update(payload);
            if (await _db.SaveChangesAsync() < 1)
            {
                _logger.LogError("Unable to save to database.");
                return Results.StatusCode(500);
            }

            return Results.NoContent();    
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return Results.StatusCode(500);
        }
    }

    // DELETE api/<QuestsController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            var entity = await _db.Quests
                .Where(e => e.Id.Equals(id))
                .FirstOrDefaultAsync();

            if (entity == null)
                return Results.NotFound();

            _db.Remove(entity);
            if (await _db.SaveChangesAsync() < 1)
            {
                _logger.LogError("Unable to save changes to database.");
                return Results.StatusCode(500);
            }

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return Results.StatusCode(500);
        }
    }
}
