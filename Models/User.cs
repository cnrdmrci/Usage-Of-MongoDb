using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Usage_Of_MongoDb.Models
{
    public class User
    {
        [BsonId] // _id
        public Guid Id { get; set; }
        [BsonRequired]
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Address")]
        public Address Address { get; set; }
    }
}
