using Microsoft.EntityFrameworkCore;
using PrashantTodo.Models;

namespace PrashantTodo.Data
{
	public class TodoDbContext : DbContext
	{
		public TodoDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<TodoItem> TodoItems { get; set; }
	}
}