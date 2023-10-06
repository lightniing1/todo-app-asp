using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDo.Infrastructure.Contracts;
using ToDo.Models;
using ToDo.Services.Contracts;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoService _todoService;

        public TodoController(ILogger<TodoController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        [HttpGet]
        [Route("get-todo")]
        [ProducesResponseType(typeof(Todo), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int todoId)
        {
            try
            {
                var result = await _todoService.GetById(todoId);

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("get-all-todos")]
        [ProducesResponseType(typeof(IList<Todo>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _todoService.GetAll());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("create-todo")]
        [ProducesResponseType(typeof(Todo), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post(Todo todo)
        {
            try
            {
                await _todoService.Insert(todo);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("delete-todo")]
        [ProducesResponseType(typeof(Todo), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int todoId)
        {
            try
            {
                await _todoService.Delete(todoId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("update-todo")]
        [ProducesResponseType(typeof(Todo), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(Todo todo)
        {
            try
            {
                await _todoService.Update(todo);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}