using api.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace api.Models
{
	public class DataGenerator
	{

		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BoxTaskDBContext(
				serviceProvider.GetRequiredService<DbContextOptions<BoxTaskDBContext>>()))
			{
				// Look for any tasks already in database.
				if (context.Tasks.Any())
				{
					return;   // Database has been seeded
				}

				// Read from file
				var tasks_file = File.ReadAllText("/run/secrets/tasks");
				var tasks = JsonSerializer.Deserialize<List<BoxTask>>(tasks_file, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});
				// Get the actual task ID from secrets
				tasks.ForEach(t => t.Id = File.ReadAllText($"/run/secrets/{t.Id}"));

				context.AddRange(tasks);

				context.SaveChanges();
			}
		}
	}
}