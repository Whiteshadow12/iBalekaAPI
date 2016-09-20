using iBalekaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.Extentions
{
    public static class EventRegistrationRepoExtensions
    {
        public static EventRegistration GetRegByRegId(this IEnumerable<EventRegistration> evntReg, int regId)
        {
            return evntReg.Where(a => a.RegistrationId == regId).SingleOrDefault();
        }
        public static ICollection<EventRegistration> GetRegByAthleteId(this IEnumerable<EventRegistration> evntReg, int athleteId)
        {
            return evntReg.Where(a => a.AthleteId == athleteId).ToList();
        }
        public static ICollection<EventRegistration> GetRegByEventId(this IEnumerable<EventRegistration> evntReg, int eventId)
        {
            return evntReg.Where(a => a.EventId == eventId).ToList();
        }
    }
    public static class AthleteRepoExtensions
    {
        public static Athlete GetAthleteByAthleteId(this IEnumerable<Athlete> athletes, int athleteId)
        {
            return athletes.Where(a => a.AthleteId == athleteId).SingleOrDefault();
        }
    }
    public static class EventRepoExtensions
    {
        public static ICollection<Event> GetEventByUserId(this IEnumerable<Event> evnts,string userId)
        {
            return evnts.Where(a => a.UserID == userId).ToList();
        }
        public static Event GetEventByEventId(this IEnumerable<Event> evnts, int? eventId)
        {
            return evnts.Where(a => a.EventId == eventId).SingleOrDefault();
        }
        public static ICollection<Event> GetEventsByClubId(this IEnumerable<Event> evnts, int? clubId)
        {
            return evnts.Where(a => a.ClubID == clubId).ToList();
        }
    }
    public static class RouteRepoExtensions
    {
        public static ICollection<Route> GetRouteByUserId(this IEnumerable<Route> rte, string userId)
        {
            return rte.Where(a => a.UserID == userId).ToList();
        }
        public static Route GetRouteByRouteId(this IEnumerable<Route> rte, int? routeId)
        {
            return rte.Where(a => a.RouteId == routeId).SingleOrDefault();
        }
    }
    public static class RunRepoExtensions
    {
        public static Run GetRunByRunId(this IEnumerable<Run> run, int runId)
        {
            return run.Where(a => a.RunId == runId).SingleOrDefault();
        }
        public static ICollection<Run> GetRunsByEventId(this IEnumerable<Run> run, int evntId)
        {
            return run.Where(a => a.EventId == evntId).ToList();
        }
        public static ICollection<Run> GetRunsByRouteId(this IEnumerable<Run> run, int routeId)
        {
            return run.Where(a => a.RouteId == routeId).ToList();
        }
        public static ICollection<Run> GetRunsByAthletePersonalRuns(this IEnumerable<Run> run)
        {
            return run.Where(a => a.EventId == null).ToList();
        }
        public static ICollection<Run> GetRunsByAthleteEventRuns(this IEnumerable<Run> run)
        {
            return run.Where(a => a.RouteId == null).ToList();
        }
        public static double GetTotalDistanceRan(this IEnumerable<Run> run)
        {
            return run.Sum(a=>a.Distance);
        }
        //add to service
        public static double GetRunCount(this IEnumerable<Run> run)
        {
            return run.Select(a=>a.RunId)
                      .Distinct()
                      .Count();
        }
        public static double GetEventRunCount(this IEnumerable<Run> run)
        {
            return run.Where(a => a.EventId != null)
                      .Select(a => a.RunId)
                      .Distinct()
                      .Count();
        }
        public static double GetPersonalRunCount(this IEnumerable<Run> run)
        {
            return run.Where(a => a.EventId == null)
                      .Select(a => a.RunId)
                      .Distinct()
                      .Count();
        }
        public static double GetCaloriesOverTime(this IEnumerable<Run> run,string startDate,string endDate)
        {
            DateTime start, end;
            start = DateTime.Parse(startDate);
            end = DateTime.Parse(endDate);
            return run.Where(a => DateTime.Parse(a.DateRecorded) >= start && DateTime.Parse(a.DateRecorded) <= end)
                      .Sum(a => a.CaloriesBurnt);
        }
        public static double GetDistanceOverTime(this IEnumerable<Run> run, string startDate, string endDate)
        {
            DateTime start, end;
            start = DateTime.Parse(startDate);
            end = DateTime.Parse(endDate);
            return run.Where(a => DateTime.Parse(a.DateRecorded) >= start && DateTime.Parse(a.DateRecorded) <= end)
                      .Sum(a => a.Distance);
        }
    }
    public static class ClubRepoExtensions
    {
        public static Club GetClubByClubId(this IEnumerable<Club> run, int clubId)
        {
            return run.Where(a => a.ClubId == clubId).SingleOrDefault();
        }
        public static ICollection<Club> GetClubByUserId(this IEnumerable<Club> run, string userId)
        {
            return run.Where(a => a.UserId == userId).ToList();
        }
    }
    public static class ClubMemberRepoExtensions
    {
        public static ICollection<ClubMember> GetMembersByClubId(this IEnumerable<ClubMember> members, int clubId)
        {
            return members.Where(a => a.ClubId == clubId).ToList();
        }
        public static ClubMember GetMembersById(this IEnumerable<ClubMember> members, int memberId)
        {
            return members.Where(a => a.MemberId == memberId).SingleOrDefault();
        }
    }


}
