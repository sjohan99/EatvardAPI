﻿using Domain.DTOs;
using EatvardDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatvardDataAccessLibrary.DTOExtensions;

public static class UserDTOExtensions
{
    public static UserDTO AsDTO(this User user, string? jwtToken = null)
    {
        return new UserDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            JWTToken = jwtToken
        };
    }
}
