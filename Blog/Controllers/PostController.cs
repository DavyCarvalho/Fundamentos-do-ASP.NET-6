using Blog.Data;
using Blog.ViewsModels.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class PostController : ControllerBase
    {
        [HttpGet("v1/posts")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context)
        {
            var posts = await context
                .Posts
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Select(x =>
                new ListPostsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = $"{x.Author.Name} ({x.Author.Email})",
                    Category = x.Category.Name,
                    LastUpdateDate = x.LastUpdateDate,
                    Slug = x.Slug
                })
                .ToListAsync();
            return Ok(posts);
        }

    }
}

