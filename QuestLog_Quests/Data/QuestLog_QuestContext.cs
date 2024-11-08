using Microsoft.EntityFrameworkCore;
using QuestLog_Quests.Data.Entities;
using Quests.Data.Entities;

namespace QuestLog_Quests.Data;
public class QuestLog_QuestContext : DbContext
{
    public DbSet<Quest> Quests => Set<Quest>();
    public DbSet<Category> Categories => Set<Category>();

    public QuestLog_QuestContext(DbContextOptions<QuestLog_QuestContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // PK's
        modelBuilder.Entity<Quest>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Category>()
            .HasKey(e => e.Id);

        // Relations
        modelBuilder.Entity<Quest>()
            .HasOne(q => q.Category)
            .WithMany(c => c.Quests)
            .HasForeignKey(q => q.CategoryId);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Quests)
            .WithOne(q => q.Category);
    }
}
