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
        [Route("RegisterMember")]
        [HttpPost]
        public IActionResult RegisterMember(ClubMember club)
        {
            if (ModelState.IsValid)
            {
                _memberRepo.RegisterMember(club);
                _memberRepo.SaveMember();
                return Ok(club.MemberId);
            }
            else
                return BadRequest(ModelState);
        }
        [Route("DeRegisterMember")]
        [HttpPut]
        public IActionResult DeRegisterMember(ClubMember club)
        {
            if (ModelState.IsValid)
            {
                _memberRepo.DeRegisterMember(club);
                _memberRepo.SaveMember();
                return Ok();
            }
            else
                return BadRequest(ModelState);
        }
    }
}
