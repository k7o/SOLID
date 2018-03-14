using Entities;
using Contracts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Crosscutting.Contracts;
using System;

namespace Contexts
{
    public class WhitelistUnitOfWork : DbContext, IUnitOfWork
    {
        public WhitelistUnitOfWork(DbContextOptions options) 
            : base(options)
        {
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(Set<TEntity>());
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        // usefull for debugging
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<BsnUzovi> BsnUzovis { get; set; }

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
        }
    }
}