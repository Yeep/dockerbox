using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using api.Models;
using api.DataContext;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class TasksController : ControllerBase
    {
        private BoxTaskDBContext _context;
        private ILogger<TasksController> _logger;

        public TasksController(BoxTaskDBContext context, ILogger<TasksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<BoxTask>> GetAll() => _context.Tasks.ToList();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BoxTask>> GetById(string id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        /*[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BoxTask>> Create(BoxTask task)
        {
            task.Id = new Guid().ToString();
            await _context.Tasks.AddAsync(task);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }*/

        public class CompleteData
        {
            public string Id { get; set; }
        }

        [HttpPost("Complete")]
        public async Task<ActionResult> Complete(CompleteData data)
        {
            _logger.LogInformation($"Completing task {data.Id}");
            var task = await _context.Tasks.FindAsync(data.Id);

            if (task == null)
            {
                _logger.LogError($"Task {data.Id} not found.");
                return NotFound();
            }

            task.Complete = true;
            _context.Update(task);
            await _context.SaveChangesAsync();

            return AcceptedAtAction(nameof(GetById), new { id = task.Id }, task);
        }
    }
}