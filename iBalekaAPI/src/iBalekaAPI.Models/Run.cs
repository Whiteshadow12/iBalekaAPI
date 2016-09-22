using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaAPI.Models
{
    public enum RunType
    {
        Personal,
        Event
    }
    public class Run
    {
        [Key]
        public int RunId { get; set; }
        public int AthleteId { get; set; }
        public double CaloriesBurnt { get; set; }
        public double Distance { get; set; }
        public string DateRecorded { get; set; }
        public bool Deleted { get; set; }
        public string EndTime { get; set; }
        public RunType RunType { get; set; }
        public int? EventId { get; set; }
        public int? RouteId { get; set; }
        public string StartTime { get; set; }

        public virtual Rating Rating { get; set; }
        [JsonIgnore]
        public virtual Athlete Athlete { get; set; }
        public virtual Event Event { get; set; }
        public virtual Route Route { get; set; }
    }
}
