using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iBalekaAPI.Models
{
    public enum EventType
    {
        Open,
        Active,
        Closed
    }
    public class Event
    {
        public Event()
        {
            EventRegistration = new HashSet<EventRegistration>();
            EventRoute = new HashSet<EventRoute>();
            
            DateCreated = DateTime.Now.ToString();
            ClubID = 0;
            EventStatus = EventType.Open;
            Deleted = false;
        }
        public Event(Event evnt)
        {
            Date = evnt.Date;
            Description = evnt.Description;
            DateCreated = evnt.DateCreated;
            DateModified = evnt.DateModified;
            Title = evnt.Title;
            Time = evnt.Time;
            EventRoute = evnt.EventRoute;
            EventStatus = EventType.Open;
            Deleted = false;
            Location = evnt.Location;
        }
        public Event(int evntId, string userId, string descript, string title, string location, List<EventRoute> evntRoutes, string eventDate, string eventTime, string dateAdded, string dateModified)
        {
            EventId = evntId;
            UserID = userId;
            Date = eventDate;
            Time = eventTime;
            DateCreated = dateAdded;
            Description = descript;
            Title = title;
            Location = location;
            EventRoute = evntRoutes;
            DateModified = dateModified;
            EventStatus = EventType.Open;
            Deleted = false;
        }


        
        [Key]
        [DisplayName("Event ID")]
        public int EventId { get; set; }
        [DisplayName("User ID")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Event Time is required")]
        public string Time { get; set; }
        [DisplayName("Date Modified")]
        public string DateModified { get; set; }
        [DisplayName("Date Created ")]
        public string DateCreated { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public EventType EventStatus { get; set; }
        public bool Deleted { get; set; }
        [DisplayName("Club ID")]
        public int ClubID { get; set; }
        [DisplayName("Event Registrations")]
        public virtual ICollection<EventRegistration> EventRegistration { get; set; }
        [DisplayName("Event Routes")]
        public virtual ICollection<EventRoute> EventRoute { get; set; }
        

    }

}
