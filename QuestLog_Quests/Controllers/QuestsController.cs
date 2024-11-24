using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data;
using QuestLog_Quests.Data.Entities;
using Quests.Data.DTOs;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace QuestLog_Quests.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestsController : ControllerBase
{
    private readonly QuestLog_QuestContext _db;
    private readonly ILogger<QuestsController> _logger;
    private readonly JsonSerializerOptions _jsonOpt;
    public QuestsController(QuestLog_QuestContext db, ILogger<QuestsController> logger)
    {
        _db = db;
        _logger = logger;

        _jsonOpt = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            PropertyNameCaseInsensitive = true
        };
    }

    // GET: api/<QuestsController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        var entities = await _db.Quests.Include(q => q.Category).ToListAsync();
        if (entities.Count == 0) return Results.Ok(new List<QuestDTO>());
        return Results.Ok(QuestsToDTO(entities));
    }

    // GET api/<QuestsController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var entity = await _db.Quests
            .Include(q => q.Category)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        if (entity == null)
            return Results.NotFound();

        return Results.Ok(QuestsToDTO(entity));
    }

    [HttpPost]
    public async Task<IResult> Post(string payload)
    {
        try
        {
            var quest = JsonSerializer.Deserialize<Quest>(payload, _jsonOpt);
            var result = _db.Quests
                .Add(quest);

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

    private List<QuestDTO> QuestsToDTO(List<Quest> entities)
    {
        return entities.Select(e => new QuestDTO
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Experience = e.Experience,
            Complete = e.Complete,
            Category = new CategoryDTO
            {
                Id = e.Category.Id,
                Name = e.Category.Name,
            }
        }).ToList();
    }
    private QuestDTO QuestsToDTO(Quest entity)
    {
        return new QuestDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Experience = entity.Experience,
            Complete = entity.Complete,
            Category = new CategoryDTO
            {
                Id = entity.Category.Id,
                Name = entity.Category.Name,
            }
        };
    }
}
