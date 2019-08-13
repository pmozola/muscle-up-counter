using System;
using System.Threading.Tasks;
using MUCNotification.Application;
using MongoDB.Driver;


namespace MUCNotification.Infrastructure
{
    public class DailyRepsCounterRepository
    {
        public IMongoDatabase Database { get; }
        public DailyRepsCounterRepository(IMongoClient client)
        {
            Database = client.GetDatabase("mucdb");
        }

        public async Task Add(DailyRepsCounter model)
        {
            var collectionName = GetCollectionName();
            var collection = Database.GetCollection<DailyRepsCounter>(collectionName);

            await collection.InsertOneAsync(model);
        }


        public async Task Remove(DailyRepsCounter model)
        {
            var collectionName = GetCollectionName();
            var collection = Database.GetCollection<DailyRepsCounter>(collectionName);

            await collection.DeleteOneAsync(x => x.Id == model.Id);
        }

        public async Task Update(DailyRepsCounter model)
        {
            var collectionName = GetCollectionName();
            var collection = Database.GetCollection<DailyRepsCounter>(collectionName);

            await collection.ReplaceOneAsync(x => x.Id == model.Id, model);
        }

        public async Task<DailyRepsCounter> GetCurrent()
        {
            var collectionName = GetCollectionName();
            var collection = Database.GetCollection<DailyRepsCounter>(collectionName);

            var result = await collection.Find(x => true).SortByDescending(x => x.Id).Limit(1).FirstOrDefaultAsync();
            return result?.Date.Date == DateTime.Now.Date ? result : null;
        }

        private static string GetCollectionName() { return "DailyRepsCounter"; }
    }
}
