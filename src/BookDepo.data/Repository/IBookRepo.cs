using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookRepo.Data.Entities;
using MongoDB.Bson;

namespace BookRepo.Data.Repository {
	public interface IBookRepo {
		Task<List<TModel>> GetAll<TModel>(Expression<Func<Book, TModel>> projection);

		Task<TModel> GetOne<TModel>(ObjectId id, Expression<Func<Book, TModel>> projection);

		Task<TModel> GetOne<TModel>(string isbn, Expression<Func<Book, TModel>> projection);

		Task Insert(Book book);

		Task<ExtnBookData> GetRawData(string isbn);

		Task StoreRawData(ExtnBookData data);
	}
}
