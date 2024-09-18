using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<CampaignType> CampaignTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<InteractionType> InteractionTypes { get; set; }
        public DbSet<Domain.Entities.Project> Projects { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Domain.Entities.TaskStatus> TaskStatuses { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CampaignType>(entity =>
            {
                entity.ToTable("CampaignTypes");
                entity.HasKey(ct => ct.Id); //estatico

                entity.Property(ct => ct.Name)
                .HasColumnType("varchar(25)")  
                .IsRequired();
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasKey(c => c.ClientID);
                entity.Property(c => c.ClientID)
                .ValueGeneratedOnAdd();

                entity.Property(c => c.Name)
                .HasColumnType("varchar(255)")
                .IsRequired();

                entity.Property(c => c.Email)
                .HasColumnType("varchar(255)")
                .IsRequired();

                entity.Property(c => c.Phone)
                .HasColumnType("varchar(255)")
                .IsRequired();

                entity.Property(c => c.Company)
                .HasColumnType("varchar(100)")
                .IsRequired();

                entity.Property(c => c.Address)
                .HasColumnType("varchar(max)")
                .IsRequired();

                entity.Property(t => t.CreateDate)
                .IsRequired();
            });

            modelBuilder.Entity<InteractionType>(entity =>
            {
                entity.ToTable("InteractionTypes");
                entity.HasKey(it => it.Id); //estatico

                entity.Property(it => it.Name)
                .HasColumnType("nvarchar(25)")
                .IsRequired();
            });

            modelBuilder.Entity<Interaction>(entity =>
            {
                entity.ToTable("Interactions"); //guid

                entity.Property(i => i.Date)
                .IsRequired();

                entity.Property(i => i.Notes)
                .HasColumnType("varchar(max)")
                .IsRequired();

                entity.HasOne(i => i.InteractionTypes)
                .WithMany(it => it.Interactions)
                .HasForeignKey(i => i.InteractionType);
            }
            );

            modelBuilder.Entity<Domain.Entities.Task>(entity =>
            {
                entity.ToTable("Tasks"); //guid

                entity.Property(t => t.Name)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

                entity.Property(t => t.DueDate)
                .IsRequired();
                entity.Property(t => t.CreateDate)
                .IsRequired();

                entity.HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectID);
                entity.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedTo);
                entity.HasOne(t => t.TaskStatus)
                .WithMany(ts => ts.Tasks)
                .HasForeignKey(t => t.Status);
            }
            );

            modelBuilder.Entity<Domain.Entities.TaskStatus>(entity =>
            {
                entity.ToTable("TaskStatus");
                entity.HasKey(ts => ts.Id); //estatico

                entity.Property(ts => ts.Name)
                .HasColumnType("varchar(25)")
                .IsRequired();
            }
            );

            modelBuilder.Entity<Domain.Entities.Project>(entity =>
            {
                entity.ToTable("Projects"); //guid

                entity.Property(p => p.ProjectName)
                .HasColumnType("varchar(255)")
                .IsRequired();

                entity.Property(p => p.StartDate)
                .IsRequired();
                entity.Property(p => p.EndDate)
                .IsRequired();
                entity.Property(t => t.CreateDate)
                .IsRequired();

                entity.HasOne(p => p.CampaignTypes)
                .WithMany(ct => ct.Projects)
                .HasForeignKey(p => p.CampaignType);
                entity.HasOne(p => p.Client)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ClientID);
                entity.HasMany(p => p.Interactions)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectID);
            }
            );

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.UserID); //estatico

                entity.Property(u => u.Name)
                .HasColumnType("nvarchar(225)")
                .IsRequired();

                entity.Property(u => u.Email)
                .HasColumnType("nvarchar(225)")
                .IsRequired();
            }
           );
          PreloadedData.Preload(modelBuilder);
        }
    }
}