using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookRepo.Data.Common;
using BookRepo.Data.Entities;
using BookRepo.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookRepo.Data.Repository.Impl {
	public class MongoBookRepoImpl : IBookRepo {
		private readonly IMongoCollection<Book> _bookColl;
		private readonly IMongoCollection<ExtnBookData> _rawColl;

		public MongoBookRepoImpl(IMongoDatabase db) {
			_bookColl = db.GetEntityCollection<Book>();
			_rawColl = db.GetEntityCollection<ExtnBookData>();
		}

		#region Implementation of IBookRepo

		/// <inheritdoc />
		public async Task<List<TModel>> GetAll<TModel>(Expression<Func<Book, TModel>> projection) => await _bookColl.Find(x => true).Project(projection).ToListAsync();

		/// <inheritdoc />
		public async Task<TModel> GetOne<TModel>(ObjectId id, Expression<Func<Book, TModel>> projection) => await _bookColl.Find(x => x.Id == id).Project(projection).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task<TModel> GetOne<TModel>(string isbn, Expression<Func<Book, TModel>> projection) => await _bookColl.Find(x => x.Isbn.Equals(isbn)).Project(projection).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task Insert(Book book) => await _bookColl.InsertOneAsync(book);

		/// <inheritdoc />
		public async Task<ExtnBookData> GetRawData(string isbn) =>
			await _rawColl.Find(x => x.Isbn.Equals(isbn))
				.SortByDescending(x => x.ImportedOn)
				.FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task StoreRawData(ExtnBookData data) => await _rawColl.InsertOneAsync(data);

		/// <inheritdoc />
		public async Task<List<TModel>> GetAllRaw<TModel>(Expression<Func<ExtnBookData, TModel>> projection) =>
			await _rawColl.Find(x => true).Project(projection).ToListAsync();

		/// <inheritdoc />
		public async Task<LibraryStatsModel> GetStats() {
			var num = await _bookColl.CountDocumentsAsync(x => true);
			var cutoff = DateTime.UtcNow.AddDays(-7);
			var latest = await _bookColl.Find(x => x.CreatedOn >= cutoff).Project(BookInfoModel.GetMap()).ToListAsync();
			return new LibraryStatsModel {NumBooks = num, LatestUploaded = latest};
		}

		#endregion
	}
}
