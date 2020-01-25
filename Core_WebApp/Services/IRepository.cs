﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.Services
{
	/// <summary>
	/// The Repository Interface that is used to define 
	/// standard Repository methods for DAL
	/// Async method for Performing DAL operations
	/// </summary>
	public interface IRepository<TEntity, in TPk> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAsync();
		Task<TEntity> GetAsync(TPk id);
		Task<TEntity> CreateAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TPk id, TEntity entity);
		Task<bool> DeleteAsync(TPk id);
	}
}
