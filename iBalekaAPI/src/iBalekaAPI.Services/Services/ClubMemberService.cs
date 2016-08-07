using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Data.Repositories;
using iBalekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaAPI.Services
{
    public interface IClubMemberService
    {
        ClubMember GetMemberByID(int id);
        IEnumerable<ClubMember> GetMembers(int clubId);
        void RegisterMember(ClubMember member);
        void DeRegisterMember(ClubMember memberId);
        void SaveMember();

    }
    public class ClubMemberService:IClubMemberService
    {
        private readonly IClubMemberRepository _clubMemberRepo;
        private readonly IUnitOfWork unitOfWork;

        public ClubMemberService(IClubMemberRepository _repo,IUnitOfWork _unitOfWork)
        {
            _clubMemberRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        
        public ClubMember GetMemberByID(int id)
        {
            return _clubMemberRepo.GetMemberByID(id);
        }
        public IEnumerable<ClubMember> GetMembers(int clubId)
        {
            return _clubMemberRepo.GetMembers(clubId);
        }
        public void RegisterMember(ClubMember member)
        {
            _clubMemberRepo.Add(member);
        }
        public void DeRegisterMember(ClubMember member)
        {
            
            _clubMemberRepo.Delete(member);
        }
   
        public void SaveMember()
        {
            unitOfWork.Commit();
        }
    }
}
