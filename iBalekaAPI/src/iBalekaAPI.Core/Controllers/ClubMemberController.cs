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
    [Route("api/[controller]")]
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
        [Route("GetClubMembers")]
        [HttpGet]
        public async Task<IActionResult> GetClubMembers(int clubId)
        {
            var response = new ListModelResponse<ClubMember>()
                as IListModelResponse<ClubMember>;
            try
            {
                if (clubId <1)
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
        [Route("GetClubMember")]
        [HttpGet]
        public async Task<IActionResult> GetClubMember(int memberId)
        {
            var response = new SingleModelResponse<ClubMember>()
              as ISingleModelResponse<ClubMember>;
            try
            {
                if (memberId <1)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    ClubMember member =_context.GetMemberByID(memberId);
                    if(member==null)
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
        [Route("RegisterMember")]
        [HttpPost]
        public async Task<IActionResult> RegisterMember(ClubMember clubmember)
        {
            var response = new SingleModelResponse<ClubMember>()
              as ISingleModelResponse<ClubMember>;
            try
            {
                if (clubmember == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.RegisterMember(clubmember);
                    _context.SaveMember();
                    return clubmember;
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
        /// <param name="clubmember" type="ClubMember">ClubMember Model</param>
        /// <remarks>DeRegister club member</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeRegisterMember")]
        [HttpPut]
        public async Task<IActionResult> DeRegisterMember(ClubMember clubmember)
        {
            var response = new SingleModelResponse<ClubMember>()
              as ISingleModelResponse<ClubMember>;
            try
            {
                if (clubmember == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.DeRegisterMember(clubmember);
                    _context.SaveMember();
                    return clubmember;
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
