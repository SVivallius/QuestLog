using QuestLog_Quests.Data.Entities;

namespace Quests.Data.DTOs;
public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<QuestDTO>? Quests { get; set; }
}
