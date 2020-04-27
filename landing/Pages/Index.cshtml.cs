using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace landing.Pages
{
	public class IndexModel : PageModel
	{
		public class BoxTask
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public bool Complete { get; set; }
			public string Address { get; set; }
		}

		public List<BoxTask> Tasks { get; set; }

		public double PercentComplete => ((double)Tasks.Count(t => t.Complete) / (double)Tasks.Count()) * 100.0;

		public async Task OnGetAsync()
		{
			using (var client = new HttpClient())
			{
				var tasks = await client.GetStringAsync("http://api/api/Tasks");
				Tasks = JsonSerializer.Deserialize<List<BoxTask>>(tasks, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});
				Tasks.ForEach(t => t.Address = t.Address.Replace("[hostname]", Request.Host.Host));
			}
		}
	}
}