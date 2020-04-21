using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace obvious_password
{
    public class TaskApi : ITaskApi
    {
        private readonly ILogger<TaskApi> _logger;

        public TaskApi(ILogger<TaskApi> logger)
        {
            _logger = logger;
        }

        public async Task Complete()
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                    ["id"] = GetTaskId()
                });
                await client.PostAsync(
                    "http://api/api/Tasks/Complete", 
                    new StringContent("{\"id\":\"" + GetTaskId() + "\"}", Encoding.UTF8, MediaTypeNames.Application.Json));
            }
        }

        private string GetTaskId()
        {
            var id = File.ReadAllText("/run/secrets/task_id");
            _logger.LogInformation($"Got task id {id}");
            return id;
        }
    }
}