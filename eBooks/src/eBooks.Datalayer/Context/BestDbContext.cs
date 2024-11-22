using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eBooks.Datalayer.Context;
using eBooks.Domain.Entities.Identity;
using eBooks.Domain.Entities.Library;
using eBooks.Domain.Entities.Common;
using eBooks.Utility;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using eBooks.Domain.Entities.Logs;
using eBooks.Utility.Common;

namespace eBooks.DataLayer.Context
{
    public class BestDbContext : IdentityDbContext<User, Role, Guid,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BestDbContext(DbContextOptions<BestDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BestDbContext).Assembly);

            var entitiesAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
        }

        #region IdentityDbSet

        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }
        public DbSet<LibraryCategory> LibraryCategories { get; set; }
        public DbSet<ELMAH_Error> ELMAH_Error { get; set; }

        #endregion IdentityDbSet

        #region SaveChanges

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            if (_httpContextAccessor.HttpContext?.User.Identity == null ||
                _httpContextAccessor.HttpContext == null ||
                !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) return;
            var auditUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (_httpContextAccessor.HttpContext.Connection.RemoteIpAddress == null) return;
            var auditUserIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var auditUserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
            var auditDate = DateTime.Now;
            foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.Id == Guid.Empty)
                            entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
                        entry.Entity.CreatedOn = auditDate;
                        entry.Entity.CreatedById = auditUserId;
                        entry.Entity.ModifiedOn = auditDate;
                        entry.Entity.Action = AuditAction.Create;
                        entry.Entity.ModifiedById = auditUserId;
                        entry.Entity.CreatorIp = auditUserIp;
                        entry.Entity.ModifierIp = auditUserIp;
                        entry.Entity.CreatorAgent = auditUserAgent;
                        entry.Entity.ModifierAgent = auditUserAgent;
                        entry.Entity.Version = 1;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = auditDate;
                        entry.Entity.ModifiedById = auditUserId;
                        entry.Entity.ModifierIp = auditUserIp;
                        entry.Entity.ModifierAgent = auditUserAgent;
                        entry.Entity.Version = ++entry.Entity.Version;
                        entry.Entity.Action = entry.Entity.IsDeleted ? AuditAction.SoftDelete : AuditAction.Update;
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion SaveChanges
    }
}