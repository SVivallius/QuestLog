namespace QuestLog.Models;
public class QuestViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Experience { get; set; }
    public bool Complete { get; set; } = false;

    public override string ToString()
    {
        return $"{{\n\tId: {Id},\n\tName: {Name},\n\tDescription: {Description},\n\tExperience: {Experience},\n\tCompleted: {Complete}\n}}";
    }
}