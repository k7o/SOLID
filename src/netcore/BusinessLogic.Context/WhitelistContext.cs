using Microsoft.EntityFrameworkCore;
using Crosscutting.Contracts;
using System.Threading.Tasks;
using System.Threading;
using Contexts.Contracts;
using BusinessLogic.Contexts.Entities;

namespace BusinessLogic.Contexts
{
    public class WhitelistContext : DbContext, IContext
    {
        public WhitelistContext(DbContextOptions options) 
            : base(options)
        {
        }

        public IContextTransaction BeginTransaction()
        {
            return new WhitelistContextTransaction(Database.BeginTransaction());
        }

        public Task<IContextTransaction> BeginTransactionAsync()
        {
            return new Task<IContextTransaction>(() => new WhitelistContextTransaction(Database.BeginTransactionAsync()));
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

            // make sure bsnnummer becomes no identity column
            modelBuilder.Entity<Bsn>().Property(c => c.Bsnnummer)
                .ValueGeneratedNever();
            modelBuilder.Entity<Bsn>(bsn =>
            {
                bsn.HasKey(primaryKey => primaryKey.Bsnnummer);
            });
        }
    }
}
