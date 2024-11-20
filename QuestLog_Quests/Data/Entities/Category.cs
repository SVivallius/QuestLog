using QuestLog_Quests.Data.Entities;
using System.Text.Json.Serialization;

namespace Quests.Data.Entities;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    //[JsonIgnore]
    public virtual ICollection<Quest> Quests { get; set; }
}