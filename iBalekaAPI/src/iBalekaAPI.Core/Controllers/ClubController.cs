using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using iBalekaAPI.Models.Responses;
using iBalekaAPI.Core.Extensions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public class ClubController : Controller
    {
        private IClubService _context;
        public ClubController(IClubService clubRepo)
        {
            _context = clubRepo;;
        }
        // GET: api/value
        /// <summary>
        /// Get all clubs
        /// </summary>
        /// <remarks>Get all clubs</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllClubs()
        {
            var response = new ListModelResponse<Club>()
               as IListModelResponse<Club>;
            try
            {
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Club> clubs = _context.GetAll();
                    if (clubs == null)
                        throw new System.ArgumentNullException("Clubs do not does not Exist");
                    return clubs;
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
        /// Get all user created clubs
        /// </summary>
        /// <param name="userId" type="int">User Id</param>
        /// <remarks>Get all user created clubs</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("User/[action]")]
        public async Task<IActionResult> GetUserClubs([FromQuery]string userId)
        {
            var response = new ListModelResponse<Club>()
                as IListModelResponse<Club>;
            try
            {
                if (userId == null)
                    throw new Exception("User Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Club> clubs = _context.GetUserClubs(userId);
                    if (clubs == null)
                        throw new System.ArgumentNullException("Clubs do not does not Exist");
                    return clubs;
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
        /// Get a particular club
        /// </summary>
        /// <param name="clubId" type="int">Club Id</param>
        /// <remarks>Get a particular club</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetClub([FromQuery]int clubId)
        {
            var response = new SingleModelResponse<Club>()
                as ISingleModelResponse<Club>;
            try
            {
                if (clubId < 1)
                    throw new Exception("Club Id is null");
                response.Model = await Task.Run(() =>
                {
                    Club evnt = _context.GetClubByID(clubId);
                    if (evnt == null)
                        throw new Exception("Club does not Exist");
                    return evnt;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        // POST api/values
        /// <summary>
        /// Create a club
        /// </summary>
        /// <param name="club" type="Club">Club Model</param>
        /// <remarks>Create a Club</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateClub([FromBody]Club club)
        {
            var response = new SingleModelResponse<Club>()
              as ISingleModelResponse<Club>;
            try
            {
                if (club == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.AddClub(club);
                    _context.SaveClub();
                    return club;
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
        /// Update a club
        /// </summary>
        /// <param name="club" type="Club">Club Model</param>
        /// <remarks>Update a Club</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("Update/[action]")]
        public async Task<IActionResult> UpdateClub([FromBody]Club club)
        {
            var response = new SingleModelResponse<Club>()
               as ISingleModelResponse<Club>;
            try
            {
                if (club == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.UpdateClub(club);
                    _context.SaveClub();
                    return club;
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
        /// Delete a club
        /// </summary>
        /// <param name="club" type="int">Club Id</param>
        /// <remarks>Delete a club</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeleteClub([FromQuery]int club)
        {
            var response = new SingleModelResponse<int>()
                as ISingleModelResponse<int>;
            try
            {
                if (club.ToString() == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.Delete(club);
                    _context.SaveClub();
                    return club;
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
