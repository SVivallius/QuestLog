namespace QuestLog_MVC.Models.Quests;
public class QuestViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Experience { get; set; }
    public bool Complete { get; set; } = false;
    public int? CategoryId { get; set; }
}