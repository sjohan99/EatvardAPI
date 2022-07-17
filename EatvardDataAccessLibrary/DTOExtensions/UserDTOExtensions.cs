using Domain.DTOs;
using EatvardDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatvardDataAccessLibrary.DTOExtensions;

public static class UserDTOExtensions
{
    public static UserDTO asDTO(this UserAccount userAccount, string? jwtToken = null)
    {
        return new UserDTO
        {
            Id = userAccount.Id,
            FirstName = userAccount.FirstName,
            LastName = userAccount.LastName,
            Email = userAccount.Email,
            JWTToken = jwtToken
        };
    }
}
