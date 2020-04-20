using System.IO;

namespace obvious_password
{
    internal class TaskId
    {
        public TaskId()
        {
        }

        public string GetTaskId()
        {
            return File.ReadAllText("/run/secrets/secret_id");
        }
    }
}