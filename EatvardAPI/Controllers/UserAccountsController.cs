using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EatvardDataAccessLibrary.Data;
using Microsoft.Data.SqlClient;
using EatvardDataAccessLibrary.Repositories.UserAccountRepository;
using EatvardDataAccessLibrary.Models;

namespace EatvardDataAccessLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("desc")]
        public async Task<IActionResult> GetUsersByNameDescending()
        {
            var users = await _unitOfWork.UserAccounts.GetByNameDescendingAsync();
            return Ok(users);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _unitOfWork.UserAccounts.GetAll();
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<UserAccount>> PostUserAccount(UserAccount userAccount)
        {
            _unitOfWork.UserAccounts.Add(userAccount);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction("GetUserAccount", new { id = userAccount.Id }, userAccount);
        }

        /*
        // GET: api/UserAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccount>>> GetUsers()
        {
            var users = await _repo.GetUsers();
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        // GET: api/UserAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccount>> GetUserAccount(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var userAccount = await _context.Users.FindAsync(id);

            if (userAccount == null)
            {
                return NotFound();
            }

            return userAccount;
        }

        // PUT: api/UserAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAccount(int id, UserAccount userAccount)
        {
            if (id != userAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountExists(id))
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

        // POST: api/UserAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAccount>> PostUserAccount(UserAccount userAccount)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'EatvardContext.Users'  is null.");
            }
            _context.Users.Add(userAccount);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    return Problem("SQLException, user email might be unavailable");
                }
            }
            
            return CreatedAtAction("GetUserAccount", new { id = userAccount.Id }, userAccount);
        }

        // DELETE: api/UserAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAccount(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var userAccount = await _context.Users.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAccountExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        */
    }
}
