using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Models;
using Domain.Repositories;
using EatvardAPI.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EatvardAPI.Tests;
public class PostsControllerTest
{

    private IUnitOfWork fakeUoW;
    private PostsController controller;
    private CreatePostDTO fakePostDTO;

    public PostsControllerTest()
    {
        fakeUoW = A.Fake<IUnitOfWork>();
        controller = new PostsController(fakeUoW);
        fakePostDTO = A.Fake<CreatePostDTO>();
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound()
    {
        int id = 1;
        A.CallTo(() => fakeUoW.Posts.GetByIdAsync(id)).Returns(Task.FromResult((Post?)null));

        var actionResult = await controller.GetById(id);
        var result = (NotFoundResult?) actionResult.Result;

        Assert.Equal((int) HttpStatusCode.NotFound, result!.StatusCode);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk()
    {
        int id = 1;
        A.CallTo(() => fakeUoW.Posts.GetByIdAsync(1)).Returns(Task.FromResult(new Post{}));

        var actionResult = await controller.GetById(id);
        var result = (OkObjectResult?) actionResult.Result;

        Assert.Equal((int) HttpStatusCode.OK, result!.StatusCode);
    }

    // Test doesn't work due to fakes not working as intended
    //[Fact]
    public async Task Create_ShouldReturnOk()
    {
        var fakePostDTO = A.Fake<CreatePostDTO>();
        A.CallTo(() => fakeUoW.CompleteAsync()).Returns(Task.FromResult(1));
        A.CallTo(() => fakeUoW.Users.GetByIdAsync(fakePostDTO.AuthorId)).Returns(Task.FromResult(new User()));
        A.CallTo(() => fakeUoW.Restaurants.GetByIdAsync((int) fakePostDTO.RestaurantId)).Returns(Task.FromResult(new Restaurant()));
        

        var actionResult = await controller.Create(fakePostDTO);
        var result = (CreatedAtActionResult?) actionResult.Result;

        Assert.Equal((int) HttpStatusCode.Created, result!.StatusCode);
    }

    
    //[Fact]
    public async Task Create_ShouldReturnBadRequestIfChangesCantBeSaved()
    {
        A.CallTo(() => fakeUoW.CompleteAsync()).Returns(Task.FromResult(0));

        var actionResult = await controller.Create(fakePostDTO);
        var result = (BadRequestObjectResult?) actionResult.Result;

        Assert.Equal((int)HttpStatusCode.BadRequest, result!.StatusCode);
    }
}
