using Domain.DTOs;
using Domain.Utils.Security;
using EatvardAPI.JWT;
using EatvardDataAccessLibrary;
using EatvardDataAccessLibrary.DTOExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EatvardAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JWTUtils _jwtUtils;

    public LoginController(IUnitOfWork unitOfWork, JWTUtils jwtUtils)
    {
        _unitOfWork = unitOfWork;
        _jwtUtils = jwtUtils;
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Login(LoginUserDTO loginUserDTO)
    {
        var existingUser = await _unitOfWork.Users.FindOneAsync(user => user.Email == loginUserDTO.Email);

        if (existingUser == null) {
            return Unauthorized();
        }

        bool verified = PasswordVerifier.Verify(
            loginUserDTO.Password,
            existingUser.PasswordHash,
            existingUser.PasswordSalt,
            PasswordHasherFactory.SHA256());

        if (!verified) {
            return Unauthorized("Invalid email or password");
        }

        var token = _jwtUtils.GenerateToken(existingUser.Email);
        var userDTO = existingUser.asDTO();
        userDTO.JWTToken = token;

        return Ok(existingUser.asDTO(token));
    }
}
