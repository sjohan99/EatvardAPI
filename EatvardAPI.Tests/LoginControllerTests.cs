using System;
using System.Net;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Models;
using Domain.Repositories;
using EatvardAPI.Controllers;
using EatvardAPI.JWT;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EatvardAPI.Tests;

public class LoginControllerTests
{

    User user;
    LoginUserDTO loginUserDTO;
    IUnitOfWork fakeUnitOfWork;
    LoginController controller;

    public LoginControllerTests()
    {
        loginUserDTO = new LoginUserDTO()
        {
            Email = "test@email.com",
            Password = "password"
        };

        user = new User()
        {
            Email = loginUserDTO.Email,
            PasswordHash = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f", // Correct hash for 'password' with salt '123'
            PasswordSalt = "123"
        };

        fakeUnitOfWork = A.Fake<IUnitOfWork>();
        //A.CallTo(fakeUnitOfWork.Users).WithNonVoidReturnType().Returns(Task.FromResult(user));
        A.CallTo(() => fakeUnitOfWork.Users.FindOneByEmail(loginUserDTO.Email)).Returns(Task.FromResult(user));
        controller = new LoginController(fakeUnitOfWork, new JWTUtils("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"));
    }

    [Fact]
    public async Task Login_NonExistingEmailShouldReturnUnauthorizedResponse()
    {
        // Experimenting with Moq as well
        var mockUoW = new Mock<IUnitOfWork>();
        mockUoW.Setup(uow => uow.Users.FindOneByEmail("")).ReturnsAsync((User?) null);
        var moqController = new LoginController(mockUoW.Object, new JWTUtils("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"));

        var actionResult = await moqController.Login(loginUserDTO);
        var result = actionResult.Result as UnauthorizedResult;

        Assert.Equal(401, result.StatusCode);
    }

    [Fact]
    public async Task Login_WrongPasswordShouldReturnUnauthorizedResponse()
    {
        loginUserDTO.Password = "thisisthewrongpassword";

        var actionResult = await controller.Login(loginUserDTO);
        var result = actionResult.Result as UnauthorizedObjectResult;

        Assert.Equal(401, result.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnOkUserWithToken()
    {
        var actionResult = await controller.Login(loginUserDTO);
        var result = actionResult.Result as OkObjectResult;
        var resulting_userDTO = result.Value as UserDTO;

        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(resulting_userDTO.JWTToken);
    }
}
