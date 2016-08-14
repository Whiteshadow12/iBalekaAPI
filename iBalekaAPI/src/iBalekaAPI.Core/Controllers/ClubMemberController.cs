using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Services;
using iBalekaAPI.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClubMemberController : Controller
    {
        private IClubMemberService _memberRepo;
        public ClubMemberController(IClubMemberService memberRepo)
        {
            _memberRepo = memberRepo;
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
        public IActionResult GetClubMembers(int clubId)
        {
            if (ModelState.IsValid)
            {
                return Json(_memberRepo.GetMembers(clubId));
            }
            else
                return BadRequest(ModelState);
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
        public IActionResult GetClubMember(int memberId)
        {
            if (ModelState.IsValid)
            {
                ClubMember club = _memberRepo.GetMemberByID(memberId);
                if (club == null)
                    return NotFound();
                return Json(club);
            }
            else
                return BadRequest(ModelState);
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
        public IActionResult RegisterMember(ClubMember clubmember)
        {
            if (ModelState.IsValid)
            {
                _memberRepo.RegisterMember(clubmember);
                _memberRepo.SaveMember();
                return Ok(clubmember.MemberId);
            }
            else
                return BadRequest(ModelState);
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
        public IActionResult DeRegisterMember(ClubMember clubmember)
        {
            if (ModelState.IsValid)
            {
                _memberRepo.DeRegisterMember(clubmember);
                _memberRepo.SaveMember();
                return Ok();
            }
            else
                return BadRequest(ModelState);
        }
    }
}
