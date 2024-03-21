using BookHouseAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.IRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T data);
        bool Remove(T data);
        Task<bool> RemoveById(int id);
        bool Update(T data);
    }
}
