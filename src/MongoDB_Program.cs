using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MainAsync().Wait();

            Console.ReadLine();
        }
        static async Task MainAsync()
        {
            var connectionString = "mongodb://loganTest:abc123@myhost.mydomain:27017/test";

            var client = new MongoClient(connectionString);

            IMongoDatabase db = client.GetDatabase("test");
            var collection = db.GetCollection<BsonDocument>("students");
            /*
                        var document = new BsonDocument
                            {
                              {"firstname", BsonValue.Create("Peter")},
                              {"lastname", new BsonString("Mbanugo")},
                              { "subjects", new BsonArray(new[] {"English", "Mathematics", "Physics"}) },
                              { "class", "JSS 3" },
                              { "age", 23 }
                            };

                        await collection.InsertOneAsync(document);
                        */
            var newStudents = CreateNewStudents();

            await collection.InsertManyAsync(newStudents);
        }
        private static IEnumerable<BsonDocument> CreateNewStudents()
        {
            var student1 = new BsonDocument
            {
              {"firstname", "Ugo"},
              {"lastname", "Damian"},
              {"subjects", new BsonArray {"English", "Mathematics", "Physics", "Biology"}},
              {"class", "JSS 3"},
              {"age", 23}
            };

            var student2 = new BsonDocument
            {
              {"firstname", "Julie"},
              {"lastname", "Lerman"},
              {"subjects", new BsonArray {"English", "Mathematics", "Spanish"}},
              {"class", "JSS 3"},
              {"age", 23}
            };

            var student3 = new BsonDocument
            {
              {"firstname", "Julie"},
              {"lastname", "Lerman"},
              {"subjects", new BsonArray {"English", "Mathematics", "Physics", "Chemistry"}},
              {"class", "JSS 1"},
              {"age", 25}
            };

            var newStudents = new List<BsonDocument>();
            newStudents.Add(student1);
            newStudents.Add(student2);
            newStudents.Add(student3);

            return newStudents;
        }
    }
  
}
