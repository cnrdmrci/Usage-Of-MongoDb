using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Usage_Of_MongoDb.Models;
using Usage_Of_MongoDb.MongoCRUD;

namespace Usage_Of_MongoDb
{
    public class Client
    {
        private readonly IMongoDatabase _db;
        private readonly MongoClient _mongoClient;

        public Client(string database)
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _db = _mongoClient.GetDatabase(database);
        }

        public static Client ObjeOlustur(string database) => new Client(database);

        public void RunCRUD()
        {
            //Insert Record
            User user = new User()
            {
                FirstName = "Caner",
                LastName =  "LastName2",
                Address = new Address()
                {
                    City = "Istanbul",
                    State = "Avrupa",
                    StreetAddress = "101 ok",
                    ZipCode = "12345"
                }
            };
            RecordInserter recordInserter = new RecordInserter(_db);
            recordInserter.InsertOne("Users",user);

            //Read Record
            RecordReader recordReader = new RecordReader(_db);
            List<User> userList = recordReader.LoadRecords<User>("Users");
            foreach (var oneUser in userList)
            {
                Console.WriteLine($"{oneUser.Id}, {oneUser.FirstName}, {oneUser.LastName}");
                if (oneUser.Address != null)
                {
                    Console.WriteLine(oneUser.Address.City);
                }

                Console.WriteLine();
            }

            //Read Record by id
            var recordById = recordReader.LoadRecordById<User>("Users", new Guid(""));
        }

        public void RunTransaction()
        {
            Transactions transactions = new Transactions(_mongoClient,_db);
        }
    }
}
