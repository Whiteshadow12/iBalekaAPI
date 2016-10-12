using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using iBalekaAPI.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace iBalekaAPI.Data.Configurations
{
    public partial class iBalekaDBContext : IdentityDbContext<ApplicationUser>
    {
        iBalekaDBContext() : base() { }
        public iBalekaDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);    
        }

  
        public virtual DbSet<Athlete> Athlete { get; set; }
        public virtual DbSet<Checkpoint> Checkpoint { get; set; }
        public virtual DbSet<Club> Club { get; set; }
        public virtual DbSet<ClubMember> ClubMember { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventRegistration> EventRegistration { get; set; }
        public virtual DbSet<EventRoute> EventRoute { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Run> Run { get; set; }

       
        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}