using BookHouseAPI.Application.Abstractions.IRepositories;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Domain.Entities;
using BookHouseAPI.Persistance.Contexts;
using BookHouseAPI.Persistance.Implementations.Repositories;
using BookHouseAPI.Persistance.Implementetions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementations.UnitOfWorks
{
    public class UnitOfWorkCart : IUnitOfWorkCart
    {
        private readonly BookContext _dbContext;
        private Dictionary<Type, object> _repositories;

        public UnitOfWorkCart(BookContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepositoryCart<TEntity> GetRepository<TEntity>() where TEntity : BaseEntityBasket
        {
            if (_repositories.ContainsKey(typeof(TEntity))) //key
            {
                return (IRepositoryCart<TEntity>)_repositories[typeof(TEntity)]; //value
            }
            IRepositoryCart<TEntity> repository = new RepositoryCart<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
