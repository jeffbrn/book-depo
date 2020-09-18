using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using homelib.data.Common;
using homelib.data.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace homelib.data.Repository.Impl {
	public class MongoBookRepoImpl : IBookRepo {
		private readonly IMongoCollection<Book> _repo;

		public MongoBookRepoImpl(IMongoDatabase db) {
			_repo = db.GetEntityCollection<Book>();
		}

		#region Implementation of IBookRepo

		/// <inheritdoc />
		public async Task<List<TModel>> GetAll<TModel>(Expression<Func<Book, TModel>> projection) => await _repo.Find(x => true).Project(projection).ToListAsync();

		/// <inheritdoc />
		public async Task<TModel> GetOne<TModel>(ObjectId id, Expression<Func<Book, TModel>> projection) => await _repo.Find(x => x.Id == id).Project(projection).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task Insert(Book book) => await _repo.InsertOneAsync(book);

		#endregion
	}
}
