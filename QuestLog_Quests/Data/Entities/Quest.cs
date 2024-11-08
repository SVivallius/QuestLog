using Quests.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace QuestLog_Quests.Data.Entities;
public class Quest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Experience { get; set; }
    public bool Complete { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    [AllowNull]
    public int? CategoryId { get; set; }
}
