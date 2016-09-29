using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Services;
using iBalekaAPI.Models;
using iBalekaAPI.Models.Responses;
using iBalekaAPI.Core.Extensions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public class ClubMemberController : Controller
    {
        private IClubMemberService _context;
        public ClubMemberController(IClubMemberService memberRepo)
        {
            _context = memberRepo;
        }
        // GET api/values/5
        /// <summary>
        /// Gets clubmembers for a particular club
        /// </summary>
        /// <param name="clubId" type="int">Club Id</param>
        /// <remarks>Get club members</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetClubMembers([FromQuery]int clubId)
        {
            var response = new ListModelResponse<ClubMember>()
                as IListModelResponse<ClubMember>;
            try
            {
                if (clubId < 1)
                    throw new Exception("Club Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<ClubMember> member = _context.GetMembers(clubId);
                    if (member == null)
                        throw new Exception("Club Member does not Exist");
                    return member;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        /// <summary>
        /// Gets particular club member
        /// </summary>
        /// <param name="memberId" type="int">MemberId</param>
        /// <remarks>Get club member</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Member/[action]")]
        public async Task<IActionResult> GetClubMember([FromQuery]int memberId)
        {
            var response = new SingleModelResponse<ClubMember>()
              as ISingleModelResponse<ClubMember>;
            try
            {
                if (memberId < 1)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    ClubMember member = _context.GetMemberByID(memberId);
                    if (member == null)
                        throw new Exception("Club Member does not Exist");
                    return member;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        /// <summary>
        /// Register athlete to a particular club
        /// </summary>
        /// <param name="clubmember" type="ClubMember">ClubMember Model</param>
        /// <remarks>Register club member</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterMember([FromBody]ClubMember clubmember)
        {
            var existingMember = new ListModelResponse<ClubMember>()
              as IListModelResponse<ClubMember>;
            var response = new SingleModelResponse<ClubMember>()
              as ISingleModelResponse<ClubMember>;
            try
            {
                if (clubmember == null)
                    throw new Exception("Model is missing");
                existingMember.Model = await Task.Run(() =>
                {
                    return _context.AthletClubs(clubmember.AthleteId);
                });
                //check if member is part of club
                if(existingMember.Model!=null)
                {
                    foreach(ClubMember member in existingMember.Model)
                    {
                        if(member.Status==ClubStatus.Joined)
                            throw new Exception("Athlete is already part of a club");
                    }
                }
                response.Model = await Task.Run(() =>
                {
                    ClubMember clb = _context.RegisterMember(clubmember);
                    return clb;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        /// <summary>
        /// DeRegister athlete from a particular club
        /// </summary>
        /// <param name="clubmember" type="int">ClubMember Id</param>
        /// <remarks>DeRegister club member</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeRegisterMember([FromQuery]int clubmember)
        {
            var response = new SingleModelResponse<ClubMember>()
              as ISingleModelResponse<ClubMember>;
            try
            {
                if (clubmember.ToString() == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.DeRegisterMember(clubmember);
                    ClubMember member = new ClubMember
                    {
                        MemberId = clubmember
                    };
                    return member;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
    }
}
