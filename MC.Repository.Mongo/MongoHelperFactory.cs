// Copyright Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-10-12 10:20:40
using System.Collections.Concurrent;

//Created by Alexander Fields
namespace MandalaConsulting.Repository.Mongo
{
    public interface IMongoHelperFactory
    {
        IMongoHelper Create(string dbName, string connectionString);
    }

    public class MongoHelperFactory : IMongoHelperFactory
    {
        // Cache to store MongoHelper instances with their respective database and connection strings
        private readonly ConcurrentDictionary<string, IMongoHelper> _cache = new ConcurrentDictionary<string, IMongoHelper>();

        /// <summary>
        /// This will create a IMongoHelper instance for the given dbName and connectionString. If it already exists it will return the existing one
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IMongoHelper Create(string dbName, string connectionString)
        {
            // Combine dbName and connectionString as a cache key
            string cacheKey = $"{connectionString}-{dbName}";

            // Check if the MongoHelper is already cached for the given dbName and connectionString
            if (!_cache.TryGetValue(cacheKey, out IMongoHelper cachedMongoHelper))
            {
                // If not found in cache, create a new instance
                MongoHelper mongoHelper = new MongoHelper();

                mongoHelper.database = mongoHelper.CreateMongoDbInstance(dbName, connectionString);
                mongoHelper.dbName = dbName;

                // Cache the newly created MongoHelper
                _cache.TryAdd(cacheKey, mongoHelper);

                return mongoHelper;
            }

            // If cached instance exists, return it
            return cachedMongoHelper;
        }
    }
}
