using Microsoft.EntityFrameworkCore;
using Crosscutting.Contracts;
using BusinessLogic.Entities;
using System.Threading.Tasks;
using System.Threading;
using Contexts.Contracts;

namespace Business.Context
{
    public class WhitelistContext : DbContext, IContext
    {
        public WhitelistContext(DbContextOptions options) 
            : base(options)
        {
        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(Set<TEntity>());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        // usefull for debugging
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<BsnUzovi> BsnUzovis { get; set; }
        public DbSet<Bsn> Bsns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guard.IsNotNull(modelBuilder, nameof(modelBuilder));

            modelBuilder.Entity<Adres>(adres =>
            {
                adres.HasKey(primaryKey => primaryKey.Postcode);
            });

            modelBuilder.Entity<BsnUzovi>(bsnUzovi =>
            {
                bsnUzovi.HasKey(primaryKey => new { primaryKey.Bsnnummer, primaryKey.Uzovi });
            });

            modelBuilder.Entity<Bsn>(bsn =>
            {
                bsn.HasKey(primaryKey => primaryKey.Bsnnummer);
            });
        }
    }
}
