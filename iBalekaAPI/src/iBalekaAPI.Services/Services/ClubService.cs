﻿using System;
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
        Club AddClub(Club club);
        Club UpdateClub(Club club);
        void Delete(int club);
        void SaveClub();
    }
    public interface IClubMemberService
    {
        ClubMember GetMemberByID(int id);
        IEnumerable<ClubMember> GetMembers(int clubId);
        IEnumerable<ClubMember> AthletClubs(int athleteId);
        ClubMember RegisterMember(int athleteId,int clubId,string dateJoined);
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
        public IEnumerable<ClubMember> AthletClubs(int athleteId)
        {
            return _clubRepo.GetAthleteClubs(athleteId);
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
        public Club AddClub(Club club)
        {
            return _clubRepo.CreateClub(club);
        }
        public Club UpdateClub(Club club)
        {
            return _clubRepo.UpdateClub(club);
        }
        public void Delete(int club)
        {

            _clubRepo.DeleteClub(club);
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
        public ClubMember RegisterMember(int athleteId, int clubId, string dateJoined)
        {
            return _clubRepo.JoinClub(athleteId, clubId, dateJoined);
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
