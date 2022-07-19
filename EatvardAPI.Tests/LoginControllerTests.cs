using System.Net;
using System.Threading.Tasks;
using Domain.DTOs;
using EatvardAPI.Controllers;
using EatvardAPI.JWT;
using EatvardDataAccessLibrary;
using EatvardDataAccessLibrary.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EatvardAPI.Tests;

public class LoginControllerTests
{

    [Fact]
    public async Task Login_ShouldReturnUserWithToken()
    {
        LoginUserDTO loginUserDTO = new LoginUserDTO()
        {
            Email = "test@email.com",
            Password = "password"
        };

        User user = new User()
        {
            Email = loginUserDTO.Email,
            PasswordHash = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f", // Correct hash for 'password' with salt '123'
            PasswordSalt = "123"
        };

        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(fakeUnitOfWork.Users).WithNonVoidReturnType().Returns(Task.FromResult(user));
        var controller = new LoginController(fakeUnitOfWork, new JWTUtils("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"));
        
        var actionResult = await controller.Login(loginUserDTO);
        var result = actionResult.Result as OkObjectResult;
        var resulting_userDTO = result.Value as UserDTO;
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(resulting_userDTO.JWTToken);
    }

    [Fact]
    public async Task Login_WrongPasswordShouldReturnUnauthorizedResponse()
    {
        LoginUserDTO loginUserDTO = new LoginUserDTO()
        {
            Email = "test@email.com",
            Password = "wrongpassword"
        };

        User user = new User()
        {
            Email = loginUserDTO.Email,
            PasswordHash = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f",
            PasswordSalt = "123"
        };

        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(fakeUnitOfWork.Users).WithNonVoidReturnType().Returns(Task.FromResult(user));
        var controller = new LoginController(fakeUnitOfWork, new JWTUtils("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"));

        var actionResult = await controller.Login(loginUserDTO);
        var result = actionResult.Result as UnauthorizedObjectResult;
        Assert.Equal(401, result.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorizedResponse()
    {
        LoginUserDTO loginUserDTO = new LoginUserDTO()
        {
            Email = "test@email.com"
        };
        User user = new User()
        {
            Email = "also@email.com",
            PasswordHash = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f",
            PasswordSalt = "123"
        };
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        //A.CallTo(fakeUnitOfWork.Users).WithNonVoidReturnType().Returns(user);
        //A.CallTo(fakeUnitOfWork.Users).WithNonVoidReturnType().Returns(Task.FromResult(Task.CompletedTask));
        var controller = new LoginController(fakeUnitOfWork, new JWTUtils("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"));

        var actionResult = await controller.Login(loginUserDTO);
        var result = actionResult.Result as UnauthorizedObjectResult;
        Assert.Equal(401, result.StatusCode);
    }
}
