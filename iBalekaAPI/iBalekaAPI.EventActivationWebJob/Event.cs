using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iBalekaAPI.EventActivationWebJob.Models
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

    }

}
