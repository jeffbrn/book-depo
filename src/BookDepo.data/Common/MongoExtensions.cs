using System;
using System.Linq;
using System.Reflection;
using BookRepo.data.Entities;
using MongoDB.Driver;

namespace BookRepo.data.Common {
	public static class MongoExtensions {
		public static IMongoDatabase GetConnection(this MongoConnectionSettings config) {
			var client = new MongoClient(config.GetConnectionAddr());
			var db = client.GetDatabase(config.DatabaseName);
			return db;
		}

		public static IMongoCollection<TEntity> GetEntityCollection<TEntity>(this IMongoDatabase db) where TEntity : EntityBase => db.GetCollection<TEntity>(GetCollectionName<TEntity>());

		private static string GetConnectionAddr(this MongoConnectionSettings config) => $"mongodb://{config.ServerAddr}";

		public static string GetCollectionName<TEntity>() where TEntity : EntityBase {
			var type = typeof(TEntity);
			var nameOverride = type.GetCustomAttributes<CollectionNameAttribute>().FirstOrDefault();
			if (nameOverride != null) return nameOverride.CollectionName;

			var retval = type.Name.ToLowerInvariant();
			var ending = retval[^1];
			if (ending == 'y') retval = retval[..^1] + "ies";
			else if (ending != 's') retval += 's';
			return retval;
		}
	}
}
