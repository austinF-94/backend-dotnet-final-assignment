using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;


    [ApiController]
    [Route("api/[controller]")]
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
    public ActionResult<IEnumerable<Posts>> GetAllPosts() 
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

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Posts> CreatePost(Posts post) 
    {
    if (!ModelState.IsValid || post == null) {
        return BadRequest();
    }
    if (HttpContext.User == null) {
        return Unauthorized();
    }

    var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Posts_UserId");
    post.UserId = Int32.Parse(userId.Value);

    var newPost = _postsRepository.CreatePost(post);
    return Created(nameof(GetPostById), newPost);
    }
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("{postsId:int}")]
    public ActionResult<Posts> UpdatePost(Posts post) 
    {
    if (!ModelState.IsValid || post == null) {
        return BadRequest();
    }
    return Ok(_postsRepository.UpdatePost(post));
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("{postsId:int}")]
    public ActionResult DeletePostById(int postId) 
    {
    _postsRepository.DeletePostById(postId); 
    return NoContent();
    }
 }