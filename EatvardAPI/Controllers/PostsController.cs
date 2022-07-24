using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EatvardAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PostsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET api/<PostsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetById(int id)
    {
        var post = await _unitOfWork.Posts.GetByIdAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> Create([FromBody] CreatePostDTO createPostDTO)
    {
        if (createPostDTO == null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var config = new MapperConfiguration(cfg => cfg.CreateMap<CreatePostDTO, Post>());
        var mapper = config.CreateMapper();
        Post post = new Post()
        {
            Author = await _unitOfWork.Users.GetByIdAsync(createPostDTO.AuthorId),
            Restaurant = await _unitOfWork.Restaurants.GetByIdAsync((int) createPostDTO.RestaurantId!),
            Cost = createPostDTO.Cost,
            Rating = createPostDTO.Rating,
            Text = createPostDTO.Text,
            CreatedAt = DateTime.Now
        };
        
        _unitOfWork.Posts.Create(post);
        var success = await _unitOfWork.CompleteAsync();
        if (success == 0)
        {
            return BadRequest("Changes could not be saved");
        }
        return CreatedAtAction("GetById", new {Id = post.Id}, post);
    }
}
