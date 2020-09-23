using System;
using System.Diagnostics;
using BookRepo.data.Entities;
using MongoDB.Driver;

namespace BookRepo.data.Common {
	public static class DatabaseConfiguration {
		private static readonly Version _CURR_VERSION;

		static DatabaseConfiguration() {
			var dll = typeof(EntityBase).Assembly;
			var dllInfo = FileVersionInfo.GetVersionInfo(dll.Location);
			_CURR_VERSION = Version.Parse(dllInfo.FileVersion);
		}

		public static void CheckSchema(this IMongoDatabase db) {
			var dbVers = GetSchemaVersion(db);
			if (dbVers.Equals(_CURR_VERSION)) return;

			if (dbVers < Version.Parse("0.1.00906.1")) {
				var bookColl = db.GetEntityCollection<Book>();
				var bookIdx = new CreateIndexModel<Book>(Builders<Book>.IndexKeys.Ascending(x => x.Isbn), new CreateIndexOptions {Name = "Book_Isbn_Idx", Unique = true});
				bookColl.Indexes.CreateOne(bookIdx);
				dbVers = Version.Parse("0.1.00906.1");
			}

			if (dbVers < Version.Parse("0.1.00923.1")) {
				var bookDataColl = db.GetEntityCollection<ExtnBookData>();
				var bookDataIdx = new CreateIndexModel<ExtnBookData>(
					Builders<ExtnBookData>.IndexKeys.Ascending(x => x.Isbn).Descending(x => x.ImportedOn),
					new CreateIndexOptions { Name = "Book_Isbn_Idx" });
				bookDataColl.Indexes.CreateOne(bookDataIdx);
				dbVers = Version.Parse("0.1.00923.1");

				db.DropCollection(MongoExtensions.GetCollectionName<Book>());
			}

			UpdateSchemaVersion(db, dbVers);
		}

		#region Schema Version Manager

		private static Version GetSchemaVersion(IMongoDatabase db) {
			var coll = db.GetEntityCollection<SystemConfig>();
			var sysConfig = coll.Find(x => true).FirstOrDefault();
			if (sysConfig == null) {
				sysConfig = new SystemConfig {SchemaVersion = "0.0.0.0"};
				coll.InsertOne(sysConfig);
			}

			return Version.Parse(sysConfig.SchemaVersion);
		}

		private static void UpdateSchemaVersion(IMongoDatabase db, Version dbVers) {
			var coll = db.GetEntityCollection<SystemConfig>();
			coll.UpdateMany(x => true, Builders<SystemConfig>.Update.Set(x => x.SchemaVersion, dbVers.ToString()));
		}

		#endregion
	}
}
