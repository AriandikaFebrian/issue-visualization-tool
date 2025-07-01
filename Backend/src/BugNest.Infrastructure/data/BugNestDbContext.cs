using Microsoft.EntityFrameworkCore;
using BugNest.Domain.Entities;

namespace BugNest.Infrastructure.Data;

public class BugNestDbContext : DbContext
{
    public BugNestDbContext(DbContextOptions<BugNestDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
    public DbSet<Issue> Issues => Set<Issue>();
    public DbSet<IssueHistory> IssueHistories => Set<IssueHistory>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<IssueTag> IssueTags => Set<IssueTag>();
    public DbSet<AssignedUser> AssignedUsers => Set<AssignedUser>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
    public DbSet<RecentProject> RecentProjects { get; set; }
    public DbSet<RecentProjectAccess> RecentProjectAccesses { get; set; }
    public DbSet<Notification> Notifications { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IssueTag>().HasKey(it => new { it.IssueId, it.TagId });
        modelBuilder.Entity<AssignedUser>().HasKey(au => new { au.IssueId, au.UserId });
        modelBuilder.Entity<ProjectMember>().HasKey(pm => new { pm.ProjectId, pm.UserId });

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Creator)
            .WithMany()
            .HasForeignKey(i => i.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IssueHistory>()
            .HasOne(h => h.Issue)
            .WithMany(i => i.History)
            .HasForeignKey(h => h.IssueId);

        modelBuilder.Entity<IssueHistory>()
            .HasOne(h => h.ChangedByUser)
            .WithMany()
            .HasForeignKey(h => h.ChangedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Issue)
            .WithMany(i => i.Comments)
            .HasForeignKey(c => c.IssueId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<ActivityLog>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<ActivityLog>()
            .HasOne(a => a.Project)
            .WithMany()
            .HasForeignKey(a => a.ProjectId);

        modelBuilder.Entity<User>()
       .Property(u => u.Role)
       .HasConversion<string>();

        modelBuilder.Entity<Issue>()
        .Property(u => u.Status)
        .HasConversion<string>();

        modelBuilder.Entity<Issue>()
        .Property(u => u.Priority)
        .HasConversion<string>();

        modelBuilder.Entity<ActivityLog>()
       .Property(u => u.Action)
       .HasConversion<string>();


        modelBuilder.Entity<User>()
            .Property(u => u.Department)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.Position)
            .HasConversion<string>();

        modelBuilder.Entity<Project>()
    .Property(p => p.Status)
    .HasConversion<string>();

        modelBuilder.Entity<Project>()
            .Property(p => p.Visibility)
            .HasConversion<string>();

        modelBuilder.Entity<Tag>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tags)
            .HasForeignKey(t => t.ProjectCode)
            .HasPrincipalKey(p => p.ProjectCode);



        modelBuilder.Entity<RecentProject>()
            .HasIndex(r => new { r.NRP, r.ProjectCode })
            .IsUnique();


        modelBuilder.Entity<RecentProjectAccess>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<RecentProjectAccess>()
            .HasOne(r => r.Project)
            .WithMany()
            .HasForeignKey(r => r.ProjectCode)
            .HasPrincipalKey(p => p.ProjectCode);

        base.OnModelCreating(modelBuilder);
    }
}
