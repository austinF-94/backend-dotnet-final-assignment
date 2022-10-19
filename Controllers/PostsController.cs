using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase 
{
    private readonly ILogger<PostsController> _logger;
    private readonly IPostsRepository _postsRepository;

    public PostsController(ILogger<PostsController> logger, IPostsRepository repository)
    {
        _logger = logger;
        _postsRepository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Posts>> GetCoffee() 
    {
    return Ok(_postsRepository.GetAllPosts());
    }
    
    [HttpGet]
    [Route("{postsId:int}")]
    public ActionResult<Posts> GetPostById(int postId) 
    {
    var post = _postsRepository.GetPostById(postId);
    if (post == null) {
        return NotFound();
    }
    return Ok(post);
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public ActionResult<Posts> CreatePost(Posts post) 
    {
    if (!ModelState.IsValid || post == null) {
        return BadRequest();
    }
    var newPost = _postsRepository.CreatePost(post);
    return Created(nameof(GetPostById), newPost);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut]
    [Route("{postsId:int}")]
    public ActionResult<Posts> UpdatePost(Posts post) 
    {
    if (!ModelState.IsValid || post == null) {
        return BadRequest();
    }
    return Ok(_postsRepository.UpdatePost(post));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete]
    [Route("{postsId:int}")]
    public ActionResult DeletePostById(int postId) 
    {
    _postsRepository.DeletePostById(postId); 
    return NoContent();
    }
}