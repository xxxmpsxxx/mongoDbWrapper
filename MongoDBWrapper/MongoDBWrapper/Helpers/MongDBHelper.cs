﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return collection.Find(Builders.FilterEq<T>(field, value)).CountDocuments();
        }

        public List<T> Select<T>(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            if (filter == null)
                filter = new BsonDocument();
            var result = collection.Find(filter).ToList();
            var returnList = new List<T>();
            foreach (var item in result)
            {
                returnList.Add(BsonSerializer.Deserialize<T>(item));
            }

            return returnList;
        }

        public T SelectOne<T>(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            var result = collection.Find(filter).ToList();
            if (result.Count > 1)
                throw new Exception("To many results");

            return BsonSerializer.Deserialize<T>(result.ElementAt(default(int)));
        }

        public bool Insert(string collectionName, BsonDocument doc)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.InsertOne(doc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
