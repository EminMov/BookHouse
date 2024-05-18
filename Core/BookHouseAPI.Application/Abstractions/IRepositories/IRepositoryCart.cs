﻿using BookHouseAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.IRepositories
{
    public interface IRepositoryCart<T> where T : BaseEntityBasket
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id, Expression<Func<T, object>> includeFilter = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<bool> AddAsync(T data);
        bool Remove(T data);
        Task<bool> RemoveById(int id);
        bool Update(T data);
    }
}