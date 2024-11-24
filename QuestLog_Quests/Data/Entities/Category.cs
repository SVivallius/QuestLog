using QuestLog_Quests.Data.Entities;

namespace Quests.Data.Entities;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Quest> Quests { get; set; }
}