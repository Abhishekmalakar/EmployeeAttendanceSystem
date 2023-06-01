using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _astoriaTrainingAPI.Models;

namespace _astoriaTrainingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserInfoesController : ControllerBase
    {
        private readonly astoriaTraining80Context _context;

        public UserInfoesController(astoriaTraining80Context context)
        {
            _context = context;
        }

        // GET: api/UserInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfo()
        {
            return await _context.UserInfo.ToListAsync();
        }

        // GET: api/UserInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(int id)
        {
            var userInfo = await _context.UserInfo.FindAsync(id);

            if (userInfo == null)
            {
                return NotFound();
            }

            return userInfo;
        }

        // PUT: api/UserInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInfo(int id, UserInfo userInfo)
        {
            if (id != userInfo.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserInfoes
       /// <summary>
       /// This Method Used to Post UserInfo 
       /// </summary>
       /// <param name="userInfo"></param>
       /// <returns> UserInfo entity</returns>
        [HttpPost("Userinfo")]
        [ProducesResponseType(typeof(IEnumerable<UserInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserInfo>> PostUserInfo(UserInfo userInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(userInfo.Email) ||
                    string.IsNullOrEmpty(userInfo.FirstName) ||
                     string.IsNullOrEmpty(userInfo.LastName) ||
                    string.IsNullOrEmpty(userInfo.Password)||
                    string.IsNullOrEmpty(userInfo.UserName)||
                    userInfo.UserId==null)
                  
                {
                    return BadRequest();
                }

                else
                {
                    var user = _context.UserInfo.Where(x => x.UserId == userInfo.UserId).Select(x => x).FirstOrDefault();
                    if (user != null)
                        return Conflict("EmployeeId Should Be Dublicate");
                }
                
                
                    _context.UserInfo.Add(userInfo);
                    await _context.SaveChangesAsync();
                    return Ok();
                
                  
              
               // return CreatedAtAction("GetUserInfo", new { id = userInfo.UserId }, userInfo);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        // DELETE: api/UserInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserInfo>> DeleteUserInfo(int id)
        {
            var userInfo = await _context.UserInfo.FindAsync(id);
            if (userInfo == null)
            {
                return NotFound();
            }

            _context.UserInfo.Remove(userInfo);
            await _context.SaveChangesAsync();

            return userInfo;
        }

        private bool UserInfoExists(int id)
        {
            return _context.UserInfo.Any(e => e.UserId == id);
        }
    }
}
