using Dockerbox.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace obvious_password.Pages.Private
{
	public class PrivateIndexModel : PageModel
	{
		private readonly ITaskApi _taskApi;
		public PrivateIndexModel(ITaskApi taskApi)
		{
			_taskApi = taskApi;
		}

		public void OnGet()
		{
		}

		public async Task OnPostAsync()
		{
			await _taskApi.Complete();
		}
	}
}