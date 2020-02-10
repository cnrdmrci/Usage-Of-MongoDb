using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace Usage_Of_MongoDb.MongoCRUD
{
    public class RecordDeleter
    {
        private readonly IMongoDatabase _db;

        public RecordDeleter(IMongoDatabase db)
        {
            _db = db;
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}
