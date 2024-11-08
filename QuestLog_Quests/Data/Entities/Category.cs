using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quests.Data.Entities;

[Owned]
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Quest> Quests { get; set; }
}
