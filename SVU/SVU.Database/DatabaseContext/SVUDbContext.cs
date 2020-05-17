using Microsoft.EntityFrameworkCore;
using SVU.Database.Models;
using SVU.Database.Models.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SVU.Database.DatabaseContext
{
    /// <summary>
    /// The database context for the our application
    /// </summary>
    public class SVUDbContext : DbContext
    {
        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public SVUDbContext(DbContextOptions<SVUDbContext> options) : base(options)
        {

        }

        #endregion

        #region Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            SetCreationAndLastUpdatedDates();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetCreationAndLastUpdatedDates();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SetCreationAndLastUpdatedDates();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Sets the add time and the last modified time
        /// </summary>
        private void SetCreationAndLastUpdatedDates()
        {
            //Get the entitys that are marked as added, modified or deleted
            var entitys = ChangeTracker.Entries().Where(obj => obj.Entity is BaseEntityModel && (obj.State == EntityState.Added || obj.State == EntityState.Modified || obj.State == EntityState.Deleted));
            //Loop throw the entitys
            foreach (var entity in entitys)
            {
                //Get the entity object
                var baseEntity = entity.Entity as BaseEntityModel;
                //If add set the id and the creation date
                if (entity.State == EntityState.Added)
                {
                    if (baseEntity.CreationDate == default(DateTime))
                    {
                        baseEntity.CreationDate = DateTime.UtcNow;
                    }
                    if (baseEntity.Id == Guid.Empty)
                    {
                        baseEntity.Id = Guid.NewGuid();
                    }
                }
                //Always set the last updated date if the entity is flaged
                baseEntity.LastUpdatedDate = DateTime.UtcNow;
            }
        }
        #endregion

        #region Tables
        public DbSet<Program> Programs { get; private set; }
        public DbSet<Course> Courses { get; private set; }
        public DbSet<Session> Sessions { get; private set; }
        public DbSet<Homework> Homeworks { get; private set; }
        public DbSet<ExternalLink> ExternalLinks { get; private set; }

        public DbSet<HeartDisease> HeartDiseases { get; private set; }
        public DbSet<Tennis> Tennis { get; private set; }

        public DbSet<Blog> Blogs { get; private set; }
        public DbSet<Tag> Tags { get; private set; }
        public DbSet<BlogTag> BlogTags { get; private set; }

        public DbSet<HealthRequest> HealthRequests { get; private set; }
        public DbSet<HealthRole> HealthRoles { get; private set; }
        public DbSet<HealthUser> HealthUsers { get; private set; }
        public DbSet<HealthRequestReply> HealthRequestReplies { get; private set; }

        #endregion
    }
}
