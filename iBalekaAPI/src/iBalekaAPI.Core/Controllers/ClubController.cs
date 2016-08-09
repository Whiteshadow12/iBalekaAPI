using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClubController : Controller
    {
        private IClubMemberService _memberRepo;
        private IClubService _clubRepo;
        public ClubController(IClubMemberService memberRepo, IClubService clubRepo)
        {
            _clubRepo = clubRepo;
            _memberRepo = memberRepo;
        }
        // GET: api/value
        [HttpGet]
        public IActionResult GetAllClubs()
        {
            return Json(_clubRepo.GetAll());
        }
        [HttpGet]
        public IActionResult GetUserClubs(string userId)
        {
            if (ModelState.IsValid)
            {
                return Json(_clubRepo.GetUserClubs(userId));
            }
            else
                return BadRequest(ModelState);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetClubMembers(int clubId)
        {
            if (ModelState.IsValid)
            {
                return Json(_memberRepo.GetMembers(clubId));
            }
            else
                return BadRequest(ModelState);
        }
        [HttpGet("{id}")]
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
        [HttpGet("{id}")]
        public IActionResult GetClub(int clubId)
        {
            if (ModelState.IsValid)
            {
                Club club = _clubRepo.GetClubByID(clubId);
                if (club == null)
                    return NotFound();
                return Json(club);
            }
            else
                return BadRequest(ModelState);
        }
        // POST api/values
        [HttpPost]
        public IActionResult CreateClub(Club club)
        {
            if (ModelState.IsValid)
            {
                _clubRepo.AddClub(club);
                _clubRepo.SaveClub();
                return Ok(club.ClubId);
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult UpdateClub(Club club)
        {
            if (ModelState.IsValid)
            {
                _clubRepo.UpdateClub(club);
                _clubRepo.SaveClub();
                return Ok(club.ClubId);
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult DeleteClub(Club club)
        {
            if (ModelState.IsValid)
            {
                _clubRepo.Delete(club);
                _clubRepo.SaveClub();
                return Ok();
            }
            else
                return BadRequest(ModelState);
        }
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
