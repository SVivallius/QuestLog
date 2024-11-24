using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data;
using Quests.Data.DTOs;
using Quests.Data.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quests.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly QuestLog_QuestContext _db;
    private readonly ILogger<CategoriesController> _logger;
    private readonly JsonSerializerOptions _jsonOpt;
    public CategoriesController(QuestLog_QuestContext db, ILogger<CategoriesController>logger)
    {
        _db = db;
        _logger = logger;

        _jsonOpt = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            PropertyNameCaseInsensitive = true
        };
    }
    // GET: api/<CategoriesController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        var entities = await _db.Categories.ToListAsync();
        return Results.Ok(CategoriesToDTO(entities));
    }

    // GET api/<CategoriesController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var entity = await _db.Categories
            .Where(c => c.Id.Equals(id))
            .Include(c => c.Quests)
            .FirstOrDefaultAsync();

        if (entity == null)
            return Results.NotFound();

        return Results.Ok(CategoriesToDTO(entity));
    }

    // POST api/<CategoriesController>
    [HttpPost]
    public async Task<IResult> Post(Category c)
    {
        try
        {
            _db.Categories.Add(c);

            if (await _db.SaveChangesAsync() < 1)
            {
                _logger.LogError("Unable to save to database");
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

    // ******************************************
    // TO BE IMPLEMENTED
    // ******************************************

    // PUT api/<CategoriesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CategoriesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
    private List<CategoryDTO> CategoriesToDTO(List<Category> categories)
    {
        return categories.Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name,
            Quests = (c.Quests == null ? null : c.Quests.Select(q => new QuestDTO
            {
                Id = q.Id,
                Name = q.Name,
                Description = q.Description,
                Experience = q.Experience,
                Complete = q.Complete
            }).ToList())
        }).ToList();
    }
    private CategoryDTO CategoriesToDTO(Category c)
    {
        return new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name,
            Quests = (c.Quests == null ? null : c.Quests.Select(q => new QuestDTO
            {
                Id = q.Id,
                Name = q.Name,
                Description = q.Description,
                Experience = q.Experience,
                Complete = q.Complete
            }).ToList())
        };
    }
}