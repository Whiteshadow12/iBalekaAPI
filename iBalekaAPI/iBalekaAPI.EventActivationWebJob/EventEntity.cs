using iBalekaAPI.EventActivationWebJob.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaAPI.EventActivationWebJob
{
    public class EventEntity : TableEntity
    {
        public EventEntity(int eventId, string eventDate,string location,string time,EventType status)
        {
            this.PartitionKey = eventId.ToString();
            this.RowKey = eventDate;
            Location = location;
            EventTime = time;
            EventStatus = status;
        }
        public EventEntity() { }

        public string Location { get; set; }
        public string Timezone { get; set; }
        public string EventTime { get; set; }
        public EventType EventStatus { get; set; }

    
    }
    public static class EventExtensions
    {
        public static EventEntity ToEntity(this Event evnt)
        {
            EventEntity newEntity = new EventEntity(evnt.EventId, evnt.Date, evnt.Location, evnt.Time, evnt.EventStatus);
            return newEntity;
        }
        public static List<EventEntity> ToEntities(this List<Event> evnts)
        {
            List<EventEntity> eventEntities = new List<EventEntity>();
            foreach (Event evnt in evnts)
            {
                eventEntities.Add(new EventEntity(evnt.EventId, evnt.Date, evnt.Location, evnt.Time, evnt.EventStatus));
            }
            return eventEntities;
        }
    }
    
}
