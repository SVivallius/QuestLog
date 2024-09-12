namespace QuestLog.Models;
public class Quest
{
    public string name { get; set; }
    public string description { get; set; }
    public int experience { get; set; }
    public bool complete { get; set; } = false;
}
