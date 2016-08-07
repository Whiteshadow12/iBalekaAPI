using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaAPI.Models
{
    public partial class EventRoute
    {
        public EventRoute() { }
        public EventRoute(string dateAdded)
        {            
            
            DateAdded = dateAdded;
            Deleted = false;
        }
        public EventRoute(int evntRouteId, int evntId, int routeId, string descript, string dateAdd, double distance, string title)
        {
            EventRouteID = evntRouteId;
            EventID = evntId;
            RouteID = routeId;
            Description = descript;
            Distance = distance;
            DateAdded = dateAdd;


        }
        public EventRoute(int routeId, string dateAdd, double distance, string title)
        {
            Title = title;
            RouteID = routeId;
            Distance = distance;
            DateAdded = dateAdd;


        }
        public EventRoute(int eventRouteId, int eventId, int routeId, string dateAdded, double distance, string title, string description)
        {
            EventRouteID = eventRouteId;
            EventID = eventId;
            RouteID = routeId;
            DateAdded = dateAdded;
            Distance = distance;
            Title = title;
            Description = description;
        }
        public EventRoute(Route evntRoute)
        {
            Title = evntRoute.Title;
            RouteID = evntRoute.RouteId;
            Distance = evntRoute.Distance;
            DateAdded = DateTime.Now.Date.ToString();
        }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public int EventRouteID { get; set; }
        public string DateAdded { get; set; }
        public bool Deleted { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public double Distance { get; set; }
        public int EventID { get; set; }
        public int RouteID { get; set; }

        public virtual Event Event { get; set; }
        public virtual Route Route { get; set; }
    }
}
