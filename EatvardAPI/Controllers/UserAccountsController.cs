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

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.UserAccounts.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccount>> GetUserAccount(int id)
        {
            var userAccount = await _unitOfWork.UserAccounts.GetUserByIdAsync(id);

            if (userAccount == null) {
                return NotFound();
            }

            return Ok(userAccount);
        }

        [HttpPost]
        public async Task<ActionResult<UserAccount>> CreateUserAccount(UserAccount userAccount)
        {
            if (userAccount == null) {
                return BadRequest("User account object is null");
            }

            if (!ModelState.IsValid) {
                return BadRequest("Invalid model object");
            }

            _unitOfWork.UserAccounts.Create(userAccount);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction("GetUserAccount", new { id = userAccount.Id }, userAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAccount(int id, UserAccount userAccount)
        {
            if (userAccount == null) {
                return BadRequest($"{nameof(userAccount)} object is null");
            }

            if (!ModelState.IsValid) {
                return BadRequest("Invalid model object");
            }

            var existing_user = await _unitOfWork.UserAccounts.GetUserByIdAsync(id);

            if (existing_user == null) {
                return NotFound();
            }

            if (existing_user.Id != userAccount.Id) {
                userAccount.Id = existing_user.Id;
            }

            _unitOfWork.UserAccounts.Update(userAccount);
            _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAccount(int id)
        {
            var existing_user = await _unitOfWork.UserAccounts.GetUserByIdAsync(id);

            if (existing_user == null) {
                return NotFound();
            }

            _unitOfWork.UserAccounts.Delete(existing_user);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
