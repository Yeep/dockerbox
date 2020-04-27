using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Dockerbox.Common
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
				var content = new FormUrlEncodedContent(new Dictionary<string, string>
				{
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