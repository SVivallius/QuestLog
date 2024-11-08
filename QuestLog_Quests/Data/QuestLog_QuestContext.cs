using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data.Entities;

namespace QuestLog_Quests.Data;
public class QuestLog_QuestContext : DbContext
{
    public DbSet<Quest> Quests => Set<Quest>();

    public QuestLog_QuestContext(DbContextOptions<QuestLog_QuestContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
