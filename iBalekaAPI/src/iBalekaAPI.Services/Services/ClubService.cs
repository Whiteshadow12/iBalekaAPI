using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaAPI.Models;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Data.Repositories;

namespace iBalekaAPI.Services
{
    public interface IClubService
    {
        Club GetClubByID(int id);
        IEnumerable<Club> GetAll();
        IEnumerable<Club> GetUserClubs(string userId);
        void AddClub(Club club);
        void UpdateClub(Club club);
        void Delete(int club);
        void SaveClub();
    }
    public interface IClubMemberService
    {
        ClubMember GetMemberByID(int id);
        IEnumerable<ClubMember> GetMembers(int clubId);
        void RegisterMember(ClubMember member);
        void DeRegisterMember(int memberId);
        void SaveMember();

    }
    public class ClubService:IClubService,IClubMemberService
    {
        private readonly IClubRepository _clubRepo;
        private readonly IUnitOfWork unitOfWork;

        public ClubService(IClubRepository _repo,IUnitOfWork _unitOfWork)
        {
            _clubRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        public IEnumerable<Club> GetUserClubs(string userId)
        {
            return _clubRepo.GetUserClubs(userId);
        }
        public Club GetClubByID(int id)
        {
            
            return _clubRepo.GetClubByID(id);
        }
        public IEnumerable<Club> GetAll()
        {
            return _clubRepo.GetAll();
        }
        public void AddClub(Club club)
        {
            club.Deleted = false;
            club.DateCreated = DateTime.Now;
            _clubRepo.Add(club);
        }
        public void UpdateClub(Club club)
        {
            _clubRepo.Update(club);
        }
        public void Delete(int club)
        {
            _clubRepo.Delete(club);
        }
        public void SaveClub()
        {
            unitOfWork.Commit();
        }

        //clubmember
        public ClubMember GetMemberByID(int id)
        {
            return _clubRepo.GetMemberByID(id);
        }
        public IEnumerable<ClubMember> GetMembers(int clubId)
        {
            return _clubRepo.GetMembers(clubId);
        }
        public void RegisterMember(ClubMember member)
        {
            _clubRepo.JoinClub(member);
        }
        public void DeRegisterMember(int member)
        {

            _clubRepo.LeaveClub(member);
        }

        public void SaveMember()
        {
            unitOfWork.Commit();
        }
    }
}
