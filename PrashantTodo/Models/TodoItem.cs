namespace PrashantTodo.Models
{
	public class TodoItem
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime dateTime { get; set; }

		public Status status { get; set; }

		public string? description { get; set; }
	}

	public enum Status
	{
		Completed,
		Incomplete
	}
}