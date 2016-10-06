using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaAPI.Models
{
    public class EventRoute
    {
        public EventRoute()
        {            
            Deleted = false;
        }
        public EventRoute(int routeId,string title)
        {
            Title = title;
            RouteID = routeId;

        }
        public EventRoute(int eventRouteId, int eventId, int routeId, string dateAdded,string title, string description)
        {
            EventRouteID = eventRouteId;
            EventID = eventId;
            RouteID = routeId;
            DateAdded = dateAdded;
            Title = title;
            Description = description;
        }
        public EventRoute(Route evntRoute)
        {
            Title = evntRoute.Title;
            RouteID = evntRoute.RouteId;
        }
        [Key]
        public int EventRouteID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        
        public string DateAdded { get; set; }
        public bool Deleted { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public int EventID { get; set; }
        public double Distance { get; set; }
        public int RouteID { get; set; }
        [JsonIgnore]
        public virtual Route Route { get; set; }
        [JsonIgnore]
        public virtual Event Event { get; set; }
    }
}
