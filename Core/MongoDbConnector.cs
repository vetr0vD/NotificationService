using Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public sealed class MongoDbConnector :
        IDbConnector
    {
        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly string _dbName = "default";
        private IMongoDatabase _db = null;

        public MongoDbConnector(string dbName)
        {
            _dbName = dbName;
        }

        public MongoDbConnector()
        { }


        public void Connect()
        {
            var client = new MongoClient(_connectionString);
            _db = client.GetDatabase(_dbName);
        }

        public WorkNotification ReadData(DateTimeOffset date)
        {
            var collection = _db.GetCollection<WorkNotification>(typeof(WorkNotification).Name);

            WorkNotification item = null;
            {
                var builder = Builders<WorkNotification>.Filter.Where( it => it.NotificationDate <= date);

                var builderSort = Builders<WorkNotification>.Sort.Descending(it => it.NotificationDate);


                item = collection.Find(builder).
                    Sort(builderSort).
                    Limit(1).
                    ToList().
                    FirstOrDefault();
            }

            if (item == null || item.Status == WorkNotificationStatus.Ready)
            {
                var builder = Builders<WorkNotification>.Filter.Where(it => it.NotificationDate > date && it.Status != WorkNotificationStatus.Ready);
                var builderSort = Builders<WorkNotification>.Sort.Ascending(it => it.NotificationDate);

                return collection.Find(builder).
                    Sort(builderSort).
                    Limit(1).
                    ToList().
                    FirstOrDefault() ?? item;
            }
            else
            {
                return item;
            }
        }

        public void WriteData(WorkNotification item)
        {
            var collection = _db.GetCollection<WorkNotification>(typeof(WorkNotification).Name);
            var filter = Builders<WorkNotification>.Filter.Eq(s => s.NotificationDate, item.NotificationDate);
            var update = Builders<WorkNotification>.Update.Set(it => it.Status, item.Status);
            var result = collection.UpdateOne(filter, update);
            if (result.MatchedCount == 0)
            {
                collection.InsertOne(item);
            }
        }

        public void ClearData<T>()
        {
            _db.DropCollection(typeof(WorkNotification).Name);
        }
    }
}
