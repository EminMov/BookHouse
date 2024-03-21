using BookHouseAPI.Application.Abstractions.IRepositories;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Domain.Entities;
using BookHouseAPI.Persistance.Contexts;
using BookHouseAPI.Persistance.Implementetions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookContext _dbContext;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(BookContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity))) //key
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)]; //value
            }
            IRepository<TEntity> repository = new Repository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
