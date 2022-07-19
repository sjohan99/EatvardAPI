using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Restaurant;
using EatvardDataAccessLibrary.Models;

namespace EatvardDataAccessLibrary.DTOExtensions;
public static class RestaurantDTOExtensions
{
    public static void UpdateFromDTO(this Restaurant restaurant, UpdateRestaurantDTO restaurantDTO)
    {
        if (restaurantDTO.Name != null) restaurant.Name = restaurantDTO.Name;
        if (restaurantDTO.StreetAddress != null) restaurant.Address.StreetAddress = restaurantDTO.StreetAddress;
        if (restaurantDTO.StreetNumber != null) restaurant.Address.StreetNumber = restaurantDTO.StreetNumber;
        if (restaurantDTO.City != null) restaurant.Address.City = restaurantDTO.City;
        if (restaurantDTO.State != null) restaurant.Address.State = restaurantDTO.State;
        if (restaurantDTO.ZipCode != null) restaurant.Address.ZipCode = restaurantDTO.ZipCode;
    }
}
