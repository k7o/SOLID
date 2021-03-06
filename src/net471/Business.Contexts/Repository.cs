﻿using Contracts;
using Crosscutting.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Business.Contexts
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        readonly DbSet<TEntity> _dbSet;

        public Repository(DbSet<TEntity> dbSet)
        {
            Guard.IsNotNull(dbSet, nameof(dbSet));

            _dbSet = dbSet;
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }
    }
}
