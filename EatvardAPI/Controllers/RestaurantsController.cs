using Domain.DTOs;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EatvardDataAccessLibrary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RestaurantsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
    {
        var restaurants = await _unitOfWork.Restaurants.GetAllAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
    {
        var restaurant = await _unitOfWork.Restaurants.GetAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    /*
    [HttpGet("by/{name}")]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantsWithName(string name)
    {
        var restaurants = await _unitOfWork.Restaurants.FindManyByName(name);
        return Ok(restaurants);
    }
    */

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant(int id, UpdateRestaurantDTO restaurantDTO)
    {
        var existing_restaurant = await _unitOfWork.Restaurants.GetAsync(id);

        if (existing_restaurant == null)
        {
            return BadRequest($"No restaurant with id {id}");
        }

        existing_restaurant.UpdateFromDTO(restaurantDTO);
        await _unitOfWork.CompleteAsync();
        return NoContent();

    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantDTO restaurantDTO)
    {
        var restaurantEntity = new Restaurant()
        {
            Name = restaurantDTO.Name,
            Address = new Address()
            {
                StreetAddress = restaurantDTO.StreetAddress,
                StreetNumber = restaurantDTO.StreetNumber,
                City = restaurantDTO.City,
                ZipCode = restaurantDTO.ZipCode,
                State = restaurantDTO.State,
            }
        };

        _unitOfWork.Restaurants.Create(restaurantEntity);
        var success = await _unitOfWork.CompleteAsync();
        if (success == 0)
        {
            return BadRequest("Changes could not be saved");
        }
        return CreatedAtAction("GetRestaurant", new {id = restaurantEntity.Id}, restaurantEntity);
    }
}
