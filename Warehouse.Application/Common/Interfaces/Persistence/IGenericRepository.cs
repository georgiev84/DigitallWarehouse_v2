﻿using System.Linq.Expressions;

namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task Add(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
}
