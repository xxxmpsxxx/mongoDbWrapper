using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBWrapper.Helpers
{
    public class Builders
    {
        public static FilterDefinition<BsonDocument> FilterEq(string field, string value)
        {
            return Builders<BsonDocument>.Filter.Eq(field, value);
        }

        public static FilterDefinition<BsonDocument> FilterEq<T>(string field, T value)
        {
            return Builders<BsonDocument>.Filter.Eq(field, value);
        }

        public static FilterDefinition<BsonDocument> FilterEq(string field, ObjectId id)
        {
            return Builders<BsonDocument>.Filter.Eq(field, id);
        }

        public static UpdateDefinition<BsonDocument> Update<T>(string field, T value)
        {
            return Builders<BsonDocument>.Update.Set(field, value);
        }
    }
}
