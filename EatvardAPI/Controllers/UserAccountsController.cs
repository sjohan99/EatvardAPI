using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<ActionResult<UserAccount>> CreateUserAccount(CreateUserDTO createUserDTO)
        {
            if (createUserDTO == null) {
                return BadRequest("User account object is null");
            }

            if (!ModelState.IsValid) {
                return BadRequest("Invalid model object");
            }

            var passwordHasher = PasswordHasherFactory.SHA256();
            var passwordSalt = passwordHasher.GenerateSalt();
            createUserDTO.Password = passwordHasher.Hash(createUserDTO.Password, passwordSalt);

            var userEntity = new UserAccount() {
                Email = createUserDTO.Email,
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                PasswordHash = createUserDTO.Password,
                PasswordSalt = passwordSalt
            };

            _unitOfWork.UserAccounts.Create(userEntity);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction("GetUserAccount", new { id = userEntity.Id }, userEntity.asDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAccount(int id, UpdateUserDTO updateUserDTO)
        {
            if (updateUserDTO == null) {
                return BadRequest($"{nameof(updateUserDTO)} object is null");
            }

            if (!ModelState.IsValid) {
                return BadRequest("Invalid model object");
            }

            var existing_user = await _unitOfWork.UserAccounts.GetUserByIdAsync(id);

            if (existing_user == null) {
                return NotFound();
            }

            if (updateUserDTO.FirstName != null) 
            {
                existing_user.FirstName = updateUserDTO.FirstName;
            }
            if (updateUserDTO.LastName != null) 
            {
                existing_user.LastName = updateUserDTO.LastName;
            }

            _unitOfWork.UserAccounts.Update(existing_user);
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
