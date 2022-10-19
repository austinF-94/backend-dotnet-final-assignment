using backend.Migrations;
using backend.Models;

namespace backend.Repositories;

public class PostsRepository : IPostsRepository 
{
    private readonly PostsDbContext _context;

    public PostsRepository(PostsDbContext context)
    {
        _context = context;
    }

    public Posts CreatePost(Posts newPost)
    {
        _context.Posts.Add(newPost);
        _context.SaveChanges();
        return newPost;
    }

    public void DeletePostById(int postId)
    {
        var post = _context.Posts.Find(postId);
        if (post != null) {
            _context.Posts.Remove(post); 
            _context.SaveChanges(); 
        }
    }

    public IEnumerable<Posts> GetAllPosts()
    {
        return _context.Posts.ToList();
    }

    public Posts? GetPostById(int postId)
    {
        return _context.Posts.SingleOrDefault(c => c.PostId == postId);
    }

    public Posts? UpdatePost(Posts newPost)
    {
        var originalPost = _context.Posts.Find(newPost.PostId);
        if (originalPost != null) {
            originalPost.Post = newPost.Post;
            _context.SaveChanges();
        }
        return originalPost;
    }
}