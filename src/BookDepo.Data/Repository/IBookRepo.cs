using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookRepo.data.Entities;
using MongoDB.Bson;

namespace BookRepo.data.Repository {
	public interface IBookRepo {
		Task<List<TModel>> GetAll<TModel>(Expression<Func<Book, TModel>> projection);

		Task<TModel> GetOne<TModel>(ObjectId id, Expression<Func<Book, TModel>> projection);

		Task Insert(Book book);
	}
}
