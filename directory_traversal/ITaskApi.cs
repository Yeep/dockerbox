using System.Threading.Tasks;

namespace Dockerbox.Common
{
	public interface ITaskApi
	{
		Task Complete();
	}
}