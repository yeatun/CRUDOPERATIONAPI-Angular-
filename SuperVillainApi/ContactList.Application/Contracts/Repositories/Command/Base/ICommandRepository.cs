﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contracts.Repositories.Command.Base
{
    public interface ICommandRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Insert data using EF
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);
        /// <summary>
        /// Insert multiple data using EF
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entity);
        /// <summary>
        /// Update data using EF
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);
        /// <summary>
        /// Update Multiple data using EF
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entity);
        /// <summary>
        /// Delete data using EF
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// Delete multiple data using EF
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<TEntity> entity);
        /// <summary>
        /// Delete data using Primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> DeleteAsync(object id);
    }
}
