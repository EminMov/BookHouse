using BookHouseAPI.Application.Abstractions.IRepositories;
using BookHouseAPI.Domain.Entities;
using BookHouseAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementations.Repositories
{
    public class RepositoryCart<T> : IRepositoryCart<T> where T : BaseEntityBasket
    {
        private readonly BookContext _dbContext;

        public RepositoryCart(BookContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DbSet<T> Table => _dbContext.Set<T>();

        public async Task<bool> AddAsync(T data)
        {
            var add = await Table.AddAsync(data);
            return add.State == EntityState.Added;
        }

        public IQueryable<T> GetAll()
        {
            var query = Table.AsQueryable();
            return query;
        }

        public async Task<T> GetByIdAsync(int id, Expression<Func<T, object>> includeFilter = null)
        {
            IQueryable<T> query = Table;

            if (includeFilter != null)
            {
                query = query.Include(includeFilter);
            }

            var data = await query.FirstOrDefaultAsync(d => d.Id == id);
            return data;
        }

        public bool Remove(T data)
        {
            var delete = Table.Remove(data);
            return delete.State == EntityState.Deleted; ;
        }

        public async Task<bool> RemoveById(int id)
        {
            var delete = await Table.FirstOrDefaultAsync(d => d.Id == id);
            return Remove(delete);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        => tracking ?
            await Table.FirstOrDefaultAsync(expression) :
            await Table.AsNoTracking().FirstOrDefaultAsync(expression);

        public bool Update(T data)
        {
            var update = Table.Update(data);
            return update.State == EntityState.Modified;
        }
    }
}
