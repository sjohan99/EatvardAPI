using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using EatvardAPI.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EatvardAPI.Tests;
public class PostsControllerTest
{

    IUnitOfWork fakeUoW;
    PostsController controller;

    public PostsControllerTest()
    {
        fakeUoW = A.Fake<IUnitOfWork>();
        controller = new PostsController(fakeUoW);
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
}
