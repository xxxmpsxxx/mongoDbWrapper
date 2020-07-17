using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBWrapper.Helpers
{
    public class MongDBHelper : IDisposable
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public MongDBHelper(string connectionString, string dbName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(dbName);
        }

        public long? SelectCount(string collectionName)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                return collection.Find(new BsonDocument()).CountDocuments();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long SelectCount(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filter).CountDocuments();
        }

        public long SelectCount(string collectionName, string field, string value)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builders.FilterEq(field, value)).CountDocuments();
        }

        public long SelectCount(string collectionName, string field, ObjectId id)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builders.FilterEq(field, id)).CountDocuments();
        }

        public long SelectCount<T>(string collectionName, string field, T value)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builders.FilterEq<T>(field, value)).CountDocuments;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
