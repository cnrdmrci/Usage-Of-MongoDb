using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Usage_Of_MongoDb.MongoCRUD
{
    public class RecordInserter
    {
        private readonly IMongoDatabase _db;

        public RecordInserter(IMongoDatabase db)
        {
            _db = db;
        }

        public void InsertOne<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = _db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id",id),
                record,
                new ReplaceOptions {IsUpsert = true});
        }
    }
}
