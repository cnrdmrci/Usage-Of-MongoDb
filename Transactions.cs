using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Usage_Of_MongoDb.Models;

namespace Usage_Of_MongoDb
{
    public class Transactions
    {
        private readonly IMongoDatabase _db;
        private readonly MongoClient _mongoClient;

        public Transactions(MongoClient mongoClient,IMongoDatabase db)
        {
            _db = db;
            _mongoClient = mongoClient;
        }

        public void Add<T>(string table, List<T> record)
        {
            var collection = _db.GetCollection<T>(table);
            using (var session = _mongoClient.StartSession())
            {
                //Begin transaction
                session.StartTransaction();

                try
                {
                    record.ForEach(x => { collection.InsertOne(session, x); });

                    // Made it here without error? Let's commit the transaction
                    session.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing to MongoDB: " + ex.Message);
                    session.AbortTransaction();
                }
            }
        }
    }
}
