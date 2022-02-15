using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            return Ok(context.Todos.ToList());
        }

        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromServices] AppDbContext context,
                                 [FromRoute] int id)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);

            if (todo == null)
                return NotFound();

            return Ok();
        }

        [HttpPost("/")]
        public IActionResult Post([FromServices] AppDbContext context,
                              [FromBody] TodoModel todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();

            return Created("/{todo.Id}", todo);
        }

        [HttpPut("/")]
        public IActionResult Put([FromServices] AppDbContext context,
                             [FromRoute] int id,
                             [FromBody] TodoModel todo)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null)
                return NotFound();

            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromServices] AppDbContext context,
                                [FromRoute] int id)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null)
                return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();

            return Ok(model);
        }
    }
}
