using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaAPI.Models
{
    public enum ClubStatus
    {
        Joined,
        Left
    }
    public class ClubMember
    {
        [Key]
        public int MemberId { get; set; }
        public int AthleteId { get; set; }
        public int ClubId { get; set; }
        public string DateJoined { get; set; }
        public string DateLeft { get; set; }
        public ClubStatus Status { get; set; }
        [JsonIgnore]

        public virtual Athlete Athlete { get; set; }
        [JsonIgnore]
        public virtual Club Club { get; set; }
    }
}
