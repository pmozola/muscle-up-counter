using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MUCNotification.Application
{
    public class DailyRepsCounter
    {
        public DailyRepsCounter()
        {
            this.Id = ObjectId.GenerateNewId();
        }

        public DailyRepsCounter(DateTime date)
        {
            this.Id = ObjectId.GenerateNewId();
            this.Date = date;
        }

        [BsonId]
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

        [BsonElement("repcounter")]
        public int RepetitionCounter { get;  private set; }

        public void AddRep()
        {
            this.RepetitionCounter++;
        }

        public void RemoveRep()
        {
            this.RepetitionCounter--;
        }
    }
}
