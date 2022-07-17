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
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWTUtils _jwtUtils;

        public UsersController(IUnitOfWork unitOfWork, JWTUtils jwtUtils)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.Users.GetAllUsersAsync();
            return Ok(users); // Unsafe, returns hashed passwords
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id);

            if (user == null) {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDTO createUserDTO)
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

            var userEntity = new User() {
                Email = createUserDTO.Email,
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                PasswordHash = createUserDTO.Password,
                PasswordSalt = passwordSalt
            };

            _unitOfWork.Users.Create(userEntity);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction("GetUser", new { id = userEntity.Id }, userEntity.asDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            if (updateUserDTO == null) {
                return BadRequest($"{nameof(updateUserDTO)} object is null");
            }

            if (!ModelState.IsValid) {
                return BadRequest("Invalid model object");
            }

            var existing_user = await _unitOfWork.Users.GetUserByIdAsync(id);

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

            _unitOfWork.Users.Update(existing_user);
            _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existing_user = await _unitOfWork.Users.GetUserByIdAsync(id);

            if (existing_user == null) {
                return NotFound();
            }

            _unitOfWork.Users.Delete(existing_user);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
