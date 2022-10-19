using backend.Models;

namespace backend.Repositories;
public interface IPostsRepository
{
    IEnumerable<Posts> GetAllPosts();
    Posts? GetPostById(int postId);
    Posts CreatePost(Posts newPost);
    Posts? UpdatePost(Posts newPost);
    void DeletePostById(int postId);
}