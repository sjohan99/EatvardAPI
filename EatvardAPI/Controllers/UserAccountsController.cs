using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EatvardDataAccessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Domain.Utils.Security;
using Domain.DTOs;
using EatvardDataAccessLibrary.DTOExtensions;
using EatvardAPI.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EatvardDataAccessLibrary.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWTUtils _jwtUtils;

        public UserAccountsController(IUnitOfWork unitOfWork, JWTUtils jwtUtils)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
        }

        [Authorize]
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

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginUserDTO loginUserDTO)
        {
            var existingUser = await _unitOfWork.UserAccounts.Find(user => user.Email == loginUserDTO.Email).FirstOrDefaultAsync();

            if (existingUser == null) 
            {
                return Unauthorized();
            }

            bool verified = PasswordVerifier.Verify(
                loginUserDTO.Password,
                existingUser.PasswordHash,
                existingUser.PasswordSalt);

            if (!verified) 
            {
                return Unauthorized("Invalid email or password");
            }

            existingUser.JWTToken = _jwtUtils.GenerateToken(existingUser.Email);
            
            return Ok(existingUser.asDTO());
        }

        [HttpPost]
        public async Task<ActionResult<UserAccount>> CreateUserAccount(UserDTO userDTO)
        {
            if (userDTO == null) {
                return BadRequest("User account object is null");
            }

            if (!ModelState.IsValid) {
                return BadRequest("Invalid model object");
            }

            var passwordHasher = PasswordHasherFactory.SHA256();
            var passwordSalt = passwordHasher.GenerateSalt();
            userDTO.Password = passwordHasher.Hash(userDTO.Password, passwordSalt);

            var userEntity = new UserAccount() {
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PasswordHash = userDTO.Password,
                PasswordSalt = passwordSalt
            };

            _unitOfWork.UserAccounts.Create(userEntity);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction("GetUserAccount", new { id = userEntity.Id }, userDTO);
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
