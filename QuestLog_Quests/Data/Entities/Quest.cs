﻿namespace QuestLog_Quests.Data.Entities;
public class Quest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Experience { get; set; }
    public bool Complete { get; set; }
}