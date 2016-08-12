using System;
using System.Collections.Generic;
using iBalekaAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace iBalekaAPI.Models
{
    /// <summary>
    /// Represents an athlete
    /// </summary>
    public class Athlete
    {
        public Athlete()
        {
            ClubMember = new HashSet<ClubMember>();
            EventRegistration = new HashSet<EventRegistration>();
            Run = new HashSet<Run>();
        }

        [Key]
        public int AthleteId { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public bool Deleted { get; set; }
        public string Name { get; set; }
        [DisplayName("Surname")]
        public string UserName { get; set; }
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string Password { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [DisplayName("Security Question")]
        public string SecurityQuestion { get; set; }
        [DisplayName("Security Answer")]
        public string SecurityAnswer { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }

        public virtual ICollection<ClubMember> ClubMember { get; set; }
        public virtual ICollection<EventRegistration> EventRegistration { get; set; }
        public virtual ICollection<Run> Run { get; set; }
    }
}
