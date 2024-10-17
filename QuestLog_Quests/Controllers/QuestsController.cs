using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data;
using QuestLog_Quests.Data.Entities;
namespace QuestLog_Quests.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestsController : ControllerBase
{
    private readonly QuestLog_QuestContext _db;
    public QuestsController(QuestLog_QuestContext db)
    {
        _db = db;
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
    public async Task<IResult> Post([FromBody] Quest quest)
    {
        var result = _db.Quests
            .Add(quest);

        return Results.Created();
    }

    // PUT api/<QuestsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<QuestsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
