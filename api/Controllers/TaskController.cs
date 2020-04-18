using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    internal class TaskController
    {
        [HttpPost]
        public async Task<ActionResult<BoxTask>> PostTask(BoxTask task)
        {
            return task;
        }
    }
}