using BookHouseAPI.Application.Abstractions.IRepositories;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.IUnitOfWork
{
    public interface IUnitOfWorkCart : IDisposable
    {
        IRepositoryCart<TEntity> GetRepository<TEntity>() where TEntity : BaseEntityBasket;
        Task<int> SaveAsync();
    }
}
