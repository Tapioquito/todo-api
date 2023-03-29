using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [ApiController]
   // [Route("home")]
    public class HomeController: ControllerBase
    {
        [HttpGet("/")] // outra forma de passar a Route
       // [Route("/")]
        public IActionResult Get([FromServices] AppDbContext context)
        // INJEÇÃO DE DEPENDÊNCIA
        
          => Ok(context.Todos.ToList()); 
          //Bodyless Function

          //Quando só tem 1 linha de código
          //Dispensa as chaves e o return;
          //Todo método PÚBLICO dentro de um controller é chamado de "Action"
        

        [HttpGet("/{id:int}")] // outra forma de passar a Route
       // [Route("/")]
        public IActionResult GetById(
                [FromRoute] int id,
                    [FromServices] AppDbContext context)// INJEÇÃO DE DEPENDÊNCIA
        {
            
            var todo =  context.Todos.FirstOrDefault(x => x.Id ==id);
            if(todo == null){
                return NotFound();
            }
            return Ok(todo);
            //Todo método PÚBLICO dentro de um controller é chamado de "Action"
        }



         [HttpPost("/")] // outra forma de passar a Route
       // [Route("/")]
        public IActionResult Post([FromBody] TodoModel todo,[FromServices] AppDbContext context)// INJEÇÃO DE DEPENDÊNCIA
        {

            context.Todos.Add(todo);
            context.SaveChanges();

            return Created($"/{todo.Id}",todo);
            //Todo método PÚBLICO dentro de um controller é chamado de "Action"
        }

        [HttpPut("/{id:int}")] 
        public IActionResult Put([FromRoute] int id, 
            [FromBody] TodoModel todo,
                [FromServices] AppDbContext context)// INJEÇÃO DE DEPENDÊNCIA
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if(model == null) return NotFound();
        //    return todo;
            model.Title = todo.Title;
            model.IsDone = todo.IsDone;
            


            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);

            //Todo método PÚBLICO dentro de um controller é chamado de "Action"
        }

         [HttpDelete("/{id:int}")] 
        public IActionResult Delete([FromRoute] int id, 
          ///  [FromBody] TodoModel todo,
                [FromServices] AppDbContext context)// INJEÇÃO DE DEPENDÊNCIA
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if(model == null) return NotFound();

           // model.Title = todo.Title;
           // model.IsDone = todo.IsDone;
            


            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok(model);

            //Todo método PÚBLICO dentro de um controller é chamado de "Action"
        }

    }
}