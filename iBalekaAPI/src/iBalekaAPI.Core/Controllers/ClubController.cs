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
        /// <summary>
        /// Get all clubs
        /// </summary>
        /// <remarks>Get all clubs</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetAllClubs")]
        [HttpGet]
        public IActionResult GetAllClubs()
        {
            return Json(_clubRepo.GetAll());
        }
        /// <summary>
        /// Get all user created clubs
        /// </summary>
        /// <param name="userId" type="int">User Id</param>
        /// <remarks>Get all user created clubs</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
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
        /// <summary>
        /// Get a particular club
        /// </summary>
        /// <param name="clubId" type="int">Club Id</param>
        /// <remarks>Get a particular club</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
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
        /// <summary>
        /// Create a club
        /// </summary>
        /// <param name="club" type="Club">Club Model</param>
        /// <remarks>Create a Club</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
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
        /// <summary>
        /// Update a club
        /// </summary>
        /// <param name="club" type="Club">Club Model</param>
        /// <remarks>Update a Club</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
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
        /// <summary>
        /// Delete a club
        /// </summary>
        /// <param name="club" type="Club">Club Model</param>
        /// <remarks>Delete a club</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
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
