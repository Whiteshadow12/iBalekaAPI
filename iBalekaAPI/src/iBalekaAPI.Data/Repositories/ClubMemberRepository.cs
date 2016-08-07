using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;

namespace iBalekaAPI.Data.Repositories
{
    public interface IClubMemberRepository:IRepository<ClubMember>
    {
        ClubMember GetMemberByID(int id);
        IEnumerable<ClubMember> GetMembers(int clubId);
    }
    public class ClubMemberRepository:RepositoryBase<ClubMember>,IClubMemberRepository
    {
        public ClubMemberRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public ClubMember GetMemberByID(int id)
        {
            return DbContext.ClubMember.Where(m => m.MemberId == id && m.Status == ClubStatus.Joined).FirstOrDefault();
        }
        public IEnumerable<ClubMember> GetMembers(int clubId)
        {
            return DbContext.ClubMember.Where(a => a.Status == ClubStatus.Joined && a.ClubId == clubId).ToList();
        }
        public override void Add(ClubMember entity)
        {
            ClubMember exist = DbContext.ClubMember.Single(a => a.Club == entity.Club && a.AthleteId == entity.AthleteId);
            if(exist==null)
            {
                entity.DateJoined = DateTime.Now;
                entity.Status = ClubStatus.Joined;
                Add(entity);
            }
            else
            {
                exist.DateJoined = DateTime.Now;
                exist.Status = ClubStatus.Joined;
                Update(exist);
            }
        }
        public override void Delete(ClubMember entity)
        {
            entity.Status = ClubStatus.Left;
            entity.DateLeft = DateTime.Now;
            Update(entity);
        }
    }
}
