namespace DEMO.API.SQL.Integrations.Models
{
    using Microsoft.EntityFrameworkCore;

    public partial class SqlDbDemoContext : DbContext
    {
        public SqlDbDemoContext()
        {
        }

        public SqlDbDemoContext(DbContextOptions<SqlDbDemoContext> options)
            : base(options)
        {
        }


        public virtual DbSet<IntegrationCatJob> IntegrationCatJobs { get; set; }

        public virtual DbSet<IntegrationCatalog> IntegrationCatalogs { get; set; }

        public virtual DbSet<IntegrationInputLog> IntegrationInputLogs { get; set; }

        public virtual DbSet<IntegrationOperation> IntegrationOperations { get; set; }

        public virtual DbSet<IntegrationOutputLog> IntegrationOutputLogs { get; set; }

        public virtual DbSet<IntegrationProcess> IntegrationProcesses { get; set; }

        public virtual DbSet<IntegrationSystem> IntegrationSystems { get; set; }

        public virtual DbSet<IntegrationTrigger> IntegrationTriggers { get; set; }

        public virtual DbSet<IntegrationType> IntegrationTypes { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<IntegrationCatJob>(entity =>
            {
                entity.ToTable("IntegrationCatJob", "DEMO");

                entity.Property(e => e.ChangeDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IntegrationCatalog).WithMany(p => p.IntegrationCatJobs)
                    .HasForeignKey(d => d.IntegrationCatalogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntegrationCatJob_IntegrationCatalog");
            });

            modelBuilder.Entity<IntegrationCatalog>(entity =>
            {
                entity.ToTable("IntegrationCatalog", "DEMO");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Path).HasMaxLength(100);

                entity.HasOne(d => d.IntegrationProcess).WithMany(p => p.IntegrationCatalogs)
                    .HasForeignKey(d => d.IntegrationProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntegrationCatalog_IntegrationProcess");

                entity.HasOne(d => d.IntegrationSystem).WithMany(p => p.IntegrationCatalogs)
                    .HasForeignKey(d => d.IntegrationSystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntegrationCatalog_IntegrationSystem");

                entity.HasOne(d => d.IntegrationType).WithMany(p => p.IntegrationCatalogs)
                    .HasForeignKey(d => d.IntegrationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntegrationCatalog_IntegrationType");
            });

            modelBuilder.Entity<IntegrationInputLog>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_IntegrationLog");

                entity.ToTable("IntegrationInputLog", "DEMO");

                entity.Property(e => e.IntegrationDate).HasColumnType("datetime");
                entity.Property(e => e.PageNumber).HasMaxLength(100);
                entity.Property(e => e.ResponseCode).HasMaxLength(100);

                entity.HasOne(d => d.IntegrationCatalog).WithMany(p => p.IntegrationInputLogs)
                    .HasForeignKey(d => d.IntegrationCatalogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntegrationLog_IntegrationCatalog");

                entity.HasOne(d => d.IntegrationOperation).WithMany(p => p.IntegrationInputLogs)
                    .HasForeignKey(d => d.IntegrationOperationId)
                    .HasConstraintName("FK_IntegrationLog_IntegrationOperation");

                entity.HasOne(d => d.IntegrationTrigger).WithMany(p => p.IntegrationInputLogs)
                    .HasForeignKey(d => d.IntegrationTriggerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntegrationLog_IntegrationTrigger");
            });

            modelBuilder.Entity<IntegrationOperation>(entity =>
            {
                entity.ToTable("IntegrationOperation", "DEMO");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<IntegrationOutputLog>(entity =>
            {
                entity.ToTable("IntegrationOutputLog", "DEMO");

                entity.Property(e => e.ExternalEndpoint).HasMaxLength(200);
                entity.Property(e => e.IntegrationDate).HasColumnType("datetime");
                entity.Property(e => e.ResponseCode).HasMaxLength(100);

                entity.HasOne(d => d.IntegrationCatalog).WithMany(p => p.IntegrationOutputLogs)
                    .HasForeignKey(d => d.IntegrationCatalogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntegrationOutputLog_IntegrationCatalog");

                entity.HasOne(d => d.IntegrationTrigger).WithMany(p => p.IntegrationOutputLogs)
                    .HasForeignKey(d => d.IntegrationTriggerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntegrationOutputLog_IntegrationTrigger");
            });

            modelBuilder.Entity<IntegrationProcess>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_IntegrationTrigger");

                entity.ToTable("IntegrationProcess", "DEMO");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<IntegrationSystem>(entity =>
            {
                entity.ToTable("IntegrationSystem", "DEMO");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<IntegrationTrigger>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_IntegrationTrigger_1");

                entity.ToTable("IntegrationTrigger", "DEMO");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<IntegrationType>(entity =>
            {
                entity.ToTable("IntegrationType", "DEMO");

                entity.Property(e => e.Name).HasMaxLength(100);
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
