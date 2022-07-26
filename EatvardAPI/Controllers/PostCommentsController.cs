using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EatvardAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PostCommentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PostCommentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<PostComment>>> GetById(int id)
    {
        var postComments = await _unitOfWork.PostComments.FindAsync(pc => pc.PostId == id);

        if (!postComments.Any())
        {
            return NotFound();
        }

        return Ok(postComments);
    }

    [HttpPost]
    public async Task<ActionResult<PostComment>> Create(PostComment postComment)
    {
        if (postComment == null)
        {
            return BadRequest();
        }

        _unitOfWork.PostComments.Create(postComment);
        var success = await _unitOfWork.CompleteAsync();
        if (success == 0)
        {
            return BadRequest("Changes could not be saved");
        }

        return Ok(postComment);
    }
}
