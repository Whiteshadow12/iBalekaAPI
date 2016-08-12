using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
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
        [Route("GetAllClubs")]
        [HttpGet]
        public IActionResult GetAllClubs()
        {
            return Json(_clubRepo.GetAll());
        }
        [Route("GetUserClubs")]
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
        [Route("GetClub")]
        [HttpGet]
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
        [Route("CreateClub")]
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
        [Route("UpdateClub")]
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
        [Route("DeleteClub")]
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
        
        
    }
}
