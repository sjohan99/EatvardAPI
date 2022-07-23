using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Xunit;

namespace Domain.Tests;
public class AutoMapperTests
{
    [Fact]
    public void test()
    {
        User user = new()
        {
            Id = 1,
            FirstName = "John",
            Email = "john@email.com"
        };

        var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
        var mapper = config.CreateMapper();
        UserDTO uDTO = mapper.Map<UserDTO>(user);
        
        Assert.Equal(user.FirstName, uDTO.FirstName);
        Assert.Equal(user.Email, uDTO.Email);
        Assert.Equal(user.Id, uDTO.Id);
        Assert.Null(uDTO.LastName);
    }
}
